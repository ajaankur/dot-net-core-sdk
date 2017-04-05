using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginradiusCoreSdk.Models
{
    public class LoginRadiusPostResponse
    {
        public bool isPosted { get; set; }
    }

    public class LoginRadiusEmailResponse
    {
        public bool isExist { get; set; }
    }

    public class LoginRadiusForgotPasswordTokenResponse
    {
        public string Giud { get; set; }
        public List<string> Providers { get; set; }
    }

    public class LoginRadiusEmailVerificationToken
    {
        public string Guid { get; set; }
    }

    public class LoginRadiusCustomObjectCheckResponse
    {
        public bool IsExists { get; set; }
    }
}
