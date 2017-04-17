using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;

namespace GlassFilm.Class
{
    public static class DBManager
    {
        static SQLiteConnection _mainConnection = null;
        static SQLiteConnection _modelConnection = null;
        static string _mainDbName = "GlassFilm.db";
        static string _modelDbname = "Modelos.db";

        public static void InitDB()
        {
            _mainConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3", _mainDbName));
            _mainConnection.Open();

            _modelConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3", _modelDbname));
            _modelConnection.Open();
        }
        
        public static Dictionary<int, string> CarregarMarcas(bool todas=false)
        {


            return null;
        }

        public static Dictionary<int, string> CarregarModelos()
        {


            return null;
        }


        public static string CarregarModelo(int mar, int modelo, int ano)
        {


            return "";
        }

    }
}
