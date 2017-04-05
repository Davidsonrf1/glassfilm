using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace VectorView.SVG
{
    public enum ArqumentSequenceTokenType { Ident, String, Number, Comma, Eql, Eof, Unknown }
    public class ArgumentSequenceToken
    {
        ArqumentSequenceTokenType type = ArqumentSequenceTokenType.String;
        string tokenText = null;

        float value = 0;

        public ArgumentSequenceToken()
        {

        }

        public ArgumentSequenceToken(ArqumentSequenceTokenType tp)
        {
            type = tp;

            switch (tp)
            {
                case ArqumentSequenceTokenType.Comma:
                    tokenText = ",";
                    break;
                case ArqumentSequenceTokenType.Eql:
                    tokenText = ",";
                    break;
            }
        }

        internal ArqumentSequenceTokenType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public string TokenText
        {
            get
            {
                return tokenText;
            }

            set
            {
                tokenText = value;
            }
        }

        public float Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public override string ToString()
        {
            return "[" + type.ToString() + "]: " + (type == ArqumentSequenceTokenType.Number ? value.ToString() : TokenText);
        }
    }

    public class SVGSequenceParser
    {
        string data = null;
        char[] dataChars = null;
        int curPos = 0;
        int maxIdLen = int.MaxValue;

        bool MoveToNext()
        {
            if (dataChars == null)
                return false;

            if (curPos < dataChars.Length)
            {
                curPos++;
                return true;
            }

            return false;
        }

        char CurChar
        {
            get
            {
                if (dataChars == null)
                    return '\0';

                if (curPos < dataChars.Length)
                    return dataChars[curPos];

                return '\0';
            }
        }

        public int MaxIdLen
        {
            get
            {
                return maxIdLen;
            }

            set
            {
                maxIdLen = value;
            }
        }

        void SkipWhiteSpace()
        {
            if (dataChars == null || curPos >= dataChars.Length)
                return;

            while (curPos <= dataChars.Length && char.IsWhiteSpace(dataChars[curPos]))
                if (!MoveToNext()) break;
        }

        ArgumentSequenceToken ReadIdent()
        {
            ArgumentSequenceToken t = new ArgumentSequenceToken();
            t.Type = ArqumentSequenceTokenType.Ident;

            StringBuilder sb = new StringBuilder();

            int len = 0;
            while (char.IsLetterOrDigit(CurChar))
            {
                sb.Append(CurChar);
                if (!MoveToNext()) break;

                len++;

                if (len >= maxIdLen)
                    break;
            }

            t.TokenText = sb.ToString();

            return t;
        }

        ArgumentSequenceToken ReadNumber()
        {
            ArgumentSequenceToken t = new ArgumentSequenceToken();
            t.Type = ArqumentSequenceTokenType.Number;

            float sign = 1f;

            if (CurChar == '+')
            {
                MoveToNext();
            }
            else if (CurChar == '-')
            {
                sign = -1f;
                MoveToNext();
            }

            SkipWhiteSpace();

            bool hasDot = false;

            StringBuilder sb = new StringBuilder();
            while (char.IsDigit(CurChar) || (CurChar == '.' && !hasDot))
            {
                if (CurChar == '.')
                    hasDot = true;

                sb.Append(CurChar);
                if (!MoveToNext()) break;
            }

            t.TokenText = sb.ToString();

            float f = 0;

            if (float.TryParse(t.TokenText, NumberStyles.AllowDecimalPoint, new CultureInfo("en-US"), out f))
                t.Value = f * sign;

            return t;
        }

        ArgumentSequenceToken ReadString()
        {
            ArgumentSequenceToken t = new ArgumentSequenceToken();
            t.Type = ArqumentSequenceTokenType.String;

            StringBuilder sb = new StringBuilder();
            while (CurChar != '"')
            {
                sb.Append(CurChar);
                if (!MoveToNext()) break;
            }

            t.TokenText = sb.ToString();
            return t;
        }

        public ArgumentSequenceToken NextToken()
        {
            if (dataChars == null)
                return new ArgumentSequenceToken(ArqumentSequenceTokenType.Eof);

            if (curPos >= dataChars.Length)
                return new ArgumentSequenceToken(ArqumentSequenceTokenType.Eof);

            SkipWhiteSpace();

            if (char.IsLetter(CurChar) || CurChar == '_')
            {
                return ReadIdent();
            }

            if (char.IsDigit(CurChar) || CurChar == '.' || CurChar == '+' || CurChar == '-')
            {
                return ReadNumber();
            }

            if (CurChar == '"')
            {
                MoveToNext();
                ReadString();
            }

            if (CurChar == ',')
            {
                MoveToNext();
                return new ArgumentSequenceToken(ArqumentSequenceTokenType.Comma);
            }

            if (CurChar == '=')
            {
                MoveToNext();
                return new ArgumentSequenceToken(ArqumentSequenceTokenType.Eql);
            }

            return new ArgumentSequenceToken(ArqumentSequenceTokenType.Unknown);
        }

        public void SetData(string data)
        {
            this.data = data;

            dataChars = null;
            if (data != null && !string.IsNullOrEmpty(data))
            {
                dataChars = data.ToCharArray();
            }

            curPos = 0;
        }
    }
}
