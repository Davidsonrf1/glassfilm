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
                sb.Append(string.Format("\t\"{0}\": [\n", tbName));

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

                sb.Append("\n]}\n");
                json = sb.ToString();

                return true;
            }

            return false;
        }

        public static void SyncDown(int versao)
        {
            var request = (HttpWebRequest)WebRequest.Create(GlassService.GetUrl("sync_down.php"));

            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            string json = string.Format("{\"versaoMinima\": \"{0}\"}", versao);

            byte[] data = Encoding.UTF8.GetBytes(json);
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

            string keyvalue = "''";

            foreach (XmlNode n in node.ChildNodes)
            {
                if (n.Name.Equals(tbKey, StringComparison.InvariantCultureIgnoreCase))
                {
                    keyvalue = string.Format("'{0}'", n.InnerText);
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
                sb.Append(" VALUES(");

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

                    first = false;   
                }
            }
            else
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
                        RestoreData(json);

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
