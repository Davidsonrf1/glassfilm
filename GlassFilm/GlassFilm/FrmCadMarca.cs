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
    public partial class FrmCadMarca : Form
    {
        private Carregamentos carregamento;
        private Controles controle;
        private Marca marca;

        private string linhaSelecionada = "0";
        private string linhaSelecionadaInterna = "0";
        public bool iRotinaInterna;
        public string codigoInterno;

        public FrmCadMarca()
        {
            InitializeComponent();
            carregamento = new Carregamentos();
            controle = new Controles();
        }

        #region funções

        private void carregaGridPrincipal()
        {
            string[] nomesColunas = { "Id", "Código Marca", "Descrição" };
            int[] tamColunas = { 100,120 };
            carregamento.carregarGrid(gridPrincipal, "  SELECT  " +
                                                     "       CAST(ID AS VARCHAR) AS ID," +
                                                     "       CAST(CODIGO_MARCA AS VARCHAR) AS CODIGO_MARCA," +
                                                     "       MARCA" +
                                                     "   FROM " +
                                                     "       MARCA" +
                                                     "   ORDER BY " +
                                                     "       MARCA ASC"
                                                     , tamColunas
                                                     , nomesColunas);

            tabRegistros.SelectedIndex = 0;
        }

        private void limpaCampos()
        {            
            txtCodigo.Clear();
            txtDescricao.Clear();
        }

        private void controlePanels(string tipo)
        {
            pnlGrade.Enabled = false;
            pnlManutencao.Enabled = false;
            pnlCodigo.Enabled = false;

            if (tipo == "Novo" || tipo == "Editar")
            {
                pnlManutencao.Enabled = true;
            }
            else
            {
                pnlGrade.Enabled = true;
                pnlCodigo.Enabled = true;
            }
        }

        #endregion

        #region Form

        private void Frmcadmarca_Load(object sender, EventArgs e)
        {
            carregaGridPrincipal();
        }

        private void Minimizar(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Maximizar(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void Sair(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frmcadmarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacoes.ValidaInternet.tabEnter(sender, e);
        }

        #endregion

        #region Botoes

        private void BotaoNovo(object sender, EventArgs e)
        {
            limpaCampos();
            marca = new Marca();
            controlePanels("Novo");
            toolStatus.Text = "Novo";

            controle.controlaBotoes(toolPrincipal, "Novo");
            tabRegistros.SelectedIndex = 1;
            txtDescricao.Focus();
        }

        private void BotaoEditar(object sender, EventArgs e)
        {
            toolStatus.Text = "Editando";

            marca = new Marca();
            foreach (DataRow linha in marca.carrega(linhaSelecionadaInterna).Rows)
            {
                txtCodigo.Text = linhaSelecionada;                
                txtCodigo.Text = linha["ID"].ToString();
                txtCodigoMarca.Text = linha["CODIGO_MARCA"].ToString();
                txtDescricao.Text = linha["MARCA"].ToString();
            }

            controle.controlaBotoes(toolPrincipal, "Editar");
            controlePanels("Editar");
            tabRegistros.SelectedIndex = 1;
        }

        private void BotaoSalvar(object sender, EventArgs e)
        {
            #region Validações

            if (txtDescricao.Text.Length == 0)
            {
                Mensagens.Informacao("Preencha o Campo Descrição");
                txtDescricao.Focus();
                return;
            }

            #endregion

            marca = new Marca(txtCodigo.Text,txtCodigoMarca.Text, txtDescricao.Text);

            if (toolStatus.Text == "Novo")
            {
                marca.gravar("Novo");
            }
            else if (toolStatus.Text == "Editando")
            {                
                marca.Id = txtCodigo.Text;
                marca.gravar("Atualizar");
            }

            toolStatus.Text = "...";
            controle.controlaBotoes(toolPrincipal, "Salvar");
            limpaCampos();
            controlePanels("Salvar");
            carregaGridPrincipal();
            tabRegistros.SelectedIndex = 0;

            Mensagens.Informacao("Salvo com Sucesso!");
        }

        private void BotaoExcluir(object sender, EventArgs e)
        {
            if (Mensagens.PeruntaSimNao("Deseja excluir esta Marca?") == DialogResult.No)
                return;

            marca = new Marca();
            marca.excluir(txtCodigo.Text);

            controlePanels("Excluir");
            limpaCampos();
            controle.controlaBotoes(toolPrincipal, "Excluir");
            toolStatus.Text = "...";
            carregaGridPrincipal();
            tabRegistros.SelectedIndex = 0;

            Mensagens.Informacao("Excluído com Sucesso!");
        }

        private void BotaoCancelar(object sender, EventArgs e)
        {
            limpaCampos();
            controlePanels("Cancelar");
            controle.controlaBotoes(toolPrincipal, "Cancelar");
            toolStatus.Text = "...";

            tabRegistros.SelectedIndex = 0;
        }

        #endregion

        #region Outras

        private void GridCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtFiltro.Text = gridPrincipal.Columns[gridPrincipal.CurrentCell.ColumnIndex].Name;
                linhaSelecionada = gridPrincipal.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                linhaSelecionadaInterna = gridPrincipal.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            }
        }

        private void GridCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (iRotinaInterna)
            {
                codigoInterno = linhaSelecionada;
                this.Close();
            }
            else
            {
                BotaoEditar(linhaSelecionada, EventArgs.Empty);
            }
        }

        private void ConsultaAlteraTexto(object sender, EventArgs e)
        {
            if (txtFiltro.Text.Length > 0)
                carregamento.MontaPesquisa(gridPrincipal, txtFiltro.Text, btnConsultar.Text);
        }

        private void BotaoLOG(object sender, EventArgs e)
        {
            List<string> lista = new List<string>();
            lista.Add("");            
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length > 0)
            {
                linhaSelecionadaInterna = txtCodigo.Text;
                BotaoEditar(sender,e);
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacoes.ValidaInternet.campoSomenteNumero(sender, e);
        }

    }
}
