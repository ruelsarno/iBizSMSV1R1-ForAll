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
    public class TrackCodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrackCodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TrackCodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrackCode.ToListAsync());
        }

        // GET: TrackCodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackCode = await _context.TrackCode
                .FirstOrDefaultAsync(m => m.recno == id);
            if (trackCode == null)
            {
                return NotFound();
            }

            return View(trackCode);
        }

        // GET: TrackCodes/Create
        public IActionResult Create()
        {
            ViewBag.Studentlevel = _context.Studentlevel.Select(h => new SelectListItem
            {
                Value = h.studentlevels,
                Text = h.studentlevels
            });
            return View();
        }

        // POST: TrackCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,studentlevels,trackcodes,descriptions")] TrackCode trackCode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trackCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trackCode);
        }

        // GET: TrackCodes/Edit/5
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

            var trackCode = await _context.TrackCode.FindAsync(id);
            if (trackCode == null)
            {
                return NotFound();
            }
            return View(trackCode);
        }

        // POST: TrackCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,studentlevels,trackcodes,descriptions")] TrackCode trackCode)
        {
            if (id != trackCode.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trackCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackCodeExists(trackCode.recno))
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
            return View(trackCode);
        }

        // GET: TrackCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackCode = await _context.TrackCode
                .FirstOrDefaultAsync(m => m.recno == id);
            if (trackCode == null)
            {
                return NotFound();
            }

            return View(trackCode);
        }

        // POST: TrackCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trackCode = await _context.TrackCode.FindAsync(id);
            _context.TrackCode.Remove(trackCode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackCodeExists(int id)
        {
            return _context.TrackCode.Any(e => e.recno == id);
        }
    }
}
