using GlassFilm.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

        /// <summary>
        /// Função para validar busca de Valores de Campo
        /// </summary>
        /// <param name="txtCampo"> TextBox do Código</param>
        /// <param name="txtDescricao"> TextBox da Descricao</param>
        /// <param name="_sql">query a buscar</param>
        /// <param name="msg"> Mensagem de retorno negativo</param>
        public static void validarCampoExiste(TextBox txtCampo, TextBox txtDescricao, string _sql, string msg)
        {
            if (txtCampo.Text.Length > 0)
            {
                txtDescricao.Text = Comandos.busca_campo(_sql);

                if (txtDescricao.Text.Length == 0)
                {
                    txtCampo.Clear();
                    Mensagens.Informacao(msg);
                    txtCampo.Focus();
                    return;
                }
            }
        }

        public static void campoSomenteNumero(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        public static void tabEnter(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar.CompareTo((char)Keys.Return)) == 0)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
    }
}
