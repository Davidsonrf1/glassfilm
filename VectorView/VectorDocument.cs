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

        }

        public VectorDocument(): base(null)
        {
            RegisterTools();
        }
    }
}
