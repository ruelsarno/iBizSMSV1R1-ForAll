using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBizSMSV1R1.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public string idno { get; set; }       
        public string cardid { get; set; }        
        public string name { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Claims { get; set; }
    }
}


