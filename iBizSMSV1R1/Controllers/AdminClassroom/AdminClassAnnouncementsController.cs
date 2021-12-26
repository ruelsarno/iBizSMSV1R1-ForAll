using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.ModelsClassroom;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using iBizSMSV1R1.Functions;
using Microsoft.AspNetCore.Authorization;
namespace iBizSMSV1R1.Controllers.AdminClassroom
{
    //[Authorize(Roles = "Registrar,Admin")]
    public class AdminClassAnnouncementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminClassAnnouncementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminClassAnnouncements
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClassAnnouncement.ToListAsync());
        }

        // GET: AdminClassAnnouncements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classAnnouncement = await _context.ClassAnnouncement
                .FirstOrDefaultAsync(m => m.recno == id);
            if (classAnnouncement == null)
            {
                return NotFound();
            }

            return View(classAnnouncement);
        }

        // GET: AdminClassAnnouncements/Create
        public IActionResult Create()
        {
            ViewBag.SchoolYear = _context.SchoolYear.Select(h => new SelectListItem
            {
                Value = h.schoolyears,
                Text = h.schoolyears
            });

            ViewBag.Teacher = _context.WebPageTeacher.Select(h => new SelectListItem
            {
                Value = h.fullname,
                Text = h.fullname
            });
            ViewBag.SubjectCode = _context.Subject.OrderBy(p => p.studentlevel).ThenBy(p => p.gradeyear).ThenBy(p => p.subjectcode).Select(h => new SelectListItem
            {
                Value = h.subjectcode,
                Text = h.studentlevel + " " + h.gradeyear + " " + h.subjectcode
            });

            ViewBag.SubjectName = _context.Subject.Select(h => new SelectListItem
            {
                Value = h.subjectname,
                Text = h.subjectname
            });
            return View();
        }

        // POST: AdminClassAnnouncements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,schoolyear,subjectcode,subjectname,proctor,announcementtitle,datestart,dateend,description,link,imagedata")] ClassAnnouncement classAnnouncement, IFormFile files)
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
                        var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "events")).Root + $@"\{fileName}";

                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            files.CopyTo(fs);
                            fs.Flush();
                        }

                        classAnnouncement.link = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                classAnnouncement.imagedata = ms.ToArray();
                            }

                            _context.Add(classAnnouncement);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
            }
            return View(classAnnouncement);
        }

        // GET: AdminClassAnnouncements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.SchoolYear = _context.SchoolYear.Select(h => new SelectListItem
            {
                Value = h.schoolyears,
                Text = h.schoolyears
            });

            ViewBag.Teacher = _context.WebPageTeacher.Select(h => new SelectListItem
            {
                Value = h.fullname,
                Text = h.fullname
            });
            ViewBag.SubjectCode = _context.Subject.OrderBy(p => p.studentlevel).ThenBy(p => p.gradeyear).ThenBy(p => p.subjectcode).Select(h => new SelectListItem
            {
                Value = h.subjectcode,
                Text = h.studentlevel + " " + h.gradeyear + " " + h.subjectcode
            });

            ViewBag.SubjectName = _context.Subject.Select(h => new SelectListItem
            {
                Value = h.subjectname,
                Text = h.subjectname
            });

            if (id == null)
            {
                return NotFound();
            }

            var classAnnouncement = await _context.ClassAnnouncement.FindAsync(id);
            if (classAnnouncement == null)
            {
                return NotFound();
            }
            return View(classAnnouncement);
        }

        // POST: AdminClassAnnouncements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,schoolyear,subjectcode,subjectname,proctor,announcementtitle,datestart,dateend,description,link,imagedata")] ClassAnnouncement classAnnouncement, IFormFile files)
        {
            string notifymessage = "";
            if (id != classAnnouncement.recno)
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
                        var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "events")).Root + $@"\{fileName}";

                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            files.CopyTo(fs);
                            fs.Flush();
                        }

                        classAnnouncement.link = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                classAnnouncement.imagedata = ms.ToArray();
                            }

                            _context.Update(classAnnouncement);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { id = classAnnouncement.recno, message = Messages.messageupdatesuccess });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {
                    string sql = "update ClassAnnouncement set schoolyear = '" +
                        classAnnouncement.schoolyear + "',subjectcode ='" +
                        classAnnouncement.subjectcode + "',subjectname ='" +
                        classAnnouncement.subjectname + "',proctor ='" +
                        classAnnouncement.proctor + "',announcementtitle ='" +
                        classAnnouncement.announcementtitle + "',datestart ='" +
                        classAnnouncement.datestart + "',dateend ='" +
                        classAnnouncement.dateend + "',description ='" +
                         classAnnouncement.description + "',link ='" +
                        classAnnouncement.link + "'" + " where recno = " + id;

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                }
                return RedirectToAction(nameof(Index));
            }
            return View(classAnnouncement);
        }

        // GET: AdminClassAnnouncements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classAnnouncement = await _context.ClassAnnouncement
                .FirstOrDefaultAsync(m => m.recno == id);
            if (classAnnouncement == null)
            {
                return NotFound();
            }

            return View(classAnnouncement);
        }

        // POST: AdminClassAnnouncements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classAnnouncement = await _context.ClassAnnouncement.FindAsync(id);
            _context.ClassAnnouncement.Remove(classAnnouncement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassAnnouncementExists(int id)
        {
            return _context.ClassAnnouncement.Any(e => e.recno == id);
        }

        public JsonResult getSubjectName(string subjectcode)
        {
            IList<string> result = new List<string>();

            var query = (from p in _context.Subject
                         where p.subjectcode == subjectcode
                         select p.subjectname);
            result.Add("");
            foreach (var x in query)
            {
                result.Add(x.ToString());
            }
            return Json(result);
        }
    }
}
