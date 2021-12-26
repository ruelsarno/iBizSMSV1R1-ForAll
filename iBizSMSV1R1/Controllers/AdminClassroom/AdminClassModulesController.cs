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
using Microsoft.AspNetCore.Authorization;
namespace iBizSMSV1R1.Controllers.AdminClassroom
{
    //[Authorize(Roles = "Registrar,Admin")]
    public class AdminClassModulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminClassModulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClassModules
        public async Task<IActionResult> Index(int recno, string subjectname)
        {
            ViewBag.Recno = recno;
            ViewBag.SubjectName = subjectname;
            return View(await _context.ClassModule.Where(p => p.recno == recno).ToListAsync());
        }

        // GET: ClassModules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classModule = await _context.ClassModule
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (classModule == null)
            {
                return NotFound();
            }

            return View(classModule);
        }

        // GET: ClassModules/Create
        public IActionResult Create(int recno, string subjectname)
        {
            ViewBag.Recno = recno;
            ViewBag.SubjectName = subjectname;
            return View();
        }

        // POST: ClassModules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recordno,recno,subjectname,lessonno,lessontitle,videolink,image,active")] ClassModule classModule, IFormFile files)
        {
            //var a = classModule.recordno;
            //var b = classModule.recno;
            //var c = classModule.subjectname;
            //var d = classModule.lessonno;
            //var e = classModule.lessontitle;
            //var f = classModule.videolink;
            //var g = classModule.image;
            //var h = classModule.active;



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
                        //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "modules")).Root + $@"\{fileName}";

                        //using (FileStream fs = System.IO.File.Create(filepath))
                        //{
                        //    files.CopyTo(fs);
                        //    fs.Flush();
                        //}

                        //classModule.videolink = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                classModule.image = ms.ToArray();
                            }

                            _context.Add(classModule);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { recno = classModule.recno, subjectname = classModule.subjectname });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                //_context.Add(classModule);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index), new { recno = classModule.recno, subjectname = classModule.subjectname });
            }
            return RedirectToAction(nameof(Index), new { recno = classModule.recno, subjectname = classModule.subjectname });
        }

        // GET: ClassModules/Edit/5
        public async Task<IActionResult> Edit(int? id, int recno, string subjectname)
        {
            ViewBag.Recno = recno;
            ViewBag.SubjectName = subjectname;
            if (id == null)
            {
                return NotFound();
            }

            var classModule = await _context.ClassModule.FindAsync(id);
            if (classModule == null)
            {
                return NotFound();
            }
            return View(classModule);
        }

        // POST: ClassModules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recordno,recno,subjectname,lessonno,lessontitle,videolink,image,active")] ClassModule classModule, IFormFile files)
        {
            string notifymessage = "";
            if (id != classModule.recordno)
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
                        //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "modules")).Root + $@"\{fileName}";

                        //using (FileStream fs = System.IO.File.Create(filepath))
                        //{
                        //    files.CopyTo(fs);
                        //    fs.Flush();
                        //}

                        //classModule.videolink = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                classModule.image = ms.ToArray();
                            }

                            _context.Update(classModule);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { id = classModule.recno, message = Functions.Messages.messageupdatesuccess });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {
                    string sql = "update webPageTeacher set recno = '" + classModule.recno + "',subjectname ='" +
                        classModule.subjectname + "',lessonno ='" +
                        classModule.lessonno + "',lessontitle ='" +
                        classModule.lessontitle + "',active ='" +                        
                        classModule.active + "'" + " where recordno = " + id;

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                }
                return RedirectToAction(nameof(Index), new { recno = classModule.recno, subjectname = classModule.subjectname });
            }
            return RedirectToAction(nameof(Index), new { recno = classModule.recno, subjectname = classModule.subjectname });
        }

        // GET: ClassModules/Delete/5
        public async Task<IActionResult> Delete(int? id, int recno, string subjectname)
        {
            ViewBag.Recno = recno;
            ViewBag.SubjectName = subjectname;
            if (id == null)
            {
                return NotFound();
            }

            var classModule = await _context.ClassModule
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (classModule == null)
            {
                return NotFound();
            }

            return View(classModule);
        }

        // POST: ClassModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classModule = await _context.ClassModule.FindAsync(id);
            _context.ClassModule.Remove(classModule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { recno = classModule.recno, subjectname = classModule.subjectname });
        }

        private bool ClassModuleExists(int id)
        {
            return _context.ClassModule.Any(e => e.recordno == id);
        }
    }
}
