using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using VectorView.Plotter;

namespace VectorView
{
    public partial class VectorDocument
    {
        class PathComparerX : IComparer<VectorPath>
        {
            public int Compare(VectorPath x, VectorPath y)
            {
                PointF p1 = new PointF(x.X, x.Y);
                PointF p2 = new PointF(y.X, y.Y);

                return x.X < y.X ? -1 : x.X == y.X ? 0 : 1;
            }
        }

        void SortPaths()
        {
            paths.Sort(new PathComparerX());
        }

        public string GeneratePlotterCommands(PlotterDriver driver, bool invertXY, bool flip)
        {
            return GeneratePlotterCommands(null, null, driver, invertXY, flip);
        }

        public string GeneratePlotterCommands(string sendBefore, string sendAfter, PlotterDriver driver, bool invertXY, bool flip)
        {
            SortPaths();

            driver.ClearPolygons();

            foreach (VectorPath p in paths)
            {
                p.GenerateCommands(driver, invertXY, flip, GetMaxY() / 2);
            }

            return driver.GetCmdString(sendBefore, sendAfter);
        }

        public string ToHPGL()
        {
            return ToHPGL(null, null);
        }

        public string ToHPGL(string sendBefore, string sendAfter)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("IN;\nIP;\nSP1;\nPA;\n");

            if (sendBefore != null)
                sb.Append(sendBefore);

            SortPaths();

            foreach (VectorPath s in paths)
            {
                sb.Append(s.ToHPGL());
                sb.Append("\n");
            }

            if (sendAfter != null)
                sb.Append(sendAfter);

            sb.Append("PU0,0\nSP;");

            return sb.ToString();
        }
    }
}
