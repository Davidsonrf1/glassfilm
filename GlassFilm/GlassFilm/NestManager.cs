using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VectorView;
using static VectorView.CutLibWrapper;

namespace GlassFilm
{
    public class NestManager
    {
        int cutSheet = -1;
        Dictionary<uint, uint> pathId = new Dictionary<uint, uint>();
        int curSize = 1000;

        public int Size
        {
            get
            {
                return curSize;
            }

            set
            {
                ResetSheet(value);
            }
        }

        public NestManager(int size)
        {
            ResetSheet(size);
        }

        public void ResetSheet(int size)
        {
            if (cutSheet < 0)
            {
                cutSheet = (int)CutLibWrapper.CreateSheet(size);
            }

            CutLibWrapper.ResetSheet((uint)cutSheet, size);

            curSize = size;
        }

        public void ClearSheet()
        {
            foreach (uint sp in pathId.Values)
            {
                CutLibWrapper.DeleteShape((uint)cutSheet, sp);
            }
        }

        public bool NestPath(VectorPath path)
        {
            if (pathId.ContainsKey(path.PathID))
            {
                uint shape = pathId[path.PathID];

                CutTestResult res = new CutTestResult();
                TestShape((uint)cutSheet, shape, ref res);

                if (res.resultOK)
                {
                    path.SetPos(res.x, res.y);
                    path.Rotate(res.angle);

                    Plot((uint)cutSheet, shape, res.angle, res.x, res.y);

                    return true;
                }
            }

            return false;
        }

        public void RegisterPath(VectorPath path)
        {
            if (!pathId.ContainsKey(path.PathID))
            {
                uint shape = path.GenerateCutShape((uint)cutSheet);
                pathId.Add(path.PathID, shape);
            }
        }

    }
}
