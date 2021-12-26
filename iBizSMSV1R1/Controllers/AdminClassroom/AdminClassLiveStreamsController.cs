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
    public class AdminClassLiveStreamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminClassLiveStreamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminClassLiveStreams
        public async Task<IActionResult> Index(int recno, string subjectname)
        {
            ViewBag.Recno = recno;
            ViewBag.SubjectName = subjectname;
            return View(await _context.ClassLiveStream.Where(p => p.recno == recno).ToListAsync());
        }

        // GET: AdminClassLiveStreams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classLiveStream = await _context.ClassLiveStream
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (classLiveStream == null)
            {
                return NotFound();
            }

            return View(classLiveStream);
        }

        // GET: AdminClassLiveStreams/Create
        public IActionResult Create(int recno, string subjectname)
        {
            ViewBag.Recno = recno;
            ViewBag.SubjectName = subjectname;
            return View();
        }

        // POST: AdminClassLiveStreams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recordno,recno,subjectname,lessonno,lessontitle,videolink,active")] ClassLiveStream classLiveStream)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classLiveStream);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { recno = classLiveStream.recno, subjectname = classLiveStream.subjectname });
            }
            return RedirectToAction(nameof(Index), new { recno = classLiveStream.recno, subjectname = classLiveStream.subjectname });
        }

        // GET: AdminClassLiveStreams/Edit/5
        public async Task<IActionResult> Edit(int? id, int recno, string subjectname)
        {
            ViewBag.Recno = recno;
            ViewBag.SubjectName = subjectname;
            if (id == null)
            {
                return NotFound();
            }

            var classLiveStream = await _context.ClassLiveStream.FindAsync(id);
            if (classLiveStream == null)
            {
                return NotFound();
            }
            return View(classLiveStream);
        }

        // POST: AdminClassLiveStreams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recordno,recno,subjectname,lessonno,lessontitle,videolink,active")] ClassLiveStream classLiveStream)
        {
            if (id != classLiveStream.recordno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classLiveStream);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassLiveStreamExists(classLiveStream.recordno))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { recno = classLiveStream.recno, subjectname = classLiveStream.subjectname });
            }
            return RedirectToAction(nameof(Index), new { recno = classLiveStream.recno, subjectname = classLiveStream.subjectname });
        }

        // GET: AdminClassLiveStreams/Delete/5
        public async Task<IActionResult> Delete(int? id, int recno, string subjectname)
        {
            ViewBag.Recno = recno;
            ViewBag.SubjectName = subjectname;
            if (id == null)
            {
                return NotFound();
            }

            var classLiveStream = await _context.ClassLiveStream
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (classLiveStream == null)
            {
                return NotFound();
            }

            return View(classLiveStream);
        }

        // POST: AdminClassLiveStreams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classLiveStream = await _context.ClassLiveStream.FindAsync(id);
            _context.ClassLiveStream.Remove(classLiveStream);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { recno = classLiveStream.recno, subjectname = classLiveStream.subjectname });
        }

        private bool ClassLiveStreamExists(int id)
        {
            return _context.ClassLiveStream.Any(e => e.recordno == id);
        }
    }
}
