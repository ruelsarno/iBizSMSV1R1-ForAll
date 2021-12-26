using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.Models
{
    public class ToPay
    {
        [Key]
        [Display(Name = "Rec No")]
        public long referenceno { get; set; }

        [Required(ErrorMessage = "Identity Code is a must!")]
        [Display(Name = "Identity Code")]
        public string id { get; set; }

        [Required(ErrorMessage = "School Year")]
        [StringLength(25)]
        [Display(Name = "School Year")]
        public string schoolyear { get; set; }

        [Required(ErrorMessage = "Bill Name")]
        [Display(Name = "Bill Name")]
        [StringLength(50)]
        public string billnames { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Billing")]
        public DateTime billdate { get; set; }

        [Display(Name = "Amount To Pay (PHP)")]
        public double amount { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime duedate { get; set; }

        [Display(Name = "Other Instructions")]
        [StringLength(50)]
        public string remarks { get; set; }
    }
}
