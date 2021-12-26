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
using System.IO;
using Microsoft.AspNetCore.Http;
using iBizSMSV1R1.Functions;
using Microsoft.Extensions.FileProviders;

namespace iBizSMSV1R1.Controllers.Webpage
{
    //[Authorize(Roles = "Admin")]
    public class WebPageCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WebPageCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WebPageCourses
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebPageCourse.ToListAsync());
        }

        // GET: WebPageCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageCourse = await _context.WebPageCourse
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageCourse == null)
            {
                return NotFound();
            }

            return View(webPageCourse);
        }

        // GET: WebPageCourses/Create
        public IActionResult Create()
        {
            ViewBag.WebPageTitle = _context.WebPageTitles.Select(h => new SelectListItem
            {
                Value = h.title,
                Text = h.title
            });
            return View();
        }

        // POST: WebPageCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,title,tagline,category,coursetitle,description,icon,controller,action,image,link")] WebPageCourse webPageCourse, IFormFile files)
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
                        var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images","courses")).Root + $@"\{fileName}";

                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            files.CopyTo(fs);
                            fs.Flush();
                        }

                        webPageCourse.link = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageCourse.image = ms.ToArray();
                            }

                            _context.Add(webPageCourse);
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
            return View(webPageCourse);
        }

        // GET: WebPageCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.WebPageTitle = _context.WebPageTitles.Select(h => new SelectListItem
            {
                Value = h.title,
                Text = h.title
            });
            if (id == null)
            {
                return NotFound();
            }

            var webPageCourse = await _context.WebPageCourse.FindAsync(id);
            if (webPageCourse == null)
            {
                return NotFound();
            }
            return View(webPageCourse);
        }

        // POST: WebPageCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,title,tagline,category,coursetitle,description,icon,controller,action,image,link")] WebPageCourse webPageCourse, IFormFile files)
        {
            string notifymessage = "";
            if (id != webPageCourse.recno)
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
                        //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "courses")).Root + $@"\{fileName}";

                        //using (FileStream fs = System.IO.File.Create(filepath))
                        //{
                        //    files.CopyTo(fs);
                        //    fs.Flush();
                        //}

                        webPageCourse.link = fileName;
                       
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageCourse.image = ms.ToArray();
                            }

                            _context.Update(webPageCourse);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { id = webPageCourse.recno, message = Messages.messageupdatesuccess });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {
                    string sql = "update WebPageCourse set title = '" +
                        webPageCourse.title + "',tagline ='" +
                        webPageCourse.tagline + "',category ='" +
                        webPageCourse.category + "',coursetitle ='" +
                        webPageCourse.coursetitle + "',description ='" +
                        webPageCourse.description + "',icon ='" +
                        webPageCourse.icon + "',controller ='" +
                        webPageCourse.controller + "',action ='" +
                        webPageCourse.action + "'" + " where recno = " + id;                 

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                }
                return RedirectToAction(nameof(Index), new { id = webPageCourse.recno, message = notifymessage });
            }
            return RedirectToAction(nameof(Index), new { id = webPageCourse.recno, message = Messages.messagenotexists });
        }

        // GET: WebPageCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageCourse = await _context.WebPageCourse
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageCourse == null)
            {
                return NotFound();
            }

            return View(webPageCourse);
        }

        // POST: WebPageCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webPageCourse = await _context.WebPageCourse.FindAsync(id);
            _context.WebPageCourse.Remove(webPageCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebPageCourseExists(int id)
        {
            return _context.WebPageCourse.Any(e => e.recno == id);
        }
    }
}
