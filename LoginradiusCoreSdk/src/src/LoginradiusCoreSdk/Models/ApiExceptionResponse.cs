using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginradiusCoreSdk.Models
{
    public class ApiExceptionResponse
    {
        public string description { get; set; }
        public int errorCode { get; set; }
        public string message { get; set; }
        public bool isProviderError { get; set; }
        public string providerErrorResponse { get; set; }
    }
}
