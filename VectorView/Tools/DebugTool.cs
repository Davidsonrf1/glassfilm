using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView.Tools
{
    public class DebugTool: VectorTool
    {
        public DebugTool(string name, VectorDocument doc): base(name, doc)
        {

        }

        public override void Render()
        {
            /*
            PointF mousePos = Doc.DocumentToViewPoint(Doc.MouseState.MousePos.X, Doc.MouseState.MousePos.Y);
            PointF mouseDownPos = Doc.DocumentToViewPoint(Doc.MouseState.MouseDownPos.X, Doc.MouseState.MouseDownPos.Y);
            PointF mouseUpPos = Doc.DocumentToViewPoint(Doc.MouseState.MouseUpPos.X, Doc.MouseState.MouseUpPos.Y);

            g.FillEllipse(Brushes.OrangeRed, mousePos.X - 3, mousePos.Y - 3, 6, 6);
            g.FillEllipse(Brushes.Red, mouseDownPos.X - 3, mouseDownPos.Y - 3, 6, 6);
            g.FillEllipse(Brushes.DarkGreen, mouseUpPos.X - 3, mouseUpPos.Y - 3, 6, 6);
            */
        }
    }
}
