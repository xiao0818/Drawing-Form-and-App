namespace DrawingModel
{
    class Rectangle : Shape
    {
        double _x1;
        double _y1;
        double _x2;
        double _y2;

        //SetX1
        public double SetX1
        {
            set
            {
                _x1 = value;
            }
        }

        //SetY1
        public double SetY1
        {
            set
            {
                _y1 = value;
            }
        }

        //SetX2
        public double SetX2
        {
            set
            {
                _x2 = value;
            }
        }

        //SetY2
        public double SetY2
        {
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
            hint.SetX1 = _x1;
            hint.SetY1 = _y1;
            hint.SetX2 = _x2;
            hint.SetY2 = _y2;
            return hint;
        }

        //TakeLarger
        private double TakeLarge(double number1, double number2)
        {
            if (number1 > number2)
            {
                return number1;
            }
            return number2;
        }

        //TakeSmaller
        private double TakeSmall(double number1, double number2)
        {
            if (number1 < number2)
            {
                return number1;
            }
            return number2;
        }
    }
}
