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
    public partial class FrnCadRolo : Form
    {
        private Carregamentos carregamento;
        private Controles controle;
        private Rolo rolo;

        private string linhaSelecionada = "0";
        public bool iRotinaInterna;
        public string codigoInterno;

        public FrnCadRolo()
        {
            InitializeComponent();
            carregamento = new Carregamentos();
            controle = new Controles();
        }

        #region funções

        private void carregaGridPrincipal()
        {
            string[] nomesColunas = { "Código", "Descrição", "Largura"};
            int[] tamColunas = { 100, 300};
            carregamento.carregarGrid(gridPrincipal, "  SELECT  " +
                                                     "       ID," +
                                                     "       DESCRICAO," +
                                                     "       LARGURA" +
                                                     "   FROM ROLO" +
                                                     "       " +
                                                     "   ORDER BY " +
                                                     "       ID DESC"
                                                     , tamColunas
                                                     , nomesColunas);

            tabRegistros.SelectedIndex = 0;
        }

        private void limpaCampos()
        {
            txtCodigo.Clear();
            txtDescricao.Clear();
            txtLargura.Clear();
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

        private void Frncadrolo_Load(object sender, EventArgs e)
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

        #endregion

        #region Botoes

        private void BotaoNovo(object sender, EventArgs e)
        {
            limpaCampos();
            rolo = new Rolo();
            controlePanels("Novo");
            toolStatus.Text = "Novo";

            controle.controlaBotoes(toolPrincipal, "Novo");
            tabRegistros.SelectedIndex = 1;
            txtDescricao.Focus();
        }

        private void BotaoEditar(object sender, EventArgs e)
        {
            toolStatus.Text = "Editando";

            rolo = new Rolo();
            foreach (DataRow linha in rolo.carrega(linhaSelecionada).Rows)
            {
                txtCodigo.Text = linhaSelecionada;
                txtDescricao.Text = linha["DESCRICAO"].ToString();
                txtLargura.Text = linha["LARGURA"].ToString();
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

            if (txtLargura.Text.Length == 0)
            {
                Mensagens.Informacao("Preencha o Campo Largura");
                txtLargura.Focus();
                return;
            }

            #endregion

            rolo = new Rolo(txtDescricao.Text,txtLargura.Text);

            if (toolStatus.Text == "Novo")
            {
                rolo.gravar("Novo");
            }
            else if (toolStatus.Text == "Editando")
            {
                //LogAlteracao.CriaLog(pnlManutencao, "", "Id", txtCodigo.Text);

                rolo.Id = txtCodigo.Text;
                rolo.gravar("Atualizar");
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
            rolo = new Rolo();
            rolo.excluir(txtCodigo.Text);

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
                linhaSelecionada = gridPrincipal.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
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

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length > 0)
            {
                linhaSelecionada = txtCodigo.Text;
                BotaoEditar(sender, e);
            }
        }

        private void txtLargura_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacoes.ValidaInternet.campoSomenteNumero(sender, e);
        }

    }
}
