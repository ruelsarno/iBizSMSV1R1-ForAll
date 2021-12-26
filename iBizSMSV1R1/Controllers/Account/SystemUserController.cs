using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iBizSMSV1R1.Controllers.Account
{
    //[Authorize(Roles = "Admin")]
    public class SystemUserController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public SystemUserController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: SystemUser
        [Authorize(Roles = "Admin")]
        public ActionResult UserList(int id, string idno, string username, string search_data, bool notUsed, int page, int pagedivider, string message, string location, DateTime periodstart, DateTime periodend, bool download)
        {
            ViewBag.SearchData = search_data;
            ViewBag.Message = message;
            if (page <= 0)
            {
                page = 1;
            }
            if (pagedivider <= 0)
            {
                pagedivider = 50;
            }
            ViewBag.Page = page;

            if ((string.IsNullOrEmpty(search_data)))
            {
                var result = _userManager.Users.Where(r => r.UserName.Contains("@@@@") || r.Email.Contains("@@@@")).Select(r => new SystemUser
                {
                    Id = r.Id,
                    UserName = r.UserName,
                    Email = r.Email,
                    PhoneNumber = r.PhoneNumber,
                    IdNo = r.idno,
                    Name = r.name,
                    CardId = r.cardid,
                    EmailConfirmed = r.EmailConfirmed
                }).OrderByDescending(r => r.UserName).ToList();

                List<SystemUser> model = new List<SystemUser>();
                model = ReflectionIT.Mvc.Paging.PagingList.Create(result, pagedivider, page);
                var RecordCount = result.Count();
                ViewBag.RecordCount = RecordCount;
                return View(model);

            }
            else
            {
                bool isNumeric = int.TryParse(search_data, out int n);

                if (isNumeric == false)
                {
                    var result = _userManager.Users.Where(r => r.UserName.Contains(search_data) || r.Email.Contains(search_data)).Select(r => new SystemUser
                    {
                        Id = r.Id,
                        UserName = r.UserName,
                        Email = r.Email,
                        PhoneNumber = r.PhoneNumber,
                        IdNo = r.idno,
                        Name = r.name,
                        CardId = r.cardid,
                        EmailConfirmed = r.EmailConfirmed
                    }).ToList();

                    List<SystemUser> model = new List<SystemUser>();
                    model = ReflectionIT.Mvc.Paging.PagingList.Create(result, pagedivider, page);
                    var RecordCount = result.Count();
                    ViewBag.RecordCount = RecordCount;
                    return View(model);
                }
                else
                {
                    var result = _userManager.Users.OrderByDescending(c=>c.Id).Take(Convert.ToInt32(search_data)).Select(r => new SystemUser
                    {
                        Id = r.Id,
                        UserName = r.UserName,
                        Email = r.Email,
                        PhoneNumber = r.PhoneNumber,
                        IdNo = r.idno,
                        Name = r.name,
                        CardId = r.cardid,
                        EmailConfirmed = r.EmailConfirmed
                    }).ToList();

                    List<SystemUser> model = new List<SystemUser>();
                    model = ReflectionIT.Mvc.Paging.PagingList.Create(result, pagedivider, page);
                    var RecordCount = result.Count();
                    ViewBag.RecordCount = RecordCount;
                    return View(model);
                }
               
            }
        }
    


        // GET: SystemUser/Create
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleToUser(string id, string userrole, string search_data)
        {
            ViewBag.SearchData = search_data;
            if (!String.IsNullOrEmpty(userrole))
            {
                ViewBag.Message = "MESSAGE: Role " + userrole + " Added.";
            }
            
            ViewBag.RoleID = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            });

            ApplicationUser user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            var UserRoles = new List<SelectListItem>();
            UserRoles.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });

            foreach (string role in roles)
            {
                UserRoles.Add(new SelectListItem { Text = role, Value = role });
            }
            ViewBag.UserRole = UserRoles;

            ViewData["UserId"] = id;
            RoleOfUser roleofuser = new RoleOfUser();
            return View(roleofuser);
        }

        // POST: SystemUser/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleToUser(string id, string role, [Bind("UserId,RoleId")] RoleOfUser roleofuser)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                await _userManager.AddToRoleAsync(user, roleofuser.RoleId);
                return RedirectToAction(nameof(AddRoleToUser),new {id = id, userrole = roleofuser.ApplicationRoles, message = "User Role Successfully ADDED!" });
                
            }

            return RedirectToAction(nameof(UserList));
        }

        // GET: SystemUser/Create
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRoleToUser(string id, string username, string userrole, string search_data)
        {
            ViewBag.SearchData = search_data;
            if (String.IsNullOrEmpty(userrole))
            {
                ViewBag.Message = "MESSAGE: Role " + userrole + " Removed.";
            }

            using (var context = new ApplicationDbContext())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
            }


            ViewBag.RoleID = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            });


            ApplicationUser user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            var UserRoles = new List<SelectListItem>();
            UserRoles.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });

            foreach (string role in roles)
            {
                UserRoles.Add(new SelectListItem { Text = role, Value = role });
            }
            ViewBag.UserRole = UserRoles;


            ViewData["UserName"] = user.UserName;
            ViewData["UserID"] = id;
            ViewData["UserRole"] = UserRoles;
            RoleOfUser roleofuser = new RoleOfUser();
            return View(roleofuser);
        }

        // POST: SystemUser/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRoleToUser(string id, [Bind("UserId,RoleId")] RoleOfUser roleofuser)
        {
            var roleToDelete = roleofuser.ApplicationRoles;
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                await _userManager.RemoveFromRoleAsync(user, roleofuser.RoleId);               
                return RedirectToAction(nameof(RemoveRoleToUser), new { id = id, userrole = roleToDelete, message = "User Role Successfully REMOVED!" });
            }

            return RedirectToAction(nameof(UserList));
        }

        // GET: SystemUser/Edit/5
        //[Authorize(Roles = "Admin")]
        public ActionResult EditUser(string id, string username, string phonenumber, string email, string idno, string name, string cardid, bool EmailConfirmed, string search_data)
        {
            ViewData["UserName"] = username;
            ViewData["EMail"] = email;
            ViewData["PhoneNumber"] = phonenumber;
            ViewData["IdNo"] = idno;
            ViewData["Name"] = name;
            ViewData["CardID"] = cardid;
            ViewData["EmailConfirmed"] = EmailConfirmed;

            ViewBag.SearchData = search_data;
            //ViewBag.Message = message;
            var model = new EditUserViewModel
            {
                Id = id,
                UserName = username,
                PhoneNumber = phonenumber,
                EMail = email,
                idno = idno,
                name = name,
                cardid = cardid,
                EmailConfirmed = EmailConfirmed
            };

            return View(model);
        }

        // POST: SystemUser/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(string id, [Bind("Id,UserName,Email,IdNo,Name,CardId,PhoneNumber,EmailConfirmed")] SystemUser systemuser, string search_data)
        {
            //var a = systemuser.Id;
            //var b = systemuser.UserName;
            //var c = systemuser.Email;
            //var d = systemuser.PhoneNumber;
            //var e = systemuser.IdNo;
            //var f = systemuser.Name;
            //var g= systemuser.CardId;
            //var h = systemuser.EmailConfirmed;         

            try
            {
                if (ModelState.IsValid)
                {
                    bool isExist = !String.IsNullOrEmpty(id);
                    var applicationUser = await _userManager.FindByIdAsync(id);
                    //{                     
                    //    CreatedDate = DateTime.UtcNow                     
                    //};  
                    applicationUser.Id = systemuser.Id;
                    applicationUser.UserName = systemuser.UserName;
                    applicationUser.PhoneNumber = systemuser.PhoneNumber;
                    applicationUser.Email = systemuser.Email;
                    applicationUser.idno = systemuser.IdNo;
                    applicationUser.name = systemuser.Name;
                    applicationUser.cardid = systemuser.CardId;
                    applicationUser.EmailConfirmed = systemuser.EmailConfirmed;
                    //applicationUser.cardid = systemuser.ConfirmPassword;

                    IdentityResult roleRusult = await _userManager.UpdateAsync(applicationUser);
                    if (roleRusult.Succeeded)
                    {
                        return RedirectToAction("UserList", new { search_data = search_data, message = "User Account Successfully UPDATED" });
                    }
                }
                return RedirectToAction("UserList", new { search_data = search_data, message = "User Account NOT Successfully UPDATED" });
            }
            catch
            {
                return View();
            }            
           
        }

        // GET: SystemUser/Delete/5
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id, [Bind("Id,UserName,Email,IdNo,Name,CardId,PhoneNumber,EmailConfirmed")] SystemUser systemuser, string search_data)
        {
            ViewData["ID"] = systemuser.Id;
            ViewData["UserName"] = systemuser.UserName;
            ViewData["EMail"] = systemuser.Email;
            ViewData["PhoneNumber"] = systemuser.PhoneNumber;
            ViewData["IdNo"] = systemuser.IdNo;
            ViewData["Name"] = systemuser.Name;
            ViewData["CardID"] = systemuser.CardId;
            ViewData["EmailConfirmed"] = systemuser.EmailConfirmed;

            ViewBag.SearchData = search_data;
            var model = new EditUserViewModel
            {
                Id = systemuser.Id,
                UserName = systemuser.UserName,
                PhoneNumber = systemuser.PhoneNumber,
                EMail = systemuser.Email,
                idno = systemuser.IdNo,
                name = systemuser.Name,
                cardid = systemuser.CardId,
                EmailConfirmed = systemuser.EmailConfirmed
            };

            return View(model);
        }

        // POST: SystemUser/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(string id, string search_data, IFormCollection collection)
        {
            ViewBag.SearchData = search_data;
            try
            {
                // TODO: Add delete logic here
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with ID {id} does not exists";
                    return RedirectToAction("UserList", "SystemUser", new { search_data = search_data });
                }
                else {
                    if (user.EmailConfirmed == false)
                    {
                        var result = await _userManager.DeleteAsync(user);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("UserList", "SystemUser", new { search_data = search_data, message = "User Account information Successfully DELETED!" });
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                    else {
                        return RedirectToAction("UserList", "SystemUser", new { search_data = search_data, message = "E-Mail has beed CONFIRMED. Can not be deleted" });
                    }
                }

                return RedirectToAction("UserList", "SystemUser", new { search_data = search_data });
            }
            catch
            {
                return RedirectToAction("UserList", "SystemUser", new { search_data = search_data });
            }
        }


        // GET: SystemUser/Create
        //[Authorize(Roles = "Admin")]
        public ActionResult ResetUserPassword(string id, string username, string search_data)
        {
            ViewBag.SearchData = search_data;
            ViewBag.Username = username.ToString();
            ViewBag.UserId = id.ToString();
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetUserPassword(ResetUserPasswordViewModel model, string search_data)
        {
            var a = model.UserId;
            var b = model.Username;
            var c = model.Password;
            var d = model.ConfirmPassword;

            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(model.Username);

                // UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());

                if (user.PasswordHash != null)
                {
                    if (user.PasswordHash != "")
                    {
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user, model.Password);
                    }
                }
                else
                {
                    await _userManager.AddPasswordAsync(user, model.Password);
                }

                TempData["Message"] = "Password successfully reset to " + model.ConfirmPassword;
                TempData["MessageValue"] = "1";

                return RedirectToAction("UserList", "SystemUser", new { area = "", });
            }

            // If we got this far, something failed, redisplay form
            TempData["Message"] = "Invalid User Details. Please try again in some minutes ";
            TempData["MessageValue"] = "0";
            return RedirectToAction("UserList", "SystemUser", new { message = "User Password RESET Successfully DONE!", search_data = search_data });
        }

        // GET: SystemUser/Create
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageUserClaims(string id, string userclaims, string search_data)
        {
            ViewBag.SearchData = search_data;
            ViewBag.UserClaims = userclaims;
            ViewBag.UserId = id.ToString();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with ID {id} can not be found!";
                return View("Not Found");
            }
            var existingUserClaims = await _userManager.GetClaimsAsync(user);
            var model = new UserClaimsViewModel
            {
                userID = id
            };
            foreach (Claim claim in UserClaimStore.AllClaims)
            {
                UserClaim userclaim = new UserClaim
                {
                    ClaimType = claim.Type
                };

                if (existingUserClaims.Any(c => c.Type == claim.Type))
                {
                    userclaim.IsSelected = true;
                }
                else
                {
                    userclaim.IsSelected = false;
                }
                model.Claims.Add(userclaim);
            }

            return View(model);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageUserClaims(string id, UserClaimsViewModel model, string search_data)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with ID {id} can not be found!";
                return View("Not Found");
            }

            var claims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, claims);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Can not remove exxisting claims");
                return View(model);
            }

            result = await _userManager.AddClaimsAsync(
                user,
                model.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType)));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Can not add exxisting claims");
                return View(model);
            }
            else
            {
                return RedirectToAction(nameof(ManageUserClaims), new { id = id, userclaims = claims, message = "User Role Successfully ADDED!" });
            }

            return View();
        }


        public async Task<JsonResult> getUserRoles(string roleid)
        {

            IList<string> userrole = new List<string>();
            ApplicationUser user = await _userManager.FindByIdAsync(roleid);

            var roles = await _userManager.GetRolesAsync(user);
            foreach (string role in roles)
            {
                userrole.Add(role); // = userrole + "," + role;
            }

            return Json(userrole);
        }
    }
}
