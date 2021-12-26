using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.ModelsAccounting;
using Microsoft.AspNetCore.Authorization;

namespace iBizSMSV1R1.Controllers.AdminAccounting
{
    //[Authorize(Roles = "Accounting,Admin")]
    public class FeeTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeeTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FeeTables
        public async Task<IActionResult> Index(string schoolyear, string studentlevel, string SearchString)
        {
            if (!String.IsNullOrEmpty(SearchString))
            {
                return View(await _context.FeeTable.OrderByDescending(p => p.recno).Where(p => p.gradeyear == SearchString).ToListAsync());
                
            }
            else
            {
                if (String.IsNullOrEmpty(schoolyear))
                {
                    return View(await _context.FeeTable.OrderByDescending(p => p.recno).ToListAsync());
                }
                else
                {
                    return View(await _context.FeeTable.OrderByDescending(p => p.recno).Where(p => p.schoolyear == schoolyear && p.gradeyear == studentlevel).ToListAsync());
                }
            }
        }

        // GET: FeeTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeTable = await _context.FeeTable
                .FirstOrDefaultAsync(m => m.recno == id);
            if (feeTable == null)
            {
                return NotFound();
            }

            return View(feeTable);
        }

        // GET: FeeTables/Create
        public IActionResult Create()
        {
            ViewBag.SchoolYear = _context.SchoolYear.OrderByDescending(h=>h.schoolyears).Select(h => new SelectListItem
            {
                Value = h.schoolyears,
                Text = h.schoolyears
            });
            ViewBag.StudentLevel = _context.GradeYear.Select(h => new SelectListItem
            {
                Value = h.gradeyears,
                Text = h.gradeyears
            });
            ViewBag.PaymentMode = _context.ModeOfPayment.Select(h => new SelectListItem
            {
                Value = h.modeofpayments,
                Text = h.modeofpayments
            });
            return View();
        }

        // POST: FeeTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,schoolyear,gradeyear,paymentmode,tuitionfee,reservationfee,uponenrollment,installmentcount,paymentschedule,installmentamount")] FeeTable feeTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feeTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feeTable);
        }

        // GET: FeeTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.SchoolYear = _context.SchoolYear.OrderByDescending(h => h.schoolyears).Select(h => new SelectListItem
            {
                Value = h.schoolyears,
                Text = h.schoolyears
            });
            ViewBag.StudentLevel = _context.GradeYear.Select(h => new SelectListItem
            {
                Value = h.gradeyears,
                Text = h.gradeyears
            });
            ViewBag.PaymentMode = _context.ModeOfPayment.Select(h => new SelectListItem
            {
                Value = h.modeofpayments,
                Text = h.modeofpayments

            });
            if (id == null)
            {
                return NotFound();
            }

            var feeTable = await _context.FeeTable.FindAsync(id);
            if (feeTable == null)
            {
                return NotFound();
            }
            return View(feeTable);
        }

        // POST: FeeTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,schoolyear,gradeyear,paymentmode,tuitionfee,reservationfee,uponenrollment,installmentcount,paymentschedule,installmentamount")] FeeTable feeTable)
        {
            if (id != feeTable.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feeTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeeTableExists(feeTable.recno))
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
            return View(feeTable);
        }

        // GET: FeeTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeTable = await _context.FeeTable
                .FirstOrDefaultAsync(m => m.recno == id);
            if (feeTable == null)
            {
                return NotFound();
            }

            return View(feeTable);
        }

        // POST: FeeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feeTable = await _context.FeeTable.FindAsync(id);
            _context.FeeTable.Remove(feeTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeeTableExists(int id)
        {
            return _context.FeeTable.Any(e => e.recno == id);
        }
    }
}
