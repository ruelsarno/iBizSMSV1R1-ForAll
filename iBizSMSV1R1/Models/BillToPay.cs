using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.Models
{
    public class BillToPay
    {
        [Key]
        [Display(Name = "Rec No")]
        public long recno { get; set; }

        [Display(Name = "Reference No")]
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

        [Required(ErrorMessage = "Payment Type")]
        [Display(Name = "Payment Type")]
         [StringLength(50)]
        public string paymenttype { get; set; }

        [Required(ErrorMessage = "Payment Office")]
        [Display(Name = "Payment Office")]
        [StringLength(50)]
        public string paymentoffice { get; set; }

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

        [Display(Name = "Proof of Payment")]
        public byte[] proofofpayment { get; set; }

        [Display(Name = "Confirmed")]
        public bool confirm { get; set; }

        [StringLength(150)]
        [Display(Name = "Confirmed By")]
        public string confirmedby { get; set; }

        [Display(Name = "Notified")]
        public bool notified { get; set; }


        //[Display(Name = "Payment Update")]
        //public bool paymentupdate { get; set; }
        
    }

    public class Payment
    {
        [Key]
        [Display(Name = "Record No")]
        public int recordno { get; set; }

        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Identity Code is a must!")]
        [Display(Name = "Identity Code")]
        public string id { get; set; }

        [Required(ErrorMessage = "Reference No")]
        [Display(Name = "Reference No")]
        public long referenceno { get; set; } //This number will be used as reference to "Bills To Pay" Records

        [Required(ErrorMessage = "School Year")]
        [StringLength(25)]
        [Display(Name = "School Year")]
        public string schoolyear { get; set; }

        [Required(ErrorMessage = "Bill Name")]
        [StringLength(50)]
        [Display(Name = "Bill Name")]
        public string billnames { get; set; }

        [Required(ErrorMessage = "Date of Billing")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Billing")]
        public DateTime billdate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Payment")]
        public DateTime paymentdate { get; set; }

        [Display(Name = "Payment Type")]
        [StringLength(50)]
        public string paymenttypes { get; set; }

        [Display(Name = "Payment Office")]
        [StringLength(150)]
        public string paymentoffices { get; set; }

        [Display(Name = "Amount Paid (PHP)")]
        public double amountpaid { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Payment Posting Date")]
        public DateTime paymentpostdate { get; set; }
    }
}
