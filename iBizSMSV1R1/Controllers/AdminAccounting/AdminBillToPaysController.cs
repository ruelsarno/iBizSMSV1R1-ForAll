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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace iBizSMSV1R1.Controllers.AdminAccounting
{
    //[Authorize(Roles = "Accounting,Admin")]
    public class AdminBillToPaysController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly ApplicationDbContext _context;
        private static IEmailSender _emailSender;
        private static IOptions<EmailSetting> _emailSettings;
        public AdminBillToPaysController(SignInManager<ApplicationUser> signInManager,
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

        // GET: BillToPays
        public async Task<IActionResult> Index(string id, string referenceno, string message)
        {
            ViewBag.Message = message;
            ViewBag.ID = id;

            if (id == null)
            {
                return RedirectToAction("Index", "AdminStudents", null);
            }
            var result = _context.BillToPay.Where(p => p.id == id);
            return View(result);
        }

        // GET: BillToPays/Details/5
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

        // GET: BillToPays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillToPays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,referenceno,id,schoolyear,billnames,billdate,paymenttype,paymentoffice,amount,duedate,payment,proofofpayment,confirm,confirmedby,notified,paymentupdate")] BillToPay billToPay)
        {
            ViewBag.ID = billToPay.id;
            if (ModelState.IsValid)
            {
                _context.Add(billToPay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billToPay);
        }

        // GET: BillToPays/Edit/5
        public async Task<IActionResult> Edit(long? recno, string id)
        {
            ViewBag.ID = id;

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //return NotFound($"Your session is expired! Try logging in again.");
                return RedirectToAction("Index", "Home", new { message = Messages.messagesessionexpire });
            }
            ViewBag.LoginID = user.UserName;

            if (recno == null)
            {
                return NotFound();
            }

            var billToPay = await _context.BillToPay.FindAsync(recno);
            if (billToPay == null)
            {
                return NotFound();
            }
            return View(billToPay);
        }

        // POST: BillToPays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long recno, [Bind("recno,referenceno,id,schoolyear,billnames,billdate,paymenttype,paymentoffice,amount,duedate,payment,proofofpayment,confirm,confirmedby,notified,paymentupdate")] BillToPay billToPay)
        {
            if (recno != billToPay.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                try
                {             
                    
                    string sql = "Update billtopay set confirm = 'True', confirmedby = '" + billToPay.confirmedby + "' where recno = " + billToPay.recno;
                    string notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);
                    return RedirectToAction(nameof(Index), null, new { id = billToPay.id, message = Messages.messageupdatesuccess });
                }
                catch
                {
                    return RedirectToAction(nameof(Index), null, new { id = billToPay.id, message = Messages.messagenotexists }); 
                }
                
            }
            return RedirectToAction(nameof(Index), null, new { id = billToPay.id,  message = Messages.messagenotexists }); 
        }

        // GET: BillToPays/Delete/5
        public async Task<IActionResult> Delete(long? recno, string id)
        {
            ViewBag.ID = id;

            if (id == null)
            {
                return NotFound();
            }

            var billToPay = await _context.BillToPay
                .FirstOrDefaultAsync(m => m.recno == recno);
            if (billToPay == null)
            {
                return NotFound();
            }

            return View(billToPay);
        }

        // POST: BillToPays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long recno)
        {
            var billToPay = await _context.BillToPay.FindAsync(recno);
            _context.BillToPay.Remove(billToPay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), null, new { id = billToPay.id, message = Messages.messagenotexists });
        }

        private bool BillToPayExists(long id)
        {
            return _context.BillToPay.Any(e => e.recno == id);
        }

        [HttpGet]
        public FileResult DownLoadFile(int id)
        {
            //List<BillToPay> ObjFiles = GetFileList(id);

            var FileById = (from BTP in _context.BillToPay
                            where BTP.recno.Equals(id)
                            select new { BTP.billnames, BTP.proofofpayment }).ToList().FirstOrDefault();
            if (FileById != null)
                return File(FileById.proofofpayment, "application/jpg", FileById.billnames);
            else
                return null;
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendeMail(int recno, string id, int referenceno, string schoolyear, string billname, string billdate)
        {
           //DateTime dateofbill = Convert.ToDateTime(billdate);
            string result = "";
            var user = await _userManager.FindByIdAsync(id);
            if (id == null)
            {
                return RedirectToAction(nameof(Index), "BillToPays", new { id = id, message = Messages.messagenotexists });
            }

            string subject = "Payment Notification";
            string message = "Please be informed that your payment has been confirmed.\n" +
                "Thank you very much!";

            DateTime paymentpostdate = DateTime.Now;
            DateTime paymentdate = DateTime.Now;
            double amountpaid = 0;
            string paymenttype = "";
            string paymentoffice = "";
            var billstopay = _context.BillToPay.Where(c => c.recno == recno);
            foreach (var item in billstopay)
            {
                amountpaid = item.amount;
                paymenttype =  item.paymenttype;
                paymentoffice =  item.paymentoffice;
            }
            
            try
            {
                if (PaymentNotified(recno) == false && PaymentConfirmed(recno) == true && PaymentDone(recno) == true)
                {
                    await _emailSender.SendEmailAsync(user.Email, subject, message);
                    string sql2 = "insert into payment(referenceno, recno, id, schoolyear, billnames, billdate,paymenttypes,paymentoffices, amountpaid, paymentpostdate,paymentdate)values('" + referenceno + "'," + recno + ",'" + id + "','" + schoolyear + "','" + billname + "','" + billdate + "','" + paymenttype + "','" + paymentoffice + "','" + amountpaid + "','" + paymentpostdate + "','" + paymentdate + "')";
                    myGlobalFunctions.InsertDeleteUpdate(sql2);
                    string sql = "Update billtopay set notified = 'True' where recno = " + recno;
                    string notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);                    
                   
                    result = "Message/Notification SUCCESSFULLY Sent!";
                }
                else
                {
                    result = "Message/Either Notification ALREADY Sent or Payment NOT yet CONFIRMED!";
                }
            }
            catch
            {
                result = "Message/Notification Failed!";
            }

            return RedirectToAction(nameof(Index), null, new { id = id, referenceno = referenceno, message = result });
        }
        private bool PaymentNotified(int id)
        {
            bool result = false;
            var record = _context.BillToPay.Where(p => p.recno == id);
            foreach (var item in record)
            {
                result = Convert.ToBoolean(item.notified);
            }

            return result;
        }

        private bool PaymentDone(int id)
        {
            bool result = false;
            var record = _context.BillToPay.Where(p => p.recno == id);
            foreach (var item in record)
            {
                result = Convert.ToBoolean(item.payment);
            }

            return result;
        }

        private bool PaymentConfirmed(int id)
        {
            bool result = false;
            var record = _context.BillToPay.Where(p => p.recno == id);
            foreach (var item in record)
            {
                result = Convert.ToBoolean(item.confirm);
            }

            return result;
        }
    }
}


