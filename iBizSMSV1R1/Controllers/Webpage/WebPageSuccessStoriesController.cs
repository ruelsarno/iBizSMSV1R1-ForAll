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
    public class WebPageSuccessStoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WebPageSuccessStoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuccessStories
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebPageSuccessStory.ToListAsync());
        }

        // GET: SuccessStories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var successStory = await _context.WebPageSuccessStory
                .FirstOrDefaultAsync(m => m.recno == id);
            if (successStory == null)
            {
                return NotFound();
            }

            return View(successStory);
        }

        // GET: SuccessStories/Create
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

        // POST: SuccessStories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,title,subtitle,storydate,author,paragraph1,paragraph2,paragraph3,paragraph4,image,icon,link,controller,action")] WebPageSuccessStory successStory, IFormFile files)
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
                                successStory.image = ms.ToArray();
                            }

                            _context.Add(successStory);
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
            return View(successStory);
        }

        // GET: SuccessStories/Edit/5
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

            var successStory = await _context.WebPageSuccessStory.FindAsync(id);
            if (successStory == null)
            {
                return NotFound();
            }
            return View(successStory);
        }

        // POST: SuccessStories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,title,subtitle,storydate,author,paragraph1,paragraph2,paragraph3,paragraph4,image,icon,link,controller,action")] WebPageSuccessStory successStory, IFormFile files)
        {
            string notifymessage = "";
            if (id != successStory.recno)
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
                                successStory.image = ms.ToArray();
                            }

                            _context.Update(successStory);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { id = successStory.recno, message = Messages.messageupdatesuccess });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {
                    string sql = "update WebPageSuccessStory set title = '" + successStory.title + "',subtitle ='" +                        
                        successStory.subtitle + "',storydate ='" +
                        successStory.storydate + "',author ='" +
                        successStory.author + "',paragraph1 ='" +
                        successStory.paragraph1 + "',paragraph2 ='" +                      
                        successStory.icon + "',link ='" +
                        successStory.link + "',controller ='" +
                        successStory.controller + "',action ='" +
                        successStory.action + "'" + " where recno = " + id;
                    
                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                }
                return RedirectToAction(nameof(Index), new { id = successStory.recno, message = notifymessage });
            }
            return RedirectToAction(nameof(Index), new { id = successStory.recno, message = Messages.messagenotexists });
        }

        // GET: SuccessStories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var successStory = await _context.WebPageSuccessStory
                .FirstOrDefaultAsync(m => m.recno == id);
            if (successStory == null)
            {
                return NotFound();
            }

            return View(successStory);
        }

        // POST: SuccessStories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var successStory = await _context.WebPageSuccessStory.FindAsync(id);
            _context.WebPageSuccessStory.Remove(successStory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuccessStoryExists(int id)
        {
            return _context.WebPageSuccessStory.Any(e => e.recno == id);
        }
    }
}
