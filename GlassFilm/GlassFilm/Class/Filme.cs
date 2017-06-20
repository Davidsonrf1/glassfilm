using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassFilm.Class
{
    public class Filme
    {
        public Filme(int size, string descricao)
        {
            this.largura = size;
            this.descricao = descricao;
        }

        int largura = 0;
        string descricao = "";

        public int Largura
        {
            get
            {
                return largura;
            }

            set
            {
                largura = value;
            }
        }

        public string Descricao
        {
            get
            {
                return descricao;
            }

            set
            {
                descricao = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}mm", descricao, largura);
        }
    }
}
