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
    public class ClassModulesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;
        public ClassModulesController(SignInManager<ApplicationUser> signInManager,
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

        // GET: ClassModules
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

            return View(await _context.ClassModule.Where(p => p.subjectname == subjectname).ToListAsync());
        }

        // GET: ClassModules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classModule = await _context.ClassModule
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (classModule == null)
            {
                return NotFound();
            }

            return View(classModule);
        }

        // GET: ClassModules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClassModules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recordno,recno,subjectname,lessonno,lessontitle,videolink,image,active")] ClassModule classModule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classModule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classModule);
        }

        // GET: ClassModules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classModule = await _context.ClassModule.FindAsync(id);
            if (classModule == null)
            {
                return NotFound();
            }
            return View(classModule);
        }

        // POST: ClassModules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recordno,recno,subjectname,lessonno,lessontitle,videolink,image,active")] ClassModule classModule)
        {
            if (id != classModule.recordno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classModule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassModuleExists(classModule.recordno))
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
            return View(classModule);
        }

        // GET: ClassModules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classModule = await _context.ClassModule
                .FirstOrDefaultAsync(m => m.recordno == id);
            if (classModule == null)
            {
                return NotFound();
            }

            return View(classModule);
        }

        // POST: ClassModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classModule = await _context.ClassModule.FindAsync(id);
            _context.ClassModule.Remove(classModule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassModuleExists(int id)
        {
            return _context.ClassModule.Any(e => e.recordno == id);
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


        [HttpGet]        
        public FileResult DownLoadFile(int id)
        {
            //List<BillToPay> ObjFiles = GetFileList(id);

            var FileById = (from Docu in _context.ClassModule
                            where Docu.recno.Equals(id)
                            select new { Docu.image, Docu.lessontitle }).ToList().FirstOrDefault();

            return File(FileById.image, "application/pdf", FileById.lessontitle);

        }
    }
}
