namespace DrawingModel
{
    class Ellipse : Shape
    {
        public double x1;
        public double y1;
        public double x2;
        public double y2;
        public void SetX1(double number)
        {
            x1 = number;
        }
        public void SetY1(double number)
        {
            y1 = number;
        }
        public void SetX2(double number)
        {
            x2 = number;
        }
        public void SetY2(double number)
        {
            y2 = number;
        }
        public void Draw(IGraphics graphics)
        {
            graphics.DrawEllipse(x1, y1, x2, y2);
        }
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
