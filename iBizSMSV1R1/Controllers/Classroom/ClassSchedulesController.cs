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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using iBizSMSV1R1.ViewModels;
using iBizSMSV1R1.Functions;
using ReflectionIT.Mvc.Paging;

namespace iBizSMSV1R1.Controllers.Classroom
{
    //[Authorize(Roles = "Registrar,Faculty,Student,Admin")]
    public class ClassSchedulesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;
        public ClassSchedulesController(SignInManager<ApplicationUser> signInManager,
            ILogger<ViewModels.LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        string currentid = "";
        string studentlevel = "";
        string gradeyear = "";
        string section = "";

        // GET: Reservations
        public async Task<IActionResult> Index(string id, string idno, string username, string SearchString, bool notUsed, int page, int pagedivider, string message)
        {
            ViewBag.SearchString = SearchString;
            ViewBag.Message = message;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //return NotFound($"Your session is expired! Try logging in again.");
                return RedirectToAction("Index", "Home", new { message = Messages.messagesessionexpire });
            }


            ViewBag.StudentImage = GetStudentImage(user.Id);
            currentid = user.Id;
            string schoolyear = getActiveSchoolYear();

            //Get Enrollment info for Class Schedule
            IList<string> result = new List<string>();
            var enrollmentinfo = _context.Enrolment.Where(p => p.schoolyear == schoolyear  && p.id == currentid).ToList();
            foreach (var item in enrollmentinfo)
            {
                studentlevel = item.studentlevel;
                gradeyear = item.gradeyear;
                section =  item.section;
            }

            if (String.IsNullOrEmpty(section))
            {
                return RedirectToAction("Index", "Classrooms", new { message = Messages.messagenotexists });
            }

            var schedule = _context.ClassSchedule.Where(p => p.schoolyear == schoolyear && p.studentgradeyear == gradeyear &&
                   p.section == section).OrderBy(p => p.weekday).ThenBy(p=>p.starttime);           
            return View(schedule);
        }

        // GET: ClassSchedules/Details/5
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

        // GET: ClassSchedules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClassSchedules/Create
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

        // GET: ClassSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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

        // POST: ClassSchedules/Edit/5
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

        // GET: ClassSchedules/Delete/5
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

        // POST: ClassSchedules/Delete/5
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

        private string getActiveSchoolYear()
        {
            string schoolyear = "";

            var result = _context.SchoolYear.Where(p => p.active == true).ToList();
            foreach (var item in result)
            {
                schoolyear = item.schoolyears;
            }
            return schoolyear;
        }

        private IList<string> getEnrollmentInfo()
        {
            
            string activeschoolyear = getActiveSchoolYear();
            IList<string> result = new List<string>();

            var enrollmentinfo = _context.Enrolment.Where(p => p.schoolyear == activeschoolyear && p.id == currentid).ToList();
            foreach (var item in enrollmentinfo)
            {
                result.Add(item.studentlevel);
                result.Add(item.gradeyear);
                result.Add(item.section);
            }
            return result;
        }

        public static IEnumerable<StudentImage> GetStudentImage(string id)
        {
            try
            {
                IEnumerable<StudentImage> Image = from a in _context.StudentImage
                                                  where a.id == id
                                                  select new StudentImage
                                                  {
                                                      recno = a.recno,
                                                      link = a.link,
                                                      image = a.image,
                                                      id = a.id
                                                  };

                return Image;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
