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
    public class AdminReservationsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;        
        private readonly ApplicationDbContext _context;
        private static IEmailSender _emailSender;
        private static IOptions<EmailSetting> _emailSettings;
        public AdminReservationsController(SignInManager<ApplicationUser> signInManager,
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

        // GET: Reservations
        public async Task<IActionResult> Index(string id, string referenceno, string message)
        {
            ViewBag.Message = message;           
            ViewBag.ID = id;            

            if (id == null)
            {
                return RedirectToAction("Index", "AdminStudents", null);
            }
            var result = _context.Reservation.Where(p => p.id == id);            
            return View(result);
          
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.referenceno == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create(string id)
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
            ViewBag.SchoolType = _context.SchoolType.Select(h => new SelectListItem
            {
                Value = h.schooltypes,
                Text = h.schooltypes
            });
            ViewBag.PaymentStatus = _context.StatusOfPayment.Select(h => new SelectListItem
            {
                Value = h.paymentstatus,
                Text = h.paymentstatus
            });
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,referenceno,studenttype,datereserve,idno,schoolyear,studentlevel,gradeyear,trackcode,schoollastattended,schooltype,paymentstatus,confirm,confirmedby,notified")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), null, new { searchString = reservation.referenceno.ToString(), id = reservation.id });
            }
            return RedirectToAction(nameof(Index), null, new { searchString = reservation.referenceno.ToString(),id = reservation.id });
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(long? referenceno, string idno, string id)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.ID = id;
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
            ViewBag.SchoolType = _context.SchoolType.Select(h => new SelectListItem
            {
                Value = h.schooltypes,
                Text = h.schooltypes
            });
            ViewBag.PaymentStatus = _context.StatusOfPayment.Select(h => new SelectListItem
            {
                Value = h.paymentstatus,
                Text = h.paymentstatus
            });
            if (referenceno == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(referenceno);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long referenceno, [Bind("id,referenceno,studenttype,datereserve,idno,schoolyear,studentlevel,gradeyear,trackcode,schoollastattended,schooltype,paymentstatus,confirm,confirmedby,notified")] Reservation reservation)
        {
            if (referenceno != reservation.referenceno)
            {
                return RedirectToAction(nameof(Index), null, new { id = reservation.id, message = Messages.messagenotexists });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.referenceno))
                    {
                        return RedirectToAction(nameof(Index), null, new { id = reservation.id, message = Messages.messagenotexists });
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), null, new { id = reservation.id, message = Messages.messagesuccess });
            }
            return RedirectToAction(nameof(Index), null, new { id = reservation.id, message = Messages.messagenotexists });
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(long? referenceno, string id)
        {
            ViewBag.ID = id;

            if (referenceno == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.referenceno == referenceno);
            if (reservation == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index), null, new { id = reservation.id, message = Messages.messagenotexists });
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long referenceno)
        {
            var reservation = await _context.Reservation.FindAsync(referenceno);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), null, new { id = reservation.id, message = Messages.messagenotexists });
        }

        private bool ReservationExists(long id)
        {
            return _context.Reservation.Any(e => e.referenceno == id);
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


        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendeMail(int referenceno, string id)
        {
            string result = "";
            var user = await _userManager.FindByIdAsync(id);
            if (id == null)
            {
                return RedirectToAction(nameof(Index), "Reservations", new { id = id, message = Messages.messagenotexists });
            }

            string subject = "Reservation Notification";
            string message = "Please be informed that your reservation has been confirmed.\n" +
                "Please proceed to payment to process your reservation\n\n" +
                "Thank you very much!";

            try
            {
                if (ReservationNotified(referenceno) == false)
                {
                    await _emailSender.SendEmailAsync(user.Email, subject, message);
                    string sql = "Update reservation set notified = 'True' where referenceno = " + referenceno;
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

            return RedirectToAction(nameof(Index), null, new { id = id, referenceno = referenceno, message = result });
        }
        private bool ReservationNotified(int id)
        {
            bool result = false;
            var record = _context.Reservation.Where(p => p.referenceno == id);
            foreach (var item in record)
            {
                result = item.notified;
            }

            return result;
        }
    }
}