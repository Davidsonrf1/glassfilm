using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassFilm.Class
{
    public class ModeloAno
    {
        string codigo_ano;
        string codigo_modelo;
        string ano;

        public ModeloAno(string codigo_ano, string codigo_modelo, string ano)
        {
            this.codigo_ano = codigo_ano;
            this.codigo_modelo = codigo_modelo;
            this.ano = ano;
        }

        public string Codigo_ano
        {
            get
            {
                return codigo_ano;
            }

            set
            {
                codigo_ano = value;
            }
        }

        public string Codigo_modelo
        {
            get
            {
                return codigo_modelo;
            }

            set
            {
                codigo_modelo = value;
            }
        }

        public string Ano
        {
            get
            {
                return ano;
            }

            set
            {
                ano = value;
            }
        }

        public override string ToString()
        {
            return this.ano;
        }

    }
}
