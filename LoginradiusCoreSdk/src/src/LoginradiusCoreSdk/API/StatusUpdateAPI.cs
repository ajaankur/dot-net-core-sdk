using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginradiusCoreSdk.Entity;
using LoginradiusCoreSdk.Utility.Http;

namespace LoginradiusCoreSdk.API
{
    public class StatusUpdateApi : ILoginRadiusApi
    {
        readonly HttpRequestClient _requestClient = new HttpRequestClient();

        const string Endpoint = "api/v2/status?access_token={0}";

        private string _title;
        private string _url;
        private string _imageurl;
        private string _caption;
        private string _status;
        private string _description;

        /// <summary>
        /// A title for status message.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// A web link of the status message
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// An image URL of the status message
        /// </summary>
        public string Imageurl
        {
            get { return _imageurl; }
            set { _imageurl = value; }
        }

        /// <summary>
        /// The status message text
        /// </summary>
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// A caption of the status message
        /// </summary>
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        /// <summary>
        /// A description of the status message
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// The Status API is used to update the status on the user’s wall.
        /// </summary>
        /// <param name="token">A valid session token,which is fetch from Access Token API.</param>
        /// <returns></returns>
        public string ExecuteApi(Guid token)
        {
            var url = string.Format(Constants.ApiRootDomain + Endpoint, token);

            HttpRequestParameter httprequestparameter = new HttpRequestParameter
            {
                {"title", _title},
                {"url", _url},
                {"imageurl", _imageurl},
                {"status", _status},
                {"caption", _caption},
                {"description", _description}
            };

            return _requestClient.Request(url + "&" + httprequestparameter, null, HttpMethod.POST);
        }

        /// <summary>
        /// No documentation available.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string ExecuteRawApi(Guid token)
        {
            throw new NotImplementedException();
        }
    }
}
