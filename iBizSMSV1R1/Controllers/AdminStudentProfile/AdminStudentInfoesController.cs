using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.Models;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Authorization;
using iBizSMSV1R1.Functions;
using Microsoft.AspNetCore.Identity;

namespace iBizSMSV1R1.Controllers.AdminStudentProfile
{
    //[Authorize(Roles = "Registrar,Admin")]
    public class AdminStudentInfoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminStudentInfoesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: StudentInfoes
        public IActionResult Index(string id, string idno, string message)
        {
            //var user =  _userManager.GetUserAsync(User);
            //var username = User.Identity.Name;
            
            ViewBag.IDNO = idno;
            ViewBag.ID = id;
            ViewBag.Message = message;
            if (id == null)
            {
                return RedirectToAction("Index","AdminStudents",new {message=Messages.messagenotexists });
            }
            var result = _context.StudentInfo.Where(p => p.id == id);               
            return View(result);
           
        }

        // GET: StudentInfoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "AdminStudents", new { message = Messages.messagenotexists });
            }

            var studentInfo = await _context.StudentInfo
                .FirstOrDefaultAsync(m => m.idno == id);
            if (studentInfo == null)
            {
                return RedirectToAction("Index", "AdminStudents", new { message = Messages.messagenotexists });
            }

            return View(studentInfo);
        }

        // GET: StudentInfoes/Create
        public IActionResult Create(string id, string idno)
        {
            ViewBag.IDNO = idno;
            ViewBag.ID = id;

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
        public async Task<IActionResult> Create([Bind("idno,lrn,surname,firstname,middlename,extension,birthday,birthplace,nationality,civilstatus,gender,active,id")] StudentInfo studentInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), null, new { idno = studentInfo.idno, id = studentInfo.id });
            }
            return RedirectToAction(nameof(Index), null, new { idno = studentInfo.idno, id = studentInfo.id });
        }

        // GET: StudentInfoes/Edit/5
        public async Task<IActionResult> Edit(string id, string idno)
        {
            ViewBag.IDNO = idno;
            ViewBag.ID = id;

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
                return RedirectToAction("Index", "AdminStudents", new { message = Messages.messagenotexists });
            }

            var studentInfo = await _context.StudentInfo.FindAsync(id);
            if (studentInfo == null)
            {
                return RedirectToAction("Index", "AdminStudents", new { message = Messages.messagenotexists });
            }
            return View(studentInfo);
        }

        // POST: StudentInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string idno, [Bind("idno,lrn,surname,firstname,middlename,extension,birthday,birthplace,nationality,civilstatus,gender,active,id")] StudentInfo studentInfo)
        {
            if (idno != studentInfo.idno)
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
                        return RedirectToAction("Index", "AdminStudents", new { message = Messages.messagenotexists });
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), null, new { idno = studentInfo.idno, id = id });
            }
            return RedirectToAction(nameof(Index), null, new { idno = studentInfo.idno, id = id });
        }

        // GET: StudentInfoes/Delete/5
        public async Task<IActionResult> Delete(string id, string idno)
        {
            ViewBag.IDNO = idno;
            ViewBag.ID = id;

            if (id == null)
            {
                return RedirectToAction("Index", "AdminStudents", new { message = Messages.messagenotexists });
            }

            var studentInfo = await _context.StudentInfo
                .FirstOrDefaultAsync(m => m.idno == id);
            if (studentInfo == null)
            {
                return RedirectToAction("Index", "AdminStudents", new { message = Messages.messagenotexists });
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
            return RedirectToAction(nameof(Index), new { idno = studentInfo.idno, id = studentInfo.id });
        }

        private bool StudentInfoExists(string id)
        {
            return _context.StudentInfo.Any(e => e.idno == id);
        }
    }
}