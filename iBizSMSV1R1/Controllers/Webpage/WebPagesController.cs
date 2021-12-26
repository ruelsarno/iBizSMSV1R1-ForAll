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
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace iBizSMSV1R1.Controllers.Webpage
{
    //[Authorize(Roles = "Admin")]
    public class WebPagesController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public WebPagesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: WebPages
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebPages.ToListAsync());
        }

        // GET: WebPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPages = await _context.WebPages
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPages == null)
            {
                return NotFound();
            }

            return View(webPages);
        }

        // GET: WebPages/Create
        public IActionResult Create()
        {
            ViewBag.WebPageTitle = _context.WebPageTitles.Select(h => new SelectListItem
            {
                Value = h.title,
                Text = h.title
            });
            return View();
        }

        // POST: WebPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,title,subtitle,tagline,description,image,icon,link,controller,action,active")] WebPages webPages, IFormFile files)
        {
            string notifymessage = "";
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
                        var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{fileName}";

                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            files.CopyTo(fs);
                            fs.Flush();
                        }

                        webPages.link = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPages.image = ms.ToArray();
                            }

                            _context.Add(webPages);
                            await _context.SaveChangesAsync();                            
                            return RedirectToAction(nameof(Index));
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {                    
                    string sql = "insert into webPages(title,subtitle,tagline,description,icon,link,controller,action)values('" +
                        webPages.title + "','" +
                        webPages.subtitle + "','" +
                        webPages.tagline + "','" +
                        webPages.description + "','" +
                        webPages.icon + "','" +
                        webPages.link + "','" +
                        webPages.controller + "','" +
                        webPages.action + "')";

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                }
                return RedirectToAction(nameof(Index), new { id = webPages.recno, message = notifymessage });
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: WebPages/Edit/5
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

            var webPages = await _context.WebPages.FindAsync(id);
            if (webPages == null)
            {
                return NotFound();
            }
            return View(webPages);
        }

        // POST: WebPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,title,subtitle,tagline,description,image,icon,link,controller,action,active")] WebPages webPages, IFormFile files)
        {
            string notifymessage = "";
            if (id != webPages.recno)
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
                        var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{fileName}";                      

                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            files.CopyTo(fs);
                            fs.Flush();
                        }

                        webPages.link = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPages.image = ms.ToArray();
                            }

                            _context.Update(webPages);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { id = webPages.recno, message = Messages.messageupdatesuccess });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {
                    string sql = "update webPages set title = '" + webPages.title + "',subtitle ='" +
                        webPages.subtitle + "',tagline ='" +
                        webPages.tagline + "',description ='" +
                        webPages.description + "',icon ='" +
                        webPages.icon + "',link ='" +
                        webPages.link + "',controller ='" +
                        webPages.controller + "',action ='" +
                        webPages.action + "',active ='" +
                        webPages.active + "'" + " where recno = " + id;

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                }
                return RedirectToAction(nameof(Index), new { id = webPages.recno, message = notifymessage });
            }
            return RedirectToAction(nameof(Index), new { id = webPages.recno, message = Messages.messagenotexists });
        }

        // GET: WebPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPages = await _context.WebPages
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPages == null)
            {
                return NotFound();
            }

            return View(webPages);
        }

        // POST: WebPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webPages = await _context.WebPages.FindAsync(id);
            _context.WebPages.Remove(webPages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebPagesExists(int id)
        {
            return _context.WebPages.Any(e => e.recno == id);
        }

      
    }
}
