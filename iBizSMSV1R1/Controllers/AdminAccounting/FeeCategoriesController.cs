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
    public class FeeCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeeCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FeeCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.FeeCategory.ToListAsync());
        }

        // GET: FeeCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeCategory = await _context.FeeCategory
                .FirstOrDefaultAsync(m => m.recno == id);
            if (feeCategory == null)
            {
                return NotFound();
            }

            return View(feeCategory);
        }

        // GET: FeeCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FeeCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,feecategoryname")] FeeCategory feeCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feeCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feeCategory);
        }

        // GET: FeeCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeCategory = await _context.FeeCategory.FindAsync(id);
            if (feeCategory == null)
            {
                return NotFound();
            }
            return View(feeCategory);
        }

        // POST: FeeCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,feecategoryname")] FeeCategory feeCategory)
        {
            if (id != feeCategory.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feeCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeeCategoryExists(feeCategory.recno))
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
            return View(feeCategory);
        }

        // GET: FeeCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeCategory = await _context.FeeCategory
                .FirstOrDefaultAsync(m => m.recno == id);
            if (feeCategory == null)
            {
                return NotFound();
            }

            return View(feeCategory);
        }

        // POST: FeeCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feeCategory = await _context.FeeCategory.FindAsync(id);
            _context.FeeCategory.Remove(feeCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeeCategoryExists(int id)
        {
            return _context.FeeCategory.Any(e => e.recno == id);
        }
    }
}
