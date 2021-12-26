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
using iBizSMSV1R1.ModelsAccounting;
using Microsoft.AspNetCore.Authorization;

namespace iBizSMSV1R1.Controllers.Accounting
{
    //[Authorize(Roles = "Registrar,Student,Admin")]
    public class BillToPaysController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ViewModels.LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private static ApplicationDbContext _context;
        private static ApplicationDbContext _context2;
        public BillToPaysController(SignInManager<ApplicationUser> signInManager,
            ILogger<ViewModels.LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ApplicationDbContext context,
            ApplicationDbContext context2)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _context2 = context2;
        }

        string month = "";

        // GET: BillToPays
        public async Task<IActionResult> Index(int id, string idno, string username, string SearchString,
            bool notUsed, int page, int pagedivider, string message, string identity, int referenceno, string billname, string schoolyear,
            string billdate, string studentlevel, string modeofpayment)
        {
            ViewBag.SearchString = SearchString;
            ViewBag.Message = message;
            ViewBag.ID = id;
            ViewBag.ReferenceNo = referenceno;
            ViewBag.BillName = billname;
            ViewBag.SchoolYear = schoolyear;
            ViewBag.StudentLevel = studentlevel;
            ViewBag.ModeOfPayment = modeofpayment;
           

            string result = "";
            string payschedule = "";
            string nameofbill = "";
            string dateofbill = "";
            double amount = 0;
            int year = 0;
            int installmentcount = 0;
            string[] payscheduleArr = null;
            string sql = "";
            try
            {
                //The data for this process is from Reservations, Enrollments and NOT with this controller's process
                if (billname != null && identity != null && schoolyear != null && billdate != null)
                {
                    if (referenceno > 0 && billname != "" && identity != "" && schoolyear != "" && billdate != "")
                    {
                        var paymentinfolist = getTuitionFees(schoolyear, studentlevel, modeofpayment);
                        foreach (var item in paymentinfolist)
                        {
                            payschedule = item.paymentschedule;
                            payscheduleArr = payschedule.Split(',');
                            installmentcount = item.installmentcount;
                            for (int x = 0; x < (installmentcount + 2); x++)
                            {
                                if (payscheduleArr[x].ToLower().Contains("reserv"))
                                {
                                    nameofbill = "Reservation";
                                    amount = item.reservationfee;
                                    dateofbill = billdate;
                                }
                                else if (payscheduleArr[x].ToLower().Contains("enroll"))
                                {
                                    nameofbill = "Upon Enrollment";
                                    amount = item.uponenrollment;
                                    dateofbill = billdate;
                                }
                                else
                                {
                                    if (x < 6)
                                    {
                                        year = DateTime.Now.Year;
                                    }
                                    else
                                    {
                                        year = DateTime.Now.Year + 1;
                                    }

                                    nameofbill = "Installment Due";
                                    amount = item.installmentamount;

                                    if (payscheduleArr[x].ToLower().Contains("jan"))
                                        month = "01";
                                    if (payscheduleArr[x].ToLower().Contains("feb"))
                                        month = "02";
                                    if (payscheduleArr[x].ToLower().Contains("mar"))
                                        month = "03";
                                    if (payscheduleArr[x].ToLower().Contains("apr"))
                                        month = "04";
                                    if (payscheduleArr[x].ToLower().Contains("may"))
                                        month = "05";
                                    if (payscheduleArr[x].ToLower().Contains("jun"))
                                        month = "06";
                                    if (payscheduleArr[x].ToLower().Contains("jul"))
                                        month = "07";
                                    if (payscheduleArr[x].ToLower().Contains("aug"))
                                        month = "08";
                                    if (payscheduleArr[x].ToLower().Contains("sep"))
                                        month = "09";
                                    if (payscheduleArr[x].ToLower().Contains("oct"))
                                        month = "10";
                                    if (payscheduleArr[x].ToLower().Contains("nov"))
                                        month = "11";
                                    if (payscheduleArr[x].ToLower().Contains("dec"))
                                        month = "12";

                                    dateofbill = year.ToString() + "-" + month + "-15";
                                }
                                if (TransactionsToBillsToPay(referenceno, nameofbill, schoolyear, dateofbill) == false)
                                {
                                    sql = "insert into billtopay(referenceno,id,billnames,schoolyear,billdate,confirm,notified,payment,amount)values(" +
                                    referenceno + ",'" +
                                    identity + "','" +
                                    nameofbill + "','" +
                                    schoolyear + "','" +
                                    dateofbill + "'," + "'False','False','False'," +                                  
                                    amount + ")";
                                }

                                string notifymessage = myGlobalFunctions.InsertDeleteUpdate(sql);


                            }

                            break;

                        }
                    }
                }
            }
            catch
            {
                result = "Message/Notification Failed!";
            }


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
                var RecordCount = billstopay.OrderBy(p => p.billdate).Count();
                ViewBag.RecordCount = RecordCount;
                return View(model);
            }
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
        public IActionResult Create(string id)
        {
            ViewBag.StudentImage = GetStudentImage(id);
            return View();
        }

        // POST: BillToPays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recno,referenceno,id,schoolyear,billnames,billdate,paymenttype,paymentoffice,amount,duedate,payment,proofofpayment,confirm,confirmedby,notified")] BillToPay billToPay, IFormFile files)
        {
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
            ViewBag.RecordNo = recno;
            ViewBag.StudentImage = GetStudentImage(id);
            var user = await _userManager.GetUserAsync(User);

            ViewBag.PaymentType = _context.PaymentType.OrderByDescending(h => h.paymenttypes).Select(h => new SelectListItem
            {
                Value = h.paymenttypes,
                Text = h.paymenttypes
            });

            ViewBag.PaymentOffice = _context.PaymentOffice.OrderByDescending(h => h.paymentoffices).Select(h => new SelectListItem
            {
                Value = h.paymentoffices,
                Text = h.paymentoffices
            });

            if (user == null)
            {
                //return NotFound($"Your session is expired! Try logging in again.");
                return RedirectToAction("Index", "Home", new { message = Messages.messagesessionexpire });
            }

            ViewBag.LoginID = user.UserName;

            if (recno == 0)
            {
                return RedirectToAction("Index", "Home", new { message = Messages.messageexists });
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
        public async Task<IActionResult> Edit(long recno, [Bind("recno,referenceno,id,schoolyear,billnames,billdate,paymenttype,paymentoffice,amount,duedate,payment,proofofpayment,confirm,confirmedby,notified")] BillToPay billToPay, IFormFile files)
        {
            var ID = billToPay.id;
            var Referenceno = billToPay.referenceno;
            var RECNO = billToPay.recno;
            if (recno != billToPay.recno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        string fileName = files.FileName;
                        //2 Get the extension of the file
                        string extension = Path.GetExtension(fileName);
                        //3 check the file extension as png
                        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                        {
                            using (var ms = new MemoryStream())
                            {
                                files.CopyTo(ms);
                                billToPay.proofofpayment = ms.ToArray();                                
                            }

                            _context.Update(billToPay);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index), new { id = billToPay.id });
                        }

                        else
                        {
                            throw new Exception("File must be either .pdf or .jpg or .png");
                        }
                    }

                    return RedirectToAction(nameof(Index), new { id = billToPay.id });
                }
            }
            return RedirectToAction(nameof(Index), new { id = billToPay.id });
        }

        // GET: BillToPays/Delete/5
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

        // POST: BillToPays/Delete/5
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

        private bool TransactionsToBillsToPay(int referenceno, string billname, string schoolyear, string dateofbill)
        {
            bool result = false;
            try
            {
                int record = _context.BillToPay.Where(p => p.referenceno == referenceno && p.billnames == billname && p.schoolyear == schoolyear && p.billdate == dateofbill).Count(); ;
                if (record > 0)
                {
                    result = true;
                }

                
            }
            catch (Exception ex)
            {
                string message = ex.ToString();
            }

            return result;

        }

        public FileContentResult GetProofOfPayment(int recno)
        {
            try
            {
                BillToPay billToPay = _context.BillToPay.FirstOrDefault(p => p.recno == recno);
                if (billToPay != null && recno > 0)
                {
                    return File(billToPay.proofofpayment, null);
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

        [HttpGet]
        public FileResult DownLoadFile(int id)
        {
            //List<BillToPay> ObjFiles = GetFileList(id);

            var FileById = (from BTP in _context.BillToPay
                            where BTP.recno.Equals(id)
                            select new { BTP.billnames, BTP.proofofpayment }).ToList().FirstOrDefault();

            return File(FileById.proofofpayment, "application/pdf", FileById.billnames);

        }

        private bool BillToPayConfirmed(long id)
        {
            bool confirmed = false;

            var result = _context.BillToPay.Where(p => p.recno == id).ToList();
            foreach (var item in result)
            {
                confirmed = Convert.ToBoolean(item.confirm);
            }
            return confirmed;
        }

        public static IEnumerable<FeeTable> getTuitionFees(string schoolyear, string studentlevel, string paymentmode)
        {
            IEnumerable<FeeTable> smlist = (from c in _context.FeeTable
                                            where c.schoolyear == schoolyear && c.gradeyear == studentlevel && c.paymentmode == paymentmode
                                            select new FeeTable
                                            {
                                                schoolyear = c.schoolyear,
                                                gradeyear = c.gradeyear,
                                                paymentmode = c.paymentmode,
                                                tuitionfee = c.tuitionfee,
                                                reservationfee = c.reservationfee,
                                                installmentcount = c.installmentcount,
                                                paymentschedule = c.paymentschedule,
                                                uponenrollment = c.uponenrollment,
                                                installmentamount = c.installmentamount,

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
