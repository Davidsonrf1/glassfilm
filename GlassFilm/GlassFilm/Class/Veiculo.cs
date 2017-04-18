using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassFilm.Class
{
    public class Veiculo
    {
        int id;
        string veiculo;

        public Veiculo(int id, string veiculo)
        {
            this.id = id;
            this.veiculo = veiculo;
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

        public string DescVeiculo
        {
            get
            {
                return veiculo;
            }

            set
            {
                veiculo = value;
            }
        }

        public override string ToString()
        {
            return veiculo;
        }
    }
}
