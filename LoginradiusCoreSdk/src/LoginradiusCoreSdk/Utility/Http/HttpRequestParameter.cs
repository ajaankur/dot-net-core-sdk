using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginradiusCoreSdk.Utility.Http
{
    public class HttpRequestParameter : Dictionary<string, string>
    {
        public override string ToString()
        {
            return Count > 0 ? string.Join("&", this.Select(x => x.Key + "=" + x.Value).ToArray()) : string.Empty;
        }

        public string ToString(string uri)
        {
            if (uri.Contains("?"))
            {
                uri = uri + "&" + ToString();
            }
            else
            {
                uri = uri + "?" + ToString();
            }

            return uri;
        }
    }
}
