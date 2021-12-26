using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.ModelsAccounting
{
    public class BankAccount
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Bank Name")]
        [StringLength(150)]
        [Display(Name = "Bank Name")]
        public string bankname { get; set; }

        [Required(ErrorMessage = "Bank Branch")]
        [StringLength(150)]
        [Display(Name = "Bank Branch")]
        public string branch { get; set; }

        [Required(ErrorMessage = "Bank Account Name/Type")]
        [StringLength(150)]
        [Display(Name = "Account Type")]
        public string accounttype { get; set; }

        [Required(ErrorMessage = "Account No")]
        [StringLength(150)]
        [Display(Name = "Account No")]
        public string accountno { get; set; }


    }
}
