using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBizSMSV1R1.Models
{
    public class Reservation
    {
        [Key]
        [Display(Name = "Rec No")]
        public long referenceno { get; set; } //This number will be used as reference to "Bills To Pay" Records

        [Required(ErrorMessage = "Identity Code is a must!")]
        [Display(Name = "Identity Code")]
        public string id { get; set; }

        [Required(ErrorMessage = "Student Type")]
        [StringLength(25)]
        [Display(Name = "Student Type")]
        public string studenttype { get; set; }     

        [DataType(DataType.Date)]
        [Display(Name = "Date Of Reservation")]
        public string datereserve { get; set; }

        //Applicable for old students only
        [StringLength(25)]       
        [Display(Name = "ID No")]
        public string idno { get; set; }

        [Required(ErrorMessage = "School Year")]
        [StringLength(25)]
        [Display(Name = "School Year")]
        public string schoolyear { get; set; }     

        [Required(ErrorMessage = "Student Level")]
        [StringLength(25)]
        [Display(Name = "Student Level")]
        public string studentlevel { get; set; }

        [Required(ErrorMessage = "Grade Year")]
        [StringLength(25)]
        [Display(Name = "Grade Year")]
        public string gradeyear { get; set; }

        [StringLength(25)]
        [Display(Name = "Track Code")]
        public string trackcode { get; set; }      

        [StringLength(150)]
        [Display(Name = "School Last Attended")]
        public string schoollastattended { get; set; }

        [StringLength(25)]
        [Display(Name = "Public or Private")]
        public string schooltype { get; set; }

        [StringLength(25)]
        [Display(Name = "Payment Status")]
        public string paymentstatus { get; set; }
       
        [Display(Name = "Confirmed")]
        public bool confirm { get; set; }

        [StringLength(150)]
        [Display(Name = "Confirmed By")]
        public string confirmedby { get; set; }
        [Display(Name = "Notified")]
        public bool notified { get; set; }
    }
    
}
