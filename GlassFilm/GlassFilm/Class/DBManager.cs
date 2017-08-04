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
        public static SQLiteConnection _mainConnection = null;
        public static SQLiteConnection _modelConnection = null;
        public static string _mainDbName = string.Format("Data Source={0};Version=3", "GlassFilm.db");
        public static string _modelDbname = string.Format("Data Source={0};Version=3", "Modelos.db");

        public static void InitDB()
        {
            _mainConnection = new SQLiteConnection(_mainDbName);
            _mainConnection.Open();

            _modelConnection = new SQLiteConnection(_modelDbname);
            _modelConnection.Open();
        }

        public static bool conectado()
        {
            return _mainConnection != null && _mainConnection.State == ConnectionState.Open;
        }

        public static int Count(string tableName)
        {
            SQLiteCommand cmd = _mainConnection.CreateCommand();

            cmd.CommandText = string.Format("SELECT COUNT(*) FROM {0}", tableName);
            int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            return count;
        }

        public static bool ColExiste(string tb, string col, SQLiteConnection con)
        {
            SQLiteCommand cmd = con.CreateCommand();
            
            cmd.CommandText = string.Format("PRAGMA table_info({0})", tb);

            IDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                if (dr["name"].ToString().Equals(col, StringComparison.InvariantCultureIgnoreCase))
                {
                    dr.Close();
                    return true;
                }
            }

            dr.Close();

            return false;
        }

        public static DbType GetDbType(string type)
        {
            if (type.StartsWith("int", StringComparison.InvariantCultureIgnoreCase) ||
                type.StartsWith("uint", StringComparison.InvariantCultureIgnoreCase)) 
            {
                return DbType.Int32;
            }

            if (type.StartsWith("blob", StringComparison.InvariantCultureIgnoreCase))
            {
                return DbType.Binary;
            }

            return DbType.String;
        }

        public static Dictionary<string, string> GetTableInfo(string tb, SQLiteConnection con)
        {
            SQLiteCommand cmd = con.CreateCommand();

            cmd.CommandText = string.Format("PRAGMA table_info({0})", tb);

            IDataReader dr = cmd.ExecuteReader();

            Dictionary<string, string> ret = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            while (dr.Read())
            {
                ret.Add(dr["name"].ToString(), dr["type"].ToString());
            }

            dr.Close();

            return ret;
        }

        public static DataTable LoadDataTable(string cmdText, SQLiteConnection con)
        {
            SQLiteCommand cmd = con.CreateCommand();

            cmd.CommandText = string.Format(cmdText);
            DataTable dt = new DataTable();

            IDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();

            return dt;
        }

        public static List<ConfigValue> CarregaConfig()
        {
            List<ConfigValue> cfg = new List<ConfigValue>();

            SQLiteCommand cmd = _mainConnection.CreateCommand();

            cmd.CommandText = "SELECT * FROM PARAMETROS ORDER BY NOME";
            IDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ConfigValue c = new ConfigValue(Convert.ToInt32(dr["ID"].ToString()), dr["NOME"].ToString(), dr["VALOR_PADRAO"].ToString());
                c.Valor = dr["VALOR"].ToString();
                cfg.Add(c);                
            }

            dr.Close();
            return cfg;
        }

        public static void GravaConfig(List<ConfigValue> cfg)
        {
            SQLiteCommand cmd = _mainConnection.CreateCommand();

            cmd.CommandText = "DELETE FROM PARAMETROS";
            cmd.ExecuteNonQuery();

            foreach (ConfigValue c in cfg)
            {
                SQLiteParameter p; 

                cmd.CommandText = "INSERT INTO PARAMETROS(NOME, VALOR_PADRAO, VALOR) VALUES(@NOME, @VALOR_PADRAO, @VALOR)";

                p = new SQLiteParameter("NOME", DbType.String);
                p.Value = c.Nome;
                cmd.Parameters.Add(p);

                p = new SQLiteParameter("VALOR_PADRAO", DbType.String);
                p.Value = c.ValorPadrao;
                cmd.Parameters.Add(p);

                p = new SQLiteParameter("VALOR", DbType.String);
                p.Value = c.Valor;
                cmd.Parameters.Add(p);

                cmd.ExecuteNonQuery();
            }
        }

        public static void VerificaTabelasAuxiliares()
        {
            SQLiteCommand cmd = _mainConnection.CreateCommand();

            cmd.CommandText = "SELECT COUNT(*) FROM ELIMINA_REGISTRO";
            try
            {
                cmd.ExecuteScalar();
            }
            catch
            {
                cmd.CommandText = "CREATE TABLE ELIMINA_REGISTRO (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, TABELA	TEXT NOT NULL, CODIGO INTEGER NOT NULL, ELIMINADA INTEGER DEFAULT 0)";
                cmd.ExecuteNonQuery();
            }
        }

        public static void EliminaRegistro(string tabela, string codigo)
        {
            SQLiteCommand cmd = _mainConnection.CreateCommand();

            if (tabela != null && codigo != null)
            {
                cmd.CommandText = string.Format("INSERT INTO ELIMINA_REGISTRO (TABELA, CODIGO, SINCRONIZAR) VALUES ('{0}', {1}, 1)", tabela, codigo);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Filme> CarregarRolos()
        {
            List<Filme> rolos = new List<Class.Filme>();

            SQLiteCommand cmd = _mainConnection.CreateCommand();

            cmd.CommandText = "SELECT * FROM ROLO ORDER BY LARGURA";
            IDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
                rolos.Add(new Filme(Convert.ToInt32(dr["ID"].ToString()), Convert.ToInt32(dr["LARGURA"].ToString()), dr["DESCRICAO"].ToString()));

            dr.Close();
            return rolos;
        }

        public static List<Marca> CarregarMarcas(bool todas = false)
        {
            SQLiteCommand cmd = _mainConnection.CreateCommand();

            cmd.CommandText = "SELECT * FROM MARCA " + (!todas ? "WHERE POSSUI_DESENHO = 1" : "") + (!todas ? " AND " : " WHERE ") + "(SELECT CODIGO_MODELO FROM MODELO WHERE CODIGO_MARCA = MARCA.CODIGO_MARCA)>0 ORDER BY MARCA";
            IDataReader dr = cmd.ExecuteReader();

            List<Marca> marcas = new List<Marca>();

            while (dr.Read())
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

        public static List<ModeloAno> CarregaModeloANO(int modelo)
        {
            SQLiteCommand cmd = _mainConnection.CreateCommand();

            cmd.CommandText = "SELECT * FROM MODELO_ANO WHERE CODIGO_MODELO = " + modelo.ToString() + " ORDER BY ANO ASC";
            IDataReader dr = cmd.ExecuteReader();

            List<ModeloAno> lma = new List<ModeloAno>();

            while (dr.Read())
            {
                lma.Add(new ModeloAno(dr["CODIGO_ANO"].ToString(),dr["CODIGO_MODELO"].ToString(), dr["ANO"].ToString()));
            }

            dr.Close();
            return lma;
        }

        public static string CarregarDesenho(int veiculo, out int codigo_desenho)
        {
            codigo_desenho = -1;

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

        public static void SalvarDesenho(int codigo_ano, string svg)
        {
            byte[] svgData = Encoding.UTF8.GetBytes(svg);

            int versao = 0;
            SQLiteCommand cmd = _modelConnection.CreateCommand();

            SQLiteTransaction tr = _modelConnection.BeginTransaction();

            try
            {
                cmd.CommandText = "SELECT IFNULL(MAX(VERSAO), 0) VERSAO FROM DESENHOS";
                versao = Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1;

                cmd.CommandText = "DELETE FROM DESENHOS WHERE VEICULO = " + codigo_ano.ToString();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO DESENHOS (VEICULO, VERSAO, DESENHO, TAMANHO) VALUES (@veic,@versao,@dados,@tamanho)";
                cmd.Parameters.Add("@veic", DbType.Int32).Value = codigo_ano;
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

            cmd.CommandText = "UPDATE MODELO_ANO SET POSSUI_DESENHO = 1 WHERE CODIGO_ANO = " + codigo_ano.ToString();
            cmd.ExecuteNonQuery();

            cmd.CommandText = "UPDATE MODELO SET POSSUI_DESENHO = 1 WHERE CODIGO_MODELO IN (SELECT CODIGO_MODELO FROM MODELO_ANO WHERE POSSUI_DESENHO = 1)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "UPDATE MARCA SET POSSUI_DESENHO = 1 WHERE CODIGO_MARCA IN (SELECT CODIGO_MARCA FROM MODELO WHERE POSSUI_DESENHO = 1)";
            cmd.ExecuteNonQuery();
        }
    }
}
