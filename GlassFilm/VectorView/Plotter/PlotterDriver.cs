using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView.Plotter
{
    public abstract class PlotterDriver
    {
        protected abstract string Init();
        protected abstract string Finish();
        protected abstract string GetCommands(List<PointF[]> points);

        List<List<PointF[]>> polygons = new List<List<PointF[]>>();

        public void ClearPolygons()
        {
            polygons.Clear();
        }

        public void AddPolygon(List<PointF[]> poly)
        {
            polygons.Add(poly);
        }

        public string GetCmdString(string sendBefore, string sendAfter)
        {
            StringBuilder sb = new StringBuilder();

            if (sendBefore != null)
                sb.Append(sendBefore);

            sb.Append(Init());

            foreach (List<PointF[]> polyList in polygons)
            {
                sb.Append(GetCommands(polyList));
            }
            
            sb.Append(Finish());

            if (sendAfter != null)
                sb.Append(sendAfter);

            return sb.ToString();
        }
    }
}
