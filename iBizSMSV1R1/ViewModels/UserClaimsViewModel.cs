using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBizSMSV1R1.ViewModels
{
    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            Claims = new List<UserClaim>();
        }

        public string userID { get; set; }
        public List<UserClaim> Claims { get; set; }
}
}
