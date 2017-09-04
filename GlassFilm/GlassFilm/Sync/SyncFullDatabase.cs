using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.IO.Compression;
using GlassFilm.Class;
using System.Security.Cryptography;

namespace GlassFilm.Sync
{
    public class SyncFullDatabase
    {
        static string ftpUser = @"zadmin_zadmin_cutcutbase";
        static string ftpPass = @"q{Ywe<?Scn";


        //static string ftpUser = "⁠⁠⁠⁠⁠zadmin_cutcut";
        //static string ftpPass = "q{Ywe<?Scn6^yGT";

        public static string BuildDbFile()
        {
            if (File.Exists("CutFilmDB.zip"))
            {
                File.Delete("CutFilmDB.zip");
            }

            FileStream fs = File.Open("CutFilmDB.zip", FileMode.Create, FileAccess.ReadWrite);

            FileInfo modelosFi = new FileInfo("modelos.db");
            FileInfo glassFi = new FileInfo("GlassFilm.db");

            using (ZipArchive za = new ZipArchive(fs, ZipArchiveMode.Create))
            {
                ZipArchiveEntry zem = za.CreateEntryFromFile(modelosFi.FullName, "modelos.db", CompressionLevel.Optimal);
                ZipArchiveEntry zeg = za.CreateEntryFromFile(glassFi.FullName, "GlassFilm.db", CompressionLevel.Optimal);
            }

            FileInfo cutFilm = new FileInfo("CutFilmDB.zip");
            return cutFilm.FullName;
        }

        public static void SendDatabase()
        {
            DBManager.CloseDatabases();

            string file = BuildDbFile();

            GeraBackupFTP();

            MD5 md5 = MD5.Create();

            byte[] content = File.ReadAllBytes("CutFilmDB.zip");
            byte[] hash = md5.ComputeHash(content);

            File.WriteAllBytes("CutFilmDB.md5", hash);

            FileInfo fi = new FileInfo("CutFilmDB.md5");

            EnviarArquivoFTP(fi.FullName, "CutFilmDB.md5");
            EnviarArquivoFTP(file, "CutFilmDB.zip");
            DBManager.InitDB();
        }

        public static void GetDatabase()
        {
            DBManager.CloseDatabases();

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

                string dest = bkpDir + String.Format("BACKUP_{0:u}_{1}.zip", DateTime.Now, DateTime.Now.Millisecond).Replace(':', '-');

                File.Move(file, dest);
            }
            catch (Exception ex)
            {

            }

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
                
            DBManager.InitDB();
        }

        public static bool VerificaAtualizacoes()
        {
            byte[] local = File.ReadAllBytes("CutFilmDB.md5");

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
                //throw ex;
            }
        }

        public static void BaixarArquivoFTP(string url, string dest)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(url));
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(ftpUser, ftpPass);
                request.UseBinary = true;

                if (File.Exists(dest))
                {
                    File.Delete(dest);
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    using (Stream rs = response.GetResponseStream())
                    {
                        using (FileStream ws = new FileStream(dest, FileMode.Create))
                        {
                            byte[] buffer = new byte[2048];
                            int bytesRead = rs.Read(buffer, 0, buffer.Length);
                            while (bytesRead > 0)
                            {
                                ws.Write(buffer, 0, bytesRead);
                                bytesRead = rs.Read(buffer, 0, buffer.Length);
                            }
                        }
                    }
                }
            }
            catch
            {
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
    }
}
