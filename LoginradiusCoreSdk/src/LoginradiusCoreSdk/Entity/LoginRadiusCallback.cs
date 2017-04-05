using LoginradiusCoreSdk.Exception;
using LoginradiusCoreSdk.Utility.Http;
using LoginradiusCoreSdk.Utility.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using LoginradiusCoreSdk.Entity.AppSettings;

namespace LoginradiusCoreSdk.Entity
{
    /// <summary>
    /// The LoginRadius callback is used to request loginradius api when user  has successfully logged in the preferred provider.
    /// </summary>
    public class LoginRadiusCallback
    {
        readonly HttpRequestClient _requestClient = new HttpRequestClient();
        string accesstokenendpoint = "api/v2/access_token?token={0}&secret={1}";

        
        public LoginRadiusAccessToken GetAccessToken(  string RequestToken)
        {
           
            Guid guidsecret;
            if (Guid.TryParse(LoginRadiusAppSettings.AppSecret , out guidsecret))
            {
                if (!string.IsNullOrEmpty(RequestToken))
                {
                    string endpoint = string.Format(accesstokenendpoint,
                        RequestToken, LoginRadiusAppSettings.AppSecret);
                    var response = _requestClient.Request(endpoint, null, HttpMethod.GET);
                    return response.Deserialize<LoginRadiusAccessToken>();
                }
                else
                {
                    throw new LoginRadiusException("Request token not exists or Loginradius not calling back, please check IsCallback before calling this method");
                }
            }
            else
            {
                throw new ArgumentException("Secret is not valid format (Guid) or null", "secret");
            }
        }
    }
}
