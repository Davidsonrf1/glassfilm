using GlassFilm.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace GlassFilm.Controller
{
    class ModeloAno
    {
        private SQLiteDataAdapter da;
        private DataTable dt;
        private SQLiteCommand cmd;

        public string codigo_modelo { get; set; }
        public string ano { get; set; }

        public ModeloAno(string codModelo, string ano)
        {
            this.codigo_modelo = codModelo;
            this.ano = ano;
        }

        public ModeloAno(string codModelo)
        {
            this.codigo_modelo = codModelo;            
        }

        public ModeloAno()
        {
            
        }

        public DataTable carrega(string codigo)
        {
            dt = new DataTable();

            try
            {
                string _sql = " SELECT ANO FROM MODELO_ANO WHERE CODIGO_MODELO = " + codigo;                

                if (DBManager.conectado())
                {
                    da = new SQLiteDataAdapter(_sql, DBManager._mainDbName);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
            }

            return dt;
        }

        public void excluir(string codigo)
        {
            if (DBManager.conectado())
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.Connection = DBManager._mainConnection;
                    cmd.CommandText = "DELETE FROM MODELO_ANO WHERE CODIGO_MODELO = @codigo";
                    cmd.Parameters.Add(new SQLiteParameter("@codigo", codigo));
                    cmd.ExecuteNonQuery();

                    DBManager.EliminaRegistro("MODELO_ANO", codigo);
                }
                catch (Exception ex)
                {
                    Logs.Log(ex.Message);
                }
            }
        }

        public void gravar()
        {            
            if (DBManager.conectado())
            {
                try
                {
                    string _sql = " INSERT INTO MODELO_ANO (CODIGO_ANO,CODIGO_MODELO,ANO, SINCRONIZAR) VALUES (@codigo_ano,@codigo_modelo,@ano,1) ";

                    cmd = new SQLiteCommand();
                    cmd.Connection = DBManager._mainConnection;
                    cmd.CommandText = _sql;

                    string ultCodigo = Comandos.busca_campo("SELECT MAX(CODIGO_ANO)+1 FROM MODELO_ANO");
                    if (ultCodigo.Length == 0)
                    {
                        ultCodigo = "1";
                    }

                    cmd.Parameters.Add(new SQLiteParameter("@codigo_ano", ultCodigo));
                    cmd.Parameters.Add(new SQLiteParameter("@codigo_modelo", this.codigo_modelo));
                    cmd.Parameters.Add(new SQLiteParameter("@ano", this.ano));                        

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logs.Log(ex.Message);
                }
            }                       
        }

        public override string ToString()
        {
            return this.ano;
        }
    }
}
