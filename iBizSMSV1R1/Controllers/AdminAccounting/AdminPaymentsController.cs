using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.Models;
using iBizSMSV1R1.Functions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using iBizSMSV1R1.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace iBizSMSV1R1.Controllers.AdminAccounting
{
    //[Authorize(Roles = "Accounting,Admin")]
    public class AdminPaymentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly ApplicationDbContext _context;
        private static IEmailSender _emailSender;
        private static IOptions<EmailSetting> _emailSettings;
        public AdminPaymentsController(SignInManager<ApplicationUser> signInManager,
            ILogger<ViewModels.LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ApplicationDbContext context,
            IOptions<EmailSetting> emailSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _emailSender = emailSender;
            _emailSettings = emailSettings;
        }

        // GET: AdminPayments
        public IActionResult Index(string id, string idno, string message)
        {
            ViewBag.Message = message;
            ViewBag.IDNO = idno;
            ViewBag.ID = id;
            if (id == null)
            {
                return RedirectToAction("Index", "AdminStudents", null);
            }
            var result = _context.BillToPay.Where(p => p.id == id);
            return View(result);

        }

        // GET: AdminPayments/Details/5
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

        // GET: AdminPayments/Create
        public IActionResult Create(string id, long recno)
        {
            ViewBag.ID = id;

            ViewBag.PaymentType = _context.PaymentType.Select(h => new SelectListItem
            {
                Value = h.paymenttypes,
                Text = h.paymenttypes
            });
            ViewBag.PaymentOffice = _context.PaymentOffice.Select(h => new SelectListItem
            {
                Value = h.paymentoffices,
                Text = h.paymentoffices
            });
            ViewBag.ModeOfPayment = _context.ModeOfPayment.Select(h => new SelectListItem
            {
                Value = h.modeofpayments,
                Text = h.modeofpayments
            });

            ViewBag.BillToPay = _context.BillToPay.Where(p => p.recno == recno);
            return View();
        }

        // POST: AdminPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recordno,recno,id,referenceno,schoolyear,billnames,billdate,paymentdate,paymenttypes,paymentoffices,amountpaid,paymentpostdate")] Payment payment)
        {
            //var a = payment.recno;
            //var b = payment.id;
            //var c = payment.referenceno;
            //var d = payment.schoolyear;
            //var e = payment.billnames;
            //var f = payment.billdate;
            //var g = payment.paymentdate;
            //var h = payment.paymenttypes;
            //var i = payment.paymentoffices;
            //var j = payment.amountpaid;
            //var k = payment.paymentpostdate;

            bool paymentexist = PaymentExists(payment.recno);
            if (ModelState.IsValid)
            {
                if (paymentexist == false)
                {
                    _context.Add(payment);
                    await _context.SaveChangesAsync();

                    string sql = "Update billtopay set paymentupdate = 'True' where recno = " + payment.recno;
                    string notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                    return RedirectToAction(nameof(Index), new { id = payment.id, message = Messages.messageupdatesuccess });
                }
                else
                {
                    string sql = "Update billtopay set paymentupdate = 'True' where recno = " + payment.recno;
                    string notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);

                    return RedirectToAction(nameof(Index), new { id = payment.id, message = Messages.messageupdatesuccess });
                }

            }
            return RedirectToAction(nameof(Index), new { id = payment.id, message = Messages.messagenotsuccessful });

        }

        // GET: AdminPayments/Edit/5
        public async Task<IActionResult> Edit(long? recno, string id)
        {
            ViewBag.ID = id;

            ViewBag.PaymentType = _context.PaymentType.Select(h => new SelectListItem
            {
                Value = h.paymenttypes,
                Text = h.paymenttypes
            });
            ViewBag.PaymentOffice = _context.PaymentOffice.Select(h => new SelectListItem
            {
                Value = h.paymentoffices,
                Text = h.paymentoffices
            });
            ViewBag.ModeOfPayment = _context.ModeOfPayment.Select(h => new SelectListItem
            {
                Value = h.modeofpayments,
                Text = h.modeofpayments
            });

            if (recno == null)
            {
                return NotFound();
            }


            //var payment = _context.BillToPay.Where(p => p.recno == recno);
            ViewBag.BillToPay = _context.BillToPay.Where(p => p.recno == recno);
            var billtopay = await _context.BillToPay.FindAsync(recno);
            if (billtopay == null)
            {
                return NotFound();
            }
            return View(billtopay);
        }

        // POST: AdminPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int recno, [Bind("recno,id,referenceno,schoolyear,billnames,billdate,paymentdate,paymenttypes,paymentoffices,amountpaid,paymentpostdate")] Payment payment)
        {
            if (recno != payment.recno)
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
                return RedirectToAction(nameof(Index),new { id=payment.id,message =Messages.messageupdatesuccess });
            }
            return RedirectToAction(nameof(Index), new { id = payment.id, message = Messages.messagenotexists });
        }

        // GET: AdminPayments/Delete/5
        public async Task<IActionResult> Delete(int? recno, string id)
        {
            ViewBag.ID = id;

            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .FirstOrDefaultAsync(m => m.recno == recno);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: AdminPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int recno)
        {
            var payment = await _context.Payment.FindAsync(recno);
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = payment.id, message = Messages.messagenotexists });
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.recno == id);
        }
    }
}
