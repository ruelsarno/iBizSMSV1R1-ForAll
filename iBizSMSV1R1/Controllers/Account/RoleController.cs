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
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        
        [HttpGet]        
        public IActionResult RoleList()
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

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> RoleCreate(CreateRoleViewModel createroleviewmodel)
        {

            if (ModelState.IsValid)
            {
                IdentityRole identityrole = new IdentityRole
                {
                    Name = createroleviewmodel.rolename
                };

                IdentityResult result = await roleManager.CreateAsync(identityrole);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList", "Role");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(createroleviewmodel);
        }

        // GET: Awards/Edit/5
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> RoleEdit(string id)
        {
            {
                if (id == null)
                    return NotFound();
            }

            ApplicationRole model = new ApplicationRole();
            if (!String.IsNullOrEmpty(id))
            {
                var applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    model.Id = applicationRole.Id;
                    model.Name = applicationRole.Name;
                }
            }

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> RoleEdit(string id, [Bind("Name")] ApplicationRole applicationrole)
        {
            if (ModelState.IsValid)
            {
                bool isExist = !String.IsNullOrEmpty(id);
                var applicationRole = await roleManager.FindByIdAsync(id);
                
                applicationRole.Name = applicationrole.Name;

                IdentityResult roleResult = isExist ? await roleManager.UpdateAsync(applicationRole)
                                                 : await roleManager.CreateAsync(applicationRole);
                if (roleResult.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
            }
            return View(applicationrole);
        }

        // GET: Role/Delete/5
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RoleDelete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationRole model = new ApplicationRole();
            if (!String.IsNullOrEmpty(id))
            {
                var applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    model.Id = applicationRole.Id;
                    model.Name = applicationRole.Name;
                }
            }

            if (model == null)
            {
                return NotFound();
            }

            return View(model);

        }

        // POST: Nationalities/Delete/5
        [HttpPost, ActionName("RoleDelete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> RoleDeleteConfirmed(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationrole = await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(applicationrole);

            return RedirectToAction(nameof(RoleList));
        }
    }
}