using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.Models
{
    public class WebPageSuccessStory
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Title Here")]
        [StringLength(25)]
        [Display(Name = "Title")]
        public string title { get; set; }
        
        [StringLength(50)]
        [Display(Name = "SubTitle")]
        public string subtitle { get; set; }

        [StringLength(150)]
        [Display(Name = "Date/Time")]
        [DataType(DataType.Date)]
        public string storydate { get; set; }

        [StringLength(150)]
        [Display(Name = "Name")]
        public string author { get; set; }

        [Required(ErrorMessage = "Paragraph 1 Here")]
        [Display(Name = "Paragraph 1")]
        public string paragraph1 { get; set; }        
       

        [Display(Name = "Image")]
        public byte[] image { get; set; }

        [StringLength(150)]
        [Display(Name = "Icon")]
        public string icon { get; set; }

        [StringLength(150)]
        [Display(Name = "Link")]
        public string link { get; set; }


        [StringLength(150)]
        [Display(Name = "Controller")]
        public string controller { get; set; }


        [StringLength(150)]
        [Display(Name = "Controller Action")]
        public string action { get; set; }
    }
}
