namespace DrawingModel
{
    public class Rectangle : Shape
    {
        double _x1;
        double _y1;
        double _x2;
        double _y2;

        //X1
        public double X1
        {
            get
            {
                return _x1;
            }
            set
            {
                _x1 = value;
            }
        }

        //Y1
        public double Y1
        {
            get
            {
                return _y1;
            }
            set
            {
                _y1 = value;
            }
        }

        //X2
        public double X2
        {
            get
            {
                return _x2;
            }
            set
            {
                _x2 = value;
            }
        }

        //Y2
        public double Y2
        {
            get
            {
                return _y2;
            }
            set
            {
                _y2 = value;
            }
        }

        //Draw
        public void Draw(IGraphics graphics)
        {
            graphics.DrawRectangle(TakeSmall(_x1, _x2), TakeSmall(_y1, _y2), TakeLarge(_x1, _x2), TakeLarge(_y1, _y2));
        }

        //Copy
        public Shape Copy()
        {
            Rectangle hint = new Rectangle();
            hint.X1 = _x1;
            hint.Y1 = _y1;
            hint.X2 = _x2;
            hint.Y2 = _y2;
            return hint;
        }

        //GetShape
        public ShapeFlag GetShape
        {
            get
            {
                return ShapeFlag.Rectangle;
            }
        }

        //Save
        public string Save()
        {
            return "Rectangle " + X1.ToString() + " " + Y1.ToString() + " " + X2.ToString() + " " + Y2.ToString();
        }

        //TakeLarger
        public double TakeLarge(double number1, double number2)
        {
            if (number1 > number2)
            {
                return number1;
            }
            return number2;
        }

        //TakeSmaller
        public double TakeSmall(double number1, double number2)
        {
            if (number1 < number2)
            {
                return number1;
            }
            return number2;
        }
    }
}
