using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using LoginradiusCoreSdk.Entity;
using LoginradiusCoreSdk.Exception;
using LoginradiusCoreSdk.Models.Object;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginradiusCoreSdk.Utility.Http
{  /// <summary>
   /// The HttpRequestClient class is used to handle all http requests to loginradius api.
   /// </summary>
    public class HttpRequestClient
    {
        public HttpRequestClient()
        {
            Headers = new HttpHeader();
        }

        public HttpHeader Headers { get; private set; }

        public HttpResponse HttpGet(string uri, HttpRequestParameter httpParameters)
        {
            if (httpParameters != null && httpParameters.Count > 0)
            {
                uri = httpParameters.ToString(uri);
            }

            return HttpRequest(uri, null, "GET", "application/json");
        }

        public HttpResponse HttpPost(string uri, HttpRequestParameter postHttpParameters)
        {
            return HttpPost(uri, null, postHttpParameters);
        }
        public HttpResponse HttpPost(string uri, HttpRequestParameter getHttpParameters, HttpRequestParameter postHttpParameters)
        {
            if (getHttpParameters != null && getHttpParameters.Count > 0)
            {
                uri = getHttpParameters.ToString(uri);
            }

            return HttpPost(uri, postHttpParameters.ToString(), "application/x-www-form-urlencoded");
        }

        public HttpResponse HttpPostJson(string uri, HttpRequestParameter getHttpParameters, string json)
        {
            if (getHttpParameters != null && getHttpParameters.Count > 0)
            {
                uri = getHttpParameters.ToString(uri);
            }

            return HttpPost(uri, json, "application/json");
        }

        private HttpResponse HttpPost(string uri, string postData, string contentType)
        {
            return HttpRequest(uri, postData, "POST", contentType);
        }

        private HttpResponse HttpRequest(string uri, string requestData, string method, string contentType)
        {
            HttpResponse _HttpResponse = new HttpResponse();
            if (method == "GET")
            {
                _HttpResponse = GetRequest(uri).Result;
            }
            if (method == "POST")
            {
                _HttpResponse = PostRequests(uri, requestData).Result;
            }
            if (method == "PUT")
            {
                _HttpResponse = PutRequest(uri, requestData).Result;
            }
            return _HttpResponse;
        }

        private static async Task<HttpResponse> PutRequest(string uri, string requestData)
        {
            HttpResponse _HttpResponse = new HttpResponse();
            using (var client = new HttpClient())
            {
                try
                {
                    string[] uripart = uri.Split(new[] { ".com/" }, StringSplitOptions.None);
                    var BaseAddress = uripart[0] + ".com/";
                    client.BaseAddress = new Uri(BaseAddress);
                    //Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestData);

                    var values = new Dictionary<string, string>();

                    string[] str = requestData.Split(new[] { "&" }, StringSplitOptions.None);
                    foreach (var item in str)
                    {
                        string[] strss = item.Split(new[] { "=" }, StringSplitOptions.None);
                        values.Add(strss[0], strss[1]);
                    }

                    FormUrlEncodedContent content = new FormUrlEncodedContent(values);


                    var response = await client.PutAsync(uripart[1], content);


                    var stringResponse = await response.Content.ReadAsStringAsync();
                    _HttpResponse.StatusCode = response.StatusCode;
                    _HttpResponse.ResponseContent = stringResponse;
                    _HttpResponse.HttpHeader = response.Headers;
                }
                catch (HttpRequestException e)
                {
                    var exception = e.Message;
                }

                return _HttpResponse;
            }
        }

        private static async Task<HttpResponse> PostRequests(string uri, string requestData)
        {
            HttpResponse _HttpResponse = new HttpResponse();
            using (var client = new HttpClient())
            {
                try
                {
                    string[] uripart = uri.Split(new[] { ".com/" }, StringSplitOptions.None);
                    var BaseAddress = uripart[0] + ".com/";
                    client.BaseAddress = new Uri(BaseAddress);
                    var values = new Dictionary<string, string>();
                    if (requestData.Contains(":"))
                        values = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestData);
                    else
                    {
                        string[] str = requestData.Split(new[] { "&" }, StringSplitOptions.None);
                        foreach (var item in str)
                        {
                            string[] strss = item.Split(new[] { "=" }, StringSplitOptions.None);
                            values.Add(strss[0], strss[1]);
                        }
                    }

                    FormUrlEncodedContent content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync(uripart[1], content);
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    _HttpResponse.StatusCode = response.StatusCode;
                    _HttpResponse.ResponseContent = stringResponse;
                    _HttpResponse.HttpHeader = response.Headers;
                }
                catch (HttpRequestException e)
                {
                    var exception = e.Message;
                }

                return _HttpResponse;
            }
        }

        private static async Task<HttpResponse> GetRequest(string uri)
        {
            HttpResponse _HttpResponse = new HttpResponse();
            using (var client = new HttpClient())
            {
                try
                {
                    string[] uripart = uri.Split(new[] { Constants.ApiRootDomain }, StringSplitOptions.None);
                    var BaseAddress = uripart[0] + ".com/";
                    client.BaseAddress = new Uri(Constants.ApiRootDomain);
                    var response = await client.GetAsync(uripart[1]);
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    _HttpResponse.StatusCode = response.StatusCode;
                    _HttpResponse.ResponseContent = stringResponse;
                    _HttpResponse.HttpHeader = response.Headers;
                }
                catch (HttpRequestException e)
                {
                    var exception = e.Message;
                }
                return _HttpResponse;
            }
        }

        public string Request(string endpoint, HttpRequestParameter parameter, HttpMethod method)
        {
            return Request(endpoint, parameter, method, null);
        }

        private string Request(string endpoint, HttpRequestParameter parameter, HttpMethod method, HttpHeader headers)
        {
            var _params = string.Empty;
            if (parameter != null && parameter.Count > 0)
            {
                _params = parameter.ToString();
            }
            if (method == HttpMethod.GET)
            {
                if (endpoint.Contains("?"))
                {
                    endpoint = endpoint + "&" + _params;
                }
                else
                {
                    endpoint = endpoint + "?" + _params;
                }
                return HttpGet(endpoint, headers);
            }
            else
            {
                return HttpPost(endpoint, _params, headers).Result;
            }
        }

        private static string HttpGet(string endpoint, HttpHeader headers)
        {
            return Get_Request(endpoint).Result;
        }

        private static async Task<string> Get_Request(string endpoint)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.ApiRootDomain);
                var response = await client.GetAsync(endpoint);
                var stringResponse = await response.Content.ReadAsStringAsync();
                return stringResponse;
            }
        }

        private static async Task<string> HttpPost(string uri, string parameters, HttpHeader headers)
        {
            HttpResponse _HttpResponse = new HttpResponse();
            using (var client = new HttpClient())
            {
                try
                {
                    string[] uripart = uri.Split(new[] { ".com/" }, StringSplitOptions.None);
                    var BaseAddress = uripart[0] + ".com/";
                    client.BaseAddress = new Uri(BaseAddress);
                    var values = new Dictionary<string, string>();
                    FormUrlEncodedContent content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync(uripart[1], content);
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    _HttpResponse.StatusCode = response.StatusCode;
                    _HttpResponse.ResponseContent = stringResponse;
                    _HttpResponse.HttpHeader = response.Headers;
                }
                catch (HttpRequestException e)
                {
                    var exception = e.Message;
                }
                return _HttpResponse.ResponseContent;
            }
        }


    }
}