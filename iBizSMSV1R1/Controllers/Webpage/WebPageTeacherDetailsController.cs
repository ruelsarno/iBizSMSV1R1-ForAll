using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.Models;

namespace iBizSMSV1R1.Controllers.Webpage
{
    public class WebPageTeacherDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WebPageTeacherDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WebPageTeacherDetails
        public async Task<IActionResult> Index(int recno, string id, string name)
        {
            ViewBag.RecNo = recno;
            ViewBag.Name = name;
            return View(await _context.WebPageTeacherDetail.Where(p=>p.recno == recno).ToListAsync());
        }

        // GET: WebPageTeacherDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageTeacherDetail = await _context.WebPageTeacherDetail
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (webPageTeacherDetail == null)
            {
                return NotFound();
            }

            return View(webPageTeacherDetail);
        }

        // GET: WebPageTeacherDetails/Create
        public IActionResult Create(int recno, string name)
        {
            ViewBag.RecNo = recno;
            ViewBag.Name = name;
            return View();
        }

        // POST: WebPageTeacherDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recordno,recno,jobdescription,biography,hobbiesinterest,contactinfo")] WebPageTeacherDetail webPageTeacherDetail, string name)
        {
            if (ModelState.IsValid)
            {
                _context.Add(webPageTeacherDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new {recno= webPageTeacherDetail.recno, name = name});
            }
            return View(webPageTeacherDetail);
        }

        // GET: WebPageTeacherDetails/Edit/5
        public async Task<IActionResult> Edit(int? id, string name)
        {
            ViewBag.Name = name;

            if (id == null)
            {
                return NotFound();
            }

            var webPageTeacherDetail = await _context.WebPageTeacherDetail.FindAsync(id);
            if (webPageTeacherDetail == null)
            {
                return NotFound();
            }
            return View(webPageTeacherDetail);
        }

        // POST: WebPageTeacherDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recordno,recno,jobdescription,biography,hobbiesinterest,contactinfo")] WebPageTeacherDetail webPageTeacherDetail, string name)
        {
            if (id != webPageTeacherDetail.recordno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webPageTeacherDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebPageTeacherDetailExists(webPageTeacherDetail.recordno))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new {name= name, recno = webPageTeacherDetail.recno, });
            }
            return View(webPageTeacherDetail);
        }

        // GET: WebPageTeacherDetails/Delete/5
        public async Task<IActionResult> Delete(int? id, string name)
        {
            ViewBag.Name = name;

            if (id == null)
            {
                return NotFound();
            }

            var webPageTeacherDetail = await _context.WebPageTeacherDetail
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (webPageTeacherDetail == null)
            {
                return NotFound();
            }

            return View(webPageTeacherDetail);
        }

        // POST: WebPageTeacherDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string name)
        {
            var webPageTeacherDetail = await _context.WebPageTeacherDetail.FindAsync(id);
            _context.WebPageTeacherDetail.Remove(webPageTeacherDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { name = name });
        }

        private bool WebPageTeacherDetailExists(int id)
        {
            return _context.WebPageTeacherDetail.Any(e => e.recordno == id);
        }
    }
}
