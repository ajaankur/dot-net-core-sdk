using System;
using LoginradiusCoreSdk.Entity;
using LoginradiusCoreSdk.Utility.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginradiusCoreSdk.API
{
    public class CompanyApi : ILoginRadiusApi
    {
        readonly HttpRequestClient _requestClient = new HttpRequestClient();

        const string Endpoint = "api/v2/company?access_token={0}";
        const string RawEndpoint = "api/v2/company/raw?access_token={0}";

        /// <summary>
        /// The Company API is used to get the followed companies data from the user’s social account. The data will be normalized into LoginRadius' standard data format.
        /// </summary>
        /// <param name="token">A valid session token,which is fetch from Access Token API.</param>
        /// <returns></returns>
        public string ExecuteApi(Guid token)
        {
            var url = string.Format(Constants.ApiRootDomain + Endpoint, token);
            return _requestClient.Request(url, null, HttpMethod.GET);
        }

        /// <summary>
        /// Get followed companies data as provided by the provider. The data will not be in a consistent response type and format across providers, so you will have to parse it yourself.
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
