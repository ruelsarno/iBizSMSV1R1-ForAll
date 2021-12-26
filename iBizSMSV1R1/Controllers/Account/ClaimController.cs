using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBizSMSV1R1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using iBizSMSV1R1.Data;
namespace iBizSMSV1R1.Controllers.Account
{
    //[Authorize(Roles = "Admin")]
    public class ClaimController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;        
        private readonly UserManager<ApplicationUser> userManager;
        public ClaimController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult ClaimList()
        {
            List<ApplicationRole> model = new List<ApplicationRole>();
            model = roleManager.Roles.Select(r => new ApplicationRole
            {
                Name = r.Name,
                Id = r.Id,
                NumberOfUsers = r.Name.Count()
            }).ToList();
            return View(model);
        }
    }
}
