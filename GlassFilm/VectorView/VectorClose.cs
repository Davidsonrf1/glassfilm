using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VectorView
{
    public class VectorClose : VectorEdge
    {
        public VectorClose(VectorPath path, float startx, float starty, float endx, float endy) : base(path, startx, starty, endx, endy)
        {
            Type = VectorEdgeType.Close;
        }

        internal override VectorEdge Clone()
        {
            return new VectorClose(Path, StartX, StartY, EndX, EndY);
        }
    }
}
