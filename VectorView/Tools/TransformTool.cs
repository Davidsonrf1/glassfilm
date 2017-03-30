using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView.Tools
{
    public class TransformTool : VectorTool
    {
        public TransformTool(string name, VectorDocument doc) : base(name, doc)
        {
        }

        public override void Render()
        {
            base.Render();
        }

        bool isTransforming = false;

        public override void MouseDown(MouseButton bt)
        {
            base.MouseDown(bt);

            if (Document.GetSelectionType() == typeof(VectorPoint))
                isTransforming = true;
        }

        public override void MouseUp(MouseButton bt)
        {
            base.MouseUp(bt);

            if (isTransforming)
            {
                isTransforming = false;
            }
        }

        public override void MouseMove()
        {
            base.MouseMove();

            if (isTransforming)
            {
                if (Document.GetSelectionType() == typeof(VectorPoint))
                {
                    foreach (VectorObject o in Document.SelectedObjects())
                    {
                        VectorPoint p = (VectorPoint)o;

                        p.X = Document.MouseState.Pos.X;
                        p.Y = Document.MouseState.Pos.Y;

                        return;
                    }                    
                }
            }
        }
    }
}
