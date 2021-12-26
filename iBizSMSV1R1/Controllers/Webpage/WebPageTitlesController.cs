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

namespace iBizSMSV1R1.Controllers.Webpage
{
    //[Authorize(Roles = "Admin")]
    public class WebPageTitlesController : Controller
    {
        private static ApplicationDbContext _context;

        public WebPageTitlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WebPageTitles
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebPageTitles.ToListAsync());
        }

        // GET: WebPageTitles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageTitles = await _context.WebPageTitles
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageTitles == null)
            {
                return NotFound();
            }

            return View(webPageTitles);
        }

        // GET: WebPageTitles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WebPageTitles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,title, tagline, description, controller,action")] WebPageTitles webPageTitles, IFormFile files)
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
                        //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "events")).Root + $@"\{fileName}";

                        //using (FileStream fs = System.IO.File.Create(filepath))
                        //{
                        //    files.CopyTo(fs);
                        //    fs.Flush();
                        //}

                        webPageTitles.link = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageTitles.image = ms.ToArray();
                            }

                            _context.Add(webPageTitles);
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
            return View(webPageTitles);
        }

        // GET: WebPageTitles/Edit/5cvvvvvvvvvvvvvvvvvvvvvvvvvvvnj
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageTitles = await _context.WebPageTitles.FindAsync(id);
            if (webPageTitles == null)
            {
                return NotFound();
            }
            return View(webPageTitles);
        }

        // POST: WebPageTitles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,title, tagline,description, controller,action")] WebPageTitles webPageTitles, IFormFile files)
        {
            string notifymessage = "";
            if (id != webPageTitles.recno)
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
                        //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "events")).Root + $@"\{fileName}";

                        //using (FileStream fs = System.IO.File.Create(filepath))
                        //{
                        //    files.CopyTo(fs);
                        //    fs.Flush();
                        //}

                        webPageTitles.link = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageTitles.image = ms.ToArray();
                            }

                            _context.Update(webPageTitles);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { id = webPageTitles.recno, message = Messages.messageupdatesuccess });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {
                    string sql = "update webPageTitle set title = '" +
                        webPageTitles.title + "',tagline ='" +
                        webPageTitles.tagline + "',description ='" +                       
                         webPageTitles.description + "',controller ='" +
                        webPageTitles.controller + "',action ='" +
                        webPageTitles.action + "',link ='" +
                        webPageTitles.link + "'" + " where recno = " + id;

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);
                }
                return RedirectToAction(nameof(Index), new { id = webPageTitles.recno, message = notifymessage });
            }
            return RedirectToAction(nameof(Index), new { id = webPageTitles.recno, message = Messages.messagenotexists });
        }

        // GET: WebPageTitles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageTitles = await _context.WebPageTitles
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageTitles == null)
            {
                return NotFound();
            }

            return View(webPageTitles);
        }

        // POST: WebPageTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webPageTitles = await _context.WebPageTitles.FindAsync(id);
            _context.WebPageTitles.Remove(webPageTitles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebPageTitlesExists(int id)
        {
            return _context.WebPageTitles.Any(e => e.recno == id);
        }
               

    }
}
