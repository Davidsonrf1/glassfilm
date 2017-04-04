using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VectorView.SVG
{
    public class SVGPathCommand
    {
        char cmd = 'z';
        List<float> values = new List<float>();
        int pos = 0;

        public SVGPathCommand(char c)
        {
            cmd = c;
        }

        public char Cmd
        {
            get
            {
                return cmd;
            }
        }

        public List<float> Values
        {
            get
            {
                return values;
            }
        }

        public override string ToString()
        {
            return cmd.ToString();
        }

        public void Reset()
        {
            pos = 0;
        }

        public bool NextValue(out float val)
        {
            if (pos < values.Count)
            {
                val = values[pos++];
                return true;
            }

            val = 0;
            return false;
        }

        public bool NextPoint(out float x, out float y)
        {
            if (pos < values.Count - 1)
            {
                x = values[pos++];
                y = values[pos++];
                return true;
            }

            x = y = 0;
            return false;
        }
    }
}
