using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.Models
{
    public class ProcessIntro
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Title Here")]
        [StringLength(25)]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Required(ErrorMessage = "Sub-Title Here")]
        [StringLength(150)]
        [Display(Name = "Sub-Title")]
        public string subtitle { get; set; }

        [Required(ErrorMessage = "Description")]
        [Display(Name = "Description")]
        public string description { get; set; }
    }
}
