using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VectorView
{
    public partial class VectorDocument
    {
        List<VectorObject> selection = new List<VectorObject>();

        public void ClearSelection()
        {
            foreach (VectorObject o in selection)
            {
                o.IsSelected = false;
            }

            selection.Clear();
        }

        public IEnumerable<VectorObject> SelectedObjects()
        {
            foreach (VectorObject o in selection)
            {
                yield return o;
            }
        }

        public void SelectObject(VectorObject obj)
        {
            if (obj.Document != this)
                return;

            obj.IsSelected = true;

            foreach (VectorObject o in selection)
            {
                if (obj == o)
                    return;
            }

            selection.Add(obj);
        }

        public VectorDocument(): base(null)
        {
            RegisterTools();
        }
    }
}
