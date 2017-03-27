using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace VectorView
{
    public partial class VectorDocument
    {
        PointF mouseDownPos = new PointF();
        PointF mouseUpPos = new PointF();
        PointF mousePos = new PointF();

        PointF mouseViewDownPos = new PointF();
        PointF mouseViewUpPos = new PointF();
        PointF mouseViewPos = new PointF();



        public PointF MouseDownPos
        {
            get
            {
                return mouseDownPos;
            }
        }

        public PointF MouseUpPos
        {
            get
            {
                return mouseUpPos;
            }
        }

        public PointF MousePos
        {
            get
            {
                return mousePos;
            }
        }

        public PointF ViewToDocumentPoint(float posx, float posy)
        {
            posx *= 1 / scale;
            posy *= 1 / scale;

            return new PointF(posx - offsetX, posy - offsetY);
        }
        
        public PointF DocumentToViewPoint(float posx, float posy)
        {
            posx = (posx + offsetX) * scale;
            posy = (posy + offsetY) * scale;

            return new PointF(posx, posy);
        }

        public virtual void MouseUp(float x, float y)
        {
            PointF p = ViewToDocumentPoint(x, y);

            mouseUpPos.X = p.X;
            mouseUpPos.Y = p.Y;

            mouseViewUpPos = DocumentToViewPoint(x, y);
        }
        
        public virtual void MouseDown(float x, float y)
        {
            PointF p = ViewToDocumentPoint(x, y);

            mouseDownPos.X = p.X;
            mouseDownPos.Y = p.Y;

            mouseViewDownPos = DocumentToViewPoint(x, y);
        }

        public virtual void MouseMove(float x, float y)
        {
            PointF p = ViewToDocumentPoint(x, y);

            mousePos.X = p.X;
            mousePos.Y = p.Y;

            mouseViewPos = DocumentToViewPoint(x, y);
        }
    }
}
