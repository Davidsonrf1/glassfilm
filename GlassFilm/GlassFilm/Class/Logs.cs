using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace GlassFilm
{
    public class Logs
    {
        private string local = Application.StartupPath;
        public string Local
        {
            get { return local; }
            set { local = value; }
        }

        private string nomeArquivo = "\\log.txt";
        public string NomeArquivo
        {
            get { return nomeArquivo; }
            set { nomeArquivo = value; }
        }
        
        /// <summary>
        /// Construtor que inicia o Logs
        /// </summary>
        public Logs()
        {
            criaArquivo();                       
        }
            
        /// <summary>
        /// Função que Cria e Adiciona Logs
        /// </summary>
        /// <param name="texto"></param>
        public static void Log(string texto)
        {
            try
            {            
                FileStream fs = new FileStream(Application.StartupPath + "\\log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                List<string> listaErros = new List<string>();
                StreamReader sr = new StreamReader(fs);

                while (!sr.EndOfStream)                           
                  listaErros.Add(sr.ReadLine());
                        
                sr.Close();
                sr.Dispose();

                FileStream fss = new FileStream(Application.StartupPath + "\\log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fss);
            
                string data = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

                foreach (string errosAnt in listaErros)            
                    sw.WriteLine(errosAnt);    
                        
                sw.WriteLine(data + " - " + texto);

                sw.Flush();
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
                fss.Close();
                fss.Dispose();
                listaErros.Clear();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Função que Cria o Arquivo de Log
        /// </summary>
        public void criaArquivo()
        {
            if (File.Exists(this.local + this.nomeArquivo))
            {
                FileInfo fi = new FileInfo(this.local + this.nomeArquivo);
                long tamanho = fi.Length;

                if (tamanho == 200000)
                    File.Delete(this.local + this.nomeArquivo);
            }

            if (!Directory.Exists(this.local))
                Directory.CreateDirectory(this.local);

            if (!File.Exists(this.local + this.nomeArquivo))
                File.Create(this.local + this.nomeArquivo);            
        }
    }
}
