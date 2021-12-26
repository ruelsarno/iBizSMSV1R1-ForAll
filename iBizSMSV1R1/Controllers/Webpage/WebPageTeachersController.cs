using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using iBizSMSV1R1.Functions;
using Microsoft.Extensions.FileProviders;
using ReflectionIT.Mvc.Paging;

namespace iBizSMSV1R1.Controllers.Webpage
{
    //[Authorize(Roles = "Admin,HR")]
    public class WebPageTeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WebPageTeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WebPageTeachers
        public async Task<IActionResult> Index(string id, string idno, string SearchString, bool notUsed, int page, int pagedivider, string message)
        {
            ViewBag.Message = message;
            ViewBag.IDNO = idno;
            ViewBag.ID = id;
            ViewBag.SearchString = SearchString;

            if (notUsed)
            {
                return View("From [HttpPost]Index: filter on " + SearchString);
            }
            if (page <= 0)
            {
                page = 1;
            }
            if (pagedivider <= 0)
            {
                pagedivider = 20;
            }
            ViewBag.Page = page;
            if (!String.IsNullOrEmpty(SearchString))
            {

                var subjects = _context.WebPageTeacher.Where(p => p.fullname.Contains(SearchString) || p.specialization.Contains(SearchString)).OrderBy(p => p.recno);
                var model = PagingList.Create(subjects, pagedivider, page);
                var RecordCount = subjects.Count();
                ViewBag.RecordCount = RecordCount;
                return View(model);
            }
            else
            {
                var subjects = _context.WebPageTeacher.OrderBy(p => p.fullname);
                var model = await PagingList.CreateAsync(subjects, pagedivider, page);
                var RecordCount = subjects.Count();
                ViewBag.RecordCount = RecordCount;
                return View(model);
            }

        }

        // GET: WebPageTeachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageTeacher = await _context.WebPageTeacher
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageTeacher == null)
            {
                return NotFound();
            }

            return View(webPageTeacher);
        }

        // GET: WebPageTeachers/Create
        public IActionResult Create()
        {
            ViewBag.WebPageTitle = _context.WebPageTitles.Select(h => new SelectListItem
            {
                Value = h.title,
                Text = h.title
            });
            return View();
        }

        // POST: WebPageTeachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,title,tagline,category,specialization,fullname,controller,action,image,link")] WebPageTeacher webPageTeacher, IFormFile files)
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
                        //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "teachers")).Root + $@"\{fileName}";

                        //using (FileStream fs = System.IO.File.Create(filepath))
                        //{
                        //    files.CopyTo(fs);
                        //    fs.Flush();
                        //}

                        //webPageTeacher.link = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageTeacher.image = ms.ToArray();
                            }

                            _context.Add(webPageTeacher);
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
            return View(webPageTeacher);
        }

        // GET: WebPageTeachers/Edit/5
        public async Task<IActionResult> Edit(int? id, string name)
        {
            ViewBag.Name = name;
            ViewBag.WebPageTitle = _context.WebPageTitles.Select(h => new SelectListItem
            {
                Value = h.title,
                Text = h.title
            });
            if (id == null)
            {
                return NotFound();
            }

            var webPageTeacher = await _context.WebPageTeacher.FindAsync(id);
            if (webPageTeacher == null)
            {
                return NotFound();
            }
            return View(webPageTeacher);
        }

        // POST: WebPageTeachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,title,tagline,category,specialization,fullname,controller,action,image,link")] WebPageTeacher webPageTeacher, IFormFile files)
        {
            string notifymessage = "";
            if (id != webPageTeacher.recno)
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
                        //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "teachers")).Root + $@"\{fileName}";

                        //using (FileStream fs = System.IO.File.Create(filepath))
                        //{
                        //    files.CopyTo(fs);
                        //    fs.Flush();
                        //}

                        //webPageTeacher.link = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageTeacher.image = ms.ToArray();
                            }

                            _context.Update(webPageTeacher);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { id = webPageTeacher.recno, message = Messages.messageupdatesuccess });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {
                    string sql = "update webPageTeacher set title = '" + webPageTeacher.title + "',tagline ='" +
                        webPageTeacher.tagline + "',category ='" +
                        webPageTeacher.category + "',specialization ='" +
                        webPageTeacher.specialization + "',fullname ='" +
                        webPageTeacher.fullname + "',controller ='" +                      
                        webPageTeacher.controller + "',action ='" +
                        webPageTeacher.action + "'" + " where recno = " + id;

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                }
                return RedirectToAction(nameof(Index), new { id = webPageTeacher.recno, message = notifymessage });
            }
            return RedirectToAction(nameof(Index), new { id = webPageTeacher.recno, message = Messages.messagenotexists });
        }

        // GET: WebPageTeachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageTeacher = await _context.WebPageTeacher
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageTeacher == null)
            {
                return NotFound();
            }

            return View(webPageTeacher);
        }

        // POST: WebPageTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webPageTeacher = await _context.WebPageTeacher.FindAsync(id);
            _context.WebPageTeacher.Remove(webPageTeacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebPageTeacherExists(int id)
        {
            return _context.WebPageTeacher.Any(e => e.recno == id);
        }
    }
}
