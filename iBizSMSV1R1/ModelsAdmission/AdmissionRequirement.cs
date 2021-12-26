using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBizSMSV1R1.ModelsAdmission
{
    public class AdmissionRequirement
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "School Year")]
        [StringLength(25)]
        [Display(Name = "School Year")]
        public string schoolyear { get; set; }

        [Required(ErrorMessage = "Enter Title")]
        [StringLength(255)]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Required(ErrorMessage = "Enter Requirement")]
        [Display(Name = "Requirement")]
        public string requirement { get; set; }
    }
}
