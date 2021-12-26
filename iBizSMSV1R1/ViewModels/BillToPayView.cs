using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.ViewModels
{
    public class BillToPayView
    {
        [Key]
        [Display(Name = "Ref. No")]
        public long recno { get; set; }

        [Display(Name = "Rec No")]
        public long referenceno { get; set; } //This number is from Reservation's reference

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

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Billing")]
        public string billdate { get; set; }

        [Display(Name = "Amount To Pay (PHP)")]
        public double amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public string duedate { get; set; }

        [Display(Name = "Payment")]
        public bool payment { get; set; }        

        [Display(Name = "Confirmed")]
        public bool confirm { get; set; }

        [StringLength(150)]
        [Display(Name = "Confirmed By")]
        public string confirmedby { get; set; }
        [Display(Name = "Notified")]
        public bool notified { get; set; }
    }
}
