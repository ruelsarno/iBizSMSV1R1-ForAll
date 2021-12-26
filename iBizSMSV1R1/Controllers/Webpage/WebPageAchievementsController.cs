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
using iBizSMSV1R1.Functions;

namespace iBizSMSV1R1.Controllers.Webpage
{
    public class WebPageAchievementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WebPageAchievementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WebPageAchievements
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebPageAchievement.ToListAsync());
        }

        // GET: WebPageAchievements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageAchievement = await _context.WebPageAchievement
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageAchievement == null)
            {
                return NotFound();
            }

            return View(webPageAchievement);
        }

        // GET: WebPageAchievements/Create
        public IActionResult Create()
        {
            ViewBag.WebPageTitle = _context.WebPageTitles.Select(h => new SelectListItem
            {
                Value = h.title,
                Text = h.title
            });
            return View();
        }

        // POST: WebPageAchievements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,title,tagline,achievementname,icon,description,controller,action,image,link,active,priority,postdate")] WebPageAchievement webPageAchievement, IFormFile files)
        {
            String datetoday = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToShortDateString();

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

                        webPageAchievement.link = fileName;
                        webPageAchievement.postdate = datetoday;
                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageAchievement.image = ms.ToArray();
                            }

                            _context.Add(webPageAchievement);
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
            return View(webPageAchievement);
        }

        // GET: WebPageAchievements/Edit/5
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

            var webPageAchievement = await _context.WebPageAchievement.FindAsync(id);
            if (webPageAchievement == null)
            {
                return NotFound();
            }
            return View(webPageAchievement);
        }

        // POST: WebPageAchievements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,title,tagline,achievementname,icon,description,controller,action,image,link,active,priority,postdate")] WebPageAchievement webPageAchievement, IFormFile files)
        {
            String datetoday = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToShortDateString();
            webPageAchievement.postdate = datetoday;
            string notifymessage = "";
            if (id != webPageAchievement.recno)
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

                        webPageAchievement.link = fileName;
                        
                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageAchievement.image = ms.ToArray();
                            }

                            _context.Update(webPageAchievement);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { id = webPageAchievement.recno, message = Messages.messageupdatesuccess });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {
                    string sql = "update webPageAchievement set title = '" +
                        webPageAchievement.title + "',tagline ='" +
                        webPageAchievement.tagline + "',achievementname ='" +
                        webPageAchievement.achievementname + "',icon ='" +
                        webPageAchievement.icon + "',description ='" +
                         webPageAchievement.description + "',controller ='" +
                        webPageAchievement.controller + "',action ='" +
                        webPageAchievement.controller + "',priority ='" +
                        webPageAchievement.priority + "',postdate ='" +
                        webPageAchievement.postdate + "',active ='" +
                        webPageAchievement.active + "'" + " where recno = " + id;

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                }
                return RedirectToAction(nameof(Index), new { id = webPageAchievement.recno, message = notifymessage });
            }
            return View(webPageAchievement);
        }

        // GET: WebPageAchievements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageAchievement = await _context.WebPageAchievement
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageAchievement == null)
            {
                return NotFound();
            }

            return View(webPageAchievement);
        }

        // POST: WebPageAchievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webPageAchievement = await _context.WebPageAchievement.FindAsync(id);
            _context.WebPageAchievement.Remove(webPageAchievement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebPageAchievementExists(int id)
        {
            return _context.WebPageAchievement.Any(e => e.recno == id);
        }
    }
}
