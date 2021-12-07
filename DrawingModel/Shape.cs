namespace DrawingModel
{
    public interface Shape
    {
        //SetX1
        void SetX1(double number);

        //SetY1
        void SetY1(double number);

        //SetX2
        void SetX2(double number);

        //SetY2
        void SetY2(double number);

        //Draw
        void Draw(IGraphics graphics);

        //Copy
        Shape Copy();
    }
}
