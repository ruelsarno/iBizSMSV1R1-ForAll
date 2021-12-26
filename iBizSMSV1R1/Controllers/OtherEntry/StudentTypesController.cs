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
    public class StudentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentType.ToListAsync());
        }

        // GET: StudentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentType = await _context.StudentType
                .FirstOrDefaultAsync(m => m.recno == id);
            if (studentType == null)
            {
                return NotFound();
            }

            return View(studentType);
        }

        // GET: StudentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,studenttypes")] StudentType studentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentType);
        }

        // GET: StudentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentType = await _context.StudentType.FindAsync(id);
            if (studentType == null)
            {
                return NotFound();
            }
            return View(studentType);
        }

        // POST: StudentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,studenttypes")] StudentType studentType)
        {
            if (id != studentType.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentTypeExists(studentType.recno))
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
            return View(studentType);
        }

        // GET: StudentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentType = await _context.StudentType
                .FirstOrDefaultAsync(m => m.recno == id);
            if (studentType == null)
            {
                return NotFound();
            }

            return View(studentType);
        }

        // POST: StudentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentType = await _context.StudentType.FindAsync(id);
            _context.StudentType.Remove(studentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentTypeExists(int id)
        {
            return _context.StudentType.Any(e => e.recno == id);
        }
    }
}
