using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView.Plotter
{
    public class PlotterHPGL : PlotterDriver
    {
        const int HPGL_UNIT = 40; // 0.025mm
        Point GetHPGLPoint(PointF p)
        {
            Point ret = new Point();

            ret.X = (int)Math.Round(p.X * HPGL_UNIT);
            ret.Y = (int)Math.Round(p.Y * HPGL_UNIT);

            return ret;
        }

        protected override string GetCommands(List<PointF[]> points)
        {
            StringBuilder sb = new StringBuilder();
            Point hp = new Point();

            foreach (PointF[] pts in points)
            {
                hp = GetHPGLPoint(pts[0]);
                sb.Append(string.Format("PU;PA{0},{1};\n", hp.X, hp.Y));
                sb.Append("PD;");

                for (int i = 1; i < pts.Length; i++)
                {
                    hp = GetHPGLPoint(pts[i]);
                    sb.Append(string.Format("PA{0},{1};", hp.X, hp.Y));
                }
            }

            sb.Append("\n");

            return sb.ToString();
        }

        protected override string Init()
        {
            //IP0,0,1016,1016;SC0,1016,0,1016;
            return "IN;\nIP;\nSP1;";
        }

        protected override string Finish()
        {
            return "PU;PA0,0;";
        }
    }
}
