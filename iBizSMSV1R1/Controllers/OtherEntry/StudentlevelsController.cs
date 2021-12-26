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
    public class StudentlevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentlevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Studentlevels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Studentlevel.ToListAsync());
        }

        // GET: Studentlevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentlevel = await _context.Studentlevel
                .FirstOrDefaultAsync(m => m.recno == id);
            if (studentlevel == null)
            {
                return NotFound();
            }

            return View(studentlevel);
        }

        // GET: Studentlevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Studentlevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,studentlevels")] Studentlevel studentlevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentlevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentlevel);
        }

        // GET: Studentlevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentlevel = await _context.Studentlevel.FindAsync(id);
            if (studentlevel == null)
            {
                return NotFound();
            }
            return View(studentlevel);
        }

        // POST: Studentlevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,studentlevels")] Studentlevel studentlevel)
        {
            if (id != studentlevel.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentlevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentlevelExists(studentlevel.recno))
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
            return View(studentlevel);
        }

        // GET: Studentlevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentlevel = await _context.Studentlevel
                .FirstOrDefaultAsync(m => m.recno == id);
            if (studentlevel == null)
            {
                return NotFound();
            }

            return View(studentlevel);
        }

        // POST: Studentlevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentlevel = await _context.Studentlevel.FindAsync(id);
            _context.Studentlevel.Remove(studentlevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentlevelExists(int id)
        {
            return _context.Studentlevel.Any(e => e.recno == id);
        }
    }
}
