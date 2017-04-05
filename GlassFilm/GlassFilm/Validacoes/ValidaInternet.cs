using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassFilm.Validacoes
{
    class ValidaInternet
    {
        public static bool existeInternet()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return false;
            }
            else
            {
                return true;
            } 
        }
    }
}
