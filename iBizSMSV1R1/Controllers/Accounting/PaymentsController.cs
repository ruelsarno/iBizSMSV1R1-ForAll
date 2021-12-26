using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using iBizSMSV1R1.ViewModels;
using iBizSMSV1R1.Functions;
using ReflectionIT.Mvc.Paging;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace iBizSMSV1R1.Controllers.Accounting
{
    //[Authorize(Roles = "Registrar,Student,Admin")]
    public class PaymentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;
        public PaymentsController(SignInManager<ApplicationUser> signInManager,
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

        // GET: BillsSummary
        // GET: BillToPays
        public async Task<IActionResult> Index(int id, string idno, string username, string SearchString,
            bool notUsed, int page, int pagedivider, string message, string identity, int referenceno, string billname, string schoolyear,
            string billdate, double amount = 5000)
        {
            ViewBag.SearchString = SearchString;
            ViewBag.Message = message;
            ViewBag.ID = id;
            ViewBag.ReferenceNo = referenceno;
            ViewBag.BillName = billname;
            ViewBag.SchoolYear = schoolyear;


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
                pagedivider = 15;
            }
            ViewBag.Page = page;
            if (!String.IsNullOrEmpty(SearchString))
            {

                long number1 = 0;
                bool canConvert = long.TryParse(SearchString, out number1);

                if (canConvert == false)
                {
                    var payments = _context.Payment.Where(p => p.id == user.Id && (p.schoolyear.Contains(SearchString)));
                    var model = PagingList.Create(payments, pagedivider, page);
                    var RecordCount = payments.OrderBy(p => p.recno).Count();
                    ViewBag.RecordCount = RecordCount;
                    return View(model);
                }
                else
                {

                    var payments = _context.Payment.Where(p => p.referenceno == Convert.ToInt32(SearchString));
                    var model = PagingList.Create(payments, pagedivider, page);
                    var RecordCount = payments.OrderBy(p => p.recno).Count();
                    ViewBag.RecordCount = RecordCount;
                    return View(model);
                }
            }
            else
            {
                var payments = _context.Payment.Where(p => p.id == (user.Id).Trim()).OrderByDescending(p => p.billdate);
                var model = await PagingList.CreateAsync(payments, pagedivider, page);
                var RecordCount = payments.OrderBy(p => p.recno).Count();
                ViewBag.RecordCount = RecordCount;
                return View(model);
            }
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .FirstOrDefaultAsync(m => m.recno == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,referenceno,paymentdate,paymentstatus,paymentoffices,amountpaid,paymentpostdate")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recno,referenceno,paymentdate,paymentstatus,paymentoffices,amountpaid,paymentpostdate")] Payment payment)
        {
            if (id != payment.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.recno))
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
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .FirstOrDefaultAsync(m => m.recno == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.recno == id);
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
