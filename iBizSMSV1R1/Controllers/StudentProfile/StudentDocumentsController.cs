using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.Models;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using iBizSMSV1R1.ViewModels;
using iBizSMSV1R1.Functions;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace iBizSMSV1R1.Controllers.StudentProfile
{
    //[Authorize(Roles = "Registrar,Student,Admin")]
    public class StudentDocumentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;
        public StudentDocumentsController(SignInManager<ApplicationUser> signInManager,
            ILogger<ViewModels.LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }


        // GET: StudentDocuments
        public async Task<IActionResult> Index(string message)
        {
            ViewBag.Message = message;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //return NotFound($"Your session is expired! Try logging in again.");
                return RedirectToAction("Index", "Home", new { message = Messages.messagesessionexpire });
            }

            ViewBag.ID = user.Id;
            ViewBag.StudentImage = GetStudentImage(user.Id);

            var studentinfo = _context.StudentDocument.Where(p => p.id == user.Id);
            return View(studentinfo);
        }

        // GET: StudentDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentDocument = await _context.StudentDocument
                .FirstOrDefaultAsync(m => m.recno == id);
            if (studentDocument == null)
            {
                return NotFound();
            }

            return View(studentDocument);
        }

        // GET: StudentDocuments/Create
        public IActionResult Create(string id)
        {
            ViewBag.ID = id;
            ViewBag.StudentImage = GetStudentImage(id);
            return View();
        }

        // POST: StudentDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,id,documentname,imagedata")] StudentDocument studentDocument, IFormFile files)
        {
            if (ModelState.IsValid)
            {
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                studentDocument.imagedata = ms.ToArray();
                            }

                            _context.Add(studentDocument);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), null, new { message = Messages.messagesuccess, id = studentDocument.id });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }

                    return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotsuccessful, id = studentDocument.id });
                }

               
            }
            return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotsuccessful, id = studentDocument.id });
        }

        // GET: StudentDocuments/Edit/5
        public async Task<IActionResult> Edit(int? recno, string id)
        {
            ViewBag.ID = id;
            ViewBag.StudentImage = GetStudentImage(id);

            if (id == null)
            {
                return NotFound();
            }

            var studentDocument = await _context.StudentDocument.FindAsync(recno);
            if (studentDocument == null)
            {
                return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotexists, id = studentDocument.id });
            }
            return View(studentDocument);
        }

        // POST: StudentDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int recno, [Bind("recno,id,documentname,imagedata")] StudentDocument studentDocument, IFormFile files)
        {
            string notifymessage = "";
            if (recno != studentDocument.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (files != null)
                    {
                        string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                studentDocument.imagedata = ms.ToArray();
                            }

                            _context.Update(studentDocument);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), null, new { message = Messages.messagesuccess, id = studentDocument.id });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                    else
                    {
                        string sql = "update studentdocument set id ='" +
                            studentDocument.id + "',documentname ='" +
                            studentDocument.documentname + "'" + " where recno = " + recno;

                        notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                    }

                    return RedirectToAction(nameof(Index), null, new { message = Messages.messageupdatesuccess, id = studentDocument.id });

                           
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentDocumentExists(studentDocument.recno))
                    {
                        return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotsuccessful, id = studentDocument.id });
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotsuccessful, id = studentDocument.id });
        }

        // GET: StudentDocuments/Delete/5
        public async Task<IActionResult> Delete(int? recno, string id)
        {
            if (recno == null)
            {
                return NotFound();
            }

            var studentDocument = await _context.StudentDocument
                .FirstOrDefaultAsync(m => m.recno == recno);
            if (studentDocument == null)
            {
                return NotFound();
            }

            return View(studentDocument);
        }

        // POST: StudentDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int recno)
        {
            var studentDocument = await _context.StudentDocument.FindAsync(recno);
            _context.StudentDocument.Remove(studentDocument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), null, new { message = Messages.messagesuccess, id = studentDocument.id });
        }

        private bool StudentDocumentExists(int id)
        {
            return _context.StudentDocument.Any(e => e.recno == id);
        }

        // GET: Image
        public static IEnumerable<StudentImage> GetStudentImage(string id)
        {
            try
            {
                IEnumerable<StudentImage> Image = from a in _context.StudentImage
                                                  where a.id == id
                                                  select new StudentImage
                                                  {
                                                      recno = a.recno,
                                                      link = a.link,
                                                      image = a.image,
                                                      id = a.id
                                                  };

                return Image;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [HttpGet]
        public FileResult DownLoadFile(int id)
        {
            //List<BillToPay> ObjFiles = GetFileList(id);

            var FileById = (from Docu in _context.StudentDocument
                            where Docu.recno.Equals(id)
                            select new {Docu.imagedata, Docu.documentname }).ToList().FirstOrDefault();

            return File(FileById.imagedata, "application/pdf", FileById.documentname);

        }
    }
}
