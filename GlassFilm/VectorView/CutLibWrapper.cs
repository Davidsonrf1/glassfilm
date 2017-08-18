using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace VectorView
{
    public class CutLibWrapper
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct CutTestResult
        {
            public int x;
            public int y;
            public int angle;
            public int maxx;
            public bool resultOK;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SegmentData
        {
            public int start;
            public int end;
            public int line;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ScanData
        {
            public int angle;
            public int segmentCount;
            public IntPtr segments;
        };

        [DllImport("CutSheet.dll")]
        public extern static uint CreateSheet(int size);

        [DllImport("CutSheet.dll")]
        public extern static uint CreateShape(uint sheet, uint id);

        [DllImport("CutSheet.dll")]
        public extern static uint DeleteShape(uint sheet, uint id);

        [DllImport("CutSheet.dll")]
        public extern static void ResetSheet(uint sheet, int size);

        [DllImport("CutSheet.dll")]
        public extern static void AddAngle(uint sheet, uint shape, int angle, int width, int height, IntPtr data);

        [DllImport("CutSheet.dll")]
        public extern static void Plot(uint sheet, uint shape, int angle, int x, int y);

        [DllImport("CutSheet.dll")]
        public extern static void PlotCurrentScan(uint sheet, uint shape, int x, int y);          

        [DllImport("CutSheet.dll")]
        public extern static void TestShape(uint sheet, uint shape, ref CutTestResult result, bool forceAngle, int angle);

        [DllImport("CutSheet.dll")]
        public extern static void RenderScan(uint sheet, uint shapeId, int angle, int x, int y, IntPtr hDC);

        [DllImport("CutSheet.dll")]
        public extern static void RenderSheet(uint sheet, int x, int y, IntPtr hDC);

        [DllImport("CutSheet.dll")]
        public extern static void SortAngles(uint sheet, uint shape);

        [DllImport("CutSheet.dll")]
        public extern static int GetSegmentCount(uint sheet, uint shapeId, int angle);

        [DllImport("CutSheet.dll")]
        public extern static void BuildScansFromPolygon(uint sheet, uint shapeId, float width, float height, IntPtr poly, int pointCount);

        [DllImport("CutSheet.dll")]
        public extern static void BuildCurrentScan(uint sheet, uint shapeId, float width, float height, IntPtr poly, int pointCount, float angle);
    }

}
