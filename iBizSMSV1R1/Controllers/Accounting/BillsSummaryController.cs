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
    public class BillsSummaryController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;
        public BillsSummaryController(SignInManager<ApplicationUser> signInManager,
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
                    var billstopay = _context.BillToPay.Where(p => p.id == user.Id && (p.schoolyear.Contains(SearchString)));
                    var model = PagingList.Create(billstopay, pagedivider, page);
                    var RecordCount = billstopay.OrderBy(p => p.recno).Count();
                    ViewBag.RecordCount = RecordCount;
                    return View(model);
                }
                else
                {

                    var billstopay = _context.BillToPay.Where(p => p.referenceno == Convert.ToInt32(SearchString));
                    var model = PagingList.Create(billstopay, pagedivider, page);
                    var RecordCount = billstopay.OrderBy(p => p.recno).Count();
                    ViewBag.RecordCount = RecordCount;
                    return View(model);
                }
            }
            else
            {
                var billstopay = _context.BillToPay.Where(p => p.id == user.Id).OrderBy(p => p.recno);
                var model = await PagingList.CreateAsync(billstopay, pagedivider, page);
                var RecordCount = billstopay.OrderBy(p => p.recno).Count();
                ViewBag.RecordCount = RecordCount;
                return View(model);
            }
        }

        // GET: BillsSummary/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billToPay = await _context.BillToPay
                .FirstOrDefaultAsync(m => m.recno == id);
            if (billToPay == null)
            {
                return NotFound();
            }

            return View(billToPay);
        }

        // GET: BillsSummary/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillsSummary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,referenceno,id,schoolyear,billnames,billdate,amount,duedate,payment,proofofpayment,confirm,confirmedby,notified")] BillToPay billToPay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billToPay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billToPay);
        }

        // GET: BillsSummary/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billToPay = await _context.BillToPay.FindAsync(id);
            if (billToPay == null)
            {
                return NotFound();
            }
            return View(billToPay);
        }

        // POST: BillsSummary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("recno,referenceno,id,schoolyear,billnames,billdate,amount,duedate,payment,proofofpayment,confirm,confirmedby,notified")] BillToPay billToPay)
        {
            if (id != billToPay.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billToPay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillToPayExists(billToPay.recno))
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
            return View(billToPay);
        }

        // GET: BillsSummary/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billToPay = await _context.BillToPay
                .FirstOrDefaultAsync(m => m.recno == id);
            if (billToPay == null)
            {
                return NotFound();
            }

            return View(billToPay);
        }

        // POST: BillsSummary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var billToPay = await _context.BillToPay.FindAsync(id);
            _context.BillToPay.Remove(billToPay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillToPayExists(long id)
        {
            return _context.BillToPay.Any(e => e.recno == id);
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
