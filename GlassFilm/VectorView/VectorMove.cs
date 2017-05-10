﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView
{
    public class VectorMove : VectorEdge
    {
        public VectorMove(VectorPath path, float startx, float starty, float endx, float endy) : base(path, endx, endy, endx, endy)
        {
            Type = VectorEdgeType.Move;
        }

        internal override VectorEdge Clone()
        {
            return new VectorMove(Path, StartX, StartY, EndX, EndY);
        }
    }
}
