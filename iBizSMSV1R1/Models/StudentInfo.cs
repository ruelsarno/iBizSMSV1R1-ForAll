using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iBizSMSV1R1.Models
{
    public class StudentInfo
    {
        
        [RegularExpression(@"[^\s]+")]
        [StringLength(25)]
        [Display(Name = "ID No")]
        public string idno { get; set; }
       
        [StringLength(25)]
        [Display(Name = "LRN No")]
        public string lrn { get; set; }

        [Key]
        [Required(ErrorMessage = "Identity Code is a must!")]
        [Display(Name = "Identity Code")]
        public string id { get; set; }

        [Required(ErrorMessage = "Enter surname")]
        [StringLength(50)]
        [Display(Name = "Surname")]
        public string surname { get; set; }

        [Required(ErrorMessage = "Enter firstname")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string firstname { get; set; }

        [StringLength(50)]
        [Display(Name = "Middle Name")]
        public string middlename { get; set; }

        [StringLength(50)]
        [Display(Name = "Extension")]
        public string extension { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public string birthday { get; set; }

        [StringLength(50)]
        [Display(Name = "Birth Place")]
        public string birthplace { get; set; }

        [StringLength(50)]
        [Display(Name = "Nationality")]
        public string nationality { get; set; }

        [StringLength(50)]
        [Display(Name = "Civil Status")]
        public string civilstatus { get; set; }

        [StringLength(6)]
        [Display(Name = "Gender")]
        public string gender { get; set; }

        public bool active { get; set; }
      
        public ICollection<StudentImage> StudentImage { get; set; }       
    }

    public class StudentImage
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Identity Code is a must!")]
        [Display(Name = "Identity Code")]
        public string id { get; set; }

        [Required(ErrorMessage = "Image Link")]
        [StringLength(150)]
        [Display(Name = "Image Link")]
        public string link { get; set; }


        [Display(Name = "Student Picture")]
        public byte[] image { get; set; }

    }
    public class StudentDocument
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Identity Code is a must!")]
        [Display(Name = "Identity Code")]
        public string id { get; set; }

        [Required(ErrorMessage = "Please enter document name")]
        [StringLength(150)]
        [Display(Name = "Document Name")]
        public string documentname { get; set; }        

        [Display(Name = "Document")]
        public byte[] imagedata { get; set; }        

    }
    public class StudentRelative
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Identity Code is a must!")]
        [Display(Name = "Identity Code")]
        public string id { get; set; }

        [Required(ErrorMessage = "Enter surname")]
        [StringLength(150)]
        [Display(Name = "First Middle Surname")]
        public string fullname { get; set; }

        [Required(ErrorMessage = "Enter Relation")]
        [StringLength(50)]
        [Display(Name = "Relation")]
        public string relation { get; set; }

        [Required(ErrorMessage = "Enter address")]
        [StringLength(255)]
        [Display(Name = "Address")]
        public string address { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Occupation")]
        public string occupation { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Phone Number")]
        public string phonenumber { get; set; }
       
        [StringLength(150)]
        [Display(Name = "EMail")]
        public string email { get; set; }
    }

    public class StudentContact
    {
        [Key]
        [Display(Name = "Rec No")]
        public int recno { get; set; }

        [Required(ErrorMessage = "Identity Code is a must!")]
        [Display(Name = "Identity Code")]
        public string id { get; set; }

        [Required(ErrorMessage = "Enter Landline No")]
        [StringLength(150)]
        [Display(Name = "Landline No")]
        public string telephoneno { get; set; }

        [Required(ErrorMessage = "Enter Cellphone No")]
        [StringLength(150)]
        [Display(Name = "Cellphone No")]
        public string cellphoneno { get; set; }      

        [Required(ErrorMessage = "E-Mail Address")]
        [StringLength(150)]
        [Display(Name = "E-Mail Address")]
        public string emailadd { get; set; }
        
    }
}
