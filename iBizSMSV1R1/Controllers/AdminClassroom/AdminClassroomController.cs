using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace iBizSMSV1R1.Controllers.AdminClassroom
{
    //[Authorize(Roles = "Admin")]
    public class AdminClassroomController : Controller
    {
        [Authorize(Roles = "Registrar,Admin,Faculty")]
        public IActionResult Index()
        {
            return View();
        }
    }
}