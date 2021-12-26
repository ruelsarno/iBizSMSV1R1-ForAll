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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using iBizSMSV1R1.Functions;

namespace iBizSMSV1R1.Controllers.Webpage
{
    //[Authorize(Roles = "Admin")]
    public class WebPageSubsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WebPageSubsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WebPageSubs
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebPageSubs.ToListAsync());
        }

        // GET: WebPageSubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageSubs = await _context.WebPageSubs
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageSubs == null)
            {
                return NotFound();
            }

            return View(webPageSubs);
        }

        // GET: WebPageSubs/Create
        public IActionResult Create()
        {
            ViewBag.WebPageTitle = _context.WebPageTitles.Select(h => new SelectListItem
            {
                Value = h.title,
                Text = h.title
            });
            ViewBag.SubWebPageTitle = _context.WebPages.Select(h => new SelectListItem
            {
                Value = h.subtitle,
                Text = h.subtitle
            }).Distinct();
            return View();
        }

        // POST: WebPageSubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,title,subtitle,subsubtitle,tagline,description,image,icon,link, controller,action,when,personname")] WebPageSubs webPageSubs, IFormFile files)
        {
            if (ModelState.IsValid)
            {
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageSubs.image = ms.ToArray();
                            }

                            _context.Add(webPageSubs);
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
            return View(webPageSubs);
        }

        // GET: WebPageSubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.WebPageTitle = _context.WebPageTitles.Select(h => new SelectListItem
            {
                Value = h.title,
                Text = h.title
            });
            ViewBag.SubWebPageTitle = _context.WebPages.Select(h => new SelectListItem
            {
                Value = h.subtitle,
                Text = h.subtitle
            }).Distinct();
            if (id == null)
            {
                return NotFound();
            }

            var webPageSubs = await _context.WebPageSubs.FindAsync(id);
            if (webPageSubs == null)
            {
                return NotFound();
            }
            return View(webPageSubs);
        }

        // POST: WebPageSubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,title,subtitle,subsubtitle,,tagline,description,image,icon,link, controller,action,when,personname")] WebPageSubs webPageSubs, IFormFile files)
        {
            string notifymessage = "";
            if (id != webPageSubs.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageSubs.image = ms.ToArray();
                            }

                            _context.Update(webPageSubs);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { id = webPageSubs.recno, message = Messages.messageupdatesuccess });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {
                    string sql = "update webPageSubs set title = '" + webPageSubs.title + "',subtitle ='" +
                        webPageSubs.subtitle + "',tagline ='" +
                        webPageSubs.tagline + "',description ='" +
                        webPageSubs.description + "',icon ='" +
                        webPageSubs.icon + "',link ='" +
                        webPageSubs.link + "',when ='" +
                        webPageSubs.when + "',personname ='" +
                        webPageSubs.personname + "',controller ='" +
                        webPageSubs.controller + "',action ='" +
                        webPageSubs.action + "'" + " where recno = " + id;

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                }
                return RedirectToAction(nameof(Index), new { id = webPageSubs.recno, message = notifymessage });
            }
            return RedirectToAction(nameof(Index), new { id = webPageSubs.recno, message = Messages.messagenotexists });
        }

        // GET: WebPageSubs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageSubs = await _context.WebPageSubs
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageSubs == null)
            {
                return NotFound();
            }

            return View(webPageSubs);
        }

        // POST: WebPageSubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webPageSubs = await _context.WebPageSubs.FindAsync(id);
            _context.WebPageSubs.Remove(webPageSubs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebPageSubsExists(int id)
        {
            return _context.WebPageSubs.Any(e => e.recno == id);
        }
    }
}
