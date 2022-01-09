namespace DrawingModel
{
    public class Ellipse : Shape
    {
        double _x1;
        double _y1;
        double _x2;
        double _y2;
        const string SHAPE_TEXT_HEAD = "Ellipse ";
        const string SPACE = " ";
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
            graphics.DrawEllipse(TakeSmall(_x1, _x2), TakeSmall(_y1, _y2), TakeLarge(_x1, _x2), TakeLarge(_y1, _y2));
        }

        //Copy
        public Shape Copy()
        {
            Ellipse hint = new Ellipse();
            hint.X1 = _x1;
            hint.Y1 = _y1;
            hint.X2 = _x2;
            hint.Y2 = _y2;
            return hint;
        }

        //GetShape
        public ShapeFlag ShapeFlag
        {
            get
            {
                return ShapeFlag.Ellipse;
            }
        }

        //SaveText
        public string SaveText
        {
            get
            {
                return SHAPE_TEXT_HEAD + X1.ToString() + SPACE + Y1.ToString() + SPACE + X2.ToString() + SPACE + Y2.ToString();
            }
        }

        //TakeLarger
        public double TakeLarge(double number1, double number2)
        {
            return number1 > number2 ? number1 : number2;
        }

        //TakeSmaller
        public double TakeSmall(double number1, double number2)
        {
            return number1 < number2 ? number1 : number2;
        }
    }
}
