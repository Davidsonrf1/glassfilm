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
        List<VectorTool> toolsOrder = new List<VectorTool>();

        void RegisterTools()
        {
            RegsiterTool("DebugTool", new DebugTool("DebugTool", this));
            RegsiterTool("SelectionTool", new SelectionTool("DebugTool", this));
            RegsiterTool("ZoomTool", new ZoomTool("ZoomTool", this));
        }

        void RegsiterTool(string name, VectorTool tool)
        {
            tools.Add(name, tool);
            toolsOrder.Add(tool);
        }

        public void RenderTools (Graphics g)
        {
            foreach (VectorTool t in tools.Values)
            {
                if (t.Active)
                {
                    t.Render();
                }
            }
        }
    }
}
