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
    public class ClassLiveStreamsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;
        public ClassLiveStreamsController(SignInManager<ApplicationUser> signInManager,
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

        // GET: ClassLiveStreams
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

            return View(await _context.ClassLiveStream.Where(p => p.subjectname == subjectname).ToListAsync());
        }

        // GET: ClassLiveStreams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classLiveStream = await _context.ClassLiveStream
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (classLiveStream == null)
            {
                return NotFound();
            }

            return View(classLiveStream);
        }

        // GET: ClassLiveStreams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClassLiveStreams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recordno,recno,subjectname,lessonno,lessontitle,videolink,active")] ClassLiveStream classLiveStream)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classLiveStream);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classLiveStream);
        }

        // GET: ClassLiveStreams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classLiveStream = await _context.ClassLiveStream.FindAsync(id);
            if (classLiveStream == null)
            {
                return NotFound();
            }
            return View(classLiveStream);
        }

        // POST: ClassLiveStreams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recordno,recno,subjectname,lessonno,lessontitle,videolink,active")] ClassLiveStream classLiveStream)
        {
            if (id != classLiveStream.recordno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classLiveStream);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassLiveStreamExists(classLiveStream.recordno))
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
            return View(classLiveStream);
        }

        // GET: ClassLiveStreams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classLiveStream = await _context.ClassLiveStream
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (classLiveStream == null)
            {
                return NotFound();
            }

            return View(classLiveStream);
        }

        // POST: ClassLiveStreams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classLiveStream = await _context.ClassLiveStream.FindAsync(id);
            _context.ClassLiveStream.Remove(classLiveStream);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassLiveStreamExists(int id)
        {
            return _context.ClassLiveStream.Any(e => e.recordno == id);
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
