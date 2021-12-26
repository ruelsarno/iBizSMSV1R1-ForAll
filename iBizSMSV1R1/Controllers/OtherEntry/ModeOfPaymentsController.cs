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
    public class ModeOfPaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModeOfPaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ModeOfPayments
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModeOfPayment.ToListAsync());
        }

        // GET: ModeOfPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeOfPayment = await _context.ModeOfPayment
                .FirstOrDefaultAsync(m => m.recno == id);
            if (modeOfPayment == null)
            {
                return NotFound();
            }

            return View(modeOfPayment);
        }

        // GET: ModeOfPayments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModeOfPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,modeofpayments")] ModeOfPayment modeOfPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modeOfPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modeOfPayment);
        }

        // GET: ModeOfPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeOfPayment = await _context.ModeOfPayment.FindAsync(id);
            if (modeOfPayment == null)
            {
                return NotFound();
            }
            return View(modeOfPayment);
        }

        // POST: ModeOfPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,modeofpayments")] ModeOfPayment modeOfPayment)
        {
            if (id != modeOfPayment.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modeOfPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeOfPaymentExists(modeOfPayment.recno))
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
            return View(modeOfPayment);
        }

        // GET: ModeOfPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeOfPayment = await _context.ModeOfPayment
                .FirstOrDefaultAsync(m => m.recno == id);
            if (modeOfPayment == null)
            {
                return NotFound();
            }

            return View(modeOfPayment);
        }

        // POST: ModeOfPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modeOfPayment = await _context.ModeOfPayment.FindAsync(id);
            _context.ModeOfPayment.Remove(modeOfPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeOfPaymentExists(int id)
        {
            return _context.ModeOfPayment.Any(e => e.recno == id);
        }
    }
}
