using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginradiusCoreSdk.Models.UserProfile
{
    public class LoginRadiusRecommendationReceived
    {
        public string Id
        {
            get;
            set;
        }
        public string RecommendationType
        {
            get;
            set;
        }
        public string RecommendationText
        {
            get;
            set;
        }
        public string Recommender
        {
            get;
            set;
        }
    }
}
