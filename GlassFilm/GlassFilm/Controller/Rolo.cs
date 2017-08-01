using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using GlassFilm.Class;
using GlassFilm;

namespace GlassFilm
{
    public class Rolo
    {
        private SQLiteDataAdapter da;
        private DataTable dt;
        private SQLiteCommand cmd;

        private string Descricao { get; set; }
        private string Largura { get; set; }

        public string Id { get; set; }

        public Rolo()
        {
            DBManager.InitDB();
        }

        public Rolo(string descricao, string largura)
        {
            this.Descricao = descricao;
            this.Largura = largura;

            DBManager.InitDB();
        }

        public DataTable carrega(string codigo)
        {
            dt = new DataTable();

            try
            {
                string _sql = " SELECT " +
                              "      ID," +
                              "      DESCRICAO," +
                              "      LARGURA" +
                              "  FROM ROLO" +
                              "      ";

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
                    cmd.CommandText = "DELETE FROM ROLO WHERE ID = @codigo";
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
                        string _sql = " INSERT INTO ROLO (ID,DESCRICAO,LARGURA, SINCRONIZAR) " +
                                      " VALUES (@id,@descricao,@largura, 1) ";

                        cmd = new SQLiteCommand();
                        cmd.Connection = DBManager._mainConnection;
                        cmd.CommandText = _sql;

                        string ultCodigo = Comandos.busca_campo("SELECT MAX(ID)+1 FROM ROLO");
                        if (ultCodigo.Length == 0 || ultCodigo.Equals("0")) { ultCodigo = "1"; }

                        cmd.Parameters.Add(new SQLiteParameter("@id", ultCodigo));
                        cmd.Parameters.Add(new SQLiteParameter("@descricao", this.Descricao));
                        cmd.Parameters.Add(new SQLiteParameter("@largura", this.Largura));

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
                        string _sql = " UPDATE ROLO SET " +
                                      "      DESCRICAO = @descricao," +
                                      "      LARGURA = @largura," +
                                      "     SINCRONIZAR = 1" +
                                      " WHERE " +
                                      "      Id = @id";

                        cmd = new SQLiteCommand();
                        cmd.Connection = DBManager._mainConnection;
                        cmd.CommandText = _sql;

                        cmd.Parameters.Add(new SQLiteParameter("@descricao", this.Descricao));
                        cmd.Parameters.Add(new SQLiteParameter("@largura", this.Largura));

                        cmd.Parameters.Add(new SQLiteParameter("@id", this.Id));

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
