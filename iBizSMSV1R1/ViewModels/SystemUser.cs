using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.ViewModels
{
    public class SystemUser
    {
        [Key]
        [StringLength(450)]
        [Display(Name = "ID")]
        public string Id { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Card ID")]
        public string CardId { get; set; }

        [StringLength(50)]
        [Display(Name = "ID No")]
        public string IdNo { get; set; }

        [StringLength(150)]
        [Display(Name = "Name")]
        public string Name { get; set; }        

        [Required(ErrorMessage = "Please User Name")]
        [StringLength(256)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please e-mail Address")]
        [StringLength(256)]
        [Display(Name = "e-Mail")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Please Phone Number")]
        [StringLength(256)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Account Verify")]
        public bool EmailConfirmed { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //public string ConfirmPassword { get; set; }
    }
}
