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
    public class AdminStudentImagesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;      
        private static ApplicationDbContext _context;       
        public AdminStudentImagesController(SignInManager<ApplicationUser> signInManager,
            ILogger<ViewModels.LoginModel> logger,
            UserManager<ApplicationUser> userManager,          
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;           
            _context = context;           
        }

        // GET: AdminStudentImages
        public IActionResult Index(string id,string message)
        {            
            ViewBag.ID = id;
            ViewBag.Message = message;
            var result = _context.StudentImage.Where(p => p.id == id);
            return View(result);
            //return View(_context.StudentImage.Where(p=>p.id == id).ToListAsync());
        }

        // GET: AdminStudentImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentImage = await _context.StudentImage
                .FirstOrDefaultAsync(m => m.recno == id);
            if (studentImage == null)
            {
                return NotFound();
            }

            return View(studentImage);
        }

        // GET: AdminStudentImages/Create
        public IActionResult Create(string id)
        {           
            ViewBag.ID = id;
            return View();
        }

        // POST: AdminStudentImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,id,link,image")] StudentImage studentImage, IFormFile files)
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

                        // Combines two strings into a path.
                        var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "students")).Root + $@"\{fileName}";

                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            files.CopyTo(fs);
                            fs.Flush();
                        }

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

                            _context.Add(studentImage);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), null, new {id = studentImage.id });
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
            return RedirectToAction(nameof(Index), null, new {id = studentImage.id });
        }

        // GET: AdminStudentImages/Edit/5
        public async Task<IActionResult> Edit(int? recno,string id)
        {
            ViewBag.ID = id;
            if (recno == null)
            {
                return NotFound();
            }

            var studentImage = await _context.StudentImage.FindAsync(recno);
            if (studentImage == null)
            {
                return NotFound();
            }
            return View(studentImage);
        }

        // POST: AdminStudentImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int recno, [Bind("recno,id,link,image")] StudentImage studentImage, IFormFile files)
        {
            string notifymessage = "";
            if (recno != studentImage.recno)
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

                        // Combines two strings into a path.
                        var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "teachers")).Root + $@"\{fileName}";

                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            files.CopyTo(fs);
                            fs.Flush();
                        }

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

                            _context.Update(studentImage);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { id = studentImage.id, message = Messages.messageupdatesuccess });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {
                    string sql = "update studentimage set id = '" + studentImage.id + "',link ='" +                        
                        studentImage.link + "'" + " where recno = " + recno;

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                }
                return RedirectToAction(nameof(Index), null, new { id = studentImage.id });
            }
            return RedirectToAction(nameof(Index), null, new { id = studentImage.id });
        }

        // GET: AdminStudentImages/Delete/5
        public async Task<IActionResult> Delete(int? recno, string id)
        {
            ViewBag.ID = id;

            if (recno == null)
            {
                return NotFound();
            }

            var studentImage = await _context.StudentImage
                .FirstOrDefaultAsync(m => m.recno == recno);
            if (studentImage == null)
            {
                return NotFound();
            }

            return View(studentImage);
        }

        // POST: AdminStudentImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int recno)
        {
            var studentImage = await _context.StudentImage.FindAsync(recno);
            _context.StudentImage.Remove(studentImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), null, new { id = studentImage.id });
        }

        private bool StudentImageExists(int id)
        {
            return _context.StudentImage.Any(e => e.recno == id);
        }

        private bool StudentImageIDExists(string id)
        {
            bool result = false;

            var x = _context.StudentImage.Where(e => e.id == id);

            if (x != null)
            {
                result = true;
            }

            return result;
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
                                return RedirectToAction("Index", "StudentInfoes", new { id = user.Id });
                            }
                            else
                            {
                                _context.Add(studentImage);
                                await _context.SaveChangesAsync();
                                return RedirectToAction("Index", "StudentInfoes", new { id = user.Id });
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

            return RedirectToAction("StudentInfoes","Index",new {id=user.Id});
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
