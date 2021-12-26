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
    public class ClassroomsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;
        public ClassroomsController(SignInManager<ApplicationUser> signInManager,
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

        public async Task<IActionResult> Index(string message)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //return NotFound($"Your session is expired! Try logging in again.");
                return RedirectToAction("Index", "Home", new { message = Messages.messagesessionexpire });
            }

            ViewBag.ID = user.Id;
            ViewBag.StudentImage = GetStudentImage(user.Id);

            ViewBag.Message = message;
            return View();
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