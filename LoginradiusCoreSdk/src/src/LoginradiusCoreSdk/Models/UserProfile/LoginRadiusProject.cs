using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginradiusCoreSdk.Models.UserProfile
{
    public class LoginRadiusProject
    {
        public string Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Summary
        {
            get;
            set;
        }

        public List<LoginRadiusNameId> With
        {
            get;
            set;
        }
        public string StartDate
        {
            get;
            set;
        }
        public string EndDate
        {
            get;
            set;
        }
        public string IsCurrent
        {
            get;
            set;
        }
    }


}
