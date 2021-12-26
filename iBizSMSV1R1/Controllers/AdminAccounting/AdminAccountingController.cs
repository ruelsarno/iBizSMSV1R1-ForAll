using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBizSMSV1R1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace iBizSMSV1R1.Controllers.AdminAccounting
{
    //[Authorize(Roles = "Accounting,Admin")]
    public class AdminAccountingController : Controller
    {
        private static ApplicationDbContext _context;

        public AdminAccountingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult getFeeRecord(string schoolyear, string studentlevel, string paymentmode)
        {
            IList<string> feerecord = new List<string>();

            var query1 = (from p in _context.FeeTable
                          where p.schoolyear == schoolyear && p.gradeyear == studentlevel && p.paymentmode == paymentmode
                          select
                              p.tuitionfee
                         );

            foreach (var x in query1)
            {
                feerecord.Add(x.ToString());
            }



            return Json(feerecord);
        }
    }
}