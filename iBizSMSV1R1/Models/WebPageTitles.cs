using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.Models
{
    public class WebPageTitles
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Title Here")]
        [StringLength(150)]
        [Display(Name = "Title")]
        public string title { get; set; }
        
        [StringLength(512)]
        [Display(Name = "Tag Line")]
        public string tagline { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        //[Required(ErrorMessage = "Controller Here")]
        [StringLength(150)]
        [Display(Name = "Controller")]
        public string controller { get; set; }
       
        //[Required(ErrorMessage = "Controller Action Here")]
        [StringLength(150)]
        [Display(Name = "Controller Action")]
        public string action { get; set; }


        [Display(Name = "Background")]
        public byte[] image { get; set; }

        [StringLength(1024)]
        [Display(Name = "Link")]
        public string link { get; set; }
    }
}
