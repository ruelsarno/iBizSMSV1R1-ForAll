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
using iBizSMSV1R1.Functions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using iBizSMSV1R1.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace iBizSMSV1R1.Controllers.AdminClassroom
{
    //[Authorize(Roles = "Registrar,Admin")]
    public class AdminClassSchedulesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly ApplicationDbContext _context;
        private static IEmailSender _emailSender;
        private static IOptions<EmailSetting> _emailSettings;
        public AdminClassSchedulesController(SignInManager<ApplicationUser> signInManager,
            ILogger<ViewModels.LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ApplicationDbContext context,
            IOptions<EmailSetting> emailSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _emailSender = emailSender;
            _emailSettings = emailSettings;
        }

        // GET: AdminClassSchedules
        public async Task<IActionResult> Index(string id, string idno, string message)
        {
            ViewBag.Message = message;
            ViewBag.IDNO = idno;
            ViewBag.ID = id;
            return View(await _context.ClassSchedule.ToListAsync());
        }

        // GET: AdminClassSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classSchedule = await _context.ClassSchedule
                .FirstOrDefaultAsync(m => m.recno == id);
            if (classSchedule == null)
            {
                return NotFound();
            }

            return View(classSchedule);
        }

        // GET: AdminClassSchedules/Create
        public IActionResult Create()
        {
            ViewBag.SchoolYear = _context.SchoolYear.OrderByDescending(p => p.schoolyears).Select(h => new SelectListItem
            {
                Value = h.schoolyears,
                Text = h.schoolyears
            });
            ViewBag.StudentType = _context.StudentType.Select(h => new SelectListItem
            {
                Value = h.studenttypes,
                Text = h.studenttypes
            });
            ViewBag.GradeYear = _context.GradeYear.Select(h => new SelectListItem
            {
                Value = h.gradeyears,
                Text = h.gradeyears
            });
            ViewBag.Section = _context.Section.Select(h => new SelectListItem
            {
                Value = h.sections,
                Text = h.gradeyears + " " + h.sections
            });

            ViewBag.Teacher = _context.WebPageTeacher.Select(h => new SelectListItem
            {
                Value = h.fullname,
                Text = h.fullname
            });
            ViewBag.SubjectCode = _context.Subject.OrderBy(p=>p.studentlevel).ThenBy(p=>p.gradeyear).ThenBy(p=>p.subjectcode).Select(h => new SelectListItem
            {
                Value = h.subjectcode,
                Text = h.studentlevel + " " + h.gradeyear + " " + h.subjectcode + " " + h.subjectname
            });

            ViewBag.SubjectName = _context.Subject.Select(h => new SelectListItem
            {
                Value = h.subjectname,
                Text = h.subjectname
            }).Distinct();
            // ViewBag.Weekday = _context.Weekday.Select(h => new SelectListItem
            //{
            //    Value = h.weekdays,
            //    Text = h.weekdays
            //});
            return View();
        }

        // POST: AdminClassSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,schoolyear,idno,starttime,endtime,teacher,subjectcode,subjectname,studentgradeyear,section,weekday,roomno")] ClassSchedule classSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classSchedule);
        }

        // GET: AdminClassSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.SchoolYear = _context.SchoolYear.Select(h => new SelectListItem
            {
                Value = h.schoolyears,
                Text = h.schoolyears
            });
            ViewBag.StudentType = _context.StudentType.Select(h => new SelectListItem
            {
                Value = h.studenttypes,
                Text = h.studenttypes
            });
            ViewBag.GradeYear = _context.GradeYear.Select(h => new SelectListItem
            {
                Value = h.gradeyears,
                Text = h.gradeyears
            });
            ViewBag.Section = _context.Section.Select(h => new SelectListItem
            {
                Value = h.sections,
                Text = h.gradeyears + " " + h.sections
            });

            ViewBag.Teacher = _context.WebPageTeacher.Select(h => new SelectListItem
            {
                Value = h.fullname,
                Text = h.fullname
            });

            ViewBag.SubjectCode = _context.Subject.OrderBy(p => p.studentlevel).ThenBy(p => p.gradeyear).ThenBy(p => p.subjectcode).Select(h => new SelectListItem
            {
                Value = h.subjectcode,
                Text = h.studentlevel + " " + h.gradeyear + " " + h.subjectcode
            });

            ViewBag.SubjectName = _context.Subject.Select(h => new SelectListItem
            {
                Value = h.subjectname,
                Text = h.subjectname
            });
            // ViewBag.Weekday = _context.Weekday.Select(h => new SelectListItem
            //{
            //    Value = h.weekdays,
            //    Text = h.weekdays
            //});

            if (id == null)
            {
                return NotFound();
            }

            var classSchedule = await _context.ClassSchedule.FindAsync(id);
            if (classSchedule == null)
            {
                return NotFound();
            }
            return View(classSchedule);
        }

        // POST: AdminClassSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,schoolyear,idno,starttime,endtime,teacher,subjectcode,subjectname,studentgradeyear,section,weekday,roomno")] ClassSchedule classSchedule)
        {
            if (id != classSchedule.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassScheduleExists(classSchedule.recno))
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
            return View(classSchedule);
        }

        // GET: AdminClassSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classSchedule = await _context.ClassSchedule
                .FirstOrDefaultAsync(m => m.recno == id);
            if (classSchedule == null)
            {
                return NotFound();
            }

            return View(classSchedule);
        }

        // POST: AdminClassSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classSchedule = await _context.ClassSchedule.FindAsync(id);
            _context.ClassSchedule.Remove(classSchedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassScheduleExists(int id)
        {
            return _context.ClassSchedule.Any(e => e.recno == id);
        }
        public JsonResult getSection(string gradeyear)
        {
            IList<string> result = new List<string>();

            var query = (from p in _context.Section
                         where p.gradeyears == gradeyear
                         select p.sections);
            result.Add("");
            foreach (var x in query)
            {
                result.Add(x.ToString());
            }
            return Json(result);
        }

        public JsonResult getSubjectName(string subjectcode)
        {
            IList<string> result = new List<string>();

            var query = (from p in _context.Subject
                         where p.subjectcode == subjectcode
                         select p.subjectname);
            //result.Add("");
            foreach (var x in query)
            {
                result.Add(x.ToString());
            }
            return Json(result);
        }
    }
}
