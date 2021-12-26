using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iBizSMSV1R1.ViewModels
{
    public class ApplicationRole
    {        
        public string Id { get; set; }

        [Display(Name = "Role Name")]
        public string Name { get; set; }

        public int NumberOfUsers { get; set; }
    }
    public class RoleOfUser
    {
        public string UserName { get; set; }

        public string UserId { get; set; }

        public string RoleId { get; set; }

        public List<SelectListItem> ApplicationRoles { get; set; }
    }

    public class RoleOfUserView
    {

        public List<RoleOfUser> ApplicationRoles { get; set; }
    }
}
