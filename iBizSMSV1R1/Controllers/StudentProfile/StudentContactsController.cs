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
using Microsoft.AspNetCore.Http;
using System.IO;

namespace iBizSMSV1R1.Controllers.StudentProfile
{
    //[Authorize(Roles = "Registrar,Student,Admin")]
    public class StudentContactsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;
        public StudentContactsController(SignInManager<ApplicationUser> signInManager,
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

        // GET: StudentContacts
        public async Task<IActionResult> Index(string message)
        {
           // return View(await _context.StudentContact.ToListAsync());

            ViewBag.Message = message;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //return NotFound($"Your session is expired! Try logging in again.");
                return RedirectToAction("Index", "Home", new { message = Messages.messagesessionexpire });
            }

            ViewBag.ID = user.Id;
            ViewBag.StudentImage = GetStudentImage(user.Id);

            var studentinfo = _context.StudentContact.Where(p => p.id == user.Id);
            return View(studentinfo);
        }

        
        // GET: StudentContacts/Create
        public IActionResult Create(string id)
        {
            ViewBag.ID = id;
            ViewBag.StudentImage = GetStudentImage(id);
            return View();
        }

        // POST: StudentContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,id,telephoneno,cellphoneno,emailadd")] StudentContact studentContact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentContact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotsuccessful, id = studentContact.id });
            }
            return RedirectToAction(nameof(Index), null, new { message = Messages.messagenotsuccessful, id = studentContact.id });
       
        }

        // GET: StudentContacts/Edit/5
        public async Task<IActionResult> Edit(int? recno, string id)
        {
            ViewBag.ID = id;
            ViewBag.StudentImage = GetStudentImage(id);

            if (id == null)
            {
                return RedirectToAction(nameof(Index), null, new { message = Messages.messagesessionexpire, id = id });
            }
           
            var studentContact = await _context.StudentContact.FindAsync(recno);
            if (studentContact == null)
            {
                return RedirectToAction(nameof(Index),"StudentContacts",new { message = Messages.messagenotexists, id = id });
            }
            return View(studentContact);
           
        }

        // POST: StudentContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int recno, [Bind("recno,id,telephoneno,cellphoneno,emailadd")] StudentContact studentContact)
        {
            if (recno != studentContact.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentContactExists(studentContact.recno))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "StudentContacts", new { message = Messages.messageupdatesuccess, id = studentContact.id });
            }
            return RedirectToAction(nameof(Index), "StudentContacts", new { message = Messages.messageunsuccessfulprocess, id = studentContact.id });
        }

        // GET: StudentContacts/Delete/5
        public async Task<IActionResult> Delete(int? recno, string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentContact = await _context.StudentContact
                .FirstOrDefaultAsync(m => m.recno == recno);
            if (studentContact == null)
            {
                return NotFound();
            }

            return View(studentContact);
        }

        // POST: StudentContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int recno)
        {
            var studentContact = await _context.StudentContact.FindAsync(recno);
            _context.StudentContact.Remove(studentContact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), null, new { message = Messages.messagesuccess, id = studentContact.id });
        }

        private bool StudentContactExists(int id)
        {
            return _context.StudentContact.Any(e => e.recno == id);
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
