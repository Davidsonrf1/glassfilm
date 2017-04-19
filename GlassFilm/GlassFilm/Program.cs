using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GlassFilm.Class;


namespace GlassFilm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DBManager.InitDB();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmPrincipal());
            Application.Run(new FrmCadastroDesenho());            
        }
    }
}
