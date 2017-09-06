using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using GlassFilm.Class;
using System.Data.SQLite;

namespace GlassFilm
{
    class Carregamentos
    {
        public Carregamentos()
        { 
        
        }

        /// <summary>
        /// Função para carregar Grid
        /// </summary>
        /// <param name="grid">Objeto GridView</param>
        /// <param name="_sql">Sintaxe para Carregar</param>
        /// <param name="tamanhos">Define a largura de cada coluna</param>
        public void carregarGrid(DataGridView grid,string _sql,int[] tamanhos = null,string[] HeaderText = null)
        {
            try
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter(_sql, DBManager._mainDbName);                               
                DataTable dt = new DataTable();                
                da.Fill(dt);

                grid.DataSource = dt;

                if (tamanhos != null)
                {
                    int i = 0;
                    foreach (int tam in tamanhos)
                    {
                        grid.Columns[i].Width = tam;                        
                        i++;
                    }
                }
                else
                {
                    grid.Columns[0].Width = 100;
                }

                if (HeaderText != null)
                {
                    int i = 0;
                    foreach (string dpn in HeaderText)
                    {
                        grid.Columns[i].HeaderText = dpn;
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
            }
        }

        public void carregarGridLog(DataGridView grid, string _sql, int[] tamanhos = null, string[] HeaderText = null)
        {
            try
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter(_sql, DBManager._accessDbname);
                DataTable dt = new DataTable();
                da.Fill(dt);

                grid.DataSource = dt;

                if (tamanhos != null)
                {
                    int i = 0;
                    foreach (int tam in tamanhos)
                    {
                        grid.Columns[i].Width = tam;
                        i++;
                    }
                }
                else
                {
                    grid.Columns[0].Width = 100;
                }

                if (HeaderText != null)
                {
                    int i = 0;
                    foreach (string dpn in HeaderText)
                    {
                        grid.Columns[i].HeaderText = dpn;
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
            }
        }

        /// <summary>
        /// Função que filtra dados de um Grid - Pesquisa dinamica.
        /// </summary>
        /// <param name="Tabela">Nome da Tabela a buscar os Dados.</param>
        /// <param name="grid">Componente DataGridView a ser Filtrado.</param>
        /// <param name="campoPesquisa">Campo do banco a ser filtrado.</param>
        /// <param name="texto">Texto que irá filtrar no Grid.</param>
        public void MontaPesquisa(DataGridView grid, string campoPesquisa, string texto)
        {
            try
            {
                DataTable dtTmp = (DataTable)(grid.DataSource);

                if (campoPesquisa.StartsWith("VALOR"))
                    return;

                if ((campoPesquisa.ToUpper().StartsWith("CODIGO") || campoPesquisa.ToUpper().StartsWith("CÓDIGO") || campoPesquisa.ToUpper().StartsWith("ID")) && texto.Length > 0)
                    dtTmp.DefaultView.RowFilter = campoPesquisa + " = " + texto;
                else if (campoPesquisa.StartsWith("CODIGO") || campoPesquisa.ToUpper().StartsWith("CÓDIGO") || campoPesquisa.ToUpper().StartsWith("ID"))
                    dtTmp.DefaultView.RowFilter = " 0=0 ";           
                else
                    dtTmp.DefaultView.RowFilter = campoPesquisa + " LIKE '%" + texto + "%'";

                grid.DataSource = dtTmp; 
            }
            catch (Exception)
            {
                Mensagens.Informacao("Não é possível buscar por este Campo");
            }
        }

        public DataTable carregaDataTable(string _sql)
        {
            DataTable tb = new DataTable();

            if (DBManager.conectado())
            {
                SQLiteDataAdapter dap = new SQLiteDataAdapter(_sql, DBManager._mainDbName);                
                dap.Fill(tb);                
            }           

            return tb;            
        }
    }
}
