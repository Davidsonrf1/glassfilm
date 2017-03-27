using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using VectorView.Tools;

namespace VectorView
{
    public partial class VectorDocument
    {
        Dictionary<string, VectorTool> tools = new Dictionary<string, VectorTool>();

        void RegisterTools()
        {
            tools.Add("DebugTool", new DebugTool(this));
            tools.Add("SelectionTool", new SelectionTool(this));
            tools.Add("RotateTool", new TransformTool(this));
        }


        public void RenderTools (Graphics g)
        {
            foreach (VectorTool t in tools.Values)
            {
                if (t.Active)
                {
                    t.Render(g);
                }
            }
        }
    }
}
