using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace GlassFilm.Class
{
    class ValidaLogin
    {                
        private HashMachine hm;       

        public static string nome;
        public static string senha;
        public static string cnpj;

        public ValidaLogin(string nome, string senha, string cnpj = "")
        {
            ValidaLogin.nome = nome;
            ValidaLogin.senha = senha;
            ValidaLogin.cnpj = cnpj;
        }

        public RetornoValidacao inicia()
        {            
            RetornoValidacao rv = new RetornoValidacao();
                           
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
            
            return rv;
        }

        public bool valida()
        {            
            hm = new HashMachine();
            string idMachine = hm.getHashMachine();
            string idLicenca = HashMachine.criptoMD5(Glass.usuario.token + idMachine, "tech");
            idLicenca = ValidaLogin.sReplace(idLicenca);

            return Glass.usuario.licenca.Equals(idLicenca);
        }

        public User buscaUsuario()
        {
            return ConexaoValidaLogin.buscaUser();
        }

        public RetornoValidacao verificaLicenca()
        {
            RetornoValidacao rv = new RetornoValidacao();
           
            if (ConexaoValidaLogin.verificaLicenca()>0)
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

        public static string sReplace(string _string)
        {
            return _string.Replace("+","|");
        }
    }    

    public class ConexaoValidaLogin {

        public static string BUSCA_USUARIO = "buscaUsuario";
        public static string VERIFICA_LICENCA = "verificaLicenca";
        public static string ATUALIZA_HASH_USER = "atualizaHashUser";
        public static string ATUALIZA_ACESSO = "atualizaAcesso";        

        public static User buscaUser()
        {
            var request = (HttpWebRequest)WebRequest.Create(GlassService.GetUrl("sistema.php"));
            var postData = "";
            User user = null;

            postData = "tipo=" + BUSCA_USUARIO;
            postData += "&cnpj=" + ValidaLogin.cnpj;
            postData += "&name=" + ValidaLogin.nome;
            postData += "&pass=" + ValidaLogin.senha;
            postData += "&machine_name=" + Environment.MachineName;
            postData += "&key=cutfilmecf";                           

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().Replace("[", "").Replace("]", "");

                if (responseString.Length > 0)
                {
                    user = JsonConvert.DeserializeObject<User>(responseString);
                    user.licenca = ValidaLogin.sReplace(user.licenca);
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
                throw;
            }            

            return user;                                  
        }

        public static int verificaLicenca()
        {
            var request = (HttpWebRequest)WebRequest.Create(GlassService.GetUrl("sistema.php"));
            var postData = "";
            int quantidade = 0;

            postData = "tipo=" + VERIFICA_LICENCA;
            postData += "&cnpj=" + ValidaLogin.cnpj;            
            postData += "&key=cutfilmecf";

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                quantidade = JsonConvert.DeserializeObject<int>(responseString);
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
                throw;
            }

            return quantidade;
        }

        public static bool atualizaHashUser(string hash)
        {
            var request = (HttpWebRequest)WebRequest.Create(GlassService.GetUrl("sistema.php"));
            var postData = "";
            bool retorno = false;            

            postData =  "tipo=" + ATUALIZA_HASH_USER;
            postData += "&licenca=" + ValidaLogin.sReplace(hash);
            postData += "&machine_name=" + Environment.MachineName;
            postData += "&token=" + Glass.usuario.token;
            postData += "&ultimo_acesso=" + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss"); ;
            postData += "&key=cutfilmecf";

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().Replace("[", "").Replace("]", "");
                retorno = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
                throw;
            }

            return retorno;
        }

        public static void atualizaAcesso()
        {
            var request = (HttpWebRequest)WebRequest.Create(GlassService.GetUrl("sistema.php"));
            var postData = "";
            bool retorno = false;

            postData = "tipo=" + ATUALIZA_ACESSO;            
            postData += "&token=" + Glass.usuario.token;
            postData += "&ultimo_acesso=" + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm"); ;
            postData += "&key=cutfilmecf";

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().Replace("[", "").Replace("]", "");
                retorno = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception)
            {                
                                
            }            
        }

        public static string mensalidadeAVencer()
        {
            string url = GlassService.GetUrl("verificaAtrasoMensalidades.php?tipo=vencer");

            string responseString = "";
            int pagina = 0;
            
            var request = (HttpWebRequest)WebRequest.Create(url);
            string postData = "";
            postData += "&id=" + Glass.usuario.id;

            byte[] data = Encoding.UTF8.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                responseString = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                return responseString;

            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
                throw;
            }              
        }

        public static string mensalidadeAtrasada()
        {
            string url = GlassService.GetUrl("verificaAtrasoMensalidades.php?tipo=atraso");

            string responseString = "";
            int pagina = 0;

            var request = (HttpWebRequest)WebRequest.Create(url);
            string postData = "";
            postData += "&id=" + Glass.usuario.id;

            byte[] data = Encoding.UTF8.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                responseString = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                return responseString;

            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
                throw;
            }
        }

        public static string mensalidadeVencida()
        {
            string url = GlassService.GetUrl("verificaAtrasoMensalidades.php?tipo=vencido");

            string responseString = "";
            int pagina = 0;

            var request = (HttpWebRequest)WebRequest.Create(url);
            string postData = "";
            postData += "&id=" + Glass.usuario.id;

            byte[] data = Encoding.UTF8.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                responseString = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                return responseString;

            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
                throw;
            }
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

            if(ConexaoValidaLogin.atualizaHashUser(idHash))
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
