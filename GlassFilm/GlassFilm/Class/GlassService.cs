using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace GlassFilm.Class
{
    public static class GlassService
    {
        private static string serverBaseUrl = "http://www.cutfilm.com.br/glass/serv/";

        public static string ServerBaseUrl
        {
            get
            {
                return serverBaseUrl;
            }

            set
            {
                serverBaseUrl = value;
            }
        }

        public static string GetUrl(string url)
        {
            return serverBaseUrl + url;
        }
    }
}
