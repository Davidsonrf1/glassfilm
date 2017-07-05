using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassFilm.Class
{
    public class ConfigValue
    {
        int id = 0;
        string nome = "";
        string valor = "";
        string valorPadrao = "";

        public ConfigValue(int id, string nome, string defValue)
        {
            this.id = id;
            this.nome = nome;
            this.valorPadrao = defValue;
        }

        public ConfigValue(string nome, string defValue)
        {
            id = -1;
            this.nome = nome;
            this.valorPadrao = defValue;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }

        public string Valor
        {
            get
            {
                return valor;
            }

            set
            {
                valor = value;
            }
        }

        public string ValorPadrao
        {
            get
            {
                return valorPadrao;
            }

            set
            {
                valorPadrao = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", nome, valor);
        }
    }
}
