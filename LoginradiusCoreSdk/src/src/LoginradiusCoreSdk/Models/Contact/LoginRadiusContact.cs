using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginradiusCoreSdk.Models.Contact
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LoginRadiusContact
    {
        public string NextCursor { get; set; }
        public List<Data> Data { get; set; }
    }
    public class Data
    {
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string PhoneNumber { get; set; }
        public string ID { get; set; }
        public string ProfileUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public string Industry { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
    }


}
