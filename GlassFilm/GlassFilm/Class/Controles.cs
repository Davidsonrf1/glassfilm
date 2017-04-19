using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GlassFilm
{
    class Controles
    {        
        public Controles()
        { 
        
        }

        /// <summary>
        /// Função que controla os Botões padrão da tela (salvar, Novo ...etc)
        /// </summary>
        /// <param name="ts">Toopstrip onde esta os Botões</param>
        /// <param name="tipo">Tipo, se é novo, salvar, cancelar</param>
        public void controlaBotoes(ToolStrip ts,string tipo)
        {
            ts.Items[0].Enabled = false; // Novo
            ts.Items[2].Enabled = false; // Editar
            ts.Items[4].Enabled = false; // Salvar
            ts.Items[6].Enabled = false; // Excluir
            ts.Items[8].Enabled = false; // Cancelar

            if (tipo == "Novo")
            {                
                ts.Items[4].Enabled = true;  // Salvar                
                ts.Items[8].Enabled = true;  // Cancelar                
            }

            if (tipo == "Editar")
            {                
                ts.Items[4].Enabled = true;  // Salvar
                ts.Items[6].Enabled = true;  // Excluir
                ts.Items[8].Enabled = true;  // Cancelar
            }

            if (tipo == "Salvar" || tipo == "Excluir" || tipo == "Cancelar")
            {
                ts.Items[0].Enabled = true;  // Novo
                ts.Items[2].Enabled = true;  // Editar                
            }            
        }

        /// <summary>
        /// Função que movimenta Painel com clicks
        /// </summary>
        /// <param name="pnl1">Painel que movimenta</param>
        /// <param name="pnl2">Painel tamanho padrão</param>
        /// <param name="inverte">Variavel que controla se o Painel aumenta ou diminui</param>
        public void controlaGrids(Panel pnl1, Panel pnl2, bool inverte, int tamanhoPadrao = 0)
        {
            //-------------------------------------------------
            // pnl1 = Painel que movimenta
            //-------------------------------------------------
            
            int iTamanhoPanel = pnl1.Height; 
            int iTamanhototal = pnl2.Height - 15;

            if (tamanhoPadrao == 0)
                tamanhoPadrao = iTamanhototal / 3;

            for (int i = 0; i < 100; i++)
            {
                if (iTamanhoPanel >= iTamanhototal && !inverte)
                    break;

                if (iTamanhoPanel <= tamanhoPadrao && inverte)
                    break;

                if (!inverte)
                    iTamanhoPanel = iTamanhoPanel + 5;
                else
                    iTamanhoPanel = iTamanhoPanel - 5;

                Application.DoEvents();
                pnl1.Size = new Size(pnl1.Width, iTamanhoPanel);
            }
        }
    }
}
