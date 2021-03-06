﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VectorView
{
    public enum VectorFitStyle
    {
        None,
        Vertical,
        Horizontal,
        Both
    }

    public enum VectorFitRegion
    {
        None,
        Document,
        Content
    }

    public partial class VectorDocument
    {
        VectorViewCtr host = null;
        List<VectorPath> paths = new List<VectorPath>();

        bool debbuging = false;

        float docWidth = 0;
        float docHeight = 0;
        float offsetX = 0;
        float offsetY = 0;
        float scale = 1;

        string marca, modelo, ano;

        PointF cursorPos = new PointF();

        public VectorViewCtr Host
        {
            get
            {
                return host;
            }

            internal set
            {
                host = value;
            }
        }

        public float OffsetX
        {
            get
            {
                return offsetX;
            }

            set
            {
                offsetX = value; host.Invalidate();
            }
        }

        public float OffsetY
        {
            get
            {
                return offsetY;
            }

            set
            {
                offsetY = value; host.Invalidate();
            }
        }

        public bool Debbuging
        {
            get
            {
                return debbuging;
            }

            set
            {
                debbuging = value; ; host.Refresh();
            }
        }

        public float Scale
        {
            get
            {
                return scale;
            }

            set
            {
                scale = value; host.Invalidate();
            }
        }

        internal VectorDocument (VectorViewCtr ctr)
        {
            host = ctr;
        }

        public List<VectorPath> Paths
        {
            get
            {
                return paths;
            }
        }

        public float DocWidth
        {
            get
            {
                return docWidth;
            }

            set
            {
                docWidth = value; Host.Invalidate();
            }
        }

        public float DocHeight
        {
            get
            {
                return docHeight;
            }

            set
            {
                docHeight = value; Host.Invalidate();
            }
        }

        public float DocArea
        {
            get
            {
                return docArea;
            }
        }

        public float UsedArea
        {
            get
            {
                return usedArea;
            }
        }

        public float Efficiency
        {
            get
            {
                return efficiency;
            }
        }

        public string Marca { get => marca; set => marca = value; }
        public string Modelo { get => modelo; set => modelo = value; }
        public string Ano { get => ano; set => ano = value; }

        public IEnumerable<VectorPath> DocPaths()
        {
            foreach (VectorPath p in paths)
            {
                yield return p;
            }
        }

        float docArea = 0;
        float usedArea = 0;
        float efficiency = 0;

        public void CalcMetrics()
        {
            float maxx = GetMaxX();
            docArea = maxx * docHeight;

            usedArea = 0;
            foreach (VectorPath p in paths)
            {
                usedArea += p.Area;
            }

            if (docArea > 0)
                efficiency = usedArea / docArea;
            else
                efficiency = 0;
        }

        static uint curId = 1;
        static string toLock = "";

        public VectorPath GetPathFromSource(VectorPath src)
        {
            foreach (VectorPath p in paths)
            {
                if (p.Source == src)
                    return p;
            }

            return null;
        }

        public VectorPath CreatePath()
        {
            VectorPath path = new VectorPath(this);

            lock (toLock)
            {
                path.Id = curId++;
                paths.Add(path);
            }

            return path;
        }

        public int CountSource(VectorPath src)
        {
            int count = 0;

            foreach (VectorPath p in paths)
            {
                if (p.Source == src)
                    count++;
            }

            return count;
        }

        void DebugRender(Graphics g)
        {
            g.ResetTransform();

            g.DrawString(string.Format("X: {0:0.00}, Y: {1:0.00}", cursorPos.X, cursorPos.Y), host.Font, Brushes.Red, 10, 10);

            g.DrawLine(Pens.Red, offsetX, 0, offsetX, host.Height);
            g.DrawLine(Pens.Red, 0, offsetY, host.Width, offsetY);
        }

        public VectorPath ImportPath(VectorPath path)
        {
            VectorPath p = CreatePath();
            p.ImportPath(path);
            return p;
        }

        public RectangleF GetBoundRect()
        {
            return new RectangleF(0, 0, GetMaxX(), GetMaxY());
        }

        public void AutoFit(Rectangle size, VectorFitStyle style, bool center, VectorFitRegion region, int margin)
        {
            RectangleF bb = new RectangleF();

            switch (region)
            {
                case VectorFitRegion.None:
                case VectorFitRegion.Document:
                    bb = new RectangleF(0, 0, docWidth, Host.Width - 100);
                    break;
                case VectorFitRegion.Content:
                    bb = GetBoundRect();
                    bb.Width += bb.X;
                    bb.Height += bb.Y;
                    break;
            }

            bb.Inflate(margin, margin);

            //size.Width -= margin;
            //size.Height -= margin;

            float s = 1;

            switch (style)
            {
                case VectorFitStyle.None:
                    return;
                case VectorFitStyle.Vertical:
                    s = size.Height / bb.Height;
                    break;
                case VectorFitStyle.Horizontal:
                    s = size.Width / bb.Width;
                    break;
                case VectorFitStyle.Both:

                    float sv = size.Height / bb.Height;
                    float sh = size.Width / bb.Width;

                    s = Math.Min(sv, sh);

                    break;
            }

            if (float.IsInfinity(s))
                return;

            scale = s;

            if (center)
            {
                offsetX = (size.Width - bb.Width * s) / 2 + margin * s / 2 + margin;
                offsetY = (size.Height - bb.Height * s) / 2 + margin * s / 2;
            }

            //UpdateLdTamanho();
        }
    }
}
