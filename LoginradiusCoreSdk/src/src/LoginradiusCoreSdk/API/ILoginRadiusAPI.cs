using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginradiusCoreSdk.API
{
    public interface ILoginRadiusApi
    {
        string ExecuteApi(Guid token);
        string ExecuteRawApi(Guid token);
    }
}
