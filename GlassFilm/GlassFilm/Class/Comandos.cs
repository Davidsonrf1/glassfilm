using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;

namespace GlassFilm.Class
{
    public static class Comandos
    {

        /// <summary>
        /// Função que busca o retorno de um campo
        /// </summary>
        /// <param name="_sql"> Query a se Busca o valor solicitado</param>
        /// <returns></returns>
        public static string busca_campo(string _sql)
        {
            string retorno = "";
            SQLiteDataAdapter da;
            DataTable dt;

            try
            {
                if (DBManager.conectado())
                {
                    da = new SQLiteDataAdapter(_sql, DBManager._mainDbName);
                    dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow item in dt.Rows)
                    {
                        retorno = item[0].ToString();
                    }

                    da.Dispose();
                    dt.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
                Mensagens.Informacao(ex.Message);
            }           

            return retorno;
        }       
    }
}
