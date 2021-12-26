using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.Models
{
    public class WebPages
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Title Here")]
        [StringLength(25)]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Required(ErrorMessage = "SubTitle Here")]
        [StringLength(50)]
        [Display(Name = "SubTitle")]
        public string subtitle { get; set; }

        //[Required(ErrorMessage = "Tag Line Here")]
        [StringLength(512)]
        [Display(Name = "Tag Line")]
        public string tagline { get; set; }

        //[Required(ErrorMessage = "Description Here")]       
        [Display(Name = "Description")]
        public string description { get; set; }
        
        [Display(Name = "Image")]
        public byte[] image { get; set; }
        
        [StringLength(150)]
        [Display(Name = "Icon")]
        public string icon { get; set; }
        
        [StringLength(1024)]
        [Display(Name = "Link")]
        public string link { get; set; }

        
        [StringLength(150)]
        [Display(Name = "Controller")]
        public string controller { get; set; }

       
        [StringLength(150)]
        [Display(Name = "Controller Action")]
        public string action { get; set; }


        [Display(Name = "Active")]
        public bool active { get; set; }

    }
}
