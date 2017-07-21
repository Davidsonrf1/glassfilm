using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Security.Cryptography;

namespace GlassFilm.Class
{
    class HashMachine
    {       
        public string getHashMachine()
        {
            //string hd = identifier("Win32_DiskDrive", "Model");            
            //string totalHd = identifier("Win32_DiskDrive", "TotalHeads");
            string matherBoard = getMotherBoardID();

            //string fmi = hd + totalHd + matherBoard;
            string fmi = matherBoard + 19991;
            
            return criptoMD5(fmi,"tech");            
        }

        public bool compareHash(string hash)
        {
            return getHashMachine() == hash;
        }

        private string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }

        private string getMotherBoardID()
        {
            string serial = "";
           
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
                ManagementObjectCollection moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    serial = mo["SerialNumber"].ToString();                 
                }
                return serial;
            }
            catch (Exception) { return serial; }
        }
       
        public static string criptoMD5(string mensagem, string senha)
        {

            byte[] resultado; System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();            
            MD5CryptoServiceProvider hp = new MD5CryptoServiceProvider();
            byte[] key = hp.ComputeHash(UTF8.GetBytes(senha));
            
            TripleDESCryptoServiceProvider tdcsp = new TripleDESCryptoServiceProvider();

            tdcsp.Key = key;
            tdcsp.Mode = CipherMode.ECB;
            tdcsp.Padding = PaddingMode.PKCS7;

            byte[] dataToEncrypt = UTF8.GetBytes(mensagem);

            try
            {
                ICryptoTransform encryptor = tdcsp.CreateEncryptor();
                resultado = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally
            {
                tdcsp.Clear();
                hp.Clear();
            }

            return Convert.ToBase64String(resultado);
        }

        private static string descriptoMD5(string mensagem, string senha)
        {
            byte[] resultado;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            MD5CryptoServiceProvider hp = new MD5CryptoServiceProvider();
            byte[] tDESKey = hp.ComputeHash(UTF8.GetBytes(senha));

            TripleDESCryptoServiceProvider tdcsp = new TripleDESCryptoServiceProvider();

            tdcsp.Key = tDESKey;
            tdcsp.Mode = CipherMode.ECB;
            tdcsp.Padding = PaddingMode.PKCS7;
            
            byte[] dataToDecrypt = Convert.FromBase64String(mensagem);
            
            try
            {
                ICryptoTransform Decryptor = tdcsp.CreateDecryptor();
                resultado = Decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            finally
            {
                tdcsp.Clear();
                hp.Clear();
            }

            return UTF8.GetString(resultado);
        }        
    }
}
