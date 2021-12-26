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
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using iBizSMSV1R1.ViewModels;
using Microsoft.Extensions.Options;

namespace iBizSMSV1R1.Controllers.AdminAdmission
{
    //[Authorize(Roles = "Registrar,Admin,Accounting")]
    public class AdminEnrolmentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly ApplicationDbContext _context;
        private static IEmailSender _emailSender;
        private static IOptions<EmailSetting> _emailSettings;
        public AdminEnrolmentsController(SignInManager<ApplicationUser> signInManager,
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

        // GET: Enrolments
        public async Task<IActionResult> Index(string id, string idno, string message)
        {
            ViewBag.Message = message;
            ViewBag.ID = id;

            if (id == null)
            {
                return RedirectToAction("Index", "AdminStudents", null);
            }
            var result = _context.Enrolment.Where(p => p.id == id);
            return View(result);

        }

        // GET: Enrolments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrolment = await _context.Enrolment
                .FirstOrDefaultAsync(m => m.recno == id);
            if (enrolment == null)
            {
                return NotFound();
            }

            return View(enrolment);
        }

        // GET: Enrolments/Create
        public IActionResult Create()
        {
            ViewBag.Studentlevel = _context.Studentlevel.Select(h => new SelectListItem
            {
                Value = h.studentlevels,
                Text = h.studentlevels
            });
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
            ViewBag.TrackCode = _context.TrackCode.Select(h => new SelectListItem
            {
                Value = h.trackcodes,
                Text = h.trackcodes
            });
            ViewBag.ModeOfPayment = _context.ModeOfPayment.Select(h => new SelectListItem
            {
                Value = h.modeofpayments,
                Text = h.modeofpayments
            });
            ViewBag.DiscountCode = _context.Discount.Select(h => new SelectListItem
            {
                Value = h.discountcodes,
                Text = h.discountcodes
            });
            ViewBag.Section = _context.Section.Select(h => new SelectListItem
            {
                Value = h.sections,
                Text = h.gradeyears + " " + h.sections
            });
            ViewBag.SchoolType = _context.SchoolType.Select(h => new SelectListItem
            {
                Value = h.schooltypes,
                Text = h.schooltypes
            });
            return View();
        }

        // POST: Enrolments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,idno,schoolyear,registrationno,registrationdate,studenttype,studentlevel,trackcode,gradeyear,section,discountcode,modeofpayment,schoollastattended,schooltype,confirmed,confirmedby,notified")] Enrolment enrolment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrolment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), null, new { searchString = enrolment.recno.ToString() });
            }
            return RedirectToAction(nameof(Index), null, new { searchString = enrolment.recno.ToString() });
        }

        // GET: Enrolments/Edit/5
        public async Task<IActionResult> Edit(int? recno, string id, string idno)
        {
            ViewBag.ID = id;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //return NotFound($"Your session is expired! Try logging in again.");
                return RedirectToAction("Index", "Home", new { message = Messages.messagesessionexpire });
            }
            ViewBag.LoginID = user.UserName;

            ViewBag.Studentlevel = _context.Studentlevel.Select(h => new SelectListItem
            {
                Value = h.studentlevels,
                Text = h.studentlevels
            });
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
            ViewBag.TrackCode = _context.TrackCode.Select(h => new SelectListItem
            {
                Value = h.trackcodes,
                Text = h.trackcodes
            });

            ViewBag.DiscountCode = _context.Discount.Select(h => new SelectListItem
            {
                Value = h.discountcodes,
                Text = h.discountcodes
            });
            ViewBag.ModeOfPayment = _context.ModeOfPayment.Select(h => new SelectListItem
            {
                Value = h.modeofpayments,
                Text = h.modeofpayments
            });
            ViewBag.Section = _context.Section.Select(h => new SelectListItem
            {
                Value = h.sections,
                Text = h.gradeyears + " " + h.sections
            });
            ViewBag.SchoolType = _context.SchoolType.Select(h => new SelectListItem
            {
                Value = h.schooltypes,
                Text = h.schooltypes
            });
            if (recno == null)
            {
                return NotFound();
            }

            var enrolment = await _context.Enrolment.FindAsync(recno);
            if (enrolment == null)
            {
                return NotFound();
            }
            return View(enrolment);
        }

        // POST: Enrolments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int recno, [Bind("id,recno,idno,schoolyear,registrationno,registrationdate,studenttype,studentlevel,trackcode,gradeyear,section,discountcode,modeofpayment,schoollastattended,schooltype,confirmed,confirmedby,notified")] Enrolment enrolment)
        {
            if (recno != enrolment.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrolment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrolmentExists(enrolment.recno))
                    {
                        return RedirectToAction(nameof(Index), null, new { id = enrolment.id, idno = enrolment.idno, message = Messages.messagenotexists });
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), null, new { id = enrolment.id, idno = enrolment.idno, message = Messages.messagesuccess });
            }
            return RedirectToAction(nameof(Index), null, new { id = enrolment.id, idno = enrolment.idno, message = Messages.messagenotexists });
        }

        // GET: Enrolments/Delete/5
        public async Task<IActionResult> Delete(int? recno, string id)
        {
            ViewBag.ID = id;
            if (recno == null)
            {
                return NotFound();
            }

            var enrolment = await _context.Enrolment
                .FirstOrDefaultAsync(m => m.recno == recno);
            if (enrolment == null)
            {
                return NotFound();
            }

            return View(enrolment);
        }

        // POST: Enrolments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int recno)
        {
            var enrolment = await _context.Enrolment.FindAsync(recno);
            _context.Enrolment.Remove(enrolment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), null, new { id = enrolment.id, idno = enrolment.idno, message = Messages.messagenotexists });
        }

        private bool EnrolmentExists(int id)
        {
            return _context.Enrolment.Any(e => e.recno == id);
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

        public JsonResult getTrackCode(string studentlevel)
        {
            IList<string> result = new List<string>();

            var query = (from p in _context.TrackCode
                         where p.studentlevels == studentlevel
                         select p.trackcodes);
            result.Add("");
            foreach (var x in query)
            {
                result.Add(x.ToString());
            }
            return Json(result);
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

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendeMail(int recno, string id, string idno)
        {
            string result = "";
            var user = await _userManager.FindByIdAsync(id);
            if (id == null)
            {
                return RedirectToAction(nameof(Index), "Enrolments", new { id = id, message = Messages.messagenotexists });
            }

            string subject = "Enrollment Notification";
            string message = "Please be informed that your enrollment has been confirmed.\n" +
                "Please proceed to payment to process your enrollment\n\n" +
                "Thank you very much!";

            try
            {
                if (EnrolmentNotified(recno) == false)
                {
                    await _emailSender.SendEmailAsync(user.Email, subject, message);
                    string sql = "Update enrolment set notified = 'True' where recno = " + recno;
                    string notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);
                    result = "Message/Notification Sent!";
                }
                else
                {
                    result = "Message/Notification already Sent!";
                }
            }
            catch
            {
                result = "Message/Notification Failed!";
            }

            return RedirectToAction(nameof(Index), null, new { id = id, idno = idno, message = result });
        }

        private bool EnrolmentNotified(int id)
        {
            bool result = false;
            var record = _context.Enrolment.Where(p => p.recno == id);
            foreach (var item in record)
            {
                result = item.notified;
            }

            return result;
        }
    }
}