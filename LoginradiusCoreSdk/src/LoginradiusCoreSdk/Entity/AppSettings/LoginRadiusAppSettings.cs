using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginradiusCoreSdk.Entity.AppSettings
{
    public static   class LoginRadiusAppSettings
    {
        public static void  AppSettingInitialization(string AppKey, string AppSecret, string AppName)
        {
            _AppKey = AppKey;
            _AppSecret = AppSecret;
            _AppName = AppName;
        }
        private static string _AppKey;
        private static string _AppSecret;
        private static string _AppName;
        static public IConfigurationRoot Configuration { get; set; }


        public static void AppsettingClass()
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }


        public static string AppKey
        {
            get
            {
                if (string.IsNullOrEmpty(_AppKey))
                {
                    AppsettingClass();
                    _AppKey = Configuration["loginradius:appkey"];
                    if (string.IsNullOrEmpty(_AppKey))
                    {
                        throw new System.Exception("appkey not found in application.json");
                    }
                }
                return _AppKey;
            }
            set
            {
                _AppKey = value;
            }
        }

        public static string AppSecret
        {
            get
            {
                if (string.IsNullOrEmpty(_AppSecret))
                {
                    AppsettingClass();
                    _AppSecret = Configuration["loginradius:appsecret"];
                    if (string.IsNullOrEmpty(_AppSecret))
                    {
                        throw new System.Exception("appsecret not found in application.json");
                    }
                }
                return _AppSecret;
            }
            set
            {
                _AppSecret = value;
            }
        }

        public static string AppName
        {
            get
            {
                if (string.IsNullOrEmpty(_AppName))
                {
                    AppsettingClass();
                    _AppName = Configuration["loginradius:appname"];
                    if (string.IsNullOrEmpty(_AppName))
                    {
                        throw new System.Exception("appname not found in application.json");
                    }
                }
                return _AppName;
            }

            set
            {
                _AppName = value;
            }
        }
    }
}
