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
    public partial class FrmLog : Form
    {
        private Carregamentos carregamento;
        string linhaSelecionada;

        public FrmLog()
        {
            InitializeComponent();
            carregamento = new Carregamentos();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLog_Load(object sender, EventArgs e)
        {
            carregaPrincipal();
        }

        private void carregaPrincipal()
        {
            string[] nomesColunas = { "Usuário", "Área Rolo", "Área Peças", "Data", "Hora" };
            int[] tamColunas = { 150, 150, 150, 150};
            carregamento.carregarGridLog(gridPrincipal, "select " +
                                                    "	 usuario," +
                                                    "	 area_rolo_usado," +
                                                    "	 area_total_pecas," +                                                    
                                                    "	 data," +
                                                    "	 hora" +
                                                    " from " +
                                                    "	 log_corte order by data desc, hora desc"
                                                    , tamColunas
                                                    , nomesColunas);
            
        }

        private void gridPrincipal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtFiltro.Text = gridPrincipal.Columns[gridPrincipal.CurrentCell.ColumnIndex].Name;
                linhaSelecionada = gridPrincipal.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            }
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text.Length > 0)
                carregamento.MontaPesquisa(gridPrincipal, txtFiltro.Text, txtPesquisa.Text);
        }
    }
}
