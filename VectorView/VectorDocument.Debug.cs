using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace VectorView
{
    public partial class VectorDocument
    {
        List<PointF> debugPoints = new List<PointF>();

        internal void AddDebugPoint(PointF p)
        {
            debugPoints.Add(p);
        }

        void DrawDebugPoints(Graphics g)
        {
            foreach (PointF p in debugPoints)
            {
                g.FillEllipse(Brushes.Red, p.X - 3, p.Y - 3, 6, 6);
            }
        }
    }
}
