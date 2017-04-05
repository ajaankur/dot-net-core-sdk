using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginradiusCoreSdk.Models.UserProfile
{
    public class LoginRadiusVolunteer
    {
        public string Id
        {
            get;
            set;
        }
        public string Role
        {
            get;
            set;
        }
        public string Organization
        {
            get;
            set;
        }
        public string Cause
        {
            get;
            set;
        }
    }
}
