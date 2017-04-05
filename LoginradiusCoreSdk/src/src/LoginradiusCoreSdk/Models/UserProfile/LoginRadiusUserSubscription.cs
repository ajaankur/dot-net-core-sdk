using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginradiusCoreSdk.Models.UserProfile
{
    public class LoginRadiusUserSubscription
    {
        public string Name { get; set; }
        public string Space { get; set; }
        public string PrivateRepos { get; set; }
        public string Collaborators { get; set; }
    }
}
