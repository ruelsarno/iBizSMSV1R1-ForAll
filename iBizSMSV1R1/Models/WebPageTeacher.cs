using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace iBizSMSV1R1.Models
{
    public class WebPageTeacher
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

        [Required(ErrorMessage = "Specialization Here")] //English, Math, Science, Physics
        [StringLength(50)]
        [Display(Name = "Specialization")]
        public string specialization { get; set; }

        [StringLength(50)]
        [Display(Name = "Full Name")]
        public string fullname { get; set; }

        [StringLength(150)]
        [Display(Name = "Controller")]
        public string controller { get; set; }

        [StringLength(150)]
        [Display(Name = "Controller Action")]
        public string action { get; set; }

        [Display(Name = "Teacher Picture")]
        public byte[] image { get; set; }

        [StringLength(1024)]
        [Display(Name = "Link")]
        public string link { get; set; }

        public virtual ICollection<WebPageTeacherDetail> WebPageTeacherDetail { get; set; }

    }

    public class WebPageTeacherDetail
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recordno { get; set; }

        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [StringLength(1024)]
        [Display(Name = "Description")]
        public string jobdescription { get; set; }
                
        [Display(Name = "Biography")]
        public string biography { get; set; }

        [StringLength(512)]
        [Display(Name = "Hobbies/Interests")]
        public string hobbiesinterest { get; set; }

        [StringLength(512)]
        [Display(Name = "Contact Info")]
        public string contactinfo { get; set; }

    }

    public class WebPageTeacherDetails
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

        [Required(ErrorMessage = "Specialization Here")] //English, Math, Science, Physics
        [StringLength(50)]
        [Display(Name = "Specialization")]
        public string specialization { get; set; }

        [StringLength(50)]
        [Display(Name = "Full Name")]
        public string fullname { get; set; }

        [StringLength(150)]
        [Display(Name = "Controller")]
        public string controller { get; set; }

        [StringLength(150)]
        [Display(Name = "Controller Action")]
        public string action { get; set; }

        [Display(Name = "Teacher Picture")]
        public byte[] image { get; set; }

        [StringLength(1024)]
        [Display(Name = "Link")]
        public string link { get; set; }
     

        [StringLength(1024)]
        [Display(Name = "Description")]
        public string jobdescription { get; set; }

        [Display(Name = "Biography")]
        public string biography { get; set; }

        [StringLength(512)]
        [Display(Name = "Hobbies/Interests")]
        public string hobbiesinterest { get; set; }

        [StringLength(512)]
        [Display(Name = "Contact Info")]
        public string contactinfo { get; set; }

    }
}
