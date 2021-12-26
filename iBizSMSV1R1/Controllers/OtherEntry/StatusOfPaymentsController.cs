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
    //[Authorize(Roles = "Registrar,Admin,Accounting")]
    public class StatusOfPaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusOfPaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StatusOfPayments
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusOfPayment.ToListAsync());
        }

        // GET: StatusOfPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusOfPayment = await _context.StatusOfPayment
                .FirstOrDefaultAsync(m => m.recno == id);
            if (statusOfPayment == null)
            {
                return NotFound();
            }

            return View(statusOfPayment);
        }

        // GET: StatusOfPayments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusOfPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,paymentstatus")] StatusOfPayment statusOfPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusOfPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusOfPayment);
        }

        // GET: StatusOfPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusOfPayment = await _context.StatusOfPayment.FindAsync(id);
            if (statusOfPayment == null)
            {
                return NotFound();
            }
            return View(statusOfPayment);
        }

        // POST: StatusOfPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,paymentstatus")] StatusOfPayment statusOfPayment)
        {
            if (id != statusOfPayment.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusOfPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusOfPaymentExists(statusOfPayment.recno))
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
            return View(statusOfPayment);
        }

        // GET: StatusOfPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusOfPayment = await _context.StatusOfPayment
                .FirstOrDefaultAsync(m => m.recno == id);
            if (statusOfPayment == null)
            {
                return NotFound();
            }

            return View(statusOfPayment);
        }

        // POST: StatusOfPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statusOfPayment = await _context.StatusOfPayment.FindAsync(id);
            _context.StatusOfPayment.Remove(statusOfPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusOfPaymentExists(int id)
        {
            return _context.StatusOfPayment.Any(e => e.recno == id);
        }
    }
}
