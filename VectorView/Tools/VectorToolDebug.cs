using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView.Tools
{
    public class VectorToolDebug: VectorTool
    {
        public VectorToolDebug(VectorDocument doc): base(doc)
        {

        }

        public override void Render(Graphics g)
        {
            PointF mousePos = Doc.DocumentToViewPoint(Doc.MousePos.X, Doc.MousePos.Y);
            PointF mouseDownPos = Doc.DocumentToViewPoint(Doc.MouseDownPos.X, Doc.MouseDownPos.Y);
            PointF mouseUpPos = Doc.DocumentToViewPoint(Doc.MouseUpPos.X, Doc.MouseUpPos.Y);

            g.FillEllipse(Brushes.OrangeRed, mousePos.X - 3, mousePos.Y - 3, 6, 6);
            g.FillEllipse(Brushes.Red, mouseDownPos.X - 3, mouseDownPos.Y - 3, 6, 6);
            g.FillEllipse(Brushes.DarkGreen, mouseUpPos.X - 3, mouseUpPos.Y - 3, 6, 6);
        }
    }
}
