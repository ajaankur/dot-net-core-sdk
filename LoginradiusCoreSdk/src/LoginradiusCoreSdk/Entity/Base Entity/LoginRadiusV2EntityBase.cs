using LoginradiusCoreSdk.Entity.AppSettings;
using LoginradiusCoreSdk.Models;
using LoginradiusCoreSdk.Utility.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginradiusCoreSdk.Entity.Base_Entity
{
    public class LoginRadiusV2EntityBase
    {
        private readonly HttpRequestClient _httpRequestClient = new HttpRequestClient();
        private readonly HttpRequestParameter _commHttpRequestParameter;

        /// <summary>
        /// LoginRadius Api and Secret key. 
        /// </summary>
        protected LoginRadiusV2EntityBase()
            : this(new UserRegistrationAuthentication()
            {
                UserRegistrationKey = LoginRadiusAppSettings.AppKey,
                UserRegistrationSecret = LoginRadiusAppSettings.AppSecret
            })
        {
        }

        private LoginRadiusV2EntityBase(UserRegistrationAuthentication authentication)
        {
            Authentication = authentication;
            _commHttpRequestParameter = new HttpRequestParameter()
            {
                {"key", Authentication.UserRegistrationKey},
                {"secret", Authentication.UserRegistrationSecret}
            };
        }

        private UserRegistrationAuthentication Authentication { get; set; }

        private static string GetEndpoint(string api)
        {
            return string.Format("https://api.loginradius.com/api/v2/{0}", api);
        }

        public string Post(LoginRadiusObject @object, string json)
        {
            var response = _httpRequestClient.HttpPostJson(GetEndpoint(@object.ObjectName), _commHttpRequestParameter, json);
            return response.ResponseContent;
        }

        protected string Post(LoginRadiusObject @object, HttpRequestParameter @params)
        {
            var response = _httpRequestClient.HttpPost(GetEndpoint(@object.ObjectName), _commHttpRequestParameter, @params);
            return response.ResponseContent;
        }

        protected string Post(LoginRadiusObject @object, HttpRequestParameter getParams, HttpRequestParameter postParams)
        {
            if (getParams == null)
            {
                getParams = _commHttpRequestParameter;
            }
            else
            {
                foreach (var par in _commHttpRequestParameter)
                {
                    getParams.Add(par.Key, par.Value);
                }
            }

            var response = _httpRequestClient.HttpPost(GetEndpoint(@object.ObjectName), getParams, postParams);
            return response.ResponseContent;
        }

        protected string Post(LoginRadiusObject @object, HttpRequestParameter getParams, string postParams)
        {
            if (getParams == null)
            {
                getParams = _commHttpRequestParameter;
            }
            else
            {
                foreach (var par in _commHttpRequestParameter)
                {
                    getParams.Add(par.Key, par.Value);
                }
            }

            var response = _httpRequestClient.HttpPostJson(GetEndpoint(@object.ObjectName), getParams, postParams);
            return response.ResponseContent;
        }
    }
}
