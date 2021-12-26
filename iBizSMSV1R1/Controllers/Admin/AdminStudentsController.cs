using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.Models;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iBizSMSV1R1.Controllers.Admin
{
    //[Authorize(Roles = "Registrar,Admin")]
    public class AdminStudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminStudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id, string idno, string username, string SearchString, bool notUsed, int page, int pagedivider, string message)
        {
            ViewBag.SearchString = SearchString;
            ViewBag.IdNo = idno;
            ViewBag.Message = message;
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
                pagedivider = 20;
            }
            ViewBag.Page = page;
            if (!String.IsNullOrEmpty(SearchString))
            {
                bool isNumeric = int.TryParse(SearchString, out int n);

                if (isNumeric == false)
                {
                    var studentinfo = _context.StudentInfo.Where(p => p.idno.Contains(SearchString) || p.surname.Contains(SearchString) ||
                    p.firstname.Contains(SearchString) || p.middlename.Contains(SearchString));
                    var model = PagingList.Create(studentinfo, pagedivider, page);
                    var RecordCount = studentinfo.OrderBy(p => p.idno).Count();
                    ViewBag.RecordCount = RecordCount;
                    return View(model);
                }
                else
                {
                    var studentinfo = _context.StudentInfo.Take(Convert.ToInt32(SearchString)).OrderBy(p => p.idno);
                    var model = await PagingList.CreateAsync(studentinfo, pagedivider, page);
                    var RecordCount = studentinfo.OrderBy(p => p.idno).Count();
                    ViewBag.RecordCount = RecordCount;
                    return View(model);
                }                    

            }
            else
            {
                bool isNumeric = int.TryParse(SearchString, out int n);

                if (isNumeric == false)
                {
                    var studentinfo = _context.StudentInfo.Where(p => p.idno == "xxxx").OrderBy(p => p.idno);
                    var model = await PagingList.CreateAsync(studentinfo, pagedivider, page);
                    var RecordCount = studentinfo.OrderBy(p => p.idno).Count();
                    ViewBag.RecordCount = RecordCount;
                    return View(model);
                }
                else
                {
                    var studentinfo = _context.StudentInfo.Take(Convert.ToInt32(SearchString)).OrderBy(p => p.idno);
                    var model = await PagingList.CreateAsync(studentinfo, pagedivider, page);
                    var RecordCount = studentinfo.OrderBy(p => p.idno).Count();
                    ViewBag.RecordCount = RecordCount;
                    return View(model);
                   
                }

                
            }
        }

        // GET: StudentInfoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentInfo = await _context.StudentInfo
                .FirstOrDefaultAsync(m => m.idno == id);
            if (studentInfo == null)
            {
                return NotFound();
            }

            return View(studentInfo);
        }

        // GET: StudentInfoes/Create
        public IActionResult Create()
        {
            ViewBag.Nationality = _context.Nationality.Select(h => new SelectListItem
            {
                Value = h.nationality,
                Text = h.nationality
            });
            ViewBag.CivilStatus = _context.CivilStatus.Select(h => new SelectListItem
            {
                Value = h.civilstatus,
                Text = h.civilstatus
            });
            ViewBag.Gender = _context.Gender.Select(h => new SelectListItem
            {
                Value = h.genders,
                Text = h.genders
            });
            return View();
        }

        // POST: StudentInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idno,surname,firstname,middlename,extension,birthday,birthplace,nationality,civilstatus,gender,active")] StudentInfo studentInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), null, new { searchString = studentInfo.idno });
            }
            return RedirectToAction(nameof(Index), null, new { searchString = studentInfo.idno });
        }

        // GET: StudentInfoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Nationality = _context.Nationality.Select(h => new SelectListItem
            {
                Value = h.nationality,
                Text = h.nationality
            });
            ViewBag.CivilStatus = _context.CivilStatus.Select(h => new SelectListItem
            {
                Value = h.civilstatus,
                Text = h.civilstatus
            });
            ViewBag.Gender = _context.Gender.Select(h => new SelectListItem
            {
                Value = h.genders,
                Text = h.genders
            });
            if (id == null)
            {
                return NotFound();
            }

            var studentInfo = await _context.StudentInfo.FindAsync(id);
            if (studentInfo == null)
            {
                return NotFound();
            }
            return View(studentInfo);
        }

        // POST: StudentInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("idno,surname,firstname,middlename,extension,birthday,birthplace,nationality,civilstatus,gender,active")] StudentInfo studentInfo)
        {
            if (id != studentInfo.idno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentInfoExists(studentInfo.idno))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), null, new { searchString = studentInfo.idno });
            }
            return RedirectToAction(nameof(Index), null, new { searchString = studentInfo.idno });
        }

        // GET: StudentInfoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentInfo = await _context.StudentInfo
                .FirstOrDefaultAsync(m => m.idno == id);
            if (studentInfo == null)
            {
                return NotFound();
            }

            return View(studentInfo);
        }

        // POST: StudentInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var studentInfo = await _context.StudentInfo.FindAsync(id);
            _context.StudentInfo.Remove(studentInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentInfoExists(string id)
        {
            return _context.StudentInfo.Any(e => e.idno == id);
        }
    }
}