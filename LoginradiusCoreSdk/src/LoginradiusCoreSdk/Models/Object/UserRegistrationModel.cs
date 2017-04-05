using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginradiusCoreSdk.Models.Object
{
    public class UserRegistrationModel : User
    {
        public string EmailVerificationUrl { get; set; }
        public string Template { get; set; }
    }
}