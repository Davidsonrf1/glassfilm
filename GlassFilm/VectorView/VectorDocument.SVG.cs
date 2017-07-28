using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Svg;
using Svg.Pathing;
using System.IO;
using System.Drawing;
using System.Globalization;

namespace VectorView
{
    public partial class VectorDocument
    {
        float ppi = 96;
        float ppmx = 37.7952728f;  // ppm = Pontos por milímetro
        float ppmy = 37.7952728f;

        public float Ppmx
        {
            get
            {
                return ppmx;
            }
        }

        public float Ppmy
        {
            get
            {
                return ppmy;
            }
        }

        public void LoadSVGFromFile(string file)
        {
            string svg = File.ReadAllText(file, Encoding.UTF8);
            LoadSVG(svg);
        }

        void ParseSvgElement(SvgElement el)
        {
            VectorPath path = null;

            if (el is SvgPolygon)
            {
                SvgPolygon p = (SvgPolygon)el;
                path = CreatePath();

                int len = p.Points.Count;

                if (len % 2 != 0)
                {
                    len--;
                }

                for (int i = 0; i < len; i += 2)
                {
                    float x, y;

                    x = p.Points[i].Value / ppmx;
                    y = p.Points[i + 1].Value / ppmy;

                    if (i == 0)
                    {
                        path.MoveTo(x, y);
                    }
                    else
                    {
                        path.LineTo(x, y);
                    }
                }

                path.ClosePolygon();
            }

            if (el is SvgPath)
            {
                SvgPath p = (SvgPath)el;
                path = CreatePath();

                string tag, side;

                tag = "";
                side = "";
                string guidStr = "";

                path.Side = VectorPathSide.None;

                if (p.TryGetAttribute("gf-tag", out tag))
                    path.Tag = Encoding.UTF8.GetString(Convert.FromBase64String(tag));

                try
                {
                    if (p.TryGetAttribute("gf-guid", out guidStr))
                        path.Guid = new Guid(guidStr);
                }
                catch
                {

                }

                if (p.TryGetAttribute("gf-side", out side))
                {
                    if (side == "left")
                        path.Side = VectorPathSide.Left;

                    if (side == "right")
                        path.Side = VectorPathSide.Right;
                }

                float sx, sy, ex, ey;

                foreach (SvgPathSegment seg in p.PathData)
                {
                    sx = (seg.Start.X / ppmx);
                    sy = (seg.Start.Y / ppmy);
                    ex = (seg.End.X / ppmx);
                    ey = (seg.End.Y / ppmy);

                    if (seg is SvgLineSegment)
                    {
                        path.LineTo(ex, ey);
                    }
                    else if (seg is SvgCubicCurveSegment)
                    {
                        SvgCubicCurveSegment q = (SvgCubicCurveSegment)seg;
                        path.CurveTo(ex, ey, (q.FirstControlPoint.X / ppmx), (q.FirstControlPoint.Y / ppmy), (q.SecondControlPoint.X / ppmx), (q.SecondControlPoint.Y / ppmy));
                    }
                    else if (seg is SvgQuadraticCurveSegment)
                    {
                        SvgQuadraticCurveSegment q = (SvgQuadraticCurveSegment)seg;
                        path.QCurveTo(ex, ey, (q.ControlPoint.X / ppmx), (q.ControlPoint.Y / ppmy) );
                    }
                    else if (seg is SvgClosePathSegment)
                    {
                        path.ClosePolygon();
                    }
                    else if (seg is SvgMoveToSegment)
                    {
                        path.MoveTo(ex, ey);
                    }
                    else
                    {

                    }
                }
            }
            else
            {

            }

            if (path != null)
            {
                path.ClosePath();
            }

            foreach (SvgElement n in el.Children)
            {
                ParseSvgElement(n);
            }
        }

        public float UnitToMilimeter(float value, SvgUnitType type)
        {
            float mm = 0;

            switch (type)
            {
                case SvgUnitType.User:
                case SvgUnitType.Pixel:
                    float inch = value / ppi;
                    mm = (inch * 2.54f) * 10;
                    break;
                case SvgUnitType.Inch:
                    mm = (value * 2.54f) * 10;
                    break;
                case SvgUnitType.Centimeter:
                    mm = value * 10;
                    break;
                case SvgUnitType.Millimeter:
                    mm = value;
                    break;
                case SvgUnitType.None:
                case SvgUnitType.Em:
                case SvgUnitType.Ex:
                case SvgUnitType.Percentage:
                case SvgUnitType.Pica:
                case SvgUnitType.Point:
                    throw new ArgumentOutOfRangeException("Unidade não suportada: " + type.ToString());
                default:
                    throw new ArgumentOutOfRangeException("Unidade não especificada: " + type.ToString());
            }

            return mm;
        }

        public void Normalize()
        {
            float minx = GetMinX();
            float miny = GetMinY();

            foreach (VectorPath p in paths)
            {
                p.SetPos(p.X - minx, p.Y - miny);
            }

            RectangleF r = new RectangleF(0, 0, GetMaxX(), GetMaxY());

            viewBox = new RectangleF(0, 0, r.Width * ppmx, r.Height * ppmy);
            docWidth = r.Width;
            docHeight = r.Height;
        }

        RectangleF viewBox = new RectangleF();

        public string ToSVG()
        {
            Normalize();

            StringBuilder sb = new StringBuilder();

            RectangleF r = Rectangle.Round(GetBoundRect());

            NumberFormatInfo ni = new NumberFormatInfo();
            ni.NumberDecimalSeparator = ".";

            sb.AppendFormat(ni, "<svg xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:cc=\"http://creativecommons.org/ns#\" xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:svg=\"http://www.w3.org/2000/svg\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 {0:0.0000} {1:0.0000}\" width=\"{2:0.0000}mm\" height=\"{3:0.0000}mm\" version=\"1.1\">\n", viewBox.Width, viewBox.Height, docWidth, docHeight);

            foreach (VectorPath s in paths)
            {
                sb.Append("   " + s.ToSVGPath(ppmx, ppmy));
                sb.Append("\n");
            }

            sb.Append("</svg>");
            return sb.ToString();
        }

        public void LoadSVG(string svg)
        {
            File.WriteAllText("last_path.svg", svg);

            paths.Clear();

            XmlDocument xdoc = new XmlDocument();
            xdoc.XmlResolver = null;
            xdoc.LoadXml(svg);
            SvgDocument doc = SvgDocument.Open(xdoc);

            ppi = doc.Ppi;

            float vw = UnitToMilimeter(doc.Width.Value, doc.Width.Type);
            float vh = UnitToMilimeter(doc.Height.Value, doc.Height.Type);

            RectangleF vb = new RectangleF(doc.ViewBox.MinX, doc.ViewBox.MinY, doc.ViewBox.Width, doc.ViewBox.Height);

            ppmx = vb.Width / vw;
            ppmy = vb.Height / vh;

            docWidth = vw;
            docHeight = vh;

            foreach (SvgElement e in doc.Children)
            {
                ParseSvgElement(e);
            }

            Normalize();
        }
    }
}
