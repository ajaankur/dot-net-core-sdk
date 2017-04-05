using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginradiusCoreSdk.Models.Page
{
    public class LoginRadiusPageLocations
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public LoginRadiusCountryCodeName Country { get; set; }
        public string Zip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Phone { get; set; }
    }
}