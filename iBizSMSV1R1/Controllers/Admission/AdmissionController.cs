using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.Areas.Identity.Pages.Account;
using iBizSMSV1R1.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using iBizSMSV1R1.ViewModels;
using iBizSMSV1R1.Functions;
using System;

namespace iBizSMSV1R1.Controllers.Admission
{
    //[Authorize(Roles = "Registrar,Admin")]
    public class AdmissionController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;

        public AdmissionController(SignInManager<ApplicationUser> signInManager,
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
        public async Task<IActionResult> AdmissionIndex(string message)
        {
            ViewBag.Message = message;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                return RedirectToAction("Index", "Home", new { message = Messages.messagesessionexpire });
            }

            ViewBag.UserName = user.UserName;
            ViewBag.ID = user.Id;

            ViewBag.StudentImage = GetStudentImage(user.Id);

            return View();
        }

        public static IEnumerable<ProcessIntro> getProcessIntro(string title)
        {

            IEnumerable<ProcessIntro> smlist = (from c in _context.ProcessIntro
                                                where c.title == title
                                                select new ProcessIntro
                                                {
                                                    recno = c.recno,
                                                    title = c.title,
                                                    subtitle = c.subtitle,
                                                    description = c.description

                                                }).Distinct();

            return smlist;
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