using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GlassFilm.Class
{
    public class SeletorVeiculo
    {
        bool listaTodas = false;

        ComboBox cbMarcas = null;
        ComboBox cbModelos = null;
        ComboBox cbVeiculos = null;

        List<Marca> marcas = new List<Marca>();
        List<Modelo> modelos = new List<Modelo>();
        List<Veiculo> veiculos = new List<Veiculo>();

        Marca marcaAtual = null;
        Modelo modeloAtual = null;
        Veiculo veiculoAtual = null;

        public bool ListaTodas
        {
            get
            {
                return listaTodas;
            }

            set
            {
                listaTodas = value;
            }
        }

        public ComboBox CbMarcas
        {
            get
            {
                return cbMarcas;
            }

            set
            {
                if (cbMarcas != null)
                {
                    cbMarcas.SelectedIndexChanged -= CbMarcas_SelectedIndexChanged;
                }

                cbMarcas = value; PreencheCbMarcas();

                if (cbMarcas != null)
                {
                    cbMarcas.SelectedIndexChanged += CbMarcas_SelectedIndexChanged;
                }
            }
        }

        public ComboBox CbModelos
        {
            get
            {
                return cbModelos;
            }

            set
            {
                if (cbModelos != null)
                    cbModelos.SelectedIndexChanged -= CbModelos_SelectedIndexChanged;

                cbModelos = value; PreencheCbModelos();

                if (cbModelos != null)
                    cbModelos.SelectedIndexChanged += CbModelos_SelectedIndexChanged;
            }
        }

        public ComboBox CbVeiculos
        {
            get
            {
                return cbVeiculos;
            }

            set
            {
                cbVeiculos = value;
            }
        }

        public Marca MarcaAtual
        {
            get
            {
                return marcaAtual;
            }
        }

        public Veiculo VeiculoAtual
        {
            get
            {
                return veiculoAtual;
            }
        }

        void PreencheCbModelos()
        {
            if (cbModelos != null)
            {
                cbModelos.Items.Clear();
                CbModelos.Text = "";

                if (marcaAtual != null)
                {
                    modelos = DBManager.CarregarModelos(marcaAtual.Id, listaTodas);

                    foreach (Modelo m in modelos)
                    {
                        cbModelos.Items.Add(m);
                    }
                }
            }            
        }

        private void CbModelos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbVeiculos != null)
            {
                CbVeiculos.Items.Clear();
                CbVeiculos.Text = "";
            }

            modeloAtual = null;
            if (cbModelos.SelectedItem != null)
            {
                modeloAtual = (Modelo)cbModelos.SelectedItem;
                PreencheCbVeiculos();
            }
        }

        void PreencheCbVeiculos()
        {
            cbVeiculos.SelectedIndexChanged += CbVeiculos_SelectedIndexChanged;

            if (cbVeiculos != null)
            {
                cbVeiculos.Items.Clear();
                cbVeiculos.Text = "";

                if (modeloAtual != null)
                {
                    veiculos = DBManager.CarregarVeiculos(modeloAtual.Id, listaTodas);
                    foreach (Veiculo m in veiculos)
                    {
                        cbVeiculos.Items.Add(m);
                    }
                }
            }
        }

        private void CbVeiculos_SelectedIndexChanged(object sender, EventArgs e)
        {
            veiculoAtual = null;
            if (cbVeiculos.SelectedItem != null)
                veiculoAtual = (Veiculo)cbVeiculos.SelectedItem;
        }

        void PreencheCbMarcas()
        {
            if (cbMarcas != null)
            {
                cbMarcas.Items.Clear();

                foreach (Marca m in marcas)
                {
                    cbMarcas.Items.Add(m);
                }
            }
        }

        private void CbMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbModelos != null)
            {
                CbModelos.Items.Clear();
                cbMarcas.Text = "";
            }

            if (CbVeiculos != null)
            {
                CbVeiculos.Items.Clear();
                CbVeiculos.Text = "";
            }

            marcaAtual = null;
            if (cbMarcas.SelectedItem != null)
            {
                marcaAtual = (Marca)cbMarcas.SelectedItem;
                PreencheCbModelos();
            }
        }

        public void Limpar()
        {
            if (cbMarcas != null)
            {
                cbMarcas.Items.Clear();
                cbMarcas.Text = "";
            }

            if (CbModelos != null)
            {
                CbModelos.Items.Clear();
                cbMarcas.Text = "";
            }

            if (cbVeiculos != null)
            {
                cbVeiculos.Items.Clear();
                cbVeiculos.Text = "";
            }
        }

        public void AtualizaMarcas()
        {
            Limpar();

            marcas = DBManager.CarregarMarcas(listaTodas);
            PreencheCbMarcas();            
        }
    }
}
