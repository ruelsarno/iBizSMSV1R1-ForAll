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

namespace iBizSMSV1R1.Controllers.StudentProfile
{
    //[Authorize(Roles = "Registrar,Student,Admin")]
    public class StudentInfoesController : Controller
    {        

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;
        public StudentInfoesController(SignInManager<ApplicationUser> signInManager,
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
               
        // GET: StudentInfoes
        public async Task<IActionResult> Index(string message, string id)
        {
            ViewBag.Message = message;
            var user = await _userManager.GetUserAsync(User);

            if (!String.IsNullOrEmpty(id))
            {                
                    ViewBag.ID = id;
                    ViewBag.StudentImage = GetStudentImage(id);
                    var studentinfo = _context.StudentInfo.Where(p => p.id == id);
                    return View(studentinfo);              
            }
            else
            {
                ViewBag.ID = user.Id;
                ViewBag.StudentImage = GetStudentImage(user.Id);
                var studentinfo = _context.StudentInfo.Where(p => p.id == user.Id);
                return View(studentinfo);
            }

            //return View();
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
        public IActionResult Create(string id)
        {
            ViewBag.ID = id;
            ViewBag.StudentImage = GetStudentImage(id);
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
        public async Task<IActionResult> Create([Bind("idno,id,lrn,surname,firstname,middlename," +
            "extension,birthday,birthplace,nationality,civilstatus,gender,active")] StudentInfo studentInfo)
        {
            var a = studentInfo.idno;
            var b = studentInfo.id;
            var c= studentInfo.lrn;
            var d = studentInfo.surname;
            var e = studentInfo.firstname;
            var f = studentInfo.middlename;
            var g = studentInfo.extension;
            var h = studentInfo.birthday;
            var i = studentInfo.birthplace;
            var j = studentInfo.nationality;
            var k = studentInfo.civilstatus;
            var l = studentInfo.gender;
            var m = studentInfo.active;


            if (ModelState.IsValid)
            {
                if (!StudentInfoExists(studentInfo.id))
                {
                    _context.Add(studentInfo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), null, new { message = Messages.messagesuccess });                   
                }
                else
                {
                    return RedirectToAction(nameof(Index), null, new { message = Messages.messageexists });
                }

                
            }
            return RedirectToAction(nameof(Index), null, new { searchString = studentInfo.idno });
        }

        // GET: StudentInfoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.StudentImage = GetStudentImage(id);
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
                return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotexists });
            }

            var studentInfo = await _context.StudentInfo.FindAsync(id);
            if (studentInfo == null)
            {
                return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotexists });
            }
            return View(studentInfo);
        }

        // POST: StudentInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("idno,id,lrn,surname,firstname,middlename,extension,birthday,birthplace,nationality,civilstatus,gender,active")] StudentInfo studentInfo)
        {
            if (id != studentInfo.id)
            {
                return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotexists });
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
                        return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotexists });
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
            ViewBag.StudentImage = GetStudentImage(id);
            if (id == null)
            {
                return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotexists });
            }

            var studentInfo = await _context.StudentInfo
                .FirstOrDefaultAsync(m => m.idno == id);
            if (studentInfo == null)
            {
                return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotexists });
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
            return _context.StudentInfo.Any(e => e.id == id);
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
