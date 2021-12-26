using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBizSMSV1R1.ModelsClassroom
{
    public class ClassAnnouncement
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "School Year")]
        [StringLength(25)]
        [Display(Name = "School Year")]
        public string schoolyear { get; set; }

        [Required(ErrorMessage = "Enter Subject Code")]
        [StringLength(50)]
        [Display(Name = "Subject Code")]
        public string subjectcode { get; set; }

        [Required(ErrorMessage = "Enter Subject Name")]
        [StringLength(512)]
        [Display(Name = "Subject Title")]
        public string subjectname { get; set; }

        [Required(ErrorMessage = "Proctor")]
        [StringLength(150)]
        [Display(Name = "Proctor")]
        public string proctor { get; set; }

        [Required(ErrorMessage = "Announcement Title")]
        [StringLength(25)]
        [Display(Name = "Announcement Title")]
        public string announcementtitle { get; set; }

        [Required(ErrorMessage = "Enter Date Start")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Start")]
        public DateTime datestart { get; set; }

        [Required(ErrorMessage = "Enter Date end")]
        [DataType(DataType.Date)]
        [Display(Name = "Date End")]
        public DateTime dateend { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(100)]
        [Display(Name = "Description")]
        public string description { get; set; }

        [StringLength(512)]
        [Display(Name = "Link")]
        public string link { get; set; }

        public byte[] imagedata { get; set; }

        //public virtual ICollection<ClassAnnouncementImage> ClassAnnouncementImage { get; set; }
        
    }

    public class ClassAnnouncementImage
    {
        [Key]
        [Display(Name = "Record No")]
        public int recordno { get; set; }

        [Display(Name = "Rec No")]
        public int recno { get; set; }        

        [StringLength(255)]
        [Display(Name = "Image Name")]
        public string imagename { get; set; }

        [StringLength(512)]
        [Display(Name = "Link")]
        public string link { get; set; }

        public byte[] imagedata { get; set; }      

    }
}
