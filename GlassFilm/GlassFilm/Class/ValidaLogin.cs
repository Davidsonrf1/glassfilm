using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace GlassFilm.Class
{
    class ValidaLogin
    {                
        private MySqlCommand cmd;
        private MySqlDataAdapter da;
        private DataTable dt;
        private HashMachine hm;       

        private string nome;
        private string senha;

        public ValidaLogin(string nome, string senha)
        {
            this.nome = nome;
            this.senha = senha;
        }

        public bool inicia()
        {
            bool retorno = false;
            if (ConexaoExterna.conectar())
            {
                Glass.usuario = buscaUsuario();

                if (Glass.usuario != null && Glass.usuario.status.Equals("A"))
                {
                    retorno = true;
                }                
            }
            return retorno;
        }

        public bool valida()
        {            
            hm = new HashMachine();
            string idMachine = hm.getHashMachine();
            string idHash = HashMachine.criptoMD5(Glass.usuario.token + idMachine, "tech");

            return Glass.usuario.idHash.Equals(idHash);
        }

        private User buscaUsuario()
        {
            DataTable dtUser = ConexaoExterna.getDataTable("select cu.id,cu.licence,cu.idHash,c.status from gf_client_user cu inner join gf_client c on c.id=cu.id_cliente where cu.login = '" + this.nome + "' and cu.senha = '" + this.senha + "'");
            User _user = null;
            foreach (DataRow r in dtUser.Rows)
            {
                _user = new User(Convert.ToInt16(r["id"].ToString()), r["licence"].ToString(), r["idHash"].ToString(), r["status"].ToString());
            }
            return _user;
        }        
    }

    public static class ConexaoExterna
    {
        // var de controle do banco de dados
        private static MySqlConnection con;
        private static MySqlCommand cmd;
        private static MySqlDataAdapter da;
        private static DataTable dt;

        public static MySqlConnection ConexaoBD
        {
            get { return ConexaoExterna.con; }
        }        

        // variavel string conexao
        private static string _stringConexao = "Persist Security Info=False;server=199.217.112.74;database=ativtec_glassfilm;uid=ativtec_glassfil;pwd='PQLz4gQq_Q7W'";

        public static string StringConexao
        {
            get { return _stringConexao; }
        }

        public static bool conectar()
        {
            bool conectado = false;

            try
            {
                con = new MySqlConnection(_stringConexao);

                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        conectado = true;
                    }
                }
                catch
                {
                    conectado = false;
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
            }

            return conectado;
        }

        public static string Busca_campo(string _sql)
        {
            string retorno = "";

            try
            {
                if (conectar())
                {
                    da = new MySqlDataAdapter(_sql, con);
                    dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow linha in dt.Rows)
                    {
                        retorno = linha[0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);               
            }
            finally
            {
                da.Dispose();
                dt.Dispose();
            }

            return retorno;
        }

        public static DataTable getDataTable(string _sql)
        {
            DataTable dtRetorno = new DataTable();

            try
            {
                if (conectar())
                {
                    da = new MySqlDataAdapter(_sql, con);
                    da.Fill(dtRetorno);
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);               
            }
            finally
            {
                da.Dispose();
                dtRetorno.Dispose();
            }

            return dtRetorno;
        }

        public static bool insert(string sql)
        {
            bool retorno = false;

            if (conectar())
            {
                cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = ConexaoBD;

                try
                {
                    cmd.ExecuteNonQuery();
                    retorno = true;
                }
                catch (Exception ex) {

                    Logs.Log(ex.Message);
                }                
            }

            return retorno;
        }
    }

    public static class Token{
     
        private static HashMachine hm;   

        public static RetornoValidacao criaToken(string _token)
        {
            RetornoValidacao rv = new RetornoValidacao();

            if (Glass.usuario == null)
            {
                rv.pronto = false;
                rv.message = "Nenhum usuário válido";
                return rv;
            }

            if (!Glass.usuario.token.Equals(_token))
            {
                rv.pronto = false;
                rv.message = "Token Inválido";
                return rv;
            }

            hm = new HashMachine();
            string idMachine = hm.getHashMachine();
            string idHash = HashMachine.criptoMD5(Glass.usuario.token + idMachine, "tech");           

            rv.pronto = atualizaHashUser(idHash);
            rv.message = "Bem vindo";
            return rv;
        }

        private static bool atualizaHashUser(string hash)
        {            
            string _sql = "update gf_client_user set idHash = '" + hash + "' where licence = '" + Glass.usuario.token + "'";
            return ConexaoExterna.insert(_sql);                       
        }        
    }

    public class RetornoValidacao
    {
        public bool pronto = false;
        public string message = "";

        public RetornoValidacao()
        {
            pronto = false;
            message = "";
        }
    }
}
