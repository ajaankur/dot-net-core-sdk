using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginradiusCoreSdk.Entity;
using LoginradiusCoreSdk.Utility.Http;

namespace LoginradiusCoreSdk.API
{
    public class FollowingApi : ILoginRadiusApi
    {
        readonly HttpRequestClient _requestClient = new HttpRequestClient();

        const string Endpoint = "api/v2/following?access_token={0}";
        const string RawEndpoint = "api/v2/following/raw?access_token={0}";

        /// <summary>
        /// The Following API is used to get the following user list from the user’s social account. The data will be normalized into LoginRadius' standard data format.
        /// </summary>
        /// <param name="token">A valid session token,which is fetch from Access Token API.</param>
        /// <returns></returns>
        public string ExecuteApi(Guid token)
        {
            var url = string.Format(Constants.ApiRootDomain + Endpoint, token);
            return _requestClient.Request(url, null, HttpMethod.GET);
        }

        /// <summary>
        /// Get user following user list as provided by the provider. The data will not be in a consistent response type and format across providers, so you will have to parse it yourself.
        /// </summary>
        /// <param name="token">A valid session token,which is fetch from Access Token API.</param>
        /// <returns></returns>
        public string ExecuteRawApi(Guid token)
        {
            var url = string.Format(Constants.ApiRootDomain + RawEndpoint, token);
            return _requestClient.Request(url, null, HttpMethod.GET);
        }
    }
}
