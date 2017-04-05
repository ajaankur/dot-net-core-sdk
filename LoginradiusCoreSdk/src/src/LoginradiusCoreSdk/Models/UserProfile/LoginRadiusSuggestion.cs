using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginradiusCoreSdk.Models.UserProfile
{
    public class LoginRadiusSuggestion //: ILoginRadiusSuggestions
    {


        public List<LoginRadiusNameId> CompaniestoFollow { get; set; }


        public List<LoginRadiusNameId> IndustriestoFollow { get; set; }

        public List<LoginRadiusNameId> NewssourcetoFollow { get; set; }


        public List<LoginRadiusNameId> PeopletoFollow { get; set; }

    }


}