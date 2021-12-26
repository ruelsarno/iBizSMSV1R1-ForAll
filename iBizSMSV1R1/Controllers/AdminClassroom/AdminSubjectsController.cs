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
using ReflectionIT.Mvc.Paging;

namespace iBizSMSV1R1.Controllers.AdminClassroom
{
    //[Authorize(Roles = "Registrar,Admin")]
    public class AdminSubjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminSubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminSubjects
        public async Task<IActionResult> Index(string id, string idno, string SearchString, bool notUsed, int page, int pagedivider, string message)
        {
            ViewBag.Message = message;
            ViewBag.IDNO = idno;
            ViewBag.ID = id;
            ViewBag.SearchString = SearchString;
            if (notUsed)
            {
                return View("From [HttpPost]Index: filter on " + SearchString);
            }
            if (page <= 0)
            {
                page = 1;
            }
            if (pagedivider <= 0)
            {
                pagedivider = 40;
            }
            ViewBag.Page = page;
            if (!String.IsNullOrEmpty(SearchString))
            {

                var subjects = _context.Subject.Where(p => p.studentlevel.Contains(SearchString) || p.gradeyear.Contains(SearchString) ||
                p.subjectname.Contains(SearchString)).OrderBy(p => p.studentlevel).ThenBy(p => p.gradeyear).ThenBy(p => p.category).ThenBy(p => p.subjectcode);
                var model = PagingList.Create(subjects, pagedivider, page);
                var RecordCount = subjects.Count();
                ViewBag.RecordCount = RecordCount;
                return View(model);
            }
            else
            {
                var subjects = _context.Subject.OrderBy(p => p.studentlevel).ThenBy(p => p.gradeyear).ThenBy(p => p.category).ThenBy(p => p.subjectcode);
                var model = await PagingList.CreateAsync(subjects, pagedivider, page);
                var RecordCount = subjects.Count();
                ViewBag.RecordCount = RecordCount;
                return View(model);
            }
            //return View(await _context.Subject.OrderBy(p=>p.studentlevel).ThenBy(p=>p.gradeyear).ThenBy(p=>p.subjectname).ToListAsync());
        }

        // GET: AdminSubjects/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                .FirstOrDefaultAsync(m => m.subjectcode == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: AdminSubjects/Create
        public IActionResult Create()
        {
            ViewBag.Category = _context.SubjectCategory.Select(h => new SelectListItem
            {
                Value = h.category,
                Text = h.category
            });
            ViewBag.StudentLevel = _context.Studentlevel.Select(h => new SelectListItem
            {
                Value = h.studentlevels,
                Text = h.studentlevels
            });
            ViewBag.GradeYear = _context.GradeYear.Select(h => new SelectListItem
            {
                Value = h.gradeyears,
                Text = h.gradeyears
            });
            return View();
        }

        // POST: AdminSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,studentlevel,gradeyear,category,subjectcode,subjectname,semester,noofhours")] Subject subject)
        {
            string newaddedsubject = "";
            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                newaddedsubject = "ADDED: " + subject.gradeyear + " : " + subject.semester + " : " + subject.category + " : " + subject.subjectcode + " : " + subject.subjectname;
                return RedirectToAction(nameof(Index),new {message = newaddedsubject });
            }
            return RedirectToAction(nameof(Create));
        }

        // GET: AdminSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Category = _context.SubjectCategory.Select(h => new SelectListItem
            {
                Value = h.category,
                Text = h.category
            });
            ViewBag.StudentLevel = _context.Studentlevel.Select(h => new SelectListItem
            {
                Value = h.studentlevels,
                Text = h.studentlevels
            });
            ViewBag.GradeYear = _context.GradeYear.Select(h => new SelectListItem
            {
                Value = h.gradeyears,
                Text = h.gradeyears
            });

            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: AdminSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,studentlevel,gradeyear,category,subjectcode,subjectname,semester,noofhours")] Subject subject)
        {
            if (id != subject.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string newaddedsubject = "";
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                    newaddedsubject = "UPDATED: " + subject.gradeyear + " : " + subject.semester + " : " + subject.category + " : " + subject.subjectcode + " : " + subject.subjectname;
                    return RedirectToAction(nameof(Index), new { message = newaddedsubject });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.subjectcode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }                
            }
            return View(subject);
        }

        // GET: AdminSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                .FirstOrDefaultAsync(m => m.recno == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: AdminSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Subject.FindAsync(id);
            _context.Subject.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(string id)
        {
            return _context.Subject.Any(e => e.subjectcode == id);
        }

        public JsonResult getGradeYear(string studentlevel)
        {
            IList<string> result = new List<string>();

            var query = (from p in _context.GradeYear
                         where p.studentlevels == studentlevel
                         select p.gradeyears);
            result.Add("");
            foreach (var x in query)
            {
                result.Add(x.ToString());
            }
            return Json(result);
        }
    }
}
