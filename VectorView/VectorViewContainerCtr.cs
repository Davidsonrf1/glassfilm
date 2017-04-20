using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VectorView
{
    public partial class VectorViewContainerCtr : UserControl
    {
        int rullerWidth = 25;
        Color rullerLineColor = Color.DarkRed;
        Color rullerColor = Color.White;
        Pen rullerLine = new Pen(Color.DarkRed);
        
        bool showRuller = true;

        public VectorViewContainerCtr()
        {
            InitializeComponent();
            AdjustView();
        }

        public VectorViewCtr View
        {
            get
            {
                return view;
            }
        } 

        public VectorDocument Document
        {
            get
            {
                return view.Document;
            }

            set
            {
                view.Document = value;
            }
        }

        public int RullerWidth
        {
            get
            {
                return rullerWidth;
            }

            set
            {
                rullerWidth = value;
            }
        }

        public bool ShowRuller
        {
            get
            {
                return showRuller;
            }

            set
            {
                showRuller = value; AdjustView();
            }
        }

        void AdjustView()
        {
            if (showRuller)
            {
                view.Left = rullerWidth;
                view.Top = rullerWidth;
                view.Width = Width - rullerWidth;
                view.Height = Height - rullerWidth;
            }
            else
            {
                view.Left = 0;
                view.Top = 0;
                view.Width = Width;
                view.Height = Height;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            AdjustView();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustView();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            if (showRuller)
            {

            }
        }
    }
}
