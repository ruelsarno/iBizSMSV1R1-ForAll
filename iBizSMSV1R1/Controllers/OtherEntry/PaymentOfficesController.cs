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
    public class PaymentOfficesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentOfficesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PaymentOffices
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentOffice.ToListAsync());
        }

        // GET: PaymentOffices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentOffice = await _context.PaymentOffice
                .FirstOrDefaultAsync(m => m.recno == id);
            if (paymentOffice == null)
            {
                return NotFound();
            }

            return View(paymentOffice);
        }

        // GET: PaymentOffices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentOffices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,paymentoffices")] PaymentOffice paymentOffice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentOffice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentOffice);
        }

        // GET: PaymentOffices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentOffice = await _context.PaymentOffice.FindAsync(id);
            if (paymentOffice == null)
            {
                return NotFound();
            }
            return View(paymentOffice);
        }

        // POST: PaymentOffices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,paymentoffices")] PaymentOffice paymentOffice)
        {
            if (id != paymentOffice.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentOffice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentOfficeExists(paymentOffice.recno))
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
            return View(paymentOffice);
        }

        // GET: PaymentOffices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentOffice = await _context.PaymentOffice
                .FirstOrDefaultAsync(m => m.recno == id);
            if (paymentOffice == null)
            {
                return NotFound();
            }

            return View(paymentOffice);
        }

        // POST: PaymentOffices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentOffice = await _context.PaymentOffice.FindAsync(id);
            _context.PaymentOffice.Remove(paymentOffice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentOfficeExists(int id)
        {
            return _context.PaymentOffice.Any(e => e.recno == id);
        }
    }
}
