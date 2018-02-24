using GlassFilm.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlassFilm
{
    public partial class FrmNovosVeiculos : Form
    {
        string pagina = null;
        string lista = null;
        int count = 0;

        public FrmNovosVeiculos()
        {
            InitializeComponent();

            Assembly a = Assembly.GetExecutingAssembly();
            string resName = "GlassFilm.Resources.atualiza.html";

            using (Stream stream = a.GetManifestResourceStream(resName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    pagina = reader.ReadToEnd();
                }
            }

            lista = MontarLista();
        }

        public static void MostraAtualizacoes()
        {
            FrmNovosVeiculos novos = new FrmNovosVeiculos();

            if (novos.count > 0)
            {
                novos.ShowDialog();
            }
        }

        public static bool PossuiDesenhos()
        {
            SQLiteCommand cmd = DBManager._mainConnection.CreateCommand();
            cmd.CommandText = "SELECT IFNULL(MAX(VERSAO_MODELO), 0) VERSAO_MODELO FROM MODELO_ANO";
            int versaoModelos = 0;

            try
            {
                versaoModelos = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            catch
            {

            }

            return !(versaoModelos == 0);
        }

        public static void GravaVersaoDesenhos()
        {
            SQLiteCommand cmd = DBManager._accessConnection.CreateCommand();
            try
            {                
                cmd.CommandText = "select ifnull(count(*), 0) qtd from empresa";
                int qtd = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (qtd == 0)
                {
                    string _sql = " INSERT INTO EMPRESA (CNPJ_CPF) VALUES (@CNPJ) ";

                    cmd = DBManager._accessConnection.CreateCommand();
                    cmd.CommandText = _sql;

                    cmd.Parameters.Add(new SQLiteParameter("@CNPJ", "1234567890"));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
            }
            
            try
            {
                cmd = DBManager._mainConnection.CreateCommand();
                cmd.CommandText = "SELECT IFNULL(MAX(VERSAO_MODELO), 0) VERSAO_MODELO FROM MODELO_ANO";
                int versaoModelos = 0;

                versaoModelos = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd = DBManager._accessConnection.CreateCommand();
                cmd.CommandText = $"UPDATE EMPRESA SET VERSAO_ATUAL_DESENHOS = {versaoModelos}";
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Logs.Log(ex.Message);
            }            
        }

        public string MontarLista()
        {
            StringBuilder sb = new StringBuilder();

            SQLiteCommand cmd = DBManager._accessConnection.CreateCommand();
            cmd.CommandText = "SELECT IFNULL(MAX(VERSAO_ATUAL_DESENHOS), 0) VERSAO_ATUAL_DESENHOS FROM EMPRESA";
            int versaoModelos = 0;

            try
            {
                versaoModelos = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            catch
            {

            }

            cmd = DBManager._mainConnection.CreateCommand();

            cmd.CommandText = $" SELECT * 																									              "
                            + $"   FROM MODELO_ANO MA INNER JOIN MODELO MO ON MO.CODIGO_MODELO = MA.CODIGO_MODELO AND MA.VERSAO_MODELO > {versaoModelos} "
                            + $"                      INNER JOIN MARCA MC WHERE MC.CODIGO_MARCA = MO.CODIGO_MARCA  							              "
                            + $"  ORDER BY CODIGO_MODELO, CODIGO_ANO																			          ";


            count = 0;
            IDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                count++;
                string desenho = dr["POSSUI_DESENHO"].ToString() == "1" ? "Sim" : "Não";
                string ppv = dr["POSSUI_PPV"].ToString() == "1" ? "Sim" : "Não";

                string style = "style=\"color:#303064;\"";
                sb.Append($"<div><SPAN {style}>{dr["MARCA"]}</SPAN><SPAN{style}>  -  </SPAN><span {style}>{dr["MODELO"]} Ano {dr["ANO"]} Window Tint: {desenho}, Plotagem: {ppv}</span></div>");
            }
            dr.Close();

            if (count == 0)
                return null;

            return sb.ToString();
        }

        private void FrmNovosVeiculos_Load(object sender, EventArgs e)
        {
            if (lista == null)
            {
                Close();
                return;
            }

            webBrowser1.AllowNavigation = true;
            webBrowser1.Navigate("about:blank");
            if (webBrowser1.Document != null)
            {
                webBrowser1.Document.Write(string.Empty);
            }
            
            webBrowser1.DocumentText = pagina.Replace("XXXXXXXMODELOSXXXXXXX", lista);
        }

        private void FrmNovosVeiculos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cbVisualizado.Checked)
            {
                GravaVersaoDesenhos();
            }

            e.Cancel = false;
        }
    }
}
