using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace iBizSMSV1R1.Controllers.Webpage
{
    public class WebPageEventDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WebPageEventDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WebPageEventDetails
        public async Task<IActionResult> Index(int recno, string eventtitle)
        {
            ViewBag.Recno = recno;
            ViewBag.EventTitle = eventtitle;
            return View(await _context.WebPageEventDetail.Where(p=>p.recno==recno).ToListAsync());
        }

        // GET: WebPageEventDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageEventDetail = await _context.WebPageEventDetail
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (webPageEventDetail == null)
            {
                return NotFound();
            }

            return View(webPageEventDetail);
        }

        // GET: WebPageEventDetails/Create
        public IActionResult Create(int recno, string eventtitle)
        {
            ViewBag.Recno = recno;
            ViewBag.EventTitle = eventtitle;
            return View();
        }

        // POST: WebPageEventDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recordno,recno,speakername,eventdate,venue,eventdetails,imagelink,imagedetails")] WebPageEventDetail webPageEventDetail, string eventtitle, IFormFile files)
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
                        //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "event-speakers")).Root + $@"\{fileName}";

                        //using (FileStream fs = System.IO.File.Create(filepath))
                        //{
                        //    files.CopyTo(fs);
                        //    fs.Flush();
                        //}

                        webPageEventDetail.imagelink = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageEventDetail.imagedetails = ms.ToArray();
                            }

                            _context.Add(webPageEventDetail);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { recno = webPageEventDetail.recno, eventtitle = eventtitle });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }

            }
                //if (ModelState.IsValid)
                //{
                //    _context.Add(webPageEventDetail);
                //    await _context.SaveChangesAsync();
                //    return RedirectToAction(nameof(Index), new { recno = webPageEventDetail.recno, eventtitle = eventtitle });
                //}
                return RedirectToAction(nameof(Index), new { recno = webPageEventDetail.recno, eventtitle = eventtitle });
        }

        // GET: WebPageEventDetails/Edit/5
        public async Task<IActionResult> Edit(int? id, int recno, string eventtitle)
        {
            ViewBag.Recno = recno;
            ViewBag.EventTitle = eventtitle;

            if (id == null)
            {
                return NotFound();
            }

            var webPageEventDetail = await _context.WebPageEventDetail.FindAsync(id);
            if (webPageEventDetail == null)
            {
                return NotFound();
            }
            return View(webPageEventDetail);
        }

        // POST: WebPageEventDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recordno,recno,speakername,eventdate,venue,eventdetails,imagelink,imagedetails")] WebPageEventDetail webPageEventDetail, string eventtitle, IFormFile files)
        {
            string notifymessage = "";
            if (id != webPageEventDetail.recordno)
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
                        //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "event-speakers")).Root + $@"\{fileName}";

                        //using (FileStream fs = System.IO.File.Create(filepath))
                        //{
                        //    files.CopyTo(fs);
                        //    fs.Flush();
                        //}

                        webPageEventDetail.imagelink = fileName;

                        //string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                webPageEventDetail.imagedetails = ms.ToArray();
                            }

                            _context.Update(webPageEventDetail);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { recno = webPageEventDetail.recno, eventtitle = eventtitle });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }
                }
                else
                {
                    string sql = "update WebPageEventDetail set recno = '" +
                        webPageEventDetail.recno + "',speakername ='" +
                        webPageEventDetail.speakername + "',eventdate ='" +
                        webPageEventDetail.eventdate + "',venue ='" +
                        webPageEventDetail.venue + "',eventdetails ='" +                       
                        webPageEventDetail.eventdetails + "'" + " where recordno = " + id;

                    notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);
                }

                    return RedirectToAction(nameof(Index),new {recno = webPageEventDetail.recno, eventtitle = eventtitle });
            }
            return RedirectToAction(nameof(Index), new { recno = webPageEventDetail.recno, eventtitle = eventtitle });
        }

        // GET: WebPageEventDetails/Delete/5
        public async Task<IActionResult> Delete(int? id, int recno, string eventtitle)
        {
            ViewBag.Recno = recno;
            ViewBag.EventTitle = eventtitle;

            if (id == null)
            {
                return NotFound();
            }

            var webPageEventDetail = await _context.WebPageEventDetail
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (webPageEventDetail == null)
            {
                return NotFound();
            }

            return View(webPageEventDetail);
        }

        // POST: WebPageEventDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string eventtitle)
        {
            var webPageEventDetail = await _context.WebPageEventDetail.FindAsync(id);
            _context.WebPageEventDetail.Remove(webPageEventDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { recno = webPageEventDetail.recno, eventtitle = eventtitle });
        }

        private bool WebPageEventDetailExists(int id)
        {
            return _context.WebPageEventDetail.Any(e => e.recordno == id);
        }
    }
}
