using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;
using System.Data;
using System.Data.Common;

namespace GlassFilm.Class
{
    public static class DBManager
    {
        static SQLiteConnection _mainConnection = null;
        static SQLiteConnection _modelConnection = null;
        static string _mainDbName = "GlassFilm.db";
        static string _modelDbname = "Modelos.db";

        public static void InitDB()
        {
            _mainConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3", _mainDbName));
            _mainConnection.Open();

            _modelConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3", _modelDbname));
            _modelConnection.Open();
        }
        
        public static List<Marca> CarregarMarcas(bool todas=false)
        {            
            SQLiteCommand cmd = _mainConnection.CreateCommand();

            cmd.CommandText = "SELECT * FROM MARCA " + (!todas ? "WHERE POSSUI_DESENHO = 1" : "") + " ORDER BY MARCA";
            IDataReader dr = cmd.ExecuteReader();

            List<Marca> marcas = new List<Marca>();

            while(dr.Read())
                marcas.Add(new Marca(Convert.ToInt32(dr["CODIGO_MARCA"].ToString()), dr["MARCA"].ToString()));

            dr.Close();
            return marcas;
        }

        public static List<Modelo> CarregarModelos(int marca, bool todos = false)
        {
            SQLiteCommand cmd = _mainConnection.CreateCommand();

            cmd.CommandText = "SELECT * FROM MODELO WHERE CODIGO_MARCA = " + marca.ToString() + (!todos ? " AND POSSUI_DESENHO = 1" : "") + " ORDER BY MODELO";
            IDataReader dr = cmd.ExecuteReader();

            List<Modelo> modelos = new List<Class.Modelo>();

            while (dr.Read())
                modelos.Add(new Modelo(Convert.ToInt32(dr["CODIGO_MODELO"].ToString()), dr["MODELO"].ToString()));

            dr.Close();
            return modelos;
        }

        public static List<Veiculo> CarregarVeiculos(int modelo, bool todos = false)
        {
            SQLiteCommand cmd = _mainConnection.CreateCommand();

            cmd.CommandText = "SELECT * FROM VEICULO WHERE CODIGO_MODELO = " + modelo.ToString() + (!todos ? " AND POSSUI_DESENHO = 1" : "") + " ORDER BY ANO";
            IDataReader dr = cmd.ExecuteReader();

            List<Veiculo> veiculos = new List<Class.Veiculo>();

            while (dr.Read())
                veiculos.Add(new Veiculo(Convert.ToInt32(dr["VEICULO"].ToString()), dr["ANO"].ToString()));

            dr.Close();
            return veiculos;
        }

        public static string CarregarDesenho(int veiculo)
        {
            SQLiteCommand cmd = _modelConnection.CreateCommand();

            cmd.CommandText = "SELECT * FROM DESENHOS WHERE VEICULO = " + veiculo.ToString();
            IDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int len = Convert.ToInt32(dr["TAMANHO"].ToString());
                byte[] buffer = new byte[len];
                dr.GetBytes(dr.GetOrdinal("DESENHO"), 0, buffer, 0, len);
                dr.Close();

                return Encoding.UTF8.GetString(buffer);                
            }            

            return null;
        }

        public static void SalvarDesenho(int veiculo, string svg)
        {
            byte[] svgData = Encoding.UTF8.GetBytes(svg);

            int versao = 0;
            SQLiteCommand cmd = _modelConnection.CreateCommand();

            SQLiteTransaction tr = _modelConnection.BeginTransaction();

            try
            {
                cmd.CommandText = "SELECT IFNULL(MAX(VERSAO), 0) VERSAO FROM DESENHOS";
                versao = Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1;

                cmd.CommandText = "DELETE FROM DESENHOS WHERE VEICULO = " + veiculo.ToString();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO DESENHOS (VEICULO, VERSAO, DESENHO, TAMANHO) VALUES (@veic,@versao,@dados,@tamanho)";
                cmd.Parameters.Add("@veic", DbType.Int32).Value = veiculo;
                cmd.Parameters.Add("@versao", DbType.Int32).Value = versao;
                cmd.Parameters.Add("@dados", DbType.Binary, svgData.Length).Value = svgData;
                cmd.Parameters.Add("@tamanho", DbType.Int32).Value = svgData.Length;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                tr.Rollback();
                throw;
            }

            tr.Commit();

            cmd = _mainConnection.CreateCommand();

            cmd.CommandText = "UPDATE VEICULO SET POSSUI_DESENHO = 1 WHERE VEICULO = " + veiculo.ToString();
            cmd.ExecuteNonQuery();

            cmd.CommandText = "UPDATE MODELO SET POSSUI_DESENHO = 1 WHERE CODIGO_MODELO IN (SELECT CODIGO_MODELO FROM VEICULO WHERE POSSUI_DESENHO = 1)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "UPDATE MARCA SET POSSUI_DESENHO = 1 WHERE CODIGO_MARCA IN (SELECT CODIGO_MARCA FROM MODELO WHERE POSSUI_DESENHO = 1)";
            cmd.ExecuteNonQuery();
        }
    }
}
