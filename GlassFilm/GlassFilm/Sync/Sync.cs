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

    public static class SyncManager
    {
        static string syncUrl = "";
        static List<string> syncTables = new List<string>();
        static List<string> synckeys = new List<string>();

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

        static string syncStep = "";
        static string syncObject = "";

        static void GetTable(string table)
        {
            table = table.Trim();
            SQLiteConnection con = DBManager._mainConnection;

            if (table.Equals("desenhos", StringComparison.InvariantCultureIgnoreCase))
            {
                table = table.Replace("!", "");
                con = DBManager._modelConnection;
            }

            IDbCommand cmd = con.CreateCommand();

            cmd.CommandText = "SELECT MAX (VERSAO) FROM " + table;
            int versao = int.Parse(cmd.ExecuteScalar().ToString());

            SyncDown(versao);
        }

        static string BuildJSONFromRow(DataRow dr)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\t\t\t\t{");

            bool first = true;
            foreach (DataColumn c in dr.Table.Columns)
            {
                if (!first)
                {
                    sb.Append(", ");
                }

                sb.Append(string.Format("\n\t\t\t\t\"{0}\": \"{1}\"", c.ColumnName, dr[c.ColumnName].ToString().Replace("\"", "\\\"")));

                first = false;
            }

            sb.Append("\n\t\t\t\t}");

            return sb.ToString();
        }

        static bool BuildJSONFromTable(string tbName, DataTable dt, int start, out int count, out string json)
        {
            json = "";
            count = 0;

            StringBuilder sb = new StringBuilder();

            if (start < dt.Rows.Count)
            {
                sb.Append("{\n");
                sb.Append(string.Format("\t\"{0}\": {{\n", tbName));

                string tbKey = tbKeys[tbName];

                sb.Append(string.Format("\t\"table_key\":\"{0}\",\n", tbKey));
                sb.Append("\"rows\": [");

                bool first = true;
                for (int i = start; i < start + 10 && i < dt.Rows.Count; i++)
                {
                    if (!first)
                    {
                        sb.Append(",\n");
                    }

                    sb.Append(BuildJSONFromRow(dt.Rows[i]));

                    count++;
                    first = false;
                }

                sb.Append("\n]}}\n");
                json = sb.ToString();

                return true;
            }

            return false;
        }

        public static void SyncDown(int versao)
        {
            string url = GlassService.GetUrl("sync_down.php");

            var request = (HttpWebRequest)WebRequest.Create(url);

            string postData = "";

            postData += "&tipo=" + "sync_down";
            postData += "&pagina=" + 1.ToString();
            postData += "&versao=" + versao.ToString();
            postData += "&itens=" + 10.ToString();

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
                string responseString = (new StreamReader(response.GetResponseStream())).ReadToEnd();

                if (responseString.Length > 0)
                {
                    RestoreData(responseString);
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
                throw;
            }
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
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.Message);
                throw;
            }
        }

        static void RestoreData(string json)
        {
            XmlDocument xdoc = JsonConvert.DeserializeXmlNode(json, "table");

            foreach (XmlNode n in xdoc.DocumentElement.ChildNodes)
            {
                string tbName = n.Name;
                string tbKey = tbKeys[tbName];

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

                if (table.Equals("desenhos", StringComparison.InvariantCultureIgnoreCase))
                {
                    con = DBManager._modelConnection;
                }

                string tbKey = tbKeys[table];

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
                else if (dbType == DbType.Int32)
                {
                    cmd.Parameters.Add("@" + n.Name, dbType).Value = Convert.ToInt32(n.InnerText);
                }
                else
                {
                    cmd.Parameters.Add("@" + n.Name, dbType).Value = n.InnerText;
                }
            }

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {

            }
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

                    DataTable dt = DBManager.LoadDataTable(string.Format("SELECT * FROM {0} WHERE SINCRONIZAR = 1", tbName), con);

                    int count = 0;
                    int start = 0;
                    string json = "";

                    while (BuildJSONFromTable(tbName, dt, start, out count, out json))
                    {
                        SyncUp(json);
                        start += count;
                    }
                }
                catch (Exception ex)
                {
                    Logs.Log(ex.Message);
                }
            }
        }

        static void SyncIncoming()
        {
            foreach (string tb in syncTables)
            {
                GetTable(tb);
            }
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
