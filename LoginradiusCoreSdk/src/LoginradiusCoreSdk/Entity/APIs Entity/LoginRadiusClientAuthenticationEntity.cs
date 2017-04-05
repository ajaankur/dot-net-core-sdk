using LoginradiusCoreSdk.Entity.Base_Entity;
using LoginradiusCoreSdk.Models;
using LoginradiusCoreSdk.Models.Object;
using LoginradiusCoreSdk.Models.UserProfile;
using LoginradiusCoreSdk.Utility;
using LoginradiusCoreSdk.Utility.Http;
using LoginradiusCoreSdk.Utility.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginradiusCoreSdk.Entity.APIs_Entity
{
    public class LoginRadiusClientAuthenticationEntity : LoginRadiusClientEntityBase
    {

        private readonly LoginRadiusObject _object = new LoginRadiusObject("auth");
        private readonly LoginRadiusArgumentValidator _validate = new LoginRadiusArgumentValidator();
        private ArrayList _valuesToCheck;

        public LoginRadiusPostResponse VerifyEmail(string vtoken, string url = null, string welcomeEmailTemplate = null)
        {
            _valuesToCheck = new ArrayList { vtoken, url };
            _validate.Validate(_valuesToCheck, "Verify Email");

            var getRequest = new HttpRequestParameter { { "vtoken", vtoken } };
            if (!string.IsNullOrWhiteSpace(url))
            {
                getRequest.Add("url", url);
            };

            if (!string.IsNullOrWhiteSpace(welcomeEmailTemplate))
            {
                getRequest.Add("welcomeEmailTemplate", welcomeEmailTemplate);
            }
            var response = Get(_object.ChildObject("verifyemail"), getRequest);
            return response.Deserialize<LoginRadiusPostResponse>();
        }


    }
}