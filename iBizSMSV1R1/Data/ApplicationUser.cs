using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.Data
{
    public class ApplicationUser : IdentityUser
    {
        //public ApplicationUser()
        //{
        //    List<string> Claims = new List<string>();
        //    List<string> Roles = new List<string>();
        //}

        [StringLength(50)]
        public string idno { get; set; }

        [StringLength(50)]
        public string cardid { get; set; }

        [StringLength(150)]
        public string name { get; set; }       

    }
}
