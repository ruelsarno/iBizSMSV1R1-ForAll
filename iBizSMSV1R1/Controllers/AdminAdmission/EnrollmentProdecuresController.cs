using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.ModelsAdmission;
using Microsoft.AspNetCore.Authorization;
namespace iBizSMSV1R1.Controllers.AdminAdmission
{
    //[Authorize(Roles = "Registrar,Admin")]
    public class EnrollmentProdecuresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentProdecuresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EnrollmentProdecures
        public async Task<IActionResult> Index()
        {
            return View(await _context.EnrollmentProdecure.ToListAsync());
        }

        // GET: EnrollmentProdecures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollmentProdecure = await _context.EnrollmentProdecure
                .FirstOrDefaultAsync(m => m.recno == id);
            if (enrollmentProdecure == null)
            {
                return NotFound();
            }

            return View(enrollmentProdecure);
        }

        // GET: EnrollmentProdecures/Create
        public IActionResult Create()
        {
            ViewBag.SchoolYear = _context.SchoolYear.Select(h => new SelectListItem
            {
                Value = h.schoolyears,
                Text = h.schoolyears
            });
            return View();
        }

        // POST: EnrollmentProdecures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,schoolyear,title,procedure")] EnrollmentProdecure enrollmentProdecures)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollmentProdecures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enrollmentProdecures);
        }

        // GET: EnrollmentProdecures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.SchoolYear = _context.SchoolYear.Select(h => new SelectListItem
            {
                Value = h.schoolyears,
                Text = h.schoolyears
            });
            if (id == null)
            {
                return NotFound();
            }

            var enrollmentProdecure = await _context.EnrollmentProdecure.FindAsync(id);
            if (enrollmentProdecure == null)
            {
                return NotFound();
            }
            return View(enrollmentProdecure);
        }

        // POST: EnrollmentProdecures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,schoolyear,title,procedure")] EnrollmentProdecure enrollmentProdecures)
        {
            if (id != enrollmentProdecures.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollmentProdecures);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentProdecureExists(enrollmentProdecures.recno))
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
            return View(enrollmentProdecures);
        }

        // GET: EnrollmentProdecures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollmentProdecure = await _context.EnrollmentProdecure
                .FirstOrDefaultAsync(m => m.recno == id);
            if (enrollmentProdecure == null)
            {
                return NotFound();
            }

            return View(enrollmentProdecure);
        }

        // POST: EnrollmentProdecures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollmentProdecure = await _context.EnrollmentProdecure.FindAsync(id);
            _context.EnrollmentProdecure.Remove(enrollmentProdecure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentProdecureExists(int id)
        {
            return _context.EnrollmentProdecure.Any(e => e.recno == id);
        }
    }
}
