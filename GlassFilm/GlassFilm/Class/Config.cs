using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassFilm.Class
{
    public class Config
    {
        Dictionary<string, ConfigValue> valores = new Dictionary<string, ConfigValue>(StringComparer.OrdinalIgnoreCase);

        public string GetValue(string name, string defValue = null)
        {
            string val = defValue;

            ConfigValue cv = valores[name];

            if (string.IsNullOrEmpty(cv.Valor.Trim()))
            {
                if (defValue != null)
                    val = cv.ValorPadrao;
            }
            else
            {
                val = cv.Valor;
            }

            return val;
        }

        public void SetValue(string name, string val)
        {
            ConfigValue cv = null;

            if (!valores.TryGetValue(name, out cv))
            {
                throw new IndexOutOfRangeException("Nome da configuração não definido");
            }

            cv.Valor = val;
        }

        public void RegValue(string name, string defValue)
        {
            ConfigValue cv = null;

            if (valores.TryGetValue(name, out cv))
            {
                return;
            }

            cv = new ConfigValue(name, defValue);
            valores[cv.Nome] = cv;
        }

        public bool TemConfig()
        {
            if (DBManager.Count("PARAMETROS") > 0)
            {
                return true;
            }

            return false;
        }

        public void SaveConfig()
        {
            List<ConfigValue> cl = new List<Class.ConfigValue>();

            cl.AddRange(valores.Values);
            DBManager.GravaConfig(cl);
        }

        public void LoadConfig()
        {
            List<ConfigValue> cl = DBManager.CarregaConfig();

            valores.Clear();

            foreach (ConfigValue c in cl)
            {
                valores.Add(c.Nome, c);
            }
        }

        public string this[string key]
        {
            get
            {
                return GetValue(key);
            }

            set
            {
                SetValue(key, value);
            }
        }
    }
}
