using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using VectorView.Tools;

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
                if(s.HitTest(mouseState.Pos.X, mouseState.Pos.Y))
                {
                    mouseHitShape = s;
                }

                foreach (VectorEdge e in s.Edges())
                {
                    List<PointF> pts = new List<PointF>();
                       
                    if(e.CrossPointCount(mouseState.Pos.Y, pts) > 0)
                    {
                        foreach (PointF p in pts)
                        {
                            if (Math.Abs(p.X - mouseState.Pos.X) <= 4)
                                mouseHitEdge = e;
                        }
                    }
                }

                foreach (VectorPoint p in s.Points())
                {
                    if (mouseState.Pos.X >= p.X - 3 && mouseState.Pos.X <= p.X + 3)
                        if (mouseState.Pos.Y >= p.Y - 3 && mouseState.Pos.Y <= p.Y + 3)
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

        public MouseState MouseState
        {
            get
            {
                return mouseState;
            }
        }

        public VectorPoint MouseHitPoint
        {
            get
            {
                return mouseHitPoint;
            }
        }

        public VectorShape MouseHitShape
        {
            get
            {
                return mouseHitShape;
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

            mouseState.MouseUp(p.X, p.Y, bts, Keys.Modifiers);

            foreach (VectorTool t in toolsOrder)
            {
                t.MouseUp(bts);
            }
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

            mouseState.MouseDown(p.X, p.Y, bts, Keys.Modifiers);

            foreach (VectorTool t in toolsOrder)
            {
                t.MouseDown(bts);
            }
        }

        public virtual void MouseWheel(float delta, float x, float y)
        {
            PointF p = ViewToDocumentPoint(x, y);

            foreach (VectorTool t in toolsOrder)
            {
                t.MouseWeel(delta, p.X, p.Y);
            }
        }

        public bool HasHitObject
        {
            get
            {
                if (mouseHitEdge != null || MouseHitPoint != null || mouseHitShape != null)
                    return true;

                return false;
            }
        }

        public virtual void MouseMove(float x, float y)
        {
            PointF p = ViewToDocumentPoint(x, y);
            mouseState.MouseMove(p.X, p.Y, Keys.Modifiers);

            UpdateHitObjects();

            foreach (VectorTool t in toolsOrder)
            {
                t.MouseMove();
            }
        }
    }
}
