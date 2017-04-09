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
    }
}
