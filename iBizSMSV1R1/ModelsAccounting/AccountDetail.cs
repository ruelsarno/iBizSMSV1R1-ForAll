using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBizSMSV1R1.ModelsAccounting
{
    public class AccountDetail
    {
        [Key]
        [Display(Name = "Rec No")]
        public int accountdetailrecno { get; set; }

        [Required(ErrorMessage = "Please enter ID No.")]
        [StringLength(25)]
        [Display(Name = "ID No")]
        public string idno { get; set; }

        [Required(ErrorMessage = "Please enter School Year")]
        [StringLength(25)]
        [Display(Name = "School Year")]
        public string schoolyear { get; set; }

        
        [StringLength(50)]
        [Display(Name = "Discount Code")]
        public string discountcode { get; set; }

       
        [StringLength(150)]
        [Display(Name = "Discount Name")]
        public string discountname { get; set; }

       
        [Display(Name = "Discount Percentage")]
        public float discount { get; set; }

        public AccountDetail()
        {
            discount = 0;
        }

        [Required(ErrorMessage = "Please enter Mode of Payment")]
        [StringLength(25)]
        [Display(Name = "Mode Of Payment")]
        public string modeofpayment { get; set; }

        [Required(ErrorMessage = "Please enter Fee Category")]
        [StringLength(50)]
        [Display(Name = "Fee Category Name")]
        public string feecategory { get; set; }

        [Required(ErrorMessage = "Please enter Fee Description")]
        [StringLength(50)]
        [Display(Name = "Fee Description")]
        public string feedescription { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.##}")]
        [Required(ErrorMessage = "Please enter Fee Amount")]
        [Display(Name = "Fee Amount")]
        public float feeamount { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.##}")]
        [Display(Name = "Amount of Discount")]
        public float amountofdiscount { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.##}")]
        [Display(Name = "Total Payment")]
        public float totalpayment { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.##}")]
        [Display(Name = "Rebate")]
        public float rebate { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.##}")]
        [Display(Name = "Balance")]
        public float balance { get; set; }

        [StringLength(25)]
        [Display(Name = "Status")]
        public string status { get; set; }

        [StringLength(25)]
        [Display(Name = "Remarks")]
        public string remarks { get; set; }
    }
}
