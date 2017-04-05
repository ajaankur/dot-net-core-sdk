using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginradiusCoreSdk.Models.UserProfile
{
    public class ProviderAccessCredential
    {
        public string AccessToken { get; set; }
        public string TokenSecret { get; set; }
    }
}
