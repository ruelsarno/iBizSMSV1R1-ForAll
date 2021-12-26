using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace iBizSMSV1R1.ViewModels
{
    public static class UserClaimStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Create Role", "Create Role"),
            new Claim("Get Role", "Get Role"),
            new Claim("Update Role", "UPdate Role"),
            new Claim("Delete Role", "Delete Role")
        };
    }
}
