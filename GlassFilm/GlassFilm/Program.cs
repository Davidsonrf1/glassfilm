using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GlassFilm.Class;


namespace GlassFilm
{
    static class Program
    {
        public static DialogResult ShowDialog(Form dialog)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form f in fc)
            {
                if (f.GetType().Equals(dialog.GetType()))
                {
                    Mensagens.Atencao("Rotina já está aberta!");
                    return DialogResult.None; 
                }
            }
            
            dialog.ShowInTaskbar = false;
            return dialog.ShowDialog();
        }

        static Config config = new Config();
        public static Config Config
        {
            get
            {
                return config;
            }
        }

        public static void InitConfig ()
        {
            config.RegValue("FilmePadrao", "0");
            config.RegValue("PlotterLang", "HPGL");
            config.RegValue("PlotterInterface", "PRINTER");
            config.RegValue("PlotterName", "");
            config.RegValue("PlotterCOMSpeed", "57600");
            config.RegValue("PlotterCOMDataBits", "8");
            config.RegValue("PlotterCOMParity", "N");
            config.RegValue("PlotterCOMStopBits", "1");
            config.RegValue("PlotterCOMFollowControl", "0");
            config.RegValue("RotateCut", "True");
            config.RegValue("FlipX", "False");
            config.RegValue("forceAutoNest", "False");
            config.RegValue("margin", "0");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DBManager.InitDB();

            Sync.SyncFullDatabase.VerificaAtualizacoes();
            //Sync.SyncFullDatabase.SendDatabase();
            Sync.SyncFullDatabase.GetDatabase();

            InitConfig();
            config.LoadConfig();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmPrincipal());
            //Application.Run(new FrmCadastroDesenho());       

            config.SaveConfig();     
        }
    }
}
