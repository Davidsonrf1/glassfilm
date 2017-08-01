using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Data.SQLite;
using System.Data.Common;
using GlassFilm.Class;
using GlassFilm;

namespace GlassFilm
{
    public class Marca
    {        
        private SQLiteDataAdapter da;
        private DataTable dt;
        private SQLiteCommand cmd;       

        public string Id { get; set; }
        public string Codigo_marca { get; set; }
        private string Descricao { get; set; }        

        public Marca()
        {
            DBManager.InitDB();
        }

        public Marca(string id, string codigo_marca, string descricao)
        {
            this.Id = id;
            this.Codigo_marca = codigo_marca;
            this.Descricao = descricao;
            DBManager.InitDB();
        }

        public DataTable carrega(string codigo)
        {            
            dt = new DataTable();

            try
            {
                string _sql = " SELECT " +
                              "      ID," +
                              "      CODIGO_MARCA," +         
                              "      MARCA" +
                              "  FROM " +
                              "      MARCA ";

                if (codigo.Length > 0)
                {
                    _sql += " WHERE ID = " + codigo;
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
                    cmd.CommandText = "DELETE FROM MARCA WHERE ID = @codigo";
                    cmd.Parameters.Add(new SQLiteParameter("@codigo", codigo));
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
                        string _sql = " INSERT INTO MARCA (ID,CODIGO_MARCA,MARCA,TIPO, SINCRONIZAR) " +
                                      " VALUES (@id,@codigo_marca,@marca,@tipo,1) ";

                        cmd = new SQLiteCommand();
                        cmd.Connection = DBManager._mainConnection;
                        cmd.CommandText = _sql;

                        string ultCodigo = Comandos.busca_campo("SELECT MAX(ID)+1 FROM MARCA");

                        cmd.Parameters.Add(new SQLiteParameter("@id", ultCodigo));
                        cmd.Parameters.Add(new SQLiteParameter("@codigo_marca", this.Codigo_marca));
                        cmd.Parameters.Add(new SQLiteParameter("@marca", this.Descricao));
                        cmd.Parameters.Add(new SQLiteParameter("@tipo", "1"));                        

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
                        string _sql = " UPDATE MARCA SET " +
                                      "      ID = @id," +
                                      "      CODIGO_MARCA = @codigo_marca," +
                                      "      MARCA = @marca," +
                                      "      SINCRONIZAR = 1" +
                                      " WHERE " +
                                      "      id = @id";

                        cmd = new SQLiteCommand();
                        cmd.Connection = DBManager._mainConnection;
                        cmd.CommandText = _sql;

                        cmd.Parameters.Add(new SQLiteParameter("@id", this.Id));
                        cmd.Parameters.Add(new SQLiteParameter("@codigo_marca", this.Codigo_marca));
                        cmd.Parameters.Add(new SQLiteParameter("@marca", this.Descricao));                        

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
