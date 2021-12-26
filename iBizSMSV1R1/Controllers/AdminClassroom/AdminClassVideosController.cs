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
namespace iBizSMSV1R1.Controllers.AdminClassroom
{
    //[Authorize(Roles = "Registrar,Admin")]
    public class AdminClassVideosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminClassVideosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClassVideos
        public async Task<IActionResult> Index(int recno, string subjectname)
        {
            ViewBag.Recno = recno;
            ViewBag.SubjectName = subjectname;
            return View(await _context.ClassVideo.Where(p=>p.recno == recno).ToListAsync());
        }

        // GET: ClassVideos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classVideo = await _context.ClassVideo
                .FirstOrDefaultAsync(m => m.recno == id);
            if (classVideo == null)
            {
                return NotFound();
            }

            return View(classVideo);
        }

        // GET: ClassVideos/Create
        public IActionResult Create(int recno, string subjectname)
        {
            ViewBag.Recno = recno;
            ViewBag.SubjectName = subjectname;
            return View();
        }

        // POST: ClassVideos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recordno,recno,subjectname,lessonno,lessontitle,videolink,active")] ClassVideo classVideo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {recno = classVideo.recno, subjectname = classVideo.subjectname });
            }
            return RedirectToAction(nameof(Index), new { recno = classVideo.recno, subjectname = classVideo.subjectname });
        }

        // GET: ClassVideos/Edit/5
        public async Task<IActionResult> Edit(int? id, int recno, string subjectname)
        {
            ViewBag.Recno = recno;
            ViewBag.SubjectName = subjectname;
            if (id == null)
            {
                return NotFound();
            }

            var classVideo = await _context.ClassVideo.FindAsync(id);
            if (classVideo == null)
            {
                return NotFound();
            }
            return View(classVideo);
        }

        // POST: ClassVideos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recordno,recno,subjectname,lessonno,lessontitle,videolink,active")] ClassVideo classVideo)
        {
            if (id != classVideo.recordno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassVideoExists(classVideo.recno))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { recno = classVideo.recno, subjectname = classVideo.subjectname });
            }
            return RedirectToAction(nameof(Index), new { recno = classVideo.recno, subjectname = classVideo.subjectname });
        }

        // GET: ClassVideos/Delete/5
        public async Task<IActionResult> Delete(int? id, int recno, string subjectname)
        {
            ViewBag.Recno = recno;
            ViewBag.SubjectName = subjectname;
            if (id == null)
            {
                return NotFound();
            }

            var classVideo = await _context.ClassVideo
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (classVideo == null)
            {
                return NotFound();
            }

            return View(classVideo);
        }

        // POST: ClassVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classVideo = await _context.ClassVideo.FindAsync(id);
            _context.ClassVideo.Remove(classVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { recno = classVideo.recno, subjectname = classVideo.subjectname });
        }

        private bool ClassVideoExists(int id)
        {
            return _context.ClassVideo.Any(e => e.recno == id);
        }
    }
}
