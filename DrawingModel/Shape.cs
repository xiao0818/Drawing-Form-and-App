namespace DrawingModel
{
    public interface Shape
    {
        void SetX1(double number);
        void SetY1(double number);
        void SetX2(double number);
        void SetY2(double number);
        void Draw(IGraphics graphics);
        Shape Copy();
    }
}
