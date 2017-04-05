using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace VectorView.SVG
{
    public class SVGPathData
    {
        SVGSequenceParser parser = new SVGSequenceParser();
        List<ArgumentSequenceToken> tokens = new List<ArgumentSequenceToken>();
        List<SVGPathCommand> cmds = new List<SVGPathCommand>();

        public List<SVGPathCommand> Cmds
        {
            get
            {
                return cmds;
            }
        }

        void ParseTokens()
        {
            ArgumentSequenceToken[] tk = tokens.ToArray();

            for (int i = 0; i < tk.Length; i++)
            {
                ArgumentSequenceToken t = tk[i];

                if (t.Type == ArqumentSequenceTokenType.Ident)
                {
                    SVGPathCommand cmd = new SVGPathCommand(t.TokenText[0]);
                    cmds.Add(cmd);

                    i++;
                    if (t.TokenText.ToLower() != "z")
                    {
                        while (i < tk.Length)
                        {
                            t = tk[i];

                            if (t.Type == ArqumentSequenceTokenType.Number)
                            {
                                cmd.Values.Add(t.Value);
                                i++;
                            }
                            else
                            {
                                if (t.Type == ArqumentSequenceTokenType.Comma)
                                {
                                    i++;
                                    continue;
                                }

                                i--;
                                break;
                            }
                        }
                    }
                    else
                    {

                    }
                }
            }
        }

        public SVGPathData(string data)
        {
            parser.MaxIdLen = 1;
            parser.SetData(data);

            ArgumentSequenceToken t = null;
            do
            {
                t = parser.NextToken();
                tokens.Add(t);
            } while (t.Type != ArqumentSequenceTokenType.Eof);

            ParseTokens();
        }
    }
}

