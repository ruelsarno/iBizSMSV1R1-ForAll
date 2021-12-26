using System;
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

namespace iBizSMSV1R1.Controllers.Account
{
    //[Authorize(Roles = "Student,Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;

        public AccountController(SignInManager<ApplicationUser> signInManager,
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

        string StatusMessage = "";
        public async Task<IActionResult> AccountIndex(string message)
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

        public async Task<IActionResult> ProfilePost(string message)
        {
            ViewBag.Message = message;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            
            ViewBag.UserName = user.UserName;
            ViewBag.PhoneNumber = user.PhoneNumber;
            ViewBag.StudentImage = GetStudentImage(user.Id);

            return View();
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfilePost(String phonenumber, string email)
        {
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            //if (!ModelState.IsValid)
            //{
            //    await LoadAsync(user);
            //    return Page();
            //}

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (phonenumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, phonenumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    StatusMessage = ($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                    return RedirectToAction("ProfilePost", new { message = StatusMessage });
                }
                else
                {
                    await _signInManager.RefreshSignInAsync(user);
                    StatusMessage = "Your profile has been updated";
                }
            }

           
            return RedirectToAction("ProfilePost",new {message= StatusMessage });
           
        }

        // GET: SystemUser/Create
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetPassword(string message)
        {
            ViewBag.Message = message;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            ViewBag.UserName = user.UserName;
            ViewBag.UserID = user.Id;
            ViewBag.StudentImage = GetStudentImage(user.Id);
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetPassword(ResetUserPasswordViewModel model)
        {           
            string message = "";

            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(model.Username);

                // UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());

                if (user.PasswordHash != null)
                {
                    if (user.PasswordHash != "")
                    {
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user, model.Password);
                    }
                }
                else
                {
                    await _userManager.AddPasswordAsync(user, model.Password);
                }

                message = "Password successfully reset!";
                TempData["MessageValue"] = "1";

                return RedirectToAction("ResetPassword", "Account", new { message = message });
            }

            // If we got this far, something failed, redisplay form
            message = "Invalid User Details. Please try again in some minutes ";
            TempData["MessageValue"] = "0";
            return RedirectToAction("ResetPassword", "Account", new { message = message });
        }

        // GET: SystemUser/Create
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeEMail(string message)
        {
            ViewBag.Message = message;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            ViewBag.UserName = user.UserName;
            ViewBag.UserID = user.Id;
            ViewBag.EMail = user.Email;
            ViewBag.EMailConfirmed = user.EmailConfirmed;
            ViewBag.StudentImage = GetStudentImage(user.Id);
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeEMail(ReNewEMailAddress model)
        {
            string message = "";

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                message = ($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                message = ($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (model.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId = userId, email = model.NewEmail, code = code },
                    protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(
                    model.NewEmail,
                    "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                message = "Confirmation link to change email sent. Please check your email.";
                return RedirectToAction("ChangeEMail", "Account", new { message = message });
            }

            message = "Your email is unchanged.";
            return RedirectToAction("ChangeEMail", "Account", new { message = message });
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