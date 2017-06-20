using GlassFilm.Controller;
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
    public partial class FrmCadModelo : Form
    {
        private Carregamentos carregamento;
        private Controles controle;
        private Modelo modelo;

        private string linhaSelecionada = "0";
        public bool iRotinaInterna;
        public string codigoInterno;

        public FrmCadModelo()
        {
            InitializeComponent();
            carregamento = new Carregamentos();
            controle = new Controles();
        }

        #region funções

        private void carregaGridPrincipal()
        {
            string[] nomesColunas = { "Código", "Modelo", "Marca" };
            int[] tamColunas = { 100, 200, 200 };
            carregamento.carregarGrid(gridPrincipal, " SELECT  " +
                                                     "	 CAST(CODIGO_MODELO AS VARCHAR) AS CODIGO_MODELO," +
                                                     "	 MODELO," +                                                                                                  
                                                     "	 (SELECT MARCA FROM MARCA WHERE MARCA.CODIGO_MARCA = MODELO.CODIGO_MARCA) as MARCA" +
                                                     " FROM " +
                                                     "	 MODELO" +
                                                     " ORDER BY " +
                                                     "   CODIGO_MODELO DESC"
                                                     , tamColunas
                                                     , nomesColunas);

            tabRegistros.SelectedIndex = 0;
        }

        private void limpaCampos()
        {
            txtCodigoAno.Clear();
            txtDescricao.Clear();
            txtDescCodigoMarca.Clear();
            txtCodigoMarca.Clear();
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

        private void Frmcadmodelo_Load(object sender, EventArgs e)
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

        private void Frmcadmodelo_KeyPress(object sender, KeyPressEventArgs e)
        {            
            Validacoes.ValidaInternet.tabEnter(sender, e);
        }

        #endregion

        #region Botoes

        private void BotaoNovo(object sender, EventArgs e)
        {
            limpaCampos();
            modelo = new Modelo();
            controlePanels("Novo");
            toolStatus.Text = "Novo";

            controle.controlaBotoes(toolPrincipal, "Novo");
            tabRegistros.SelectedIndex = 1;
            txtCodigoMarca.Focus();
        }

        private void BotaoEditar(object sender, EventArgs e)
        {
            toolStatus.Text = "Editando";

            modelo = new Modelo();
            foreach (DataRow linha in modelo.carrega(linhaSelecionada).Rows)
            {
                txtCodigo.Text = linhaSelecionada;
                txtCodigoMarca.Text = linha["CODIGO_MARCA"].ToString();                
                txtDescricao.Text = linha["MODELO"].ToString();

                txtCodigoMarca_Leave(txtCodigoMarca.Text, EventArgs.Empty);
            }

            ModeloAno ma = new ModeloAno();
            cbAnos.Items.Clear();
            foreach (DataRow ano in ma.carrega(linhaSelecionada).Rows)
            {
                cbAnos.Items.Add(ano["ANO"].ToString());
            }
            cbAnos.Sorted = true;

            if (cbAnos.Items.Count > 0)
                cbAnos.SelectedIndex = 0;

            controle.controlaBotoes(toolPrincipal, "Editar");
            controlePanels("Editar");
            tabRegistros.SelectedIndex = 1;
        }

        private void BotaoSalvar(object sender, EventArgs e)
        {
            #region Validações

            if (txtCodigoMarca.Text.Length == 0)
            {
                Mensagens.Informacao("Preencha o Campo Marca");
                txtCodigoMarca.Focus();
                return;
            }            

            if (txtDescricao.Text.Length == 0)
            {
                Mensagens.Informacao("Preencha o Campo Descrição");
                txtDescricao.Focus();
                return;
            }

            #endregion
            
            modelo = new Modelo(txtCodigo.Text,txtCodigoMarca.Text,txtDescricao.Text);

            if (toolStatus.Text == "Novo")
            {
                modelo.gravar("Novo");
            }
            else if (toolStatus.Text == "Editando")
            {                
                modelo.Codigo_modelo = txtCodigo.Text;
                modelo.gravar("Atualizar");
            }

            if (!gravarAno())
            {
                Mensagens.Informacao("Aconteceu algum erro ao Salvar os Anos selecionados.");
                return;
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
            if (Mensagens.PeruntaSimNao("Deseja excluir este Modelo?") == DialogResult.No)
                return;

            modelo = new Modelo();
            modelo.excluir(txtCodigo.Text);

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
            lista.Add("Modelo");            
        }

        #endregion

        private void txtCodigoMarca_Leave(object sender, EventArgs e)
        {
            Validacoes.ValidaInternet.validarCampoExiste(txtCodigoMarca, txtDescCodigoMarca, "SELECT MARCA FROM MARCA WHERE CODIGO_MARCA = " + txtCodigoMarca.Text.Trim(), "Marca não encontrada!");                                  
        }

        private void txtCodigoMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacoes.ValidaInternet.campoSomenteNumero(sender, e);
        }

        private void btnBuscaMarca_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FrmCadMarca>().Count() > 0)
            {
                Mensagens.Atencao("Rotina já esta aberta!");
            }
            else
            {
                FrmCadMarca frm = new FrmCadMarca();
                frm.iRotinaInterna = true;
                frm.ShowDialog();

                if (frm.codigoInterno != null)
                {
                    txtCodigoMarca.Text = frm.codigoInterno;
                    txtCodigoMarca_Leave(txtCodigoMarca.Text, EventArgs.Empty);
                    txtDescricao.Focus();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length > 0)
            {
                linhaSelecionada = txtCodigo.Text;
                BotaoEditar(sender, e);
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacoes.ValidaInternet.campoSomenteNumero(sender, e);
        }

        private void txtCodigoAno_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacoes.ValidaInternet.campoSomenteNumero(sender, e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtCodigoAno.Text.Length != 4)
            {
                Mensagens.Informacao("Este não parece um Ano válido. Por favor, verifique!");
                return;
            }

            if (txtCodigoAno.Text.Length > 0)
            {
                cbAnos.Items.Add(txtCodigoAno.Text.Trim());
                txtCodigoAno.Clear();
                txtCodigoAno.Focus();
                cbAnos.Sorted = true;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (cbAnos.Text.Length > 0)
            {                
                cbAnos.Items.RemoveAt(cbAnos.SelectedIndex);
                cbAnos.Sorted = true;
            }
        }

        private bool gravarAno()
        {
            try
            {
                ModeloAno ma = new ModeloAno(txtCodigo.Text.Trim());
                ma.excluir(txtCodigo.Text.Trim());

                foreach (string ano in cbAnos.Items)
                {
                    ma = new ModeloAno(txtCodigo.Text.Trim(), ano);
                    ma.gravar();
                }
                cbAnos.Items.Clear();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
