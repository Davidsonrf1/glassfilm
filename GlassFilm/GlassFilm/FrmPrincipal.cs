using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace GlassFilm
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient)
                {
                    ctrl.BackColor = ColorTranslator.FromHtml("#f7f6f6");
                }
            }

            FrmLogin frm = new FrmLogin();            
            frm.ShowDialog();

            if (frm.autorizado)
            {
                pnlFiltroInfo.Visible = true;
                pnlprincipal.Visible = true;
                pnlMapa.Visible = true;
            }
        }

        private void cortadoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConfigPlotter conf = new FrmConfigPlotter();

            if (conf.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void cadastrarDesenhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FrmCadastroDesenho>().Count() > 0)
            {
                Mensagens.Atencao("Rotina já está aberta!");
                
            }
            else
            {
                FrmCadastroDesenho frm = new FrmCadastroDesenho();
                frm.ShowInTaskbar = false;
                frm.ShowDialog();
            }                        
        }

        private void cadastrarMarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FrmCadMarca>().Count() > 0)
            {
                Mensagens.Atencao("Rotina já está aberta!");

            }
            else
            {
                FrmCadMarca frm = new FrmCadMarca();
                frm.ShowInTaskbar = false;
                frm.ShowDialog();
            }     
        }

        private void cadastroModeloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FrmCadModelo>().Count() > 0)
            {
                Mensagens.Atencao("Rotina já está aberta!");

            }
            else
            {
                FrmCadModelo frm = new FrmCadModelo();
                frm.ShowInTaskbar = false;
                frm.ShowDialog();
            }     
        }
    }
}
