using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginradiusCoreSdk.Models.UserProfile
{
    public class RaasUserprofile : LoginRadiusUltimateUserProfile
    {
        public string Password { get; set; }
        public bool EmailVerified { get; set; }
        public string Uid { get; set; }
        public bool IsDeleted { get; set; }
        public int NoOfLogins { get; set; }
    }
}
