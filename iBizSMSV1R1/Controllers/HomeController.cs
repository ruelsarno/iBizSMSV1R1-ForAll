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
using iBizSMSV1R1.ModelsAdmission;

namespace iBizSMSV1R1.Controllers
{
    //[Authorize(Roles = "Accounting,Admin,Registrar")]
    public class HomeController : Controller
    {     

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;


        public HomeController(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
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

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        //public async Task OnGetAsync(string returnUrl = null)
        //{
        //    if (!string.IsNullOrEmpty(ErrorMessage))
        //    {
        //        ModelState.AddModelError(string.Empty, ErrorMessage);
        //    }

        //    returnUrl = returnUrl ?? Url.Content("~/");

        //    // Clear the existing external cookie to ensure a clean login process
        //    await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        //    ReturnUrl = returnUrl;
        //}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string UserName, string password, bool rememberme = false )
        {           

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(UserName, password, rememberme, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    return Redirect("/StudentInfoes/Index");

                }
                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                //}
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Redirect("/");
                }
            }

            // If we got this far, something failed, redisplay form
            return Redirect("/");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string UserName, string email, string phonenumber, string password, string confirm, string confirmpassword)
        {           
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = UserName, Email = email, PhoneNumber = phonenumber };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                { 
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect("/");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return LocalRedirect("/");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin(string email, string phonenumber, string password, string confirm, string confirmpassword)
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = email, Email = email, PhoneNumber = phonenumber };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = email });
                    }
                    else
                    {
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect("/SystemUser/UserList");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return LocalRedirect("/");
        }

        // [Authorize(Roles = "Admin")]
        public IActionResult Index(string message, string action)
        {
            ViewBag.Action = action;
            ViewBag.Message = message;
            return View();
        }
        public IActionResult About(string message, string action)
        {
            ViewBag.Action = action;
            var webpages = getWebPageTitles("About Us");
            ViewBag.AboutUsDetail = getWebPageAboutUsDetail();
            ViewBag.Webpage = webpages;
            ViewBag.Message = message;
            return View();
        }
        public IActionResult Courses(string message, string action)
        {
            ViewBag.Action = action;
            ViewBag.Message = message;
            return View();
        }

        public IActionResult CourseDetails(string message, int recno, string coursetitle, string actionroute)
        {
            ViewBag.Action = actionroute;
            ViewBag.Message = message;
            ViewBag.CourseRecno = recno;
            ViewBag.CourseTitle = coursetitle;



            ViewBag.Course = getCourses(recno);
            ViewBag.CourseDetails = getCourseDetails(recno);
            return View();
        }
        public IActionResult Events(string message, string action)
        {
            ViewBag.Action = action;
            ViewBag.Message = message;
            return View();
        }

        public IActionResult Achievements(string message, string action)
        {
            ViewBag.Action = action;
            ViewBag.Message = message;
            return View();
        }
        public IActionResult AchievementDetails(string message, int recno, string actionroute)
        {
            ViewBag.Action = actionroute;
            ViewBag.Message = message;
            ViewBag.AchievementRecno = recno;
            ViewBag.Achievement = getEvents(recno);
            ViewBag.AchievementDetails = getAchievements(recno);
            return View();
        }
        public IActionResult EventDetails(string message, int recno, string actionroute)
        {
            ViewBag.Action = actionroute;
            ViewBag.Message = message;
            ViewBag.EventRecno = recno;
            ViewBag.Event = getEvents(recno);
            ViewBag.EventDetails = getEventDetails(recno);
            return View();
        }
       
        public IActionResult Blog(string message, string action)
        {
            ViewBag.Action = action;
            ViewBag.Message = message;
            return View();
        }
        public IActionResult Contact(string message, string action)
        {
            ViewBag.Action = action;
            ViewBag.Message = message;
            return View();
        }
        public IActionResult BlogDetails(string message, string action)
        {
            ViewBag.Action = action;
            ViewBag.Message = message;
            return View();
        }
        //public IActionResult CourseDetails(string message)
        //{
        //    ViewBag.Message = message;
        //    return View();
        //}
        //public IActionResult EventDetails(string message)
        //{
        //    ViewBag.Message = message;
        //    return View();
        //}
        public IActionResult Notice(string message, string action)
        {
            ViewBag.Action = action;
            ViewBag.Message = message;
            return View();
        }
        public IActionResult NoticeDetails(string message, string action)
        {
            ViewBag.Action = action;
            ViewBag.Message = message;
            return View();
        }
        public IActionResult Research(string message)
        {
            ViewBag.Message = message;
            return View();
        }
        public IActionResult Scholarship(string message)
        {
            ViewBag.Message = message;
            return View();
        }
        public IActionResult Teacher(string message, string action)
        {
            ViewBag.Action = action;
            ViewBag.Message = message;
            return View();
        }
        public IActionResult TeacherDetails(string message, int recno, string actionroute)
        {
            ViewBag.Action = actionroute;
            ViewBag.Message = message;
            ViewBag.TeacherRecno = recno;
            ViewBag.TeacherDetails = getTeacherDetails(recno);
            return View();
        }

        public static IEnumerable<WebPageTitles> getWebPageTitles()
        {

            IEnumerable<WebPageTitles> smlist = (from c in _context.WebPageTitles
                                                 orderby c.recno
                                                 select new WebPageTitles
                                                 {
                                                     recno = c.recno,
                                                     title = c.title,
                                                     tagline = c.tagline,
                                                     description = c.description,
                                                     controller = c.controller,
                                                     action = c.action,
                                                     
                                                 }).Distinct();

            return smlist;
        }

        public static IEnumerable<WebPageTitles> getWebPageTitles(string title)
        {
           
            IEnumerable<WebPageTitles> smlist = (from c in _context.WebPageTitles
                                                 where c.action.ToLower().Trim() == title.ToLower().Trim()
                                                 orderby c.recno
                                                 select new WebPageTitles
                                                 {
                                                     recno = c.recno,
                                                     title = c.title,
                                                     tagline = c.tagline,
                                                     description = c.description,
                                                     controller = c.controller,
                                                     action = c.action,
                                                     link = c.link,
                                                     image = c.image
                                                 }).Distinct();

            return smlist;
        }
        public static IEnumerable<WebPages> getWebPages(string subtitle)
        {

            IEnumerable<WebPages> smlist = (from c in _context.WebPages
                                            where c.subtitle == subtitle
                                            where c.active == true
                                            orderby c.recno
                                                 select new WebPages
                                                 {
                                                     recno = c.recno,
                                                     title = c.title,
                                                     subtitle = c.subtitle,
                                                     tagline = c.tagline,
                                                     description = c.description,
                                                     controller = c.controller,
                                                     action = c.action,
                                                     icon = c.icon,
                                                     link = c.link,
                                                     image = c.image

                                                 }).Distinct();

            return smlist;
        }
        public static IEnumerable<WebPageSubs> getSubWebPages(string subtitle)
        {
            IEnumerable<WebPageSubs> smlist = (from c in _context.WebPageSubs
                                            where c.subtitle == subtitle
                                            orderby c.recno
                                            select new WebPageSubs
                                            {
                                                recno = c.recno,
                                                title = c.title,
                                                subtitle = c.subtitle,
                                                subsubtitle = c.subsubtitle,
                                                tagline = c.tagline,
                                                personname = c.personname,
                                                when = c.when,
                                                description = c.description,
                                                controller = c.controller,
                                                action = c.action,
                                                icon = c.icon,
                                                link = c.link,
                                                image = c.image

                                            }).Distinct();

            return smlist;
        }
        public static IEnumerable<WebPageSuccessStory> getWebPageSuccessStory()
        {

            IEnumerable<WebPageSuccessStory> smlist = (from c in _context.WebPageSuccessStory                                                    
                                           
                                            select new WebPageSuccessStory
                                            {
                                                recno = c.recno,
                                                title = c.title,
                                                subtitle = c.subtitle,
                                                paragraph1 = c.paragraph1,                                               
                                                controller = c.controller,
                                                action = c.action,
                                                icon = c.icon,
                                                link = c.link,
                                                image = c.image

                                            }).Distinct();

            return smlist;
        }
        public static IEnumerable<WebPageContact> getWebPageContact()
        {

            IEnumerable<WebPageContact> smlist = (from c in _context.WebPageContact
                                                  select new WebPageContact
                                            {
                                                recno = c.recno,
                                                fullname = c.fullname,
                                                address = c.address,
                                                cellphoneno = c.cellphoneno,
                                                landlineno = c.landlineno,
                                                emailadd = c.emailadd,
                                                website = c.website,
                                                longitude = c.longitude,
                                                latitude = c.latitude
                                            }).Distinct();

            return smlist;
        }

        public static IEnumerable<WebPageAbout> getAbout()
        {

            IEnumerable<WebPageAbout> smlist = (from c in _context.WebPageAbout
                                                orderby c.recno
                                                select new WebPageAbout
                                                {
                                                    recno = c.recno,
                                                    title = c.title,
                                                    tagline = c.tagline,
                                                    icon = c.icon,
                                                    description = c.description,
                                                    controller = c.controller,
                                                    action = c.action,
                                                    image = c.image,
                                                    link = c.link
                                                }).Distinct();

            return smlist;
        }

        public static IEnumerable<WebPageCourse> getCourses()
        {

            IEnumerable<WebPageCourse> smlist = (from c in _context.WebPageCourse
                                                 orderby c.recno
                                            select new WebPageCourse
                                            {
                                                recno = c.recno,
                                                title = c.title,
                                                coursetitle = c.coursetitle,
                                                tagline = c.tagline,
                                                icon = c.icon,
                                                description = c.description,
                                                controller = c.controller,
                                                action = c.action,                                              
                                                image = c.image,
                                                link = c.link
                                            }).Distinct();

            return smlist;
        }

        public static IEnumerable<WebPageCourse> getCourses(int recno)
        {

            IEnumerable<WebPageCourse> smlist = (from c in _context.WebPageCourse
                                                 where c.recno == recno
                                                 orderby c.recno
                                                 select new WebPageCourse
                                                 {
                                                     recno = c.recno,
                                                     title = c.title,
                                                     coursetitle = c.coursetitle,
                                                     tagline = c.tagline,
                                                     icon = c.icon,
                                                     description = c.description,
                                                     controller = c.controller,
                                                     action = c.action,
                                                     image = c.image,
                                                     link = c.link
                                                 }).Distinct();

            return smlist;
        }
        public static IEnumerable<AdmissionRequirement> getAdmissionRequirement()
        {

            IEnumerable<AdmissionRequirement> smlist = (from c in _context.AdmissionRequirement                                                       
                                                 orderby c.recno
                                                 select new AdmissionRequirement
                                                 {
                                                     recno = c.recno,
                                                     title = c.title,
                                                     schoolyear = c.schoolyear,
                                                     requirement = c.requirement
                                                    
                                                 }).Distinct();

            return smlist;
        }

        public static IEnumerable<EnrollmentProdecure> getAdmissionProcedure()
        {

            IEnumerable<EnrollmentProdecure> smlist = (from c in _context.EnrollmentProdecure                                                                        
                                                        orderby c.recno
                                                        select new EnrollmentProdecure
                                                        {
                                                            recno = c.recno,
                                                            title = c.title,
                                                            schoolyear = c.schoolyear,
                                                            procedure = c.procedure

                                                        }).Distinct();

            return smlist;
        }

        public static IEnumerable<WebPageBlog> getBlogs()
        {

            IEnumerable<WebPageBlog> smlist = (from c in _context.WebPageBlog
                                               orderby c.recno
                                                 select new WebPageBlog
                                                 {
                                                     recno = c.recno,
                                                     title = c.title,
                                                     blogtitle = c.blogtitle,
                                                     author = c.author,
                                                     postdate = c.postdate,
                                                     tagline = c.tagline,
                                                     description = c.description,
                                                     controller = c.controller,
                                                     action = c.action,
                                                     image = c.image,
                                                     link = c.link


                                                 }).Distinct();

            return smlist;
        }
        public static IEnumerable<WebPageEvent> getEvents()
        {

            IEnumerable<WebPageEvent> smlist = (from c in _context.WebPageEvent
                                                where c.active == true
                                                orderby c.recno
                                               select new WebPageEvent
                                               {
                                                   recno = c.recno,
                                                   title = c.title,
                                                   eventtitle = c.eventtitle,                                                  
                                                   tagline = c.tagline,
                                                   icon = c.icon,
                                                   description = c.description,
                                                   controller = c.controller,
                                                   action = c.action,
                                                   image = c.image,
                                                   link = c.link


                                               }).Distinct();

            return smlist;
        }
        public static IEnumerable<WebPageEvent> getEvents(int recno)
        {

            IEnumerable<WebPageEvent> smlist = (from c in _context.WebPageEvent
                                                where c.recno == recno
                                                orderby c.recno
                                                select new WebPageEvent
                                                {
                                                    recno = c.recno,
                                                    title = c.title,
                                                    eventtitle = c.eventtitle,
                                                    tagline = c.tagline,
                                                    icon = c.icon,
                                                    description = c.description,
                                                    controller = c.controller,
                                                    action = c.action,
                                                    image = c.image,
                                                    link = c.link


                                                }).Distinct();

            return smlist;
        }
        public static IEnumerable<WebPageAchievement> getAchievements()
        {

            IEnumerable<WebPageAchievement> smlist = (from c in _context.WebPageAchievement
                                                      where c.active == true
                                                orderby c.priority
                                                select new WebPageAchievement
                                                {
                                                    recno = c.recno,
                                                    title = c.title,
                                                    achievementname = c.achievementname,
                                                    tagline = c.tagline,
                                                    icon = c.icon,
                                                    description = c.description,
                                                    controller = c.controller,
                                                    action = c.action,
                                                    image = c.image,
                                                    link = c.link


                                                }).Distinct();

            return smlist;
        }
        public static IEnumerable<WebPageAchievement> getAchievements(int recno)
        {

            IEnumerable<WebPageAchievement> smlist = (from c in _context.WebPageAchievement
                                                      where c.recno == recno
                                                orderby c.recno
                                                select new WebPageAchievement
                                                {
                                                    recno = c.recno,
                                                    title = c.title,
                                                    achievementname = c.achievementname,
                                                    tagline = c.tagline,
                                                    icon = c.icon,
                                                    description = c.description,
                                                    controller = c.controller,
                                                    action = c.action,
                                                    image = c.image,
                                                    link = c.link


                                                }).Distinct();

            return smlist;
        }

        public static IEnumerable<WebPageEventDetails> getEventDetails(int recno)
        {

            IEnumerable<WebPageEventDetails> smlist = (from c in _context.WebPageEvent
                                                       join d in _context.WebPageEventDetail on c.recno equals d.recno
                                                         where c.recno == recno
                                                         select new WebPageEventDetails
                                                         {
                                                             recno = c.recno,
                                                             title = c.title,
                                                             tagline = c.tagline,
                                                             eventtitle = c.eventtitle,
                                                             icon = c.icon,
                                                             description = c.tagline,
                                                             controller = c.controller,
                                                             action = c.action,
                                                             image = c.image,
                                                             link = c.link,
                                                             imagelink = d.imagelink,
                                                             imagedetails = d.imagedetails,
                                                             speakername = d.speakername,                                                             
                                                             venue = d.venue,
                                                             eventdetails = d.eventdetails,
                                                             eventdate = d.eventdate

                                                         }).Distinct();

            return smlist;
        }

        public static IEnumerable<WebPageCourseDetail> getCourseDetails(int recno)
        {

            IEnumerable<WebPageCourseDetail> smlist = (from c in _context.WebPageCourseDetail                                                      
                                                       where c.recno == recno
                                                       select new WebPageCourseDetail
                                                       {
                                                           recordno = c.recordno,
                                                           recno = c.recno,
                                                           aboutthecourse = c.aboutthecourse,
                                                           requirement = c.requirement,
                                                           howtoenroll = c.howtoenroll,
                                                           fees = c.fees,
                                                           image = c.image,
                                                           imagelink = c.imagelink

                                                       }).Distinct();

            return smlist;
        }
        public static IEnumerable<WebPageTeacher> getTeachers()
        {

            IEnumerable<WebPageTeacher> smlist = (from c in _context.WebPageTeacher
                                                  orderby c.recno
                                                select new WebPageTeacher
                                                {
                                                    recno = c.recno,
                                                    title = c.title,
                                                    fullname = c.fullname,
                                                    specialization = c.specialization,
                                                    category = c.category,
                                                    tagline = c.tagline,                                                    
                                                    controller = c.controller,
                                                    action = c.action,
                                                    image = c.image,
                                                    link = c.link

                                                }).Distinct();

            return smlist;
        }

        public static IEnumerable<WebPageTeacherDetails> getTeacherDetails(int recno)
        {

            IEnumerable<WebPageTeacherDetails> smlist = (from c in _context.WebPageTeacher
                                                  join d in _context.WebPageTeacherDetail on c.recno equals d.recno
                                                  where c.recno == recno                                                 
                                                  select new WebPageTeacherDetails
                                                  {
                                                      recno = c.recno,
                                                      title = c.title,
                                                      fullname = c.fullname,
                                                      specialization = c.specialization,
                                                      category = c.category,
                                                      tagline = c.tagline,
                                                      controller = c.controller,
                                                      action = c.action,
                                                      image = c.image,
                                                      link = c.link,
                                                      jobdescription = d.jobdescription,
                                                      biography = d.biography,
                                                      hobbiesinterest = d.hobbiesinterest,
                                                      contactinfo = d.contactinfo,

                                                  }).Distinct();

            return smlist;
        }

        public static IEnumerable<WebPageAbout> getWebPageAboutUsDetail()
        {

            IEnumerable<WebPageAbout> smlist = (from c in _context.WebPageAbout
                                                orderby c.recno
                                                select new WebPageAbout
                                                {
                                                    recno = c.recno,
                                                    title = c.title,
                                                    tagline = c.tagline,
                                                    description = c.description,
                                                    controller = c.controller,
                                                    action = c.action,
                                                    icon = c.icon,
                                                    link = c.link,
                                                    image = c.image,
                                                }).Distinct();

            return smlist;
        }

        public FileContentResult GetThumbnailImage(string id)
        {          
            try
            {
                WebPages image = _context.WebPages.FirstOrDefault();
                if (image != null && id != null)
                {
                    return File(image.image, null);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }        
    }
}
