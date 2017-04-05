using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
using System.Net; 

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginradiusCoreSdk.Utility.Http
{/// <summary>
 ///  Header class for for API excecution. 
 /// </summary>
    public class HttpHeader : Dictionary<string, string>
    {
        public WebHeaderCollection ToWebHeaderCollection()
        {
            var webheadercollection = new WebHeaderCollection();

            if (Count > 0)
            {
                foreach (var header in this)
                {
                    //webheadercollection.Add(header.Key, header.Value);
                }
            }

            return webheadercollection;
        }
    }

    public static class HttpHeaderExtension
    {
        public static HttpHeader ToHttpHeader(this WebHeaderCollection webHeaderCollection)
        {
            var httpheaders = new HttpHeader();

            if (webHeaderCollection.Count > 0)
            {
                foreach (var header in webHeaderCollection)
                {
                    httpheaders.Add(header.ToString(), webHeaderCollection[header.ToString()]);
                }
            }

            return httpheaders;
        }
    }

}
