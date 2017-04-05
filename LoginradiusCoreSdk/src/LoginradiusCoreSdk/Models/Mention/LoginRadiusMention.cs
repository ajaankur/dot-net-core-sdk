using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginradiusCoreSdk.Models.Mention
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LoginRadiusMention
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public object ImageUrl { get; set; }
        public string DateTime { get; set; }
        public string Source { get; set; }
        public string Place { get; set; }
        public int Likes { get; set; }
        public object LinkUrl { get; set; }
        public string Id { get; set; }
    }
}
