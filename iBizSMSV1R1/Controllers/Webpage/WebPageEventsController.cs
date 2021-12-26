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
    public class WebPageEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WebPageEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WebPageEvents
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebPageEvent.ToListAsync());
        }

        // GET: WebPageEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageEvent = await _context.WebPageEvent
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageEvent == null)
            {
                return NotFound();
            }

            return View(webPageEvent);
        }

        // GET: WebPageEvents/Create
        public IActionResult Create()
        {
            ViewBag.WebPageTitle = _context.WebPageTitles.Select(h => new SelectListItem
            {
                Value = h.title,
                Text = h.title
            });
            return View();
        }

        // POST: WebPageEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,title,tagline,eventtitle,icon,description,controller,action,image,link,active")] WebPageEvent webPageEvent, IFormFile files)
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

                        webPageEvent.link = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageEvent.image = ms.ToArray();
                            }

                            _context.Add(webPageEvent);
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
            return View(webPageEvent);
        }

        // GET: WebPageEvents/Edit/5
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

            var webPageEvent = await _context.WebPageEvent.FindAsync(id);
            if (webPageEvent == null)
            {
                return NotFound();
            }
            return View(webPageEvent);
        }

        // POST: WebPageEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,title,tagline,eventtitle,icon,description,controller,action,image,link,active")] WebPageEvent webPageEvent, IFormFile files)
        {
            string notifymessage = "";
            if (id != webPageEvent.recno)
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

                        webPageEvent.link = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageEvent.image = ms.ToArray();
                            }

                            _context.Update(webPageEvent);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { id = webPageEvent.recno, message = Messages.messageupdatesuccess });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {
                    string sql = "update WebPageEvent set title = '" +
                        webPageEvent.title + "',tagline ='" +
                        webPageEvent.tagline + "',eventtitle ='" +
                        webPageEvent.eventtitle + "',icon ='" +
                        webPageEvent.icon + "',description ='" +
                         webPageEvent.description + "',controller ='" +
                        webPageEvent.controller + "',action ='" +
                        webPageEvent.controller + "',active ='" +
                        webPageEvent.active + "'" + " where recno = " + id;

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                }
                return RedirectToAction(nameof(Index), new { id = webPageEvent.recno, message = notifymessage });
            }
            return RedirectToAction(nameof(Index), new { id = webPageEvent.recno, message = Messages.messagenotexists });
        }

        // GET: WebPageEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageEvent = await _context.WebPageEvent
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageEvent == null)
            {
                return NotFound();
            }

            return View(webPageEvent);
        }

        // POST: WebPageEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webPageEvent = await _context.WebPageEvent.FindAsync(id);
            _context.WebPageEvent.Remove(webPageEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebPageEventExists(int id)
        {
            return _context.WebPageEvent.Any(e => e.recno == id);
        }
    }
}
