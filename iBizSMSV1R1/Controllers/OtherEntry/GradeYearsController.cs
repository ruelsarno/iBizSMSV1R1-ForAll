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
namespace iBizSMSV1R1.Controllers.OtherEntry
{
    //[Authorize(Roles = "Registrar,Admin")]
    public class GradeYearsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GradeYearsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GradeYears
        public async Task<IActionResult> Index()
        {
            return View(await _context.GradeYear.ToListAsync());
        }

        // GET: GradeYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeYear = await _context.GradeYear
                .FirstOrDefaultAsync(m => m.recno == id);
            if (gradeYear == null)
            {
                return NotFound();
            }

            return View(gradeYear);
        }

        // GET: GradeYears/Create
        public IActionResult Create()
        {
            ViewBag.Studentlevel = _context.Studentlevel.Select(h => new SelectListItem
            {
                Value = h.studentlevels,
                Text = h.studentlevels
            });
            return View();
        }

        // POST: GradeYears/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,studentlevels,gradeyears")] GradeYear gradeYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gradeYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gradeYear);
        }

        // GET: GradeYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Studentlevel = _context.Studentlevel.Select(h => new SelectListItem
            {
                Value = h.studentlevels,
                Text = h.studentlevels
            });
            if (id == null)
            {
                return NotFound();
            }

            var gradeYear = await _context.GradeYear.FindAsync(id);
            if (gradeYear == null)
            {
                return NotFound();
            }
            return View(gradeYear);
        }

        // POST: GradeYears/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,studentlevels,gradeyears")] GradeYear gradeYear)
        {
            if (id != gradeYear.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gradeYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeYearExists(gradeYear.recno))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gradeYear);
        }

        // GET: GradeYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeYear = await _context.GradeYear
                .FirstOrDefaultAsync(m => m.recno == id);
            if (gradeYear == null)
            {
                return NotFound();
            }

            return View(gradeYear);
        }

        // POST: GradeYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gradeYear = await _context.GradeYear.FindAsync(id);
            _context.GradeYear.Remove(gradeYear);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeYearExists(int id)
        {
            return _context.GradeYear.Any(e => e.recno == id);
        }
    }
}
