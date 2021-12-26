using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.Models
{
    public class WebPageBlog
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
        [Display(Name = "Blog Title")]
        public string blogtitle { get; set; }

        [StringLength(150)]
        [Display(Name = "Author Name")]
        public string author { get; set; }

        [StringLength(150)]
        [Display(Name = "Post Date")]
        [DataType(DataType.Date)]
        public string postdate { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [StringLength(150)]
        [Display(Name = "Controller")]
        public string controller { get; set; }

        [StringLength(150)]
        [Display(Name = "Controller Action")]
        public string action { get; set; }
                
        [Display(Name = "Blog Image")]
        public byte[] image { get; set; }

        [StringLength(1024)]
        [Display(Name = "Link")]
        public string link { get; set; }
    }
}
