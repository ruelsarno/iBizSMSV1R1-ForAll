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
    public class AdmissionRequirementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdmissionRequirementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdmissionRequirements
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdmissionRequirement.ToListAsync());
        }

        // GET: AdmissionRequirements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admissionRequirement = await _context.AdmissionRequirement
                .FirstOrDefaultAsync(m => m.recno == id);
            if (admissionRequirement == null)
            {
                return NotFound();
            }

            return View(admissionRequirement);
        }

        // GET: AdmissionRequirements/Create
        public IActionResult Create()
        {
            ViewBag.SchoolYear = _context.SchoolYear.Select(h => new SelectListItem
            {
                Value = h.schoolyears,
                Text = h.schoolyears
            });
            return View();
        }

        // POST: AdmissionRequirements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,schoolyear,title,requirement")] AdmissionRequirement admissionRequirements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admissionRequirements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admissionRequirements);
        }

        // GET: AdmissionRequirements/Edit/5
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

            var admissionRequirement = await _context.AdmissionRequirement.FindAsync(id);
            if (admissionRequirement == null)
            {
                return NotFound();
            }
            return View(admissionRequirement);
        }

        // POST: AdmissionRequirements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,schoolyear,title,requirement")] AdmissionRequirement admissionRequirements)
        {
            if (id != admissionRequirements.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admissionRequirements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissionRequirementExists(admissionRequirements.recno))
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
            return View(admissionRequirements);
        }

        // GET: AdmissionRequirements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admissionRequirement = await _context.AdmissionRequirement
                .FirstOrDefaultAsync(m => m.recno == id);
            if (admissionRequirement == null)
            {
                return NotFound();
            }

            return View(admissionRequirement);
        }

        // POST: AdmissionRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admissionRequirement = await _context.AdmissionRequirement.FindAsync(id);
            _context.AdmissionRequirement.Remove(admissionRequirement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmissionRequirementExists(int id)
        {
            return _context.AdmissionRequirement.Any(e => e.recno == id);
        }
    }
}
