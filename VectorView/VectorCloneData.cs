using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VectorView
{
    public class VectorCloneData
    {
        Dictionary<int, VectorObject> objects = new Dictionary<int, VectorObject>();

        public void AddClone(VectorObject obj)
        {
            if (!objects.ContainsKey(obj.Id))
                objects.Add(obj.Id, obj);
        }

        public IEnumerable<VectorObject> Objects()
        {
            foreach (VectorObject o in objects.Values)
            {
                yield return o;
            }
        }
    }
}
