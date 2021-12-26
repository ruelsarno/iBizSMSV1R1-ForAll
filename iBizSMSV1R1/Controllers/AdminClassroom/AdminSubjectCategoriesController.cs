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
namespace iBizSMSV1R1.Controllers.AdminClassroom
{
    //[Authorize(Roles = "Registrar,Admin")]
    public class AdminSubjectCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminSubjectCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminSubjectCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubjectCategory.ToListAsync());
        }

        // GET: AdminSubjectCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectCategory = await _context.SubjectCategory
                .FirstOrDefaultAsync(m => m.recno == id);
            if (subjectCategory == null)
            {
                return NotFound();
            }

            return View(subjectCategory);
        }

        // GET: AdminSubjectCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminSubjectCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,category")] SubjectCategory subjectCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subjectCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subjectCategory);
        }

        // GET: AdminSubjectCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectCategory = await _context.SubjectCategory.FindAsync(id);
            if (subjectCategory == null)
            {
                return NotFound();
            }
            return View(subjectCategory);
        }

        // POST: AdminSubjectCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,category")] SubjectCategory subjectCategory)
        {
            if (id != subjectCategory.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subjectCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectCategoryExists(subjectCategory.recno))
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
            return View(subjectCategory);
        }

        // GET: AdminSubjectCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectCategory = await _context.SubjectCategory
                .FirstOrDefaultAsync(m => m.recno == id);
            if (subjectCategory == null)
            {
                return NotFound();
            }

            return View(subjectCategory);
        }

        // POST: AdminSubjectCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subjectCategory = await _context.SubjectCategory.FindAsync(id);
            _context.SubjectCategory.Remove(subjectCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectCategoryExists(int id)
        {
            return _context.SubjectCategory.Any(e => e.recno == id);
        }
    }
}
