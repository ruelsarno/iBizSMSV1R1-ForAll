using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBizSMSV1R1.ViewModels
{
    public class CreateRoleViewModel
    {       
        [Required]
        public string rolename { get; set; }
    }
}
