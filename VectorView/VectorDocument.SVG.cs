using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Threading.Tasks;

namespace VectorView
{
    public partial class VectorDocument
    {
        class PathCommand
        {
            char cmd = 'm';
            float originX=0, originY=0;

            int curValueIndex = 0;

            public override string ToString()
            {
                return cmd.ToString();
            }

            List<float> values = new List<float>();

            public bool ExtractValues(float[] vList)
            {
                int i = 0;
                for (; i < vList.Length && curValueIndex < values.Count; i++)
                    vList[i] = values[curValueIndex++];

                return i == vList.Length ? true : false; 
            }

            public PathCommand(char c)
            {
                cmd = c;

                if (char.IsUpper(c))
                {
                }
            }

            public char Cmd
            {
                get
                {
                    return cmd;
                }
            }

            public float OriginX
            {
                get
                {
                    return originX;
                }
            }

            public float OriginY
            {
                get
                {
                    return originY;
                }
            }

            public List<float> Values
            {
                get
                {
                    return values;
                }
            }
        }

        class PathParser
        {
            List<PathCommand> cmds = new List<PathCommand>();

            public List<PathCommand> Commands
            {
                get
                {
                    return cmds;
                }
                
            }

            public void Clear()
            {
                cmds.Clear();
            }           

            public void Parse (char[] d)
            {
                PathCommand curCmd = null;

                for (int i = 0; i < d.Length; i++)
                {
                    while (char.IsWhiteSpace(d[i]) && i < d.Length)
                        i++;

                    char c = d[i];

                    if (char.IsLetter(c))
                    {
                        char cc = char.ToLower(c);

                        if (cc == 'm' || cc == 'z' || cc == 'l' || cc == 'h' || cc == 'v' || cc == 'c' || cc == 's' || cc == 'q' || cc == 't' || cc == 'a')
                        {
                            curCmd = new PathCommand(c);
                            cmds.Add(curCmd);
                        }
                        else
                            throw new InvalidCastException("Comando inválido no Path: " + c);
                    }
                    else if(c == '-' || char.IsDigit(c) || c == '.')
                    {
                        bool negative = c == '-' ? true : false;
                        if (negative)
                            i++;

                        bool hasDot = c == '.' ? true : false;
                        if (hasDot)
                            i++;

                        while (char.IsWhiteSpace(c) && i < d.Length)
                            i++;

                        StringBuilder sb = new StringBuilder();

                        c = d[i];

                        while ((char.IsDigit(c) || c == '.') && i < d.Length)
                        {
                            if (c == '.')
                            {
                                if(hasDot)
                                {
                                    i--;
                                    break;
                                }

                                hasDot = true;
                            }

                            sb.Append(c);
                            i++;

                            if (i < d.Length)
                                c = d[i];
                        }

                        float valor = 0;
                        if (float.TryParse(sb.ToString(), out valor))
                            curCmd.Values.Add(valor * (negative ? -1 : 1));
                    }
                }
            }
        }

        void ParsePathData(string data, VectorShape s)
        {
            PathParser parser = new PathParser();
            parser.Parse(data.ToCharArray());

            foreach (PathCommand c in parser.Commands)
            {

                if (c.Cmd == 'm' || c.Cmd == 'M')
                {
                    float[] mPoints = new float[2];

                    while(c.ExtractValues(mPoints))
                    {

                    }
                }
            }
        }

        void ParseSVGPath(XmlNode path)
        {
            VectorShape p = CreateShape();

            foreach (XmlAttribute a in path.Attributes)
            {
                if (a.Name.ToLower() == "d")
                {
                    ParsePathData(a.Value, p);
                }
            }
        }

        bool ParseSVGElement(XmlNode node)
        {
            string nname = node.Name.ToLower();

            switch (nname)
            {
                case "path": ParseSVGPath(node); return true;
            }

            return false;
        }


    }
}
