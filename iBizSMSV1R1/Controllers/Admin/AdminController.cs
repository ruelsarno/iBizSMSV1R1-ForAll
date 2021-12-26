using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace iBizSMSV1R1.Controllers.Admin
{
    //[Authorize(Roles = "Accounting,Admin,Registrar")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}