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
    //[Authorize(Roles = "Accounting,Admin")]
    public class BillNamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillNamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BillNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.BillName.ToListAsync());
        }

        // GET: BillNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billName = await _context.BillName
                .FirstOrDefaultAsync(m => m.recno == id);
            if (billName == null)
            {
                return NotFound();
            }

            return View(billName);
        }

        // GET: BillNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,billnames")] BillName billName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billName);
        }

        // GET: BillNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billName = await _context.BillName.FindAsync(id);
            if (billName == null)
            {
                return NotFound();
            }
            return View(billName);
        }

        // POST: BillNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,billnames")] BillName billName)
        {
            if (id != billName.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillNameExists(billName.recno))
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
            return View(billName);
        }

        // GET: BillNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billName = await _context.BillName
                .FirstOrDefaultAsync(m => m.recno == id);
            if (billName == null)
            {
                return NotFound();
            }

            return View(billName);
        }

        // POST: BillNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billName = await _context.BillName.FindAsync(id);
            _context.BillName.Remove(billName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillNameExists(int id)
        {
            return _context.BillName.Any(e => e.recno == id);
        }
    }
}
