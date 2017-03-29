using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView
{
    public partial class VectorDocument
    {
        List<VectorObject> selection = new List<VectorObject>();
        Dictionary<int, VectorObject> objects = new Dictionary<int, VectorObject>();

        public override void RestoreClone(VectorObject clone)
        {
        
        }

        public void RestoreCloneData(VectorCloneData data)
        {
            foreach (VectorObject o in data.Objects())
            {
                if (o.Document == this)
                {
                    VectorObject org = null;

                    if (objects.TryGetValue(o.Id, out org))
                    {
                        org.RestoreClone(o);
                    }
                }
            }
        }

        public VectorCloneData CloneSelecion()
        {
            if (selection.Count == 0)
                return null;

            VectorCloneData cd = new VectorCloneData();

            foreach (VectorObject s in selection)
            {
                cd.AddClone(s.GetClone());
            }

            return cd;
        }

        internal void AddObject(VectorObject obj)
        {
            objects.Add(obj.Id, obj);
        }

        public void ClearSelection()
        {
            foreach (VectorObject o in selection)
            {
                o.IsSelected = false;
            }

            selection.Clear();

            needRedraw = true;
        }

        public IEnumerable<VectorObject> SelectedObjects()
        {
            foreach (VectorObject o in selection)
            {
                yield return o;
            }
        }

        RectangleF selectionBoundingBox = new RectangleF();

        void CalculateBoudingBox()
        {
            if (selection.Count == 0)
            {
                selectionBoundingBox.X = 0;
                selectionBoundingBox.Y = 0;
                selectionBoundingBox.Width = 0;
                selectionBoundingBox.Height = 0;
            }

            RectangleF r = selection[0].GetBoundBox();

            float minx = r.Left;
            float miny = r.Top;
            float maxx = r.Right;
            float maxy = r.Bottom;

            foreach (VectorObject o in selection)
            {
                r = o.GetBoundBox();

                minx = Math.Min(r.Left, minx);
                miny = Math.Min(r.Top, miny);
                maxx = Math.Min(r.Right, maxx);
                maxy = Math.Min(r.Bottom, maxy);
            }

            selectionBoundingBox.X = minx;
            selectionBoundingBox.Y = miny;
            selectionBoundingBox.Width = maxx - minx;
            selectionBoundingBox.Height = maxy - miny;

            selectionBoundingBox.Inflate(4 * (1 / Document.Scale), 4 * (1 / Document.Scale));
        }
    

        public void SelectObject(VectorObject obj)
        {
            if (obj.Document != this)
                return;

            foreach (VectorObject o in selection)
            {
                if (obj == o)
                    return;
            }

            selection.Add(obj);

            needRedraw = true;

            CalculateBoudingBox();
        }

        public void UnselectObject(VectorObject obj)
        {
            if (obj.Document != this)
                return;

            foreach (VectorObject o in selection)
            {
                if (obj == o)
                {
                    selection.Remove(o);
                    break;
                }
            }

            needRedraw = true;

            CalculateBoudingBox();
        }

        public VectorDocument(): base(null)
        {
            renderParams = new RenderParams(this);
            RegisterTools();
        }

        internal void ShapeChangeNotify(VectorShape shape)
        {
            needRedraw = true;
        }

    }
}
