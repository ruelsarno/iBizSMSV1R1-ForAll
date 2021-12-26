using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.Models
{
    public class WebPageCourse
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Title Here")]
        [StringLength(25)]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Required(ErrorMessage = "Tag Line Here")]
        [StringLength(512)]
        [Display(Name = "Tag Line")]
        public string tagline { get; set; }

        [Required(ErrorMessage = "Category Here")] //Pre-Elementary,Elementary,High School,Sr High School,Vocational
        [StringLength(50)]
        [Display(Name = "Category")]
        public string category { get; set; }

        [StringLength(50)]
        [Display(Name = "Course Title")]
        public string coursetitle { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [StringLength(50)]
        [Display(Name = "Icon")]
        public string icon { get; set; }

        [StringLength(150)]
        [Display(Name = "Controller")]
        public string controller { get; set; }

        [StringLength(150)]
        [Display(Name = "Controller Action")]
        public string action { get; set; }

        [Display(Name = "Course Image")]
        public byte[] image { get; set; }

        [StringLength(1024)]
        [Display(Name = "Link")]
        public string link { get; set; }
    }

    public class WebPageCourseDetail
    {
        [Key]
        [Display(Name = "Record No")]
        public int recordno { get; set; }

        [Display(Name = "Rec No")]
        public int recno { get; set; }


        [Display(Name = "About the Course")]
        public string aboutthecourse { get; set; }


        [Display(Name = "Requirements")]
        [DataType(DataType.DateTime)]
        public string requirement { get; set; }


        [Display(Name = "How to Enroll")]
        public string howtoenroll { get; set; }

        [Display(Name = "Fees")]
        public string fees { get; set; }

        [Display(Name = "Course Image")]
        public byte[] image { get; set; }

        [StringLength(1024)]
        [Display(Name = "ImageLink")]
        public string imagelink { get; set; }
    }
}
