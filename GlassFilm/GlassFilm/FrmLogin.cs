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
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            Environment.Exit(1);
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (ValidaInternet.existeInternet())
            {
                autorizado = true;
                this.Close();                
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
    }
}
