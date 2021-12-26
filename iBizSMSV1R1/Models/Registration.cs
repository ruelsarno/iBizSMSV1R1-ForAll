using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBizSMSV1R1.Models
{
    public class Registration
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "ID No")]
        [StringLength(25)]        
        [Display(Name = "ID No")]
        public string idno { get; set; }

        [Required(ErrorMessage = "School Year")]
        [StringLength(25)]
        [Display(Name = "School Year")]
        public string schoolyear { get; set; }

        [Required(ErrorMessage = "Registration No")]
        [StringLength(25)]
        [Display(Name = "Registration No")]
        public string registrationno { get; set; }

        [Required(ErrorMessage = "Registration Date")]
        [DataType(DataType.Date)]
        [Display(Name = "Registration Date")]
        public string registrationdate { get; set; }

        [Required(ErrorMessage = "Student Type")]
        [StringLength(25)]
        [Display(Name = "Student Type")]
        public string studenttype { get; set; }

        [Required(ErrorMessage = "Student Level")]
        [StringLength(25)]
        [Display(Name = "Student Level")]
        public string studentlevel { get; set; }


        [StringLength(25)]
        [Display(Name = "Track Code")]
        public string trackcode { get; set; }


        [StringLength(25)]
        [Display(Name = "Subject")]
        public string subject { get; set; }

        [Required(ErrorMessage = "Grade Year")]
        [StringLength(25)]
        [Display(Name = "Grade Year")]
        public string gradeyear { get; set; }

        [Required(ErrorMessage = "Section")]
        [StringLength(50)]
        [Display(Name = "Section")]
        public string section { get; set; }

        [Required(ErrorMessage = "Discount Code")]
        [StringLength(25)]
        [Display(Name = "Discount Code")]
        public string discountcode { get; set; }

        [Required(ErrorMessage = "Mode of Payment")]
        [StringLength(25)]
        [Display(Name = "Mode Of Payment")]
        public string modeofpayment { get; set; }      

        [StringLength(150)]
        [Display(Name = "School Last Attended")]
        public string schoollastattended { get; set; }

        [StringLength(25)]
        [Display(Name = "Public or Private")]
        public string schooltype { get; set; }
    }
}
