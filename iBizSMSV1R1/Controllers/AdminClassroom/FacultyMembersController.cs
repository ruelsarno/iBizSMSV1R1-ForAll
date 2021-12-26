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
    public class FacultyMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacultyMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FacultyMembers
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebPageTeacher.ToListAsync());
        }

        // GET: FacultyMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageTeacher = await _context.WebPageTeacher
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageTeacher == null)
            {
                return NotFound();
            }

            return View(webPageTeacher);
        }

        // GET: FacultyMembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FacultyMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,title,tagline,category,specialization,fullname,controller,action,image,link")] WebPageTeacher webPageTeacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(webPageTeacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(webPageTeacher);
        }

        // GET: FacultyMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageTeacher = await _context.WebPageTeacher.FindAsync(id);
            if (webPageTeacher == null)
            {
                return NotFound();
            }
            return View(webPageTeacher);
        }

        // POST: FacultyMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,title,tagline,category,specialization,fullname,controller,action,image,link")] WebPageTeacher webPageTeacher)
        {
            if (id != webPageTeacher.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webPageTeacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebPageTeacherExists(webPageTeacher.recno))
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
            return View(webPageTeacher);
        }

        // GET: FacultyMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPageTeacher = await _context.WebPageTeacher
                .FirstOrDefaultAsync(m => m.recno == id);
            if (webPageTeacher == null)
            {
                return NotFound();
            }

            return View(webPageTeacher);
        }

        // POST: FacultyMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webPageTeacher = await _context.WebPageTeacher.FindAsync(id);
            _context.WebPageTeacher.Remove(webPageTeacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebPageTeacherExists(int id)
        {
            return _context.WebPageTeacher.Any(e => e.recno == id);
        }
    }
}
