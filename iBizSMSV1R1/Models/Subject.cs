using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iBizSMSV1R1.Models
{
    public class SubjectCategory
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Enter Subject Category")]
        [StringLength(150)]
        [Display(Name = "Subject Category")]
        public string category { get; set; }
    }
    public class Subject
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Enter Student Level")]
        [StringLength(50)]
        [Display(Name = "Student Level")]
        public string studentlevel { get; set; }

        [Required(ErrorMessage = "Enter Grade Year")]
        [StringLength(50)]
        [Display(Name = "Grade Year")]
        public string gradeyear { get; set; }

        //[Required(ErrorMessage = "Enter Subject Group")]
        //[StringLength(50)]
        //[Display(Name = "Subject ID")]
        //public string subjectid { get; set; }

        [Required(ErrorMessage = "Enter Category")]
        [StringLength(100)]
        [Display(Name = "Category")]
        public string category { get; set; }


        [Required(ErrorMessage = "Enter Subject Code")]
        [StringLength(50)]
        [Display(Name = "Subject Code")]
        public string subjectcode { get; set; }

        [Required(ErrorMessage = "Enter Subject Name")]
        [StringLength(512)]
        [Display(Name = "Subject/Descriptive Title")]
        public string subjectname { get; set; }
       
        [StringLength(25)]
        [Display(Name = "Semester")]
        public string semester { get; set; }

        [Display(Name = "No. Of Hours")]
        public int noofhours { get; set; }

        ////[Required(ErrorMessage = "Enter WWP")]
        //[Display(Name = "WWP")]
        //public float wwp { get; set; }

        ////[Required(ErrorMessage = "Enter PTP")]
        //[Display(Name = "PTP")]
        //public float ptp { get; set; }

        ////[Required(ErrorMessage = "Enter MEP")]
        //[Display(Name = "MEP")]
        //public float mep { get; set; }

        ////[Required(ErrorMessage = "Enter CGrade")]
        //[Display(Name = "CGrade")]
        //public float cgrade { get; set; }

        ////[Required(ErrorMessage = "Enter ABM")]
        //[Display(Name = "ABM")]
        //public bool abm { get; set; }

        ////[Required(ErrorMessage = "Enter GAS")]
        //[Display(Name = "GAS")]
        //public bool gas { get; set; }

        ////[Required(ErrorMessage = "Enter HUMS")]
        //[Display(Name = "HUMS")]
        //public bool hums { get; set; }

        ////[Required(ErrorMessage = "Enter STEM")]
        //[Display(Name = "STEM")]
        //public bool stem { get; set; }

        ////[Required(ErrorMessage = "Enter TVL")]
        //[Display(Name = "TVL")]
        //public bool tvl { get; set; }

        ////[Required(ErrorMessage = "Enter CORE")]
        //[Display(Name = "CORE")]
        //public bool core { get; set; }

        ////[Required(ErrorMessage = "Enter APPLIED")]
        //[Display(Name = "APPLIED")]
        //public bool applied { get; set; }

        ////[Required(ErrorMessage = "Enter Specialized")]
        //[Display(Name = "Specialized")]
        //public bool specialized { get; set; }

        ////[Required(ErrorMessage = "Enter Level")]
        //[StringLength(10)]
        //[Display(Name = "Level")]
        //public string level { get; set; }

        ////[Required(ErrorMessage = "Enter Semester")]
        //[StringLength(25)]
        //[Display(Name = "Semester")]
        //public string semester { get; set; }

        ////[Required(ErrorMessage = "Enter Unit")]
        //[Display(Name = "Unit")]
        //public float unit { get; set; }

        ////[Required(ErrorMessage = "Enter ADV")]
        //[Display(Name = "ADV")]
        //public bool adv { get; set; }

        ////[Required(ErrorMessage = "Enter MAPE")]
        //[Display(Name = "MAPE")]
        //public bool mape { get; set; }

        ////[Required(ErrorMessage = "Enter ELECT")]
        //[Display(Name = "ELECT")]
        //public bool elect { get; set; }

        ////[Required(ErrorMessage = "Enter MAKABAYAN")]
        //[Display(Name = "MAKABAYAN")]
        //public bool makabayan { get; set; }

        public ICollection<ClassVideo> ClassVideo { get; set; }
        public ICollection<ClassVideo> ClassModule { get; set; }
    }

    public class ClassVideo
    {
        [Key]
        [Display(Name = "Record No")]
        public int recordno { get; set; }
      
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Subject Name")]
        [StringLength(512)]
        [Display(Name = "Subject Name")]
        public string subjectname { get; set; }

        [Required(ErrorMessage = "Lesson Number")]
        [Display(Name = "Lesson Number")]
        public int lessonno { get; set; }

        [Required(ErrorMessage = "Lesson Title")]
        [StringLength(150)]
        [Display(Name = "Lesson Title")]
        public string lessontitle { get; set; }

        [Required(ErrorMessage = "Video Link")]
        [StringLength(150)]
        [Display(Name = "Video Link")]
        public string videolink { get; set; }

        [Display(Name = "Active")]
        public bool active { get; set; }
    }

    public class ClassModule
    {
        [Key]
        [Display(Name = "Record No")]
        public int recordno { get; set; }

        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Subject Name")]
        [StringLength(512)]
        [Display(Name = "Subject Name")]
        public string subjectname { get; set; }

        [Required(ErrorMessage = "Lesson Number")]
        [Display(Name = "Lesson Number")]
        public int lessonno { get; set; }


        [Required(ErrorMessage = "Lesson Title")]
        [StringLength(150)]
        [Display(Name = "Lesson Title")]
        public string lessontitle { get; set; }

             
        [StringLength(150)]
        [Display(Name = "Module Link")]
        public string videolink { get; set; }


        [Display(Name = "Module Image")]
        public byte[] image { get; set; }      


        [Display(Name = "Active")]
        public bool active { get; set; }

    }

    public class ClassLiveStream
    {
        [Key]
        [Display(Name = "Record No")]
        public int recordno { get; set; }

        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Subject Name")]
        [StringLength(512)]
        [Display(Name = "Subject Name")]
        public string subjectname { get; set; }

        [Required(ErrorMessage = "Lesson Number")]
        [Display(Name = "Lesson Number")]
        public int lessonno { get; set; }

        [Required(ErrorMessage = "Lesson Title")]
        [StringLength(150)]
        [Display(Name = "Lesson Title")]
        public string lessontitle { get; set; }

        [Required(ErrorMessage = "Live Stream Link")]
        [StringLength(150)]
        [Display(Name = "Live Stream Link")]
        public string videolink { get; set; }

        [Display(Name = "Active")]
        public bool active { get; set; }

    }
}
