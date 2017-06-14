using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassFilm.Class
{
    class User
    {
        public int id { get; set; }
        public string token { get; set; }
        public string idHash { get; set; }
        public string status { get; set; }

        public User(int id, string token, string idhash, string status)
        {
            this.id = id;
            this.token = token;
            this.idHash = idhash;
            this.status = status;
        }
    }
}
