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
    public class ProcessIntroesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProcessIntroesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProcessIntroes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProcessIntro.ToListAsync());
        }

        // GET: ProcessIntroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processIntro = await _context.ProcessIntro
                .FirstOrDefaultAsync(m => m.recno == id);
            if (processIntro == null)
            {
                return NotFound();
            }

            return View(processIntro);
        }

        // GET: ProcessIntroes/Create
        public IActionResult Create()
        {
            ViewBag.WebPageTitle = _context.WebPageTitles.Select(h => new SelectListItem
            {
                Value = h.title,
                Text = h.title
            });
            return View();
        }

        // POST: ProcessIntroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,title,subtitle,description")] ProcessIntro processIntroes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(processIntroes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(processIntroes);
        }

        // GET: ProcessIntroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.WebPageTitle = _context.WebPageTitles.Select(h => new SelectListItem
            {
                Value = h.title,
                Text = h.title
            });
            if (id == null)
            {
                return NotFound();
            }

            var processIntro = await _context.ProcessIntro.FindAsync(id);
            if (processIntro == null)
            {
                return NotFound();
            }
            return View(processIntro);
        }

        // POST: ProcessIntroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,title,subtitle,description")] ProcessIntro processIntro)
        {
            if (id != processIntro.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(processIntro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcessIntroExists(processIntro.recno))
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
            return View(processIntro);
        }

        // GET: ProcessIntroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processIntro = await _context.ProcessIntro
                .FirstOrDefaultAsync(m => m.recno == id);
            if (processIntro == null)
            {
                return NotFound();
            }

            return View(processIntro);
        }

        // POST: ProcessIntroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var processIntro = await _context.ProcessIntro.FindAsync(id);
            _context.ProcessIntro.Remove(processIntro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcessIntroExists(int id)
        {
            return _context.ProcessIntro.Any(e => e.recno == id);
        }
    }
}
