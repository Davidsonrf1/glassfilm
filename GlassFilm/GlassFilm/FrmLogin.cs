using GlassFilm.Class;
using GlassFilm.Validacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GlassFilm
{
    public partial class FrmLogin : Form
    {
        public bool autorizado = false;

        public FrmLogin()
        {
            InitializeComponent();
            pnlAguardando.Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            Environment.Exit(1);
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {            
            msgRetorno("Entrando, aguarde...");
            
            if (ValidaInternet.existeInternet())
            {
                ValidaLogin vi = new ValidaLogin(txtNome.Text.Trim(),txtSenha.Text.Trim());

                if (vi.inicia())
                {
                    if (vi.valida())
                    {
                        autorizado = true;
                        this.Close();
                    }                    
                    else
                    {
                        if (Glass.usuario.idHash.Length > 0)
                        {
                            Mensagens.Informacao("Este Computador não está Vinculado a está Conta.");
                            pnlAguardando.Visible = false;
                            pnlToken.Visible = false;
                        }
                        else if (!Glass.usuario.status.Equals("A"))
                        {
                            Mensagens.Informacao("Cliente Bloqueado, por favor, entre em Contato.");
                            pnlAguardando.Visible = false;
                            pnlToken.Visible = false;
                        }
                        else
                        {
                            pnlAguardando.Visible = false;
                            pnlToken.Visible = true;
                        }
                    }
                }
                else
                {
                    Mensagens.Informacao("Usuário e Senha incorretos");
                    pnlAguardando.Visible = false;
                    pnlToken.Visible = false;
                }        
            }
            else
            {
                MessageBox.Show("Sem Internet");
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#f8f6f9");
            pnlLogin.BackColor = ColorTranslator.FromHtml("#434144");
        }

        private void btnValidarToken_Click(object sender, EventArgs e)
        {
            msgRetorno("Verificando Token, aguarde...");
            RetornoValidacao rv = Token.criaToken(txtToken.Text.Trim());

            if (!rv.pronto)
            {
                Mensagens.Informacao(rv.message);
                pnlAguardando.Visible = false;
            }
            else
            {
                autorizado = true;
                this.Close();     
            }
        }

        private void btnCancelarToken_Click(object sender, EventArgs e)
        {
            Close();
            Environment.Exit(1);
        }

        private void msgRetorno(string msg)
        {
            
            pnlAguardando.Visible = true;                                   
            lbMensagem.Text = msg;
            Application.DoEvents();
        }
    }
}
