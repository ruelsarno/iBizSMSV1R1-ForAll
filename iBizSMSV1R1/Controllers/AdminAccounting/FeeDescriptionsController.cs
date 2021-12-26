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
    public class FeeDescriptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeeDescriptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FeeDescriptions
        public async Task<IActionResult> Index()
        {
            return View(await _context.FeeDescription.ToListAsync());
        }

        // GET: FeeDescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeDescription = await _context.FeeDescription
                .FirstOrDefaultAsync(m => m.recno == id);
            if (feeDescription == null)
            {
                return NotFound();
            }

            return View(feeDescription);
        }

        // GET: FeeDescriptions/Create
        public IActionResult Create()
        {
            ViewBag.FeeCategory = _context.FeeCategory.Select(h => new SelectListItem
            {
                Value = h.feecategoryname,
                Text = h.feecategoryname
            });
            return View();
        }

        // POST: FeeDescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,feecategory,feename")] FeeDescription feeDescription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feeDescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feeDescription);
        }

        // GET: FeeDescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.FeeCategory = _context.FeeCategory.Select(h => new SelectListItem
            {
                Value = h.feecategoryname,
                Text = h.feecategoryname

            });
            if (id == null)
            {
                return NotFound();
            }

            var feeDescription = await _context.FeeDescription.FindAsync(id);
            if (feeDescription == null)
            {
                return NotFound();
            }
            return View(feeDescription);
        }

        // POST: FeeDescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,feecategory,feename")] FeeDescription feeDescription)
        {
            if (id != feeDescription.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feeDescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeeDescriptionExists(feeDescription.recno))
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
            return View(feeDescription);
        }

        // GET: FeeDescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeDescription = await _context.FeeDescription
                .FirstOrDefaultAsync(m => m.recno == id);
            if (feeDescription == null)
            {
                return NotFound();
            }

            return View(feeDescription);
        }

        // POST: FeeDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feeDescription = await _context.FeeDescription.FindAsync(id);
            _context.FeeDescription.Remove(feeDescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeeDescriptionExists(int id)
        {
            return _context.FeeDescription.Any(e => e.recno == id);
        }
    }
}
