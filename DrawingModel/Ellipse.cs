namespace DrawingModel
{
    class Ellipse : Shape
    {
        public double _x1;
        public double _y1;
        public double _x2;
        public double _y2;

        //SetX1
        public void SetX1(double number)
        {
            _x1 = number;
        }

        //SetY1
        public void SetY1(double number)
        {
            _y1 = number;
        }

        //SetX2
        public void SetX2(double number)
        {
            _x2 = number;
        }

        //SetY2
        public void SetY2(double number)
        {
            _y2 = number;
        }

        //Draw
        public void Draw(IGraphics graphics)
        {
            graphics.DrawEllipse(_x1, _y1, _x2, _y2);
        }

        //Shape
        public Shape Copy()
        {
            Ellipse hint = new Ellipse();
            hint.SetX1(_x1);
            hint.SetY1(_y1);
            hint.SetX2(_x2);
            hint.SetY2(_y2);
            return hint;
        }
    }
}
