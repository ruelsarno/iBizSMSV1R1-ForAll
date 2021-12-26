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
using Microsoft.Extensions.FileProviders;
using iBizSMSV1R1.Functions;
using Microsoft.AspNetCore.Http;

namespace iBizSMSV1R1.Controllers.Webpage
{
    //[Authorize(Roles = "Admin")]
    public class WebPageBlogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WebPageBlogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WebPageBlogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebPageBlog.ToListAsync());
        }

        // GET: WebPageBlogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageBlog = await _context.WebPageBlog
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageBlog == null)
            {
                return NotFound();
            }

            return View(webPageBlog);
        }

        // GET: WebPageBlogs/Create
        public IActionResult Create()
        {
            ViewBag.WebPageTitle = _context.WebPageTitles.Select(h => new SelectListItem
            {
                Value = h.title,
                Text = h.title
            });
            return View();
        }

        // POST: WebPageBlogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,title,tagline,blogtitle,author,postdate,description,controller,action,link,image")] WebPageBlog webPageBlog, IFormFile files)
        {
            if(ModelState.IsValid)
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
                        var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "blogs")).Root + $@"\{fileName}";

                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            files.CopyTo(fs);
                            fs.Flush();
                        }

                        webPageBlog.link = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageBlog.image = ms.ToArray();
                            }

                            _context.Add(webPageBlog);
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
            return View(webPageBlog);
        }

        // GET: WebPageBlogs/Edit/5
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

            var webPageBlog = await _context.WebPageBlog.FindAsync(id);
            if (webPageBlog == null)
            {
                return NotFound();
            }
            return View(webPageBlog);
        }

        // POST: WebPageBlogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,title,tagline,blogtitle,author,postdate,description,controller,action,link,image")] WebPageBlog webPageBlog, IFormFile files)
        {
            string notifymessage = "";
            if (id != webPageBlog.recno)
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
                        var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "blogs")).Root + $@"\{fileName}";

                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            files.CopyTo(fs);
                            fs.Flush();
                        }

                        webPageBlog.link = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageBlog.image = ms.ToArray();
                            }

                            _context.Update(webPageBlog);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { id = webPageBlog.recno, message = Messages.messageupdatesuccess });
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
                        webPageBlog.title + "',tagline ='" +
                        webPageBlog.tagline + "',blogtitle ='" +
                        webPageBlog.blogtitle + "',author ='" +
                        webPageBlog.author + "',postdate ='" +
                        webPageBlog.postdate + "',description ='" +
                        webPageBlog.description + "',link ='" +
                        webPageBlog.link + "',controller ='" +
                        webPageBlog.controller + "',action ='" +
                        webPageBlog.action + "'" + " where recno = " + id;

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                }
                return RedirectToAction(nameof(Index), new { id = webPageBlog.recno, message = notifymessage });
            }
            return RedirectToAction(nameof(Index), new { id = webPageBlog.recno, message = Messages.messagenotexists });
        }

        // GET: WebPageBlogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageBlog = await _context.WebPageBlog
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageBlog == null)
            {
                return NotFound();
            }

            return View(webPageBlog);
        }

        // POST: WebPageBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webPageBlog = await _context.WebPageBlog.FindAsync(id);
            _context.WebPageBlog.Remove(webPageBlog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebPageBlogExists(int id)
        {
            return _context.WebPageBlog.Any(e => e.recno == id);
        }
    }
}
