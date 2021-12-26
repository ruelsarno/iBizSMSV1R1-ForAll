using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.Models
{
    public class WebPageContact
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Full Name")]
        [StringLength(150)]
        [Display(Name = "Full Name")]
        public string fullname { get; set; }

        [Required(ErrorMessage = "Address")]
        [StringLength(512)]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "Cellphone No")]
        [StringLength(25)]
        [Display(Name = "Cellphone No")]
        public string cellphoneno { get; set; }

        [Required(ErrorMessage = "Landline No")]
        [StringLength(25)]
        [Display(Name = "Landline No")]
        public string landlineno { get; set; }

        [Required(ErrorMessage = "Email Address")]
        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "Email Address")]
        public string emailadd { get; set; }

        [Required(ErrorMessage = "Website")]
        [Url]
        [StringLength(150)]
        [Display(Name = "Website")]
        public string website { get; set; }

        [Required(ErrorMessage = "Longitude")]
        [StringLength(150)]
        [Display(Name = "Longitude")]
        public string longitude { get; set; }

        [Required(ErrorMessage = "Latitude")]
        [StringLength(150)]
        [Display(Name = "Latitude")]
        public string latitude { get; set; }
    }
}
