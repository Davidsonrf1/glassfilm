using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using VectorView.SVG;
using System.Drawing;

namespace VectorView
{
    public partial class VectorDocument
    {
        void ParseNode(XmlNode node)
        {
            if (node.Name.ToLower() == "path")
            {
                foreach (XmlAttribute a in node.Attributes)
                {
                    if (a.Name == "d")
                    {
                        VectorShape shape = CreateShape();
                        SVGPathData data = new SVGPathData(a.Value);

                        shape.BeginPath(0, 0);

                        bool firstCmd = true;
                        float ox=0, oy=0;

                        foreach (SVGPathCommand cc in data.Cmds)
                        {
                            bool relative = false;

                            if (char.IsLower(cc.Cmd))
                                relative = true;                            

                            char c = char.ToLower(cc.Cmd);

                            float x = 0, y = 0;

                            bool firstPoint = true;
                            if (c == 'm')
                            {
                                if (firstCmd)
                                {
                                    ox = 0;
                                    oy = 0;
                                }

                                while (cc.NextPoint(out x, out y))
                                {
                                    if (relative)
                                    {
                                        x += ox;
                                        y += oy;
                                    }

                                    if (firstPoint)
                                    {
                                        shape.BeginPath(x, y);
                                    }
                                    else
                                    {
                                        shape.LineTo(x, y);
                                    }

                                    firstPoint = false;
                                }

                                ox = x;
                                oy = y;
                            }

                            if (c == 'c')
                            {
                                float x1, x2, y1, y2;
                                x1 = x2 = y1 = y2 = 0;

                                int count = 0;
                                while (cc.NextPoint(out x, out y))
                                {
                                    if (relative)
                                    {
                                        x += ox;
                                        y += oy;
                                    }

                                    switch (count)
                                    {
                                        case 0:
                                            x1 = x;
                                            y1 = y;
                                            count++;
                                            break;
                                        case 1:
                                            x2 = x;
                                            y2 = y;
                                            count++;
                                            break;
                                        default:
                                            ox = x;
                                            ox = y;
                                            shape.CurveTo(x1, y1, x2, y2, x, y);
                                            count = 0;
                                            break;
                                    }

                                }
                            }

                            if (c == 'l')
                            {
                                while (cc.NextPoint(out x, out y))
                                {
                                    if (relative)
                                    {
                                        x += ox;
                                        y += oy;
                                    }

                                    shape.LineTo(x, y);
                                    ox = x;
                                    oy = y;
                                }
                            }

                            if (c == 'h' || c == 'v')
                            {
                                float to = 0;
                                while (cc.NextValue(out to))
                                {
                                    if (relative)
                                    {
                                        if (c == 'h') 
                                            x += ox;
                                        else
                                            x += oy;
                                    }

                                    if (c == 'h')
                                    {
                                        x = to;
                                        y = oy;
                                    }
                                    else
                                    {
                                        x = ox;
                                        y = to;
                                    }

                                    ox = x;
                                    oy = y;

                                    shape.LineTo(x, y);
                                }
                            }

                            if (c == 'z')
                            {
                                shape.EndPath();
                            }

                            firstCmd = false;
                        }
                    }
                }
            }

            foreach (XmlNode n in node.ChildNodes)
            {
                ParseNode(n);
            }
        }

        public void LoadSVGFromFile(string file)
        {
            //string svg = File.ReadAllText(file, Encoding.UTF8);
            //LoadSVG(svg);
        }

        public void LoadSVG(string svg)
        {
            XmlDocument xdoc = new XmlDocument();

            xdoc.LoadXml(svg);

            foreach (XmlNode n in xdoc.ChildNodes)
            {
                ParseNode(n);
            }
        }
    }
}
