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
    public class SchoolYearsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolYearsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SchoolYears
        public async Task<IActionResult> Index()
        {
            return View(await _context.SchoolYear.ToListAsync());
        }

        // GET: SchoolYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolYear = await _context.SchoolYear
                .FirstOrDefaultAsync(m => m.recno == id);
            if (schoolYear == null)
            {
                return NotFound();
            }

            return View(schoolYear);
        }

        // GET: SchoolYears/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SchoolYears/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,schoolyears,active")] SchoolYear schoolYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schoolYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolYear);
        }

        // GET: SchoolYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolYear = await _context.SchoolYear.FindAsync(id);
            if (schoolYear == null)
            {
                return NotFound();
            }
            return View(schoolYear);
        }

        // POST: SchoolYears/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,schoolyears,active")] SchoolYear schoolYear)
        {
            if (id != schoolYear.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolYearExists(schoolYear.recno))
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
            return View(schoolYear);
        }

        // GET: SchoolYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolYear = await _context.SchoolYear
                .FirstOrDefaultAsync(m => m.recno == id);
            if (schoolYear == null)
            {
                return NotFound();
            }

            return View(schoolYear);
        }

        // POST: SchoolYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schoolYear = await _context.SchoolYear.FindAsync(id);
            _context.SchoolYear.Remove(schoolYear);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolYearExists(int id)
        {
            return _context.SchoolYear.Any(e => e.recno == id);
        }
    }
}
