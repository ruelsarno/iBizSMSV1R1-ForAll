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
    public class ClassVideosController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;
        public ClassVideosController(SignInManager<ApplicationUser> signInManager,
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

        // GET: ClassVideos
        public async Task<IActionResult> Index(string subjectname, string teachername)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //return NotFound($"Your session is expired! Try logging in again.");
                return RedirectToAction("Index", "Home", new { message = Messages.messagesessionexpire });
            }

            ViewBag.ID = user.Id;
            ViewBag.StudentImage = GetStudentImage(user.Id);

            return View(await _context.ClassVideo.Where(p=>p.subjectname==subjectname).ToListAsync());
        }

        // GET: ClassVideos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classVideo = await _context.ClassVideo
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (classVideo == null)
            {
                return NotFound();
            }

            return View(classVideo);
        }

        // GET: ClassVideos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClassVideos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recordno,recno,subjectname,lessonno,lessontitle,videolink")] ClassVideo classVideo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classVideo);
        }

        // GET: ClassVideos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classVideo = await _context.ClassVideo.FindAsync(id);
            if (classVideo == null)
            {
                return NotFound();
            }
            return View(classVideo);
        }

        // POST: ClassVideos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recordno,recno,subjectname,lessonno,lessontitle,videolink")] ClassVideo classVideo)
        {
            if (id != classVideo.recordno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassVideoExists(classVideo.recordno))
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
            return View(classVideo);
        }

        // GET: ClassVideos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classVideo = await _context.ClassVideo
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (classVideo == null)
            {
                return NotFound();
            }

            return View(classVideo);
        }

        // POST: ClassVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classVideo = await _context.ClassVideo.FindAsync(id);
            _context.ClassVideo.Remove(classVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassVideoExists(int id)
        {
            return _context.ClassVideo.Any(e => e.recordno == id);
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
