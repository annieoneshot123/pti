using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VsitPrinter.Model
{
    public class TestingHMACEndpointModel
    {
        public HttpMethod Method { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Url { get; set; }

        public object Data { get; set; }

        public string Pass { get; set; }
    }
}
