using LoginradiusCoreSdk.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginradiusCoreSdk.Models.CloudStorageModel
{
    public class LoginRadiusIdentityModel : RaasUserprofile
    {
        public string SignupDate { get; set; }
        public bool IsBlocked { get; set; }
    }
}