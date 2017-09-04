using GlassFilm.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace GlassFilm.Sync
{
    public enum SyncType { Incoming, Outgoing, Both }
    public delegate void UpdateSyncStatus(SyncStatus status);
    
    public class SyncStatus
    {
        int total=0, atual=0;

        string objeto = "";
        string acao = "";

        bool show = true;
        int count = 0;

        public int Atual
        {
            get
            {
                return atual;
            }

            set
            {
                atual = value;
            }
        }

        public int Total
        {
            get
            {
                return total;
            }

            set
            {
                total = value;
            }
        }

        public string Objeto
        {
            get
            {
                return objeto;
            }

            set
            {
                objeto = value;
            }
        }

        public string Acao
        {
            get
            {
                return acao;
            }

            set
            {
                acao = value;
            }
        }

        public bool Show
        {
            get
            {
                return show;
            }

            set
            {
                show = value;
            }
        }

        public int Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
            }
        }
    }

    public static class SyncManager
    {
        static string syncUrl = "";
        static List<string> syncTables = new List<string>();
        static List<string> synckeys = new List<string>();

        static UpdateSyncStatus syncStatus = null;

        public static string SyncUrl
        {
            get
            {
                return syncUrl;
            }

            set
            {
                syncUrl = value;
            }
        }

        public static List<string> SyncTables
        {
            get
            {
                return syncTables;
            }

            set
            {
                syncTables = value;
            }
        }

        public static List<string> Synckeys
        {
            get
            {
                return synckeys;
            }

            set
            {
                synckeys = value;
            }
        }

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

        public static bool ForceAll
        {
            get
            {
                return forceAll;
            }

            set
            {
                forceAll = value;
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

        static void GetTable(string table)
        {
            table = table.Trim();
            SQLiteConnection con = DBManager._mainConnection;

            if (table.Equals("!desenhos", StringComparison.InvariantCultureIgnoreCase))
            {
                table = table.Replace("!", "");
                con = DBManager._modelConnection;
            }

            IDbCommand cmd = con.CreateCommand();

            if (forceAll)
            {
                cmd.CommandText = "DELETE FROM " + table;
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "SELECT MAX (VERSAO) FROM " + table;

            string ver = cmd.ExecuteScalar().ToString();
            if (string.IsNullOrEmpty(ver))
                ver = "0";

            int versao = int.Parse(ver);

            if (forceAll)
                versao = 0;

            SyncDown(table, versao);
        }

        static int GetTableVersion(string table)
        {
            table = table.Trim();
            SQLiteConnection con = DBManager._mainConnection;

            if (table.Equals("!desenhos", StringComparison.InvariantCultureIgnoreCase))
            {
                table = table.Replace("!", "");
                con = DBManager._modelConnection;
            }

            IDbCommand cmd = con.CreateCommand();

            cmd.CommandText = "SELECT MAX (VERSAO) FROM " + table;

            string ver = cmd.ExecuteScalar().ToString();

            if (string.IsNullOrEmpty(ver))
                ver = "0";

            return int.Parse(ver);           
        }

        static string BuildJSONFromRow(DataRow dr, string tbName, SQLiteConnection con)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\t\t\t\t{");

            Dictionary<string, string> tbInfo = DBManager.GetTableInfo(tbName, con);

            bool first = true;
            foreach (DataColumn c in dr.Table.Columns)
            {
                if (!first)
                {
                    sb.Append(", ");
                }

                string colType = tbInfo[c.ColumnName];
                DbType dbType = DBManager.GetDbType(colType);

                string val = "";

                if (dbType == DbType.Binary)
                {
                    val = Convert.ToBase64String((byte[])dr[c.ColumnName]);
                }
                else if (dbType == DbType.Int32)
                {
                    val = dr[c.ColumnName].ToString();
                }
                else
                {
                    val = dr[c.ColumnName].ToString().Replace("\"", "\\\"");
                }

                sb.Append(string.Format("\n\t\t\t\t\"{0}\": \"{1}\"", c.ColumnName, val));

                first = false;
            }

            sb.Append("\n\t\t\t\t}");

            return sb.ToString();
        }

        static bool BuildJSONFromTable(string tbName, DataTable dt, int start, out int count, out string json, SQLiteConnection con)
        {
            json = "";
            count = 0;

            StringBuilder sb = new StringBuilder();

            if (start < dt.Rows.Count)
            {
                sb.Append("{\n");
                sb.Append(string.Format("\t\"{0}\": {{\n", tbName));

                string tbKey = "";

                if (!tbKeys.TryGetValue(tbName, out tbKey))
                {
                    tbKeys.TryGetValue("!" + tbName, out tbKey);
                }

                sb.Append(string.Format("\t\"table_key\":\"{0}\",\n", tbKey));
                sb.Append("\"rows\": [");

                bool first = true;
                for (int i = start; i < start + 30 && i < dt.Rows.Count; i++)
                {
                    if (!first)
                    {
                        sb.Append(",\n");
                    }

                    sb.Append(BuildJSONFromRow(dt.Rows[i], tbName, con));

                    count++;
                    first = false;
                }

                sb.Append("\n]}}\n");
                json = sb.ToString();

                return true;
            }

            return false;
        }

        public static void SyncDown(string tabela, int versao)
        {
            string url = GlassService.GetUrl("sync_down.php");

            string responseString = "";
            int pagina = 0;

            do
            {
                var request = (HttpWebRequest)WebRequest.Create(url);

                string postData = "";

                postData += "&tipo=" + "sync_down";
                postData += "&pagina=" + pagina.ToString();
                postData += "&versao=" + versao.ToString();
                postData += "&tabela=" + tabela;
                postData += "&itens=10";

                byte[] data = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    responseString = (new StreamReader(response.GetResponseStream())).ReadToEnd();

                    if (!responseString.StartsWith("eof"))
                    {
                        RestoreData(responseString);
                    }
                    else
                    {
                        responseString = "";
                    }
                }
                catch (Exception ex)
                {
                    Logs.Log(ex.Message);
                    throw;
                }

                pagina++;
            } while (!string.IsNullOrEmpty(responseString.Trim()));
        }

        public static void SyncUp(string json)
        {
            var request = (HttpWebRequest)WebRequest.Create(GlassService.GetUrl("sync_up.php"));

            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            byte[] data = Encoding.UTF8.GetBytes(json);
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string responseString = (new StreamReader(response.GetResponseStream()).ReadToEnd());

                if (responseString.Length > 0)
                {
                    string respJson = "{\"result\":" + responseString + "}";
                    XmlDocument xdoc = JsonConvert.DeserializeXmlNode(respJson , "table");

                    foreach (XmlNode n in xdoc.DocumentElement.ChildNodes)
                    {
                        ConfirmSync(n);
                        Status("Enviando dados para o servidor", "", totalSync, syncCount++, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
                throw;
            }
        }

        static int totalSync = 0;
        static int syncCount = 0;
        static bool forceAll = false;

        public static Dictionary<string, int> SyncCheck(out int total, bool forceAll)
        {
            StringBuilder sb = new StringBuilder();

            SyncManager.forceAll = forceAll;

            sb.Append("{\"tabelas\":[");
            bool first = true;
            foreach (string table in syncTables)
            {
                string tbName = table.Replace("!", "");

                if (!first)
                {
                    sb.Append(",");
                }

                int versaoNum = 0;

                if (!forceAll)
                    versaoNum = GetTableVersion(table);

                sb.Append(string.Format("{{\"nome\":\"{0}\",\"versao\":\"{1}\"}}", tbName, versaoNum));

                first = false;
            }

            sb.Append("]}");

            string json = sb.ToString();

            var request = (HttpWebRequest)WebRequest.Create(GlassService.GetUrl("sync_check.php"));

            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            byte[] data = Encoding.UTF8.GetBytes(json);
            request.ContentLength = data.Length;

            Dictionary<string, int> ret = new Dictionary<string, int>();

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            total = 0;

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string responseString = (new StreamReader(response.GetResponseStream()).ReadToEnd());

                if (responseString.Length > 0)
                {
                    XmlDocument xdoc = JsonConvert.DeserializeXmlNode(responseString, "table");

                    foreach (XmlNode n in xdoc.DocumentElement.ChildNodes)
                    {
                        string tb = "";
                        int qtd = 0;

                        foreach (XmlNode nt in n.ChildNodes)
                        {
                            if (nt.Name.Equals("NOME", StringComparison.InvariantCultureIgnoreCase))
                                tb = nt.InnerText;

                            if (nt.Name.Equals("QTD", StringComparison.InvariantCultureIgnoreCase))
                                int.TryParse(nt.InnerText, out qtd);
                        }

                        total += qtd;

                        ret.Add(tb, qtd);
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
                throw;
            }

            syncCount = 0;
            totalSync = total;
            return ret;
        }

        static void RestoreData(string json)
        {
            if (string.IsNullOrEmpty(json))
                return;

            XmlDocument xdoc = JsonConvert.DeserializeXmlNode(json, "table");

            foreach (XmlNode n in xdoc.DocumentElement.ChildNodes)
            {
                string tbName = n.Name;
                string tbKey = "";

                if (!tbKeys.TryGetValue(tbName, out tbKey))
                {
                    tbKeys.TryGetValue("!" + tbName, out tbKey);
                }

                RestoreTable(n, tbName, tbKey);
            }
        }

        static void ConfirmSync(XmlNode node)
        {
            string table = "";
            int id = 0;
            bool status = false;

            foreach (XmlNode n in node.ChildNodes)
            {

                if (n.Name.Equals("tabela", StringComparison.InvariantCultureIgnoreCase))
                {
                    table = n.InnerText;
                }

                if (n.Name.Equals("codigo", StringComparison.InvariantCultureIgnoreCase))
                {
                    id = int.Parse(n.InnerText);
                }

                if (n.Name.Equals("inserido", StringComparison.InvariantCultureIgnoreCase))
                {
                    status = bool.Parse(n.InnerText);
                }
            }

            if (status)
            {
                SQLiteConnection con = DBManager._mainConnection;
                string tbKey = "";
                if (table.Equals("desenhos", StringComparison.InvariantCultureIgnoreCase))
                {
                    con = DBManager._modelConnection;
                    tbKey = tbKeys["!"+table];
                }
                else
                {
                    tbKey = tbKeys[table];
                }
                

                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = string.Format("update {0} set sincronizar = 0 where {1} = {2}", table, tbKey, id);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    
                }
            }
        }

        static void RestoreTable(XmlNode node, string tbName, string tbKey)
        {
            tbName = tbName.Trim();
            SQLiteConnection con = DBManager._mainConnection;

            // TODO: pensar numa forma menos porca de fazer isso, mas segundo o Diego, "funcionou tá bom"
            if (tbName.Equals("desenhos", StringComparison.InvariantCultureIgnoreCase))
            {
                tbName = tbName.Replace("!", "");
                con = DBManager._modelConnection;
            }

            string keyvalue = "";

            foreach (XmlNode n in node.ChildNodes)
            {
                if (n.Name.Equals(tbKey, StringComparison.InvariantCultureIgnoreCase))
                {
                    keyvalue = string.Format("{0}", n.InnerText);
                    break;
                }
            }

            SQLiteCommand cmd = con.CreateCommand();

            cmd.CommandText = string.Format("select count(*) from {0} where {1}={2}", tbName, tbKey, keyvalue);
            int count = 0;
            int.TryParse(cmd.ExecuteScalar().ToString(), out count);

            if (count == 0)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("INSERT INTO " + tbName);
                
                StringBuilder values = new StringBuilder();
                StringBuilder names = new StringBuilder();

                bool first = true;

                foreach (XmlNode n in node.ChildNodes)
                {
                    if (!first)
                    {
                        values.Append(',');
                        names.Append(',');
                    }

                    names.Append(n.Name);
                    values.Append("@" + n.Name);

                    first = false;   
                }

                sb.Append("(" + names.ToString() + ")");
                sb.Append(" VALUES(" + values.ToString() + ")");

                cmd.CommandText = sb.ToString();
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("UPDATE " + tbName + " SET ");

                bool first = true;

                foreach (XmlNode n in node.ChildNodes)
                {
                    if (!first)
                    {
                        sb.Append(',');
                    }

                    sb.Append(n.Name);
                    sb.Append("=@");
                    sb.Append(n.Name);

                    first = false;
                }

                sb.Append(" WHERE " + tbKey + "=@_" + tbKey);

                cmd.CommandText = sb.ToString();

                cmd.Parameters.Add("@_" + tbKey, DbType.Int32).Value = Convert.ToInt32(keyvalue);
                
            }

            Dictionary<string, string> tbInfo = DBManager.GetTableInfo(tbName, con);            

            foreach (XmlNode n in node.ChildNodes)
            {
                string colType = tbInfo[n.Name];
                DbType dbType = DBManager.GetDbType(colType);

                if (dbType == DbType.Binary)
                {
                    byte[] data = Convert.FromBase64String(n.InnerText);
                    cmd.Parameters.Add("@" + n.Name, DbType.Binary, data.Length).Value = data;
                }
                else
                {
                    string val = n.InnerText;

                    if (string.IsNullOrEmpty(val))
                        val = "null";

                    cmd.Parameters.Add("@" + n.Name, dbType).Value = val;
                }
            }

            try
            {
                cmd.ExecuteNonQuery();
                Status("Recebendo atualizações...", tbName, totalSync, syncCount++, 1);
            }
            catch
            {

            }
        }

        public static int GetSendCount(string table)
        {
            if (DBManager.conectado())
            {
                try
                {
                    string tbName = table.Trim();
                    SQLiteConnection con = DBManager._mainConnection;

                    if (tbName.StartsWith("!"))
                    {
                        tbName = tbName.Replace("!", "");
                        con = DBManager._modelConnection;
                    }

                    IDbCommand cmd = con.CreateCommand();

                    cmd.CommandText = string.Format("SELECT count(*) qtd FROM {0} WHERE SINCRONIZAR = 1", tbName);
                    return Convert.ToInt32(cmd.ExecuteScalar().ToString());
                }
                catch (Exception ex)
                {
                    Logs.Log(ex.Message);
                }
                finally
                {
                    Status(false, 0);
                }
            }

            return 0;
        }

        public static int GetSendCount()
        {
            totalSync = 0;
            syncCount = 0;

            int count = 0;
            foreach (string tb in syncTables)
            {
                count += GetSendCount(tb);
            }
            return totalSync=count;
        }

        static void SendTable(string table)
        {
            if (DBManager.conectado())
            {
                try
                {
                    string tbName = table.Trim();
                    SQLiteConnection con = DBManager._mainConnection;

                    if (tbName.StartsWith("!"))
                    {
                        tbName = tbName.Replace("!", "");
                        con = DBManager._modelConnection;
                    }

                    IDbCommand cmd = con.CreateCommand();                    

                    cmd.CommandText = string.Format("SELECT count(*) qtd FROM {0} WHERE SINCRONIZAR = 1", tbName);
                    int total = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    if (total > 0)
                        Status(true, 0);

                    DataTable dt = DBManager.LoadDataTable(string.Format("SELECT * FROM {0} WHERE SINCRONIZAR = 1", tbName), con);

                    int count = 0;
                    int start = 0;
                    string json = "";

                    Status("Enviando dados para o servidor", tbName, total, start, count);

                    while (BuildJSONFromTable(tbName, dt, start, out count, out json, con))
                    {
                        SyncUp(json);
                        start += count;

                    }                    
                }
                catch (Exception ex)
                {
                    Logs.Log(ex.Message);
                }
                finally
                {
                    Status(false, 0);
                }
            }
        }

        static void EliminarRegistros()
        {
            SQLiteCommand cmd = DBManager._mainConnection.CreateCommand();

            cmd.CommandText = "SELECT * FROM ELIMINA_REGISTRO WHERE ELIMINADA = 0";

            IDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            dt.Load(dr);
            dr.Close();

            Status("Eliminando registros...", "", dt.Rows.Count, 0, 0);

            int val = 0;

            foreach (DataRow row in dt.Rows)
            {
                string tb = row["TABELA"].ToString();
                string cod = row["CODIGO"].ToString();
                string keyName = tbKeys[tb];

                cmd.CommandText = string.Format("DELETE FROM {0} WHERE {1}={2}", tb, keyName, cod);
                cmd.ExecuteNonQuery();

                Status("Eliminando registros...", "", dt.Rows.Count, ++val, 1);
            }

            cmd.CommandText = string.Format("UPDATE ELIMINA_REGISTRO SET ELIMINADA = 1, SINCRONIZAR = 0");
            cmd.ExecuteNonQuery();
        }

        static void SyncIncoming()
        {
            foreach (string tb in syncTables)
            {
                GetTable(tb);
            }

            EliminarRegistros();
        }

        static void SyncOutgoing()
        {
            foreach (string tb in syncTables)
            {
                SendTable(tb);
            }
        }

        public static void CheckTables()
        {

            foreach (string tb in syncTables)
            {
                try
                {
                    string tbName = tb.Trim();
                    SQLiteConnection con = DBManager._mainConnection;

                    if (tbName.StartsWith("!"))
                    {
                        tbName = tbName.Replace("!", "");
                        con = DBManager._modelConnection;
                    }

                    if (!DBManager.ColExiste(tbName, "sincronizar", con))
                    {
                        SQLiteCommand cmd = con.CreateCommand();

                        cmd.CommandText = string.Format("alter table {0} add {1} integer default 1", tbName, "sincronizar");
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = string.Format("update {0} set sincronizar = 1", tbName);
                        cmd.ExecuteNonQuery();
                    }

                    if (!DBManager.ColExiste(tbName, "versao", con))
                    {
                        SQLiteCommand cmd = con.CreateCommand();

                        cmd.CommandText = string.Format("alter table {0} add {1} integer default 0", tbName, "versao");
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = string.Format("update {0} set versao = 0", tbName);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch
                {

                }
            }

        }

        static Dictionary<string, string> tbKeys = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        public static void Syncronize(SyncType type)
        {
            tbKeys.Clear();
            for (int i = 0; i < syncTables.Count; i++)
            {
                tbKeys.Add(syncTables[i], synckeys[i]);
            }

            if (type == SyncType.Incoming || type == SyncType.Both)
                SyncIncoming();

            if (type == SyncType.Outgoing || type == SyncType.Both)
                SyncOutgoing();
        }
    }
}
