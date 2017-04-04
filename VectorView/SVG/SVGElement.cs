using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace VectorView.SVG
{
    public abstract class SVGElement
    {
        List<SVGElement> childs = new List<SVGElement>();
        string id = null;

        public List<SVGElement> Childs
        {
            get
            {
                return childs;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }
             
            set
            {
                id = value;
            }
        }

        public abstract XmlNode GetNode(XmlDocument doc, XmlNode parent);
    }
}
