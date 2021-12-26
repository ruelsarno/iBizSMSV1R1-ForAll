using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using iBizSMSV1R1.Functions;

namespace iBizSMSV1R1.Controllers.Webpage
{
    public class WebPageAboutsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WebPageAboutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WebPageAbouts
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebPageAbout.ToListAsync());
        }

        // GET: WebPageAbouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageAbout = await _context.WebPageAbout
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageAbout == null)
            {
                return NotFound();
            }

            return View(webPageAbout);
        }

        // GET: WebPageAbouts/Create
        public IActionResult Create()
        {
            ViewBag.WebPageTitle = _context.WebPageTitles.Select(h => new SelectListItem
            {
                Value = h.title,
                Text = h.title
            });
            return View();
        }

        // POST: WebPageAbouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,title,tagline,description,icon,controller,action,image,link")] WebPageAbout webPageAbout, IFormFile files)
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
                        var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "About")).Root + $@"\{fileName}";

                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            files.CopyTo(fs);
                            fs.Flush();
                        }

                        webPageAbout.link = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageAbout.image = ms.ToArray();
                            }

                            _context.Add(webPageAbout);
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
            return View(webPageAbout);
        }

        // GET: WebPageAbouts/Edit/5
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

            var webPageAbout = await _context.WebPageAbout.FindAsync(id);
            if (webPageAbout == null)
            {
                return NotFound();
            }
            return View(webPageAbout);
        }

        // POST: WebPageAbouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,title,tagline,description,icon,controller,action,image,link")] WebPageAbout webPageAbout, IFormFile files)
        {
            string notifymessage = "";
            if (id != webPageAbout.recno)
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
                        //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "about")).Root + $@"\{fileName}";

                        //using (FileStream fs = System.IO.File.Create(filepath))
                        //{
                        //    files.CopyTo(fs);
                        //    fs.Flush();
                        //}

                        webPageAbout.link = fileName;

                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageAbout.image = ms.ToArray();
                            }

                            _context.Update(webPageAbout);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { id = webPageAbout.recno, message = Messages.messageupdatesuccess });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {
                    string sql = "update WebPageAbout set title = '" +
                        webPageAbout.title + "',tagline ='" +
                        webPageAbout.tagline + "',description ='" +                       
                        webPageAbout.description + "',icon ='" +
                        webPageAbout.icon + "',controller ='" +
                        webPageAbout.controller + "',action ='" +
                        webPageAbout.action + "'" + " where recno = " + id;

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                }
                return RedirectToAction(nameof(Index), new { id = webPageAbout.recno, message = notifymessage });
            }
            return RedirectToAction(nameof(Index), new { id = webPageAbout.recno, message = Messages.messagenotexists });
        }

        // GET: WebPageAbouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageAbout = await _context.WebPageAbout
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageAbout == null)
            {
                return NotFound();
            }

            return View(webPageAbout);
        }

        // POST: WebPageAbouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webPageAbout = await _context.WebPageAbout.FindAsync(id);
            _context.WebPageAbout.Remove(webPageAbout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebPageAboutExists(int id)
        {
            return _context.WebPageAbout.Any(e => e.recno == id);
        }
    }
}
