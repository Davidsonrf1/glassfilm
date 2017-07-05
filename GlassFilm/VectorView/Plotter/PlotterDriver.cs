using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView.Plotter
{
    public abstract class PlotterDriver
    {
        List<PlotterCmd> cmds = new List<PlotterCmd>();

        public PlotterDriver()
        {

        }
        


        public abstract string GetCommands();
    }
}
