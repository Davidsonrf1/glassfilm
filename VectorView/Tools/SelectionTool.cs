using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VectorView.Tools
{
    public class SelectionTool : VectorTool
    {
        List<VectorObject> selection = new List<VectorObject>();
        
        public SelectionTool(VectorDocument doc) : base(doc)
        {
        }

        public override void MouseMove()
        {
            base.MouseMove();
            
            
        }
    }
}
