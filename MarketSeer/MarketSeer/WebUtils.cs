using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MarketSeer
{
    public static class WebUtils
    {
        public static WebClient GetJSONClient()
        {
            WebClient myClient = new WebClient();

            myClient.Headers.Add("Content-Type:application/json");
            myClient.Headers.Add("Accept:application/json");

            return myClient;
        }
    }
}
