using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginradiusCoreSdk.Utility.Serialization
{
    /// <summary>
    /// LoginRadius class for JSON serialization and deserialization.
    /// </summary>
    public static class JsonExtension
    {
        public static string Serialize(this object value)
        {
            //var js = new JavaScriptSerializer();
            //return js.Serialize(value);
            return JsonConvert.SerializeObject(value);
        }

        public static T Deserialize<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
