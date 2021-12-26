using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.ViewModels
{
    public class ReNewEMailAddress
    {
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string NewEmail { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string Code { get; set; }
    }
}
