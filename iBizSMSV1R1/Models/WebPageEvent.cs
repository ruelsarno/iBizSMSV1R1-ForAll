using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.Models
{
    public class WebPageEvent
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

        [Required(ErrorMessage = "Event Name")]
        [StringLength(50)]
        [Display(Name = "Event Title")]
        public string eventtitle { get; set; }

        [StringLength(50)]
        [Display(Name = "Icon")]
        public string icon { get; set; }        

        [Display(Name = "Description")]
        public string description { get; set; }

        [StringLength(150)]
        [Display(Name = "Controller")]
        public string controller { get; set; }

        [StringLength(150)]
        [Display(Name = "Controller Action")]
        public string action { get; set; }

        [Display(Name = "Event Image")]
        public byte[] image { get; set; }

        [StringLength(1024)]
        [Display(Name = "Link")]
        public string link { get; set; }

        [Display(Name = "Active")]
        public bool active { get; set; }

        public virtual ICollection<WebPageEventDetail> WebPageEventDetail { get; set; }
    }
    public class WebPageEventDetail
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recordno { get; set; }
       
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [StringLength(512)]
        [Display(Name = "Speaker Name")]
        public string speakername { get; set; }

        [StringLength(150)]
        [Display(Name = "Event Date")]
        [DataType(DataType.DateTime)]
        public string eventdate { get; set; }

        [StringLength(150)]
        [Display(Name = "Venue")]
        public string venue { get; set; }
       
        [Display(Name = "Event Details")]
        public string eventdetails { get; set; }

        [Display(Name = "Event Image")]
        public byte[] imagedetails { get; set; }

        [StringLength(1024)]
        [Display(Name = "ImageLink")]
        public string imagelink { get; set; }
    }
    public class WebPageEventDetails
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

        [Required(ErrorMessage = "Event Name")]
        [StringLength(50)]
        [Display(Name = "Event Title")]
        public string eventtitle { get; set; }

        [StringLength(50)]
        [Display(Name = "Icon")]
        public string icon { get; set; }


        [Display(Name = "Description")]
        public string description { get; set; }

        [StringLength(150)]
        [Display(Name = "Controller")]
        public string controller { get; set; }

        [StringLength(150)]
        [Display(Name = "Controller Action")]
        public string action { get; set; }

        [Display(Name = "Image")]
        public byte[] imagedetails { get; set; }

        [Display(Name = "Image")]
        public byte[] image { get; set; }

        [StringLength(1024)]
        [Display(Name = "Link")]
        public string link { get; set; }
       
        [Display(Name = "Record No")]
        public int recordno { get; set; }    

        [StringLength(512)]
        [Display(Name = "Speaker Name")]
        public string speakername { get; set; }

        [StringLength(150)]
        [Display(Name = "Event Date")]
        [DataType(DataType.DateTime)]
        public string eventdate { get; set; }

        [StringLength(150)]
        [Display(Name = "Venue")]
        public string venue { get; set; }

        [Display(Name = "Event Details")]
        public string eventdetails { get; set; }

        [StringLength(1024)]
        [Display(Name = "ImageLink")]
        public string imagelink { get; set; }
    }
}
