using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PI_OTDAV_Services
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient();
    }
}
