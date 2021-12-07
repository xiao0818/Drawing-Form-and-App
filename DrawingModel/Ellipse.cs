namespace DrawingModel
{
    class Ellipse : Shape
    {
        public double x1;
        public double y1;
        public double x2;
        public double y2;

        //SetX1
        public void SetX1(double number)
        {
            x1 = number;
        }

        //SetY1
        public void SetY1(double number)
        {
            y1 = number;
        }

        //SetX2
        public void SetX2(double number)
        {
            x2 = number;
        }

        //SetY2
        public void SetY2(double number)
        {
            y2 = number;
        }

        //Draw
        public void Draw(IGraphics graphics)
        {
            graphics.DrawEllipse(x1, y1, x2, y2);
        }

        //Shape
        public Shape Copy()
        {
            Ellipse hint = new Ellipse();
            hint.SetX1(x1);
            hint.SetY1(y1);
            hint.SetX2(x2);
            hint.SetY2(y2);
            return hint;
        }
    }
}
