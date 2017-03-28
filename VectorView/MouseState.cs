using System;
using System.Drawing;
using System.Text;

namespace VectorView
{
    public enum MouseButton { Left, Middle, Right }

    public class MouseState
    {
        bool isLeftDown = false;
        bool isMiddleDown = false;
        bool isRightDown = false;

        PointF middleDownPos = new PointF();
        PointF leftDownPos = new PointF();
        PointF rightDownPos = new PointF();

        PointF middleUpPos = new PointF();
        PointF leftUpPos = new PointF();
        PointF rightUpPos = new PointF();

        PointF mousePos = new PointF();

        float angleLeft = 0;
        float angleMiddle = 0;
        float angleRight = 0;

        const float Rad2Deg = (float)(180.0 / Math.PI);
        const float Deg2Rad = (float)(Math.PI / 180.0);

        private float Angle(PointF start, PointF end)
        {
            return -(float)(Math.Atan2(start.Y - end.Y, end.X - start.X) * Rad2Deg);
        }

        public bool IsLeftDown
        {
            get
            {
                return isLeftDown;
            }
        }

        public bool IsMiddleDown
        {
            get
            {
                return isMiddleDown;
            }
        }

        public bool IsRightDown
        {
            get
            {
                return isRightDown;
            }
        }

        public PointF MiddleDownPos
        {
            get
            {
                return middleDownPos;
            }
        }

        public PointF LeftDownPos
        {
            get
            {
                return leftDownPos;
            }
        }

        public PointF RightDownPos
        {
            get
            {
                return rightDownPos;
            }
        }

        public PointF MiddleUpPos
        {
            get
            {
                return middleUpPos;
            }
        }

        public PointF LeftUpPos
        {
            get
            {
                return leftUpPos;
            }
        }

        public PointF RightUpPos
        {
            get
            {
                return rightUpPos;
            }
        }

        public float AngleLeft
        {
            get
            {
                return angleLeft;
            }
        }

        public float AngleMiddle
        {
            get
            {
                return angleMiddle;
            }
        }

        public float AngleRight
        {
            get
            {
                return angleRight;
            }
        }

        public PointF MousePos
        {
            get
            {
                return mousePos;
            }
        }

        public void MouseMove(float x, float y)
        {
            mousePos.X = x;
            mousePos.Y = y;

            angleLeft = Angle(leftDownPos, mousePos);
            angleMiddle = Angle(middleDownPos, mousePos);
            angleRight = Angle(rightDownPos, mousePos);
        }

        public void MouseUp(float x, float y, MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    leftUpPos.X = x;
                    leftUpPos.Y = y;
                    isLeftDown = false;
                    break;
                case MouseButton.Middle:
                    middleUpPos.X = x;
                    middleUpPos.Y = y;
                    isMiddleDown = false;
                    break;
                case MouseButton.Right:
                    rightUpPos.X = x;
                    rightUpPos.Y = y;
                    isRightDown = false;
                    break;
            }
        }

        public void MouseDown(float x, float y, MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    leftDownPos.X = x;
                    leftDownPos.Y = y;
                    isLeftDown = true;
                    break;
                case MouseButton.Middle:
                    middleDownPos.X = x;
                    middleDownPos.Y = y;
                    isMiddleDown = true;
                    break;
                case MouseButton.Right:
                    rightDownPos.X = x;
                    rightDownPos.Y = y;
                    isRightDown = true;
                    break;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Pos: ");
            sb.Append(mousePos.ToString());
            sb.Append('\n');

            if (isLeftDown)
            {
                sb.Append("Left Down: ");
                sb.Append(leftDownPos.ToString());
                sb.Append(" - Angle: ");
                sb.Append(angleLeft);
                sb.Append('\n');
            }

            if (isMiddleDown)
            {
                sb.Append("Middle Down: ");
                sb.Append(middleDownPos.ToString());
                sb.Append(" - Angle: ");
                sb.Append(angleMiddle);
                sb.Append('\n');
            }

            if (isRightDown)
            {
                sb.Append("Right Down: ");
                sb.Append(rightDownPos.ToString());
                sb.Append(" - Angle: ");
                sb.Append(angleRight);
                sb.Append('\n');
            }

            return sb.ToString();
        }

    }
}
