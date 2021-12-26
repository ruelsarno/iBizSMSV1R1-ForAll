using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace iBizSMSV1R1.Controllers.ControllerOtherEntry
{
    //[Authorize(Roles = "Admin")]
    public class HomeOtherEntryController : Controller
    {
        public IActionResult IndexAccounting()
        {
            return View();
        }
        public IActionResult IndexRegistrar()
        {
            return View();
        }
        public IActionResult IndexOther()
        {
            return View();
        }
    }
}