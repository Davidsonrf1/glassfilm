using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassFilm.Class
{
    public class User
    {
        public int id { get; set; }
        public string token { get; set; }
        public string licenca { get; set; }
        public string status { get; set; }
        public string master { get; set; }
        public string nome { get; set; }

        public User(int id, string token, string licenca, string status, string master, string nome)
        {
            this.id = id;
            this.token = token;
            this.licenca = licenca;
            this.status = status;
            this.master = master;
            this.nome = nome;
        }
    }
}
