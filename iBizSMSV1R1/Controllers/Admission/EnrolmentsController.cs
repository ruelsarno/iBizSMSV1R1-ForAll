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
    //[Authorize(Roles = "Registrar,Student,Admin")]
    public class EnrolmentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private static ApplicationDbContext _context;
        private readonly ApplicationDbContext _context2;
        public EnrolmentsController(SignInManager<ApplicationUser> signInManager,
            ILogger<ViewModels.LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            ApplicationDbContext context2)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;          
            _context = context;
            _context2 = context2;
        }

        // GET: Enrolments
        public async Task<IActionResult> Index(int id, string idno, string username, string SearchString, bool notUsed, int page, int pagedivider, string message)
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
                    var enrolment = _context.Enrolment.Where(p => p.id == user.Id && (p.studentlevel.Contains(SearchString) ||
                    p.gradeyear.Contains(SearchString) || p.trackcode.Contains(SearchString)));
                    var model = PagingList.Create(enrolment, pagedivider, page);
                    var RecordCount = enrolment.OrderBy(p => p.idno).Count();
                    ViewBag.RecordCount = RecordCount;
                    return View(model);
                }
                else
                {
                    var enrolment = _context.Enrolment.Where(p => p.recno == Convert.ToInt32(SearchString));
                    var model = PagingList.Create(enrolment, pagedivider, page);
                    var RecordCount = enrolment.OrderBy(p => p.idno).Count();
                    ViewBag.RecordCount = RecordCount;
                    return View(model);
                }
            }
            else
            {
                var enrolment = _context.Enrolment.Where(p => p.id == user.Id).OrderBy(p => p.registrationdate);
                var model = await PagingList.CreateAsync(enrolment, pagedivider, page);
                var RecordCount = enrolment.OrderBy(p => p.idno).Count();
                ViewBag.RecordCount = RecordCount;
                return View(model);
            }
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
        public IActionResult Create(string id)
        {
            ViewBag.ID = id;
            string idno = getIDNO(id);
            ViewBag.StudentImage = GetStudentImage(id);
            //if (idno == null)
            //{
            //    return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotexists });
            //}

            ViewBag.Studentlevel = _context.Studentlevel.Select(h => new SelectListItem
            {
                Value = h.studentlevels,
                Text = h.studentlevels
            });
            ViewBag.SchoolYear = _context.SchoolYear.OrderByDescending(c=>c.schoolyears).Select(h => new SelectListItem
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
                Text = h.sections
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
        public async Task<IActionResult> Create([Bind("recno,idno,id,schoolyear,registrationno,registrationdate,studenttype,studentlevel,trackcode,gradeyear,section,discountcode,modeofpayment,schoollastattended,schooltype,confirmed,confirmedby,notified")] Enrolment enrolment)
        {

            enrolment.confirmed = false;       
            enrolment.notified = false;

            if (ModelState.IsValid)
            {
                _context.Add(enrolment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), null, new { id = enrolment.id, message = Messages.messagesuccess});
            }
            return RedirectToAction(nameof(Index), null, new { id = enrolment.id, message = Messages.messagenotsuccessful });
        }

        // GET: Enrolments/Edit/5
        public async Task<IActionResult> Edit(int? recno, string id)
        {
            ViewBag.RecordNo = recno;
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
                Text = h.sections
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
        public async Task<IActionResult> Edit(int recno, [Bind("recno,idno,id,schoolyear,registrationno,registrationdate,studenttype,studentlevel,trackcode,gradeyear,section,discountcode,modeofpayment,schoollastattended,schooltype,confirmed,confirmedby,notified")] Enrolment enrolment)
        {
            enrolment.confirmed = false;
            enrolment.notified = false;

            if (recno != enrolment.recno)
            {
                return RedirectToAction("Index", "Enrolments", new { message = Messages.messagenotexists, id = enrolment.id });
            }

            bool confirmed = EnrolmentConfirmed(recno);
            if (EnrolmentConfirmed(recno) == true)
            {
                return RedirectToAction("Index", "Enrolments", new { message = Messages.messageconfirmed, id = enrolment.id });
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
                        return RedirectToAction("Index", "Home", new { message = Messages.messagenotexists});
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), null, new { message = Messages.messageupdatesuccess });
            }
            return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotsuccessful});
        }

        // GET: Enrolments/Delete/5
        public async Task<IActionResult> Delete(int? recno, bool confirm, string id)
        {
            ViewBag.StudentImage = GetStudentImage(id);
            if (recno == null)
            {
                return RedirectToAction("Index", "Enrolments", new { message = Messages.messagenotexists, searchString = recno });
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
            bool confirmed = EnrolmentConfirmed(recno);
            if (confirmed == true)
            {
                return RedirectToAction("Index", "Enrolments", new { message = Messages.messageconfirmed, searchString = recno });
            }

            var enrolment = await _context.Enrolment.FindAsync(recno);
            _context.Enrolment.Remove(enrolment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool EnrolmentExists(int id)
        {
            return _context.Enrolment.Any(e => e.recno == id);
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
               
        private bool EnrolmentConfirmed(int? id)
        {
            bool confirmed = false;

            var result = _context.Enrolment.Where(p => p.recno == id).ToList();
            foreach (var item in result)
            {
                confirmed = item.confirmed;
            }           
            return confirmed;
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
