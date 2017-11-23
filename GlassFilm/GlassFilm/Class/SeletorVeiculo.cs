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
        TipoDesenho tipoDesenho = TipoDesenho.None;

        ComboBox cbMarcas = null;
        ComboBox cbModelos = null;
        ComboBox cbVeiculos = null;
        ComboBox cbTipo = null;

        List<Marca> marcas = new List<Marca>();
        List<Modelo> modelos = new List<Modelo>();
        List<ModeloAno> modeloAno = new List<ModeloAno>();

        Marca marcaAtual = null;
        Modelo modeloAtual = null;
        ModeloAno veiculoAtual = null;

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

        public ComboBox CbTipo
        {
            get
            {
                return cbTipo;
            }

            set
            {
                if (cbTipo != null)
                    cbTipo.SelectedIndexChanged -= CbTipo_SelectedIndexChanged;

                cbTipo = value;
                
                if (cbTipo != null)
                    cbTipo.SelectedIndexChanged += CbTipo_SelectedIndexChanged;
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

        public ModeloAno VeiculoAtual
        {
            get
            {
                return veiculoAtual;
            }
        }

        public Modelo ModeloAtual
        {
            get
            {
                return modeloAtual;
            }
        }

        public TipoDesenho TipoDesenho
        {
            get
            {
                return tipoDesenho;
            }

            set
            {
                tipoDesenho = value;
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
                    modelos = DBManager.CarregarModelos(marcaAtual.Id, listaTodas, tipoDesenho);

                    foreach (Modelo m in modelos)
                    {
                        cbModelos.Items.Add(m);
                    }
                }
            }            
        }

        private void CbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbTipo.SelectedIndex)
            {
                case 0:                    
                        tipoDesenho = TipoDesenho.WindowTint;                    
                    break;
                case 1:                    
                        tipoDesenho = TipoDesenho.PPV;                    
                    break;
            }

            AtualizaMarcas();
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
            if (cbVeiculos == null)
                return;

            cbVeiculos.SelectedIndexChanged += CbVeiculos_SelectedIndexChanged;

            if (cbVeiculos != null)
            {
                cbVeiculos.Items.Clear();
                cbVeiculos.Text = "";

                if (modeloAtual != null)
                {
                    modeloAno = DBManager.CarregaModeloANO(modeloAtual.Id, listaTodas, tipoDesenho);
                    foreach (ModeloAno m in modeloAno)
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
                veiculoAtual = (ModeloAno)cbVeiculos.SelectedItem;
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

        public string AtualizaMarcas()
        {
            Limpar();

            marcas = DBManager.CarregarMarcas(listaTodas, tipoDesenho);
            PreencheCbMarcas();

            return DBManager.GetNumDesenhos();
        }
    }
}
