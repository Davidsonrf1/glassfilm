using GlassFilm.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlassFilm
{
    public partial class FrmDetalheView : Form
    {
        int codigoVeculo = 0;

        public FrmDetalheView()
        {
            InitializeComponent();
        }

        public int CodigoVeculo { get => codigoVeculo; set => codigoVeculo = value; }

        private void FrmImageView_Load(object sender, EventArgs e)
        {
            Image img = DBManager.CarregarFoto(codigoVeculo);

            if (img != null)
            {
                imagem.Image = img;
            }

            txtObs.Text = DBManager.CarregarObs(codigoVeculo);
        }
    }
}
