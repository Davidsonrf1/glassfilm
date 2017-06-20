using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassFilm.Class
{
    public class Filme
    {
        public Filme(int id, int size, string descricao)
        {
            this.id = id;
            this.largura = size;
            this.descricao = descricao;
        }

        int id = -1;
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

        public int Id
        {
            get
            {
                return id;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}mm", descricao, largura);
        }
    }
}
