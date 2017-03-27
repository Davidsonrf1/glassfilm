using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace VectorView
{
    public partial class VectorDocument: VectorObject
    {
        void ReadNode(XmlNode node)
        {
            if (ParseSVGElement(node))
                return;

            foreach (XmlNode n in node.ChildNodes)
            {
                ReadNode(n);
            }
        }

        public void Load(XmlDocument doc)
        {
            foreach (XmlNode n in doc.ChildNodes)
            {
                ReadNode(n);
            }            
        }

        public void Load(string xml)
        {

        }

        public void LoadFromFile(string path)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);

            Load(xdoc);
        }
    }
}
