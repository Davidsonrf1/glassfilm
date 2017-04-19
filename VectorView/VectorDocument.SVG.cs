using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using System.Drawing;
using Svg;
using Svg.Pathing;

namespace VectorView
{
    public partial class VectorDocument
    {
        void ParseSvgElement(SvgElement el)
        { 
            if (el is SvgPath)
            {
                SvgPath p = (SvgPath)el;

                VectorShape s = null;
                bool isEnd = false;

                foreach (SvgPathSegment seg in p.PathData)
                {
                    if (s == null)
                    {
                        s = CreateShape();
                        s.BeginPath(seg.End.X, seg.End.Y);

                        continue;
                    }

                    if (seg is SvgLineSegment)
                    {
                        s.LineTo(seg.End.X, seg.End.Y);
                    }
                    else if (seg is SvgCubicCurveSegment)
                    {
                        SvgCubicCurveSegment q = (SvgCubicCurveSegment)seg;
                        s.CurveTo(q.FirstControlPoint.X, q.FirstControlPoint.Y, q.SecondControlPoint.X, q.SecondControlPoint.Y, q.End.X, q.End.Y);
                    }
                    else if (seg is SvgQuadraticCurveSegment)
                    {
                        SvgQuadraticCurveSegment q = (SvgQuadraticCurveSegment)seg;
                        s.QCurveTo(q.ControlPoint.X, q.ControlPoint.Y, q.End.X, q.End.Y);
                    }
                    else if (seg is SvgClosePathSegment)
                    {
                        isEnd = true;
                        s.EndPath(true);
                    }
                    else if (seg is SvgMoveToSegment)
                    {
                        s.MoveTo(seg.End.X, seg.End.Y);
                    }
                    else
                    {

                    }
                }

                if (s != null && !isEnd)
                {
                    s.EndPath(false);
                }
            }
            else
            {

            }

            foreach (SvgElement n in el.Children)
            {
                ParseSvgElement(n);
            }
        }

        public void LoadSVGFromFile(string file)
        {
            string svg = File.ReadAllText(file, Encoding.UTF8);
            LoadSVG(svg);
        }

        public void LoadSVG(string svg)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(svg);
            SvgDocument doc = SvgDocument.Open(xdoc);

            foreach (SvgElement e in doc.Children)
            {
                ParseSvgElement(e);
            }
        }

        public string ToSVG()
        {
            StringBuilder sb = new StringBuilder();

            int id = 1;

            RectangleF r = Rectangle.Round(GetDocSize());
            sb.AppendFormat("<svg xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:cc=\"http://creativecommons.org/ns#\" xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:svg=\"http://www.w3.org/2000/svg\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"{0} {1} {2} {3}\" height=\"{3}\" width=\"{2}\" version=\"1.1\">\n", r.X, r.Y, r.Width, r.Height);

            foreach (VectorShape s in shapes)
            {
                sb.Append("   " + s.ToSVGPath());
                sb.Append("\n");
            }

            sb.Append("</svg>");
            return sb.ToString();
        }

        public void SaveToSVGFile(string path)
        {
            string svg = ToSVG();
            File.WriteAllText(path, svg);
        }
    }
}
