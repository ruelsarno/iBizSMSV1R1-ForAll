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
    public class SchoolTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SchoolTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SchoolType.ToListAsync());
        }

        // GET: SchoolTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolType = await _context.SchoolType
                .FirstOrDefaultAsync(m => m.recno == id);
            if (schoolType == null)
            {
                return NotFound();
            }

            return View(schoolType);
        }

        // GET: SchoolTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SchoolTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,schooltypes")] SchoolType schoolType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schoolType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolType);
        }

        // GET: SchoolTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolType = await _context.SchoolType.FindAsync(id);
            if (schoolType == null)
            {
                return NotFound();
            }
            return View(schoolType);
        }

        // POST: SchoolTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,schooltypes")] SchoolType schoolType)
        {
            if (id != schoolType.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolTypeExists(schoolType.recno))
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
            return View(schoolType);
        }

        // GET: SchoolTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolType = await _context.SchoolType
                .FirstOrDefaultAsync(m => m.recno == id);
            if (schoolType == null)
            {
                return NotFound();
            }

            return View(schoolType);
        }

        // POST: SchoolTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schoolType = await _context.SchoolType.FindAsync(id);
            _context.SchoolType.Remove(schoolType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolTypeExists(int id)
        {
            return _context.SchoolType.Any(e => e.recno == id);
        }
    }
}
