using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView.Plotter
{
    public class PlotterDMPL : PlotterDriver
    {
        const int DMPL_UNIT = 1000; // 0.025 mm
        Point GetDMPLPoint(PointF p)
        {
            Point ret = new Point();

            ret.X = (int)Math.Round(p.X * DMPL_UNIT);
            ret.Y = (int)Math.Round(p.Y * DMPL_UNIT);

            return ret;
        }

        protected override string Finish()
        {
            return "P0 @\n";
        }

        protected override string GetCommands(List<PointF[]> points)
        {
            StringBuilder sb = new StringBuilder();
            Point hp = new Point();

            foreach (PointF[] pts in points)
            {
                hp = GetDMPLPoint(pts[0]);
                sb.Append(string.Format(" U {0},{1}\n", hp.X, hp.Y));

                for (int i = 1; i < pts.Length; i++)
                {
                    hp = GetDMPLPoint(pts[i]);
                    sb.Append(string.Format(" D {0},{1}\n", hp.X, hp.Y));
                }
            }

            sb.Append("\n");

            return sb.ToString();
        }

        protected override string Init()
        {
            return ";:H A EC1\nP1\n";
        }
    }
}
