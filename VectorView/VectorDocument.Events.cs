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
        MouseState mouseState = new MouseState();

        VectorShape mouseHitShape = null;
        VectorEdge mouseHitEdge = null;
        VectorPoint mouseHitPoint = null;

        void UpdateHitObjects()
        {
            debugPoints.Clear();

            mouseHitShape = null;
            mouseHitEdge = null;
            mouseHitPoint = null;

            foreach (VectorShape s in shapes)
            {
                if(s.HitTest(mouseState.MousePos.X, mouseState.MousePos.Y))
                {
                    mouseHitShape = s;
                }

                foreach (VectorEdge e in s.Edges())
                {
                    List<PointF> pts = new List<PointF>();
                       
                    if(e.CrossPointCount(mouseState.MousePos.Y, pts) > 0)
                    {
                        foreach (PointF p in pts)
                        {
                            if (Math.Abs(p.X - mouseState.MousePos.X) <= 4)
                                mouseHitEdge = e;
                        }
                    }
                }

                foreach (VectorPoint p in s.Points())
                {
                    if (mouseState.MousePos.X >= p.X - 3 && mouseState.MousePos.X <= p.X + 3)
                        if (mouseState.MousePos.Y >= p.Y - 3 && mouseState.MousePos.Y <= p.Y + 3)
                            mouseHitPoint = p;
                }
            }

        }

        public VectorEdge MouseHitEdge
        {
            get
            {
                return mouseHitEdge;
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

        public virtual void MouseUp(float x, float y, MouseButtons bt)
        {
            PointF p = ViewToDocumentPoint(x, y);
            MouseButton bts = MouseButton.Left;

            switch (bt)
            {
                case MouseButtons.Left:
                    bts = MouseButton.Left;
                    break;
                case MouseButtons.Right:
                    bts = MouseButton.Right;
                    break;
                case MouseButtons.Middle:
                    bts = MouseButton.Middle;
                    break;
            }

            mouseState.MouseUp(p.X, p.Y, bts);
        }
        
        public virtual void MouseDown(float x, float y, MouseButtons bt)
        {
            PointF p = ViewToDocumentPoint(x, y);
            MouseButton bts = MouseButton.Left;

            switch (bt)
            {
                case MouseButtons.Left:
                    bts = MouseButton.Left;
                    break;
                case MouseButtons.Right:
                    bts = MouseButton.Right;
                    break;
                case MouseButtons.Middle:
                    bts = MouseButton.Middle;
                    break;
            }

            mouseState.MouseDown(p.X, p.Y, bts);
        }

        public virtual void MouseMove(float x, float y)
        {
            PointF p = ViewToDocumentPoint(x, y);
            mouseState.MouseMove(p.X, p.Y);

            UpdateHitObjects();
        }
    }
}
