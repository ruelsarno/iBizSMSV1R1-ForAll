using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBizSMSV1R1.Models
{
    //public enum Gender
    //{
    //    Male = 1,
    //    Female = 2
    //}

    public class Gender
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Display(Name = "Gender")]
        [StringLength(25)]
        public string genders { get; set; }
    }

    //public enum SchoolType
    //{
    //    Private = 1,
    //    Public = 2
    //}

    public class SchoolType
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Display(Name = "School Type")]
        [StringLength(25)]
        public string schooltypes { get; set; }
    }

    //public enum paymentstatus
    //{
    //    Paid = 1,
    //    Unpaid = 2,
    //    Pending = 3,
    //    Cancelled =4
    //}

   

    //public enum CivilStatus
    //{
    //    Single = 1,
    //    Married = 2,
    //    Separated = 3,
    //    Widow = 4
    //}

    public class CivilStatus
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Display(Name = "Civil Status")]
        [StringLength(25)]
        public string civilstatus { get; set; }
    }

    public class Nationality
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Display(Name = "Nationality")]
        [StringLength(100)]
        public string nationality { get; set; }
    }


    public class SchoolYear
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Display(Name = "School Year")]
        [StringLength(25)]
        public string schoolyears { get; set; }

        [Display(Name = "Active")]
        public bool active { get; set; }
    }

    public class StudentType
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Display(Name = "Student Type")]
        [StringLength(25)]
        public string studenttypes { get; set; }
    }

    public class Studentlevel
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Display(Name = "Student Level")]
        [StringLength(25)]
        public string studentlevels { get; set; }
    }

    public class GradeYear
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Display(Name = "Student Level")]
        [StringLength(25)]
        public string studentlevels { get; set; }

        [Display(Name = "Grade Year")]
        [StringLength(25)]
        public string gradeyears { get; set; }
    }

    public class Section
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }       

        [Display(Name = "Grade Year")]
        [StringLength(25)]
        public string gradeyears { get; set; }

        [Display(Name = "Section")]
        [StringLength(150)]
        public string sections { get; set; }
    }

    public class TrackCode
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Display(Name = "Student Level")]
        [StringLength(25)]
        public string studentlevels { get; set; }

        [Display(Name = "Track Code")]
        [StringLength(25)]
        public string trackcodes { get; set; }

        [Display(Name = "Description")]
        [StringLength(512)]
        public string descriptions { get; set; }
    }

    public class Discount
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Display(Name = "Discount Code")]
        [StringLength(25)]
        public string discountcodes { get; set; }

        [Display(Name = "Discount")]   
        public double discounts { get; set; }

        [Display(Name = "Description")]
        [StringLength(255)]
        public string description { get; set; }
    }

    public class ModeOfPayment
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Display(Name = "Mode Of Payment")]
        [StringLength(50)]
        public string modeofpayments { get; set; }
    }

    public class PaymentOffice
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }


        [Display(Name = "Payment Office")]
        [StringLength(150)]
        public string paymentoffices { get; set; }
    }

    public class PaymentType //Money Transfer,PayPal,PayMaya, Thru Bank
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Display(Name = "Payment Type")]
        [StringLength(50)]
        public string paymenttypes { get; set; }
    }

    public class BillName //Reservations, Tuition
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Display(Name = "Bill Name")]
        [StringLength(50)]
        public string billnames { get; set; }
    }

    public class StatusOfPayment
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Display(Name = "Payment Status")]
        [StringLength(25)]
        public string paymentstatus { get; set; }
    }
}
