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
    public class AccountDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AccountDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccountDetail.ToListAsync());
        }

        // GET: AccountDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountDetail = await _context.AccountDetail
                .FirstOrDefaultAsync(m => m.accountdetailrecno == id);
            if (accountDetail == null)
            {
                return NotFound();
            }

            return View(accountDetail);
        }

        // GET: AccountDetails/Create
        public IActionResult Create()
        {
            ViewBag.SchoolYear = _context.SchoolYear.Select(h => new SelectListItem
            {
                Value = h.schoolyears,
                Text = h.schoolyears

            });
            ViewBag.DiscountCode = _context.Discount.Select(h => new SelectListItem
            {
                Value = h.discountcodes,
                Text = h.discountcodes

            });
            ViewBag.PaymentMode = _context.ModeOfPayment.Select(h => new SelectListItem
            {
                Value = h.modeofpayments,
                Text = h.modeofpayments

            });
            ViewBag.FeeCategory = _context.FeeCategory.Select(h => new SelectListItem
            {
                Value = h.feecategoryname,
                Text = h.feecategoryname

            });

            return View();
        }

        // POST: AccountDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("accountdetailrecno,idno,schoolyear,discountcode,discountname,discount,modeofpayment,feecategory,feedescription,feeamount,amountofdiscount,totalpayment,rebate,balance,status,remarks")] AccountDetail accountDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountDetail);
        }

        // GET: AccountDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.SchoolYear = _context.SchoolYear.Select(h => new SelectListItem
            {
                Value = h.schoolyears,
                Text = h.schoolyears

            });
            ViewBag.DiscountCode = _context.Discount.Select(h => new SelectListItem
            {
                Value = h.discountcodes,
                Text = h.discountcodes

            });
            ViewBag.PaymentMode = _context.ModeOfPayment.Select(h => new SelectListItem
            {
                Value = h.modeofpayments,
                Text = h.modeofpayments

            });
            ViewBag.FeeCategory = _context.FeeCategory.Select(h => new SelectListItem
            {
                Value = h.feecategoryname,
                Text = h.feecategoryname

            });


            if (id == null)
            {
                return NotFound();
            }

            var accountDetail = await _context.AccountDetail.FindAsync(id);
            if (accountDetail == null)
            {
                return NotFound();
            }
            return View(accountDetail);
        }

        // POST: AccountDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("accountdetailrecno,idno,schoolyear,discountcode,discountname,discount,modeofpayment,feecategory,feedescription,feeamount,amountofdiscount,totalpayment,rebate,balance,status,remarks")] AccountDetail accountDetail)
        {
            if (id != accountDetail.accountdetailrecno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountDetailExists(accountDetail.accountdetailrecno))
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
            return View(accountDetail);
        }

        // GET: AccountDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountDetail = await _context.AccountDetail
                .FirstOrDefaultAsync(m => m.accountdetailrecno == id);
            if (accountDetail == null)
            {
                return NotFound();
            }

            return View(accountDetail);
        }

        // POST: AccountDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountDetail = await _context.AccountDetail.FindAsync(id);
            _context.AccountDetail.Remove(accountDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountDetailExists(int id)
        {
            return _context.AccountDetail.Any(e => e.accountdetailrecno == id);
        }
    }
}
