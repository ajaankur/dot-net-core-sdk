using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
using System.Net;
using System.Net.Http.Headers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginradiusCoreSdk.Utility.Http
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseContent { get; set; }
        public HttpResponseHeaders HttpHeader { get; set; }
    }
}
