using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.IO.Compression;
using GlassFilm.Class;
using System.Security.Cryptography;
using System.Data;
using System.Windows.Forms;

namespace GlassFilm.Sync
{
    public class SyncFullDatabase
    {
        static string ftpUser = @"zadmin_zadmin_cutcutbase";
        static string ftpPass = @"q{Ywe<?Scn";
        static UpdateSyncStatus syncStatus = null;

        public static UpdateSyncStatus SyncStatus
        {
            get
            {
                return syncStatus;
            }

            set
            {
                syncStatus = value;
            }
        }

        static void Status(bool show, int count)
        {
            if (syncStatus != null)
            {
                SyncStatus status = new SyncStatus();
                status.Show = show;
                status.Count = count;
                syncStatus(status);
            }
        }

        static void Status(string acao, string objeto, int max, int val, int count)
        {
            if (syncStatus != null)
            {
                SyncStatus status = new SyncStatus();

                status.Acao = acao;
                status.Atual = val > max ? max : val;
                status.Total = max < 0 ? 0 : max;
                status.Objeto = objeto;
                status.Count = count;

                syncStatus(status);
            }
        }



        public static string BuildDbFile()
        {
            try
            {
                Status("EFETUANDO BACKUP...", null, 100, 0, 0);

                if (File.Exists("CutFilmDB.zip"))
                {
                    File.Delete("CutFilmDB.zip");
                }

                FileStream fs = File.Open("CutFilmDB.zip", FileMode.Create, FileAccess.ReadWrite);

                FileInfo modelosFi = new FileInfo("modelos.db");
                Status("EFETUANDO BACKUP...", null, 100, 30, 30);
                FileInfo glassFi = new FileInfo("GlassFilm.db");

                using (ZipArchive za = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    Status("EFETUANDO BACKUP...", null, 100, 40, 40);

                    File.Copy(modelosFi.FullName, modelosFi.FullName + ".tmp", true);
                    ZipArchiveEntry zem = za.CreateEntryFromFile(modelosFi.FullName + ".tmp", "modelos.db", CompressionLevel.Optimal);
                    Status("EFETUANDO BACKUP...", null, 100, 60, 60);

                    File.Copy(glassFi.FullName, glassFi.FullName + ".tmp", true);
                    ZipArchiveEntry zeg = za.CreateEntryFromFile(glassFi.FullName + ".tmp", "GlassFilm.db", CompressionLevel.Optimal);
                }

                FileInfo cutFilm = new FileInfo("CutFilmDB.zip");
                Status("EFETUANDO BACKUP...", null, 100, 100, 100);

                return cutFilm.FullName;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return null;
        }

        public static void CriptografarBase(ProgressBar pb)
        {
            IDbCommand cmd = null;

            if (!DBManager.ColExiste("DESENHOS", "DESENHOC", DBManager._modelConnection))
            {
                cmd = DBManager._modelConnection.CreateCommand();
                cmd.CommandText = "ALTER TABLE DESENHOS ADD DESENHOC TEXT DEFAULT 'N'";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "UPDATE DESENHOS SET DESENHOC = 'N'";
                cmd.ExecuteNonQuery();
            }

            cmd = DBManager._modelConnection.CreateCommand();

            cmd.CommandText = "SELECT VEICULO FROM DESENHOS WHERE DESENHOC = 'N'";
            DataTable tb = new DataTable();
            IDataReader dr = cmd.ExecuteReader();
            tb.Load(dr);
            dr.Close();
            int codigo_desenho = 0;
            int count = 0;

            int total = Convert.ToInt16(DBManager.GetNumDesenhos());

            pb.Value = 0;
            pb.Maximum = total;

            foreach (DataRow r in tb.Rows)
            {
                int codigo = int.Parse(r["VEICULO"].ToString());
                string svg = DBManager.CarregarDesenho(codigo, out codigo_desenho);
                DBManager.SalvarDesenho(codigo, svg);

                count++;

                if (count <= total)
                    pb.Value = count;

                if (count %10 == 0)
                    Application.DoEvents();
            }

            pb.Value = pb.Maximum;
        }

        public static void SendDatabase()
        {
            string file = BuildDbFile();

            Status("GERANDO BACKUP NO SERVIDOR...", null, 100, 0, 0);
            GeraBackupFTP();
            Status("GERANDO BACKUP NO SERVIDOR...", null, 100, 100, 100);

            Status("ENVIANDO DADOS...", null, 100, 0, 0);
            MD5 md5 = MD5.Create();

            byte[] content = File.ReadAllBytes("CutFilmDB.zip");
            byte[] hash = md5.ComputeHash(content);

            File.WriteAllBytes("CutFilmDB.md5", hash);

            FileInfo fi = new FileInfo("CutFilmDB.md5");

            EnviarArquivoFTP(fi.FullName, "CutFilmDB.md5");
            Status("ENVIANDO DADOS...", null, 100, 100, 100);

            Status("ENVIANDO DADOS...", null, 100, 0, 0);
            EnviarArquivoFTP(file, "CutFilmDB.zip");
            Status("ENVIANDO DADOS...", null, 100, 100, 100);
        }

        public static void GetDatabase()
        {
            FileInfo modelosFi = new FileInfo("modelos.db");
            FileInfo glassFi = new FileInfo("GlassFilm.db");

            try
            {
                string file = BuildDbFile();

                string bkpDir = Environment.CurrentDirectory + "\\backups\\";

                if (!Directory.Exists(bkpDir))
                {
                    Directory.CreateDirectory(bkpDir);
                }

                try
                {

                    string[] files = Directory.GetFiles(bkpDir);

                    foreach (string f in files)
                    {
                        FileInfo fi = new FileInfo(f);
                        if (fi.LastAccessTime < DateTime.Now.AddMonths(-1))
                        {
                            fi.Delete();
                        }
                    }
                }
                catch
                {

                }

                string dest = bkpDir + String.Format("BACKUP_{0:u}_{1}.zip", DateTime.Now, DateTime.Now.Millisecond).Replace(':', '-');

                File.Move(file, dest);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            try
            {
                modelosFi.Delete();
                glassFi.Delete();

                BaixarArquivoFTP("ftp://cutfilm.com.br/glass/serv/db/CutFilmDB.zip", "CutFilmDB.zip");
                BaixarArquivoFTP("ftp://cutfilm.com.br/glass/serv/db/CutFilmDB.md5", "CutFilmDB.md5");

                FileStream fs = File.Open("CutFilmDB.zip", FileMode.Open, FileAccess.ReadWrite);

                ZipArchive za = new ZipArchive(fs, ZipArchiveMode.Read);

                ZipArchiveEntry ze = za.GetEntry("modelos.db");
                ze.ExtractToFile(modelosFi.FullName);

                ze = za.GetEntry("GlassFilm.db");
                ze.ExtractToFile(glassFi.FullName);

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public static bool VerificaAtualizacoes()
        {
            byte[] local = null;

            if (File.Exists("CutFilmDB.md5"))
                local = File.ReadAllBytes("CutFilmDB.md5");
            else
                local = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            BaixarArquivoFTP("ftp://cutfilm.com.br/glass/serv/db/CutFilmDB.md5", "CutFilmDB.md5.remote");
            byte[] remote = File.ReadAllBytes("CutFilmDB.md5.remote");

            string sl = Convert.ToBase64String(local);
            string sr = Convert.ToBase64String(remote);

            if (!sl.Equals(sr))
            {
                return true;
            }

            return false;
        }

        public static void GeraBackupFTP()
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri("ftp://cutfilm.com.br/glass/serv/db/CutFilmDB.zip"));
                request.Method = WebRequestMethods.Ftp.Rename;
                request.Credentials = new NetworkCredential(ftpUser, ftpPass);
                request.UseBinary = true;

                DateTime dt = DateTime.Now;

                request.RenameTo = string.Format("/glass/serv/db/backups/backup_{0:u}_{1}.zip", dt, dt.Millisecond);

                FtpWebResponse ftpResponse = (FtpWebResponse)request.GetResponse();
                ftpResponse.Close();
                request = null;

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public static void BaixarArquivoFTP(string url, string dest)
        {

            FtpWebResponse response = null;
            FtpWebRequest request = null;

            try
            {
                
                request = (FtpWebRequest)WebRequest.Create(new Uri(url));
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(ftpUser, ftpPass);
                request.UseBinary = true;

                if (File.Exists(dest))
                {
                    File.Delete(dest);
                }

                int max = 0x1FFFFFE;

                using (response = (FtpWebResponse)request.GetResponse())
                {
                    Status("RECEBENDO ATUALIZAÇÃO AGUARDE...", null, 100, 0, 0);

                    using (Stream rs = response.GetResponseStream())
                    {
                        using (FileStream ws = new FileStream(dest, FileMode.Create))
                        {
                            byte[] buffer = new byte[2048];
                            int bytesRead = rs.Read(buffer, 0, buffer.Length);
                            int count = 0;
                            while (bytesRead > 0)
                            {
                                ws.Write(buffer, 0, bytesRead);
                                bytesRead = rs.Read(buffer, 0, buffer.Length);

                                count += bytesRead;

                                if (count > max)
                                    count = max;

                                Status("RECEBENDO ATUALIZAÇÃO AGUARDE...", null, max, count, count);
                            }

                            Status("RECEBENDO ATUALIZAÇÃO AGUARDE...", null, 100, 100, 100);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static void EnviarArquivoFTP(string arquivo, string dest)
        {
            try
            {
                FileInfo arquivoInfo = new FileInfo(arquivo);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri("ftp://cutfilm.com.br/glass/serv/db/"+ dest));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUser, ftpPass);
                request.UseBinary = true;
                request.ContentLength = arquivoInfo.Length;

                Status("ENVIANDO DADOS...", null, (int)arquivoInfo.Length, 0, 0);

                using (FileStream fs = arquivoInfo.OpenRead())
                {
                    byte[] buffer = new byte[2048];
                    int bytesSent = 0;
                    int bytes = 0;
                    using (Stream stream = request.GetRequestStream())
                    {
                        while (bytesSent < arquivoInfo.Length)
                        {
                            bytes = fs.Read(buffer, 0, buffer.Length);
                            stream.Write(buffer, 0, bytes);
                            bytesSent += bytes;

                            Status("ENVIANDO DADOS...", null, (int)arquivoInfo.Length, bytesSent, bytesSent);
                        }
                    }
                }

                Status("ENVIANDO DADOS...", null, (int)arquivoInfo.Length, (int)arquivoInfo.Length, (int)arquivoInfo.Length);
            }
            catch (Exception ex)
            {
                //throw ex;
                Console.Write(ex.Message);
            }
        }
    }
}
