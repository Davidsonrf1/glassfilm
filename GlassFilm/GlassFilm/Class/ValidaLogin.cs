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
        public string cnpj;

        public ValidaLogin(string nome, string senha, string cnpj = "")
        {
            this.nome = nome;
            this.senha = senha;
            this.cnpj = cnpj;
        }

        public RetornoValidacao inicia()
        {            
            RetornoValidacao rv = new RetornoValidacao();            

            if (ConexaoExterna.conectar())
            {                
                Glass.usuario = buscaUsuario();

                if (Glass.usuario != null && Glass.usuario.status.Equals("A"))
                {
                    rv.pronto = true;
                }
                else
                {
                    rv.pronto = false;
                    rv.message = "Usuário e Senha incorretos";
                }                                                 
            }
            return rv;
        }

        public bool valida()
        {            
            hm = new HashMachine();
            string idMachine = hm.getHashMachine();
            string idLicenca = HashMachine.criptoMD5(Glass.usuario.token + idMachine, "tech");

            return Glass.usuario.licenca.Equals(idLicenca);
        }

        private User buscaUsuario()
        {
            string _sql = "SELECT" +
                            "   cu.id," +
                            "   cl.token," +
                            "   cl.licenca," +
                            "   c.status" +
                            " FROM" +
                            "   gf_client_user cu" +
                            " INNER JOIN gf_client c on cu.id_cliente = c.id" +
                            " INNER JOIN gf_client_licence cl on cl.id_cliente = c.id" +
                            " WHERE" +
                            "   c.cnpj_cpf = '"+this.cnpj+"'" +
                            "   and cu.login = '"+this.nome+"'" +
                            "   and cu.senha = '"+ this.senha + "'";

            DataTable dtUser = ConexaoExterna.getDataTable(_sql);
            User _user = null;
            foreach (DataRow r in dtUser.Rows)
            {
                _user = new User(Convert.ToInt16(r["id"].ToString()), r["token"].ToString(), r["licenca"].ToString(), r["status"].ToString());
            }
            return _user;
        }

        public RetornoValidacao verificaLicenca()
        {
            RetornoValidacao rv = new RetornoValidacao();

            string _sql = "SELECT count(*) FROM gf_client_licence cl" +
                          " INNER JOIN gf_client c on c.id = cl.id_cliente" +
                          " WHERE c.cnpj_cpf = '" + this.cnpj + "'" +
                          " and length(cl.licenca) = 0";

            DataTable dtUser = ConexaoExterna.getDataTable(_sql);
            bool retorno = false;
            foreach (DataRow r in dtUser.Rows)
            {
                retorno = !r[0].ToString().Equals("0");
            }

            if (retorno)
            {
                rv.pronto = true;
                rv.message = "";
            }
            else
            {
                rv.pronto = false;
                rv.message = "Nenhuma Licença disponível para este Computador";
            }

            return rv;
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

            if(atualizaHashUser(idHash))
            {
                rv.pronto = true;
                rv.message = "Bem vindo";
            }
            else 
            {
                rv.pronto = false;
                rv.message = "Não foi possível atualizar seu acesso!";
            }
            
            return rv;
        }

        private static bool atualizaHashUser(string hash)
        {
            string _sql = "update gf_client_licence set licenca = '" + hash + "', machine_name = '" + Environment.MachineName + "' where token = '" + Glass.usuario.token + "'";
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
