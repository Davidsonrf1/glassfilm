using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassFilm.Class
{
    public class Marca
    {
        int id;
        string marca;

        public Marca(int id, string marca)
        {
            this.id = id;
            this.marca = marca;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string DescMarca
        {
            get
            {
                return marca;
            }

            set
            {
                marca = value;
            }
        }

        public override string ToString()
        {
            return marca;
        }
    }
}
