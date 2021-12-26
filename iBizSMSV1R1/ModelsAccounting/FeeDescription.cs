using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.ModelsAccounting
{
    public class FeeDescription
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Please enter Fee Category")]
        [StringLength(50)]
        [Display(Name = "Fee Category Name")]
        public string feecategory { get; set; }

        [Required(ErrorMessage = "Please enter Fee Description")]
        [StringLength(100)]
        [Display(Name = "Fee Description")]
        public string feename { get; set; }
    }
}
