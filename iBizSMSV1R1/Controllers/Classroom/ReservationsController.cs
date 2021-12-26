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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using iBizSMSV1R1.ViewModels;
using iBizSMSV1R1.Functions;

namespace iBizSMSV1R1.Controllers.Admission
{
    //[Authorize(Roles = "Registrar,Admin")]
    public class ReservationsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;
        public ReservationsController(SignInManager<ApplicationUser> signInManager,
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

            ViewBag.ID = user.Id;
            ViewBag.StudentImage = GetStudentImage(user.Id);

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
                pagedivider = 5;
            }
            ViewBag.Page = page;
            if (!String.IsNullOrEmpty(SearchString))
            {
               
                long number1 = 0;
                bool canConvert = long.TryParse(SearchString, out number1);
              
                if (canConvert == false)
                {                  
                    var reservation = _context.Reservation.Where(p => p.id == user.Id && ( p.studentlevel.Contains(SearchString) ||
                    p.gradeyear.Contains(SearchString) || p.trackcode.Contains(SearchString)));
                    var model = PagingList.Create(reservation, pagedivider, page);
                    var RecordCount = reservation.OrderBy(p => p.idno).Count();
                    ViewBag.RecordCount = RecordCount;
                    return View(model);
                }
                else
                {                              
                    var reservation = _context.Reservation.Where(p => p.referenceno == Convert.ToInt32(SearchString));
                    var model = PagingList.Create(reservation, pagedivider, page);
                    var RecordCount = reservation.OrderBy(p => p.idno).Count();
                    ViewBag.RecordCount = RecordCount;
                    return View(model);
                }
               
            }
            else
            {
                var reservation = _context.Reservation.Where(p => p.id == user.Id).OrderByDescending(p => p.datereserve);
                var model = await PagingList.CreateAsync(reservation, pagedivider, page);
                var RecordCount = reservation.OrderBy(p => p.idno).Count();
                ViewBag.RecordCount = RecordCount;
                return View(model);
            }
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
            string idno = getIDNO(id);
            ViewBag.StudentImage = GetStudentImage(id);
            //if (idno == null)
            //{
            //    return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotexists });
            //}

            ViewBag.ID = id;
            ViewBag.IDNO = idno;
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
        public async Task<IActionResult> Create([Bind("referenceno,id,studenttype,datereserve,idno,schoolyear,studentlevel,gradeyear,trackcode,schoollastattended,schooltype,paymentstatus,confirm,confirmedby")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),null, new {searchString = reservation.referenceno.ToString() });
            }
            return RedirectToAction(nameof(Index), null, new { searchString = reservation.referenceno.ToString() });
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(long? referenceno, string id)
        {
            ViewBag.StudentImage = GetStudentImage(id);
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
        public async Task<IActionResult> Edit(int referenceno, [Bind("referenceno,id,studenttype,datereserve,idno,schoolyear,studentlevel,gradeyear,trackcode,schoollastattended,schooltype,paymentstatus,confirm,confirmedby")] Reservation reservation)
        {
            if (referenceno != reservation.referenceno)
            {
                return RedirectToAction("Index", "Reservations", new { message = Messages.messagenotexists, searchString = reservation.referenceno.ToString() });
            }

            bool confirmed = ReservationConfirmed(referenceno);
            if (confirmed == true)
            {
                return RedirectToAction("Index", "Reservations", new { message = Messages.messageconfirmed, searchString = reservation.referenceno.ToString() });
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
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), null, new { searchString = reservation.referenceno.ToString() });
            }
            return RedirectToAction(nameof(Index), null, new { searchString = reservation.referenceno.ToString() });
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id, bool confirm)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Reservations", new { message = Messages.messagenotexists, searchString = id });
            }

            

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.referenceno == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool confirmed = ReservationConfirmed(id);
            if (confirmed == true)
            {
                return RedirectToAction("Index", "Reservations", new { message = Messages.messageconfirmed, searchString = id });
            }

            var reservation = await _context.Reservation.FindAsync(id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(long id)
        {
            return _context.Reservation.Any(e => e.referenceno == id);
        }

        private bool ReservationConfirmed(int? id)
        {
            bool confirmed = false;

            var result = _context.Reservation.Where(p => p.referenceno == id).ToList();
            foreach (var item in result)
            {
                confirmed = item.confirm;
            }
            return confirmed;
        }

        private string getIDNO(string id)
        {
            string idno = "";

            var result = _context.StudentInfo.Where(p => p.id == id).ToList();
            foreach (var item in result)
            {
                idno = item.idno;
            }
            return idno;
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

        // GET: Image
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
