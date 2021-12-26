using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBizSMSV1R1.Models
{
    public class ClassSchedule
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }
        
        [Required(ErrorMessage = "School Year")]
        [StringLength(25)]
        [Display(Name = "School Year")]
        public string schoolyear { get; set; }

        [Required(ErrorMessage = "Grade Year")]
        [StringLength(50)]
        [Display(Name = "GradeYear")]
        public string studentgradeyear { get; set; }

        [Required(ErrorMessage = "Student Section")]
        [StringLength(50)]
        [Display(Name = "Section")]
        public string section { get; set; }

        [StringLength(150)]
        [Display(Name = "Subject Code")]
        public string subjectcode { get; set; }

        [StringLength(512)]
        [Display(Name = "Subject Name")]
        public string subjectname { get; set; }

        //[Required(ErrorMessage = "Teacher Name")]
        [StringLength(150)]
        [Display(Name = "Teacher")]
        public string teacher { get; set; }

        //[Required(ErrorMessage = "Day of the week")]
        [StringLength(50)]
        [Display(Name = "Day of the week")]
        public string weekday { get; set; }

        //[Required(ErrorMessage = "Room Number")]
        [StringLength(25)]
        [Display(Name = "Room Number")]
        public string roomno { get; set; }

        //[Required(ErrorMessage = "Start Time ")]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public string starttime { get; set; }

        //[Required(ErrorMessage = "End Time ")]
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public string endtime { get; set; }
        
    }
}
