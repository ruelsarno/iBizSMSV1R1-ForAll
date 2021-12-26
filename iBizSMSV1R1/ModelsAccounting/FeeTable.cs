using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.ModelsAccounting
{
    public class FeeTable
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Please enter School Year")]
        [StringLength(25)]
        [Display(Name = "School Year")]
        public string schoolyear { get; set; }

        //[Required(ErrorMessage = "Please enter  Student Level")]
        //[StringLength(25)]
        //[Display(Name = "Student Level")]
        //public string studentlevel { get; set; }

        [Required(ErrorMessage = "Please enter  Student Level")]
        [StringLength(25)]
        [Display(Name = "Grade Year")]
        public string gradeyear { get; set; }

        [Required(ErrorMessage = "Please enter Grade Year")]
        [StringLength(25)]
        [Display(Name = "Payment Mode")]
        public string paymentmode { get; set; }

        [Display(Name = "Tuition Fee")]
        public float tuitionfee { get; set; }

        [Display(Name = "Reservation Fee")]
        public float reservationfee { get; set; }

        [Display(Name = "Upon Enrollment")]
        public float uponenrollment { get; set; }

        [Display(Name = "Count Of Installment")]
        public int installmentcount { get; set; }


        [StringLength(255)]
        [Display(Name = "Payment Schedule")]
        public string paymentschedule { get; set; }

        [Display(Name = "Installment Amount")]
        public float installmentamount { get; set; }
    }
}
