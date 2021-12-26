using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.FileProviders;
using iBizSMSV1R1.Functions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace iBizSMSV1R1.Controllers.AdminStudentProfile
{
    //[Authorize(Roles = "Registrar,Admin")]
    public class AdminStudentDocumentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private static ApplicationDbContext _context;
        public AdminStudentDocumentsController(SignInManager<ApplicationUser> signInManager,
            ILogger<ViewModels.LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        // GET: AdminStudentDocuments
        public async Task<IActionResult> Index(string id, string message)
        {
            ViewBag.ID = id;
            ViewBag.Message = message;
            var result = _context.StudentDocument.Where(p => p.id == id);
            return View(result);
        }

        // GET: AdminStudentDocuments/Details/5
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

        // GET: AdminStudentDocuments/Create
        public IActionResult Create(string id)
        {
            ViewBag.ID = id;
            return View();
        }

        // POST: AdminStudentDocuments/Create
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
                        //Getting FileName
                        var fileName = Path.GetFileName(files.FileName);

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var fileExtension = Path.GetExtension(fileName);

                        // concatenating  FileName + FileExtension
                        var newFileName = String.Concat(myUniqueFileName, fileExtension);

                        //// Combines two strings into a path.
                        //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "students")).Root + $@"\{fileName}";

                        //using (FileStream fs = System.IO.File.Create(filepath))
                        //{
                        //    files.CopyTo(fs);
                        //    fs.Flush();
                        //}

                        //studentDocument.link = fileName;

                        //string fileName = files.FileName;
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
                            return RedirectToAction(nameof(Index), null, new { id = studentDocument.id });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                //_context.Add(studentImage);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), null, new { id = studentDocument.id });
        }

        // GET: AdminStudentDocuments/Edit/5
        public async Task<IActionResult> Edit(int? recno, string id)
        {
            ViewBag.ID = id;
            if (recno == null)
            {
                return NotFound();
            }

            var studentDocument = await _context.StudentDocument.FindAsync(recno);
            if (studentDocument == null)
            {
                return NotFound();
            }
            return View(studentDocument);
        }

        // POST: AdminStudentDocuments/Edit/5
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
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        //Getting FileName
                        var fileName = Path.GetFileName(files.FileName);

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var fileExtension = Path.GetExtension(fileName);

                        // concatenating  FileName + FileExtension
                        var newFileName = String.Concat(myUniqueFileName, fileExtension);

                        //// Combines two strings into a path.
                        //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "teachers")).Root + $@"\{fileName}";

                        //using (FileStream fs = System.IO.File.Create(filepath))
                        //{
                        //    files.CopyTo(fs);
                        //    fs.Flush();
                        //}

                        //studentImage.link = fileName;

                        //string fileName = files.FileName;
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
                            return RedirectToAction(nameof(Index), new { id = studentDocument.id, message = Messages.messageupdatesuccess });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {
                    string sql = "update studentimage set id = '" + studentDocument.id + "',link ='" +
                        studentDocument.documentname + "'" + " where recno = " + recno;

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                }
                return RedirectToAction(nameof(Index), null, new { id = studentDocument.id });
            }
            return RedirectToAction(nameof(Index), null, new { id = studentDocument.id });
        }

        // GET: AdminStudentDocuments/Delete/5
        public async Task<IActionResult> Delete(int? recno, string id)
        {
            ViewBag.ID = id;
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

        // POST: AdminStudentDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? recno, string id)
        {
            var studentDocument = await _context.StudentDocument.FindAsync(recno);
            _context.StudentDocument.Remove(studentDocument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), null, new { id = studentDocument.id });
        }

        private bool StudentDocumentExists(int id)
        {
            return _context.StudentDocument.Any(e => e.recno == id);
        }

        // POST: AdminStudentImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImage(StudentImage studentImage, IFormFile files)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //return NotFound($"Your session is expired! Try logging in again.");
                return RedirectToAction("Index", "Home", new { message = Messages.messagesessionexpire });
            }

            studentImage.id = user.Id;

            //if (ModelState.IsValid)
            //{
            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);

                    //Assigning Unique Filename (Guid)
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);

                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                    //// Combines two strings into a path.
                    //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "students")).Root + $@"\{fileName}";

                    //using (FileStream fs = System.IO.File.Create(filepath))
                    //{
                    //    files.CopyTo(fs);
                    //    fs.Flush();
                    //}

                    studentImage.link = fileName;

                    //string fileName = files.FileName;
                    //2 Get the extension of the file
                    string extension = Path.GetExtension(fileName);
                    //3 check the file extension as png
                    if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                    {
                        using (var ms = new MemoryStream())
                        {
                            files.CopyTo(ms);
                            studentImage.image = ms.ToArray();
                        }

                        var studentimage = GetStudentImage(user.Id);
                        foreach (var item in studentimage)
                        {
                            studentImage.recno = item.recno;
                        }

                        if (studentimage != null)
                        {
                            _context.Update(studentImage);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            _context.Add(studentImage);
                            await _context.SaveChangesAsync();
                        }
                    }

                    else
                    {
                        throw new Exception("File must be either .pdf or .jpg or .png");
                    }
                }
                //}
                //_context.Add(studentImage);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("AccountIndex", "Account", new { id = user.Id });
        }

        // GET: AdminStudentImages
        public IActionResult GetStudentImage(string id, string message)
        {
            ViewBag.ID = id;
            ViewBag.Message = message;
            var result = _context.StudentImage.Where(p => p.id == id);
            return View(result);
            //return View(_context.StudentImage.Where(p=>p.id == id).ToListAsync());
        }

        // GET: CompanyImages
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
    }
}
