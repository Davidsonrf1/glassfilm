using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using GlassFilm.Class;

namespace GlassFilm
{
    public class Modelo
    {
        private SQLiteDataAdapter da;
        private DataTable dt;
        private SQLiteCommand cmd;
 
        public string Codigo_modelo { get; set; }
        private string Codigo_marca { get; set; }
        private string Codigo_fipe { get; set; }
        private string Descricao { get; set; }
                
        public Modelo()
        {
            DBManager.InitDB();
        }
 
        public Modelo(string codigo_modelo,string codigo_marca,string codigo_fipe,string descricao)
        {
            this.Codigo_modelo = codigo_modelo;
            this.Codigo_marca = codigo_marca;
            this.Codigo_fipe = codigo_fipe;
            this.Descricao = descricao;

            DBManager.InitDB();
        }
 
        public DataTable carrega(string codigo)
        {
            dt = new DataTable();
 
            try
            {
                string _sql = " SELECT "+                              
                              "      CODIGO_MODELO," +
                              "      CODIGO_MARCA," +
                              "      CODIGO_FIPE," +
                              "      MODELO" +
                              "  FROM " +
	                          "      MODELO";
 
                if (codigo.Length > 0)
                {
                    _sql += " WHERE CODIGO_MODELO = " + codigo;
                }

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
                    cmd.CommandText = "DELETE FROM MODELO WHERE CODIGO_MODELO = @codigo";
                    cmd.Parameters.Add(new SqlParameter("@codigo", codigo));
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logs.Log(ex.Message);
                }
            }
        }
 
        public void gravar(string tipo = "")
        {
            if (tipo.Length == 0 || tipo == "Novo")
            {
                if (DBManager.conectado())
                {
                    try
                    {
                        string _sql = " INSERT INTO modelo (CODIGO_MODELO,CODIGO_MARCA,CODIGO_FIPE,MODELO) " +
                                      " VALUES (@codigo_modelo,@codigo_marca,@codigo_fipe,@modelo) ";

                        cmd = new SQLiteCommand();
                        cmd.Connection = DBManager._mainConnection;
                        cmd.CommandText = _sql;

                        string ultCodigo = Comandos.busca_campo("SELECT MAX(CODIGO_MODELO)+1 FROM MODELO");

                        cmd.Parameters.Add(new SQLiteParameter("@codigo_modelo", ultCodigo));
                        cmd.Parameters.Add(new SQLiteParameter("@codigo_marca", this.Codigo_marca));
                        cmd.Parameters.Add(new SQLiteParameter("@codigo_fipe", this.Codigo_fipe));
                        cmd.Parameters.Add(new SQLiteParameter("@modelo", this.Descricao));

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logs.Log(ex.Message);
                    }
                }
            }
            else if (tipo == "Atualizar")
            {
                if (DBManager.conectado())
                {
                    try
                    {
                        string _sql = " UPDATE modelo SET " +                                      
                                      "      CODIGO_MARCA = @codigo_marca," +
                                      "      CODIGO_FIPE = @codigo_fipe," +
                                      "      MODELO = @modelo" +
                                      " WHERE " +
                                      "      CODIGO_MODELO = @codigo_modelo";

                        cmd = new SQLiteCommand();
                        cmd.Connection = DBManager._mainConnection;
                        cmd.CommandText = _sql;

                        cmd.Parameters.Add(new SQLiteParameter("@codigo_modelo", this.Codigo_modelo));
                        cmd.Parameters.Add(new SQLiteParameter("@codigo_marca", this.Codigo_marca));
                        cmd.Parameters.Add(new SQLiteParameter("@codigo_fipe", this.Codigo_fipe));
                        cmd.Parameters.Add(new SQLiteParameter("@modelo", this.Descricao));                        

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logs.Log(ex.Message);
                    }
                }
            }
        }
    }
}
