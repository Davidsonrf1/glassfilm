using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassFilm.Class
{
    public class Modelo
    {
        int id;
        string modelo;
        string ano_modelo;

        public Modelo(int id, string modelo, string ano)
        {
            this.id = id;
            this.modelo = modelo;
            this.ano_modelo = ano;
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

        public string DescModelo
        {
            get
            {
                return modelo;
            }

            set
            {
                modelo = value;
            }
        }       

        public override string ToString()
        {
            return modelo + " ( ANO: " + this.ano_modelo + " )";
        }
    }
}
