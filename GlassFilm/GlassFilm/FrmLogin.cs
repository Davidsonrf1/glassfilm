using GlassFilm.Class;
using GlassFilm.Validacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
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

            if (buscaCnpj().Length > 0)
            {
                pnlLogin.Size = new Size(358, 80);
                this.Size = new Size(358, 300);
                txtCnpj.Visible = false;
                lbCnpjCpf.Visible = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            Environment.Exit(1);
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (Debugger.IsAttached)
            {
                autorizado = true;
                Close();
                return;
            }

            if (txtNome.Text.Trim().Length == 0 || txtSenha.Text.Trim().Length == 0)
            {
                Mensagens.Informacao("Preencha o Login e Senha corretamente para Continuar");
                return;
            }

            if (txtCnpj.Text.Length == 0 && txtCnpj.Visible)
            {
                Mensagens.Informacao("Preencha o Cnpj ou Cpf para Continuar");
                return;
            }

            pnlLogin.Enabled = false;
            msgRetorno("Entrando, aguarde...");
            
            if (ValidaInternet.existeInternet())
            {
                ValidaLogin vi = new ValidaLogin(txtNome.Text.Trim(), txtSenha.Text.Trim(), txtCnpj.Text.Trim());
                RetornoValidacao rv = new RetornoValidacao();

                if (txtCnpj.Text.Trim().Length > 0)
                {
                    rv = vi.verificaLicenca();
                }

                if (rv.pronto || txtCnpj.Text.Trim().Length == 0)
                {
                    if (txtCnpj.Text.Trim().Length == 0)
                        vi.cnpj = buscaCnpj();

                    rv = vi.inicia();

                    if (rv.pronto)
                    {
                        if (vi.valida())
                        {
                            autorizado = true;
                            this.Close();
                        }
                        else
                        {
                            if (Glass.usuario.licenca.Length > 0)
                            {
                                Mensagens.Informacao("Este Computador não possui uma Licença Ativa.");                                
                                pnlToken.Visible = false;
                                pnlToken.Enabled = true;
                                pnlLogin.Enabled = true;
                                lbMensagem.Text = "";
                            }
                            else if (!Glass.usuario.status.Equals("A"))
                            {
                                Mensagens.Informacao("Cliente Bloqueado, por favor, entre em Contato.");                                
                                pnlToken.Visible = false;
                                pnlToken.Enabled = true;
                                pnlLogin.Enabled = true;
                                lbMensagem.Text = "";
                            }
                            else
                            {                               
                                pnlToken.Visible = true;
                                pnlToken.Enabled = true;
                                pnlLogin.Enabled = true;
                                lbMensagem.Text = "";

                                pnlLogin.Size = new Size(352, 99);
                                this.Size = new Size(358, 317);
                            }
                        }
                    }
                    else
                    {
                        Mensagens.Informacao(rv.message);                        
                        pnlToken.Visible = false;
                        pnlToken.Enabled = true;
                        pnlLogin.Enabled = true;
                        lbMensagem.Text = "";
                    }  
                }
                else
                {
                    Mensagens.Informacao(rv.message);                   
                    pnlToken.Visible = false;
                    pnlToken.Enabled = true;
                    pnlLogin.Enabled = true;
                    lbMensagem.Text = "";
                }      
            }
            else
            {
                MessageBox.Show("Sem Internet");
                pnlLogin.Enabled = true;
                lbMensagem.Text = "";
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
            pnlToken.Enabled = false;
            RetornoValidacao rv = Token.criaToken(txtToken.Text.Trim());

            if (!rv.pronto)
            {
                Mensagens.Informacao(rv.message);
                pnlToken.Enabled = true;
                lbMensagem.Text = "";
            }
            else
            {
                addCnpj();
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
            lbMensagem.Text = msg;
            Application.DoEvents();
        }

        private string buscaCnpj()
        {
            string retorno = "";
            DataTable dt = new DataTable();

            try
            {
                string _sql = " SELECT cnpj_cpf FROM empresa ";                

                if (DBManager.conectado())
                {
                    SQLiteDataAdapter da = new SQLiteDataAdapter(_sql, DBManager._mainDbName);
                    da.Fill(dt);

                    foreach (DataRow l in dt.Rows)
                    {
                        retorno = l["cnpj_cpf"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
            }

            return retorno;        
        }

        public void addCnpj()
        {           
            if (DBManager.conectado() && txtCnpj.Text.Trim().Length > 0)
            {
                try
                {
                    string _sql = " INSERT INTO EMPRESA (CNPJ_CPF) VALUES (@CNPJ) ";

                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = DBManager._mainConnection;
                    cmd.CommandText = _sql;                        

                    cmd.Parameters.Add(new SQLiteParameter("@CNPJ", txtCnpj.Text.Trim()));                        

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logs.Log(ex.Message);
                } 
            }                             
        }

        private void pnlLogin_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
