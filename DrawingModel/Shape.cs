namespace DrawingModel
{
    public interface Shape
    {
        //SetX1
        double SetX1
        {
            set;
        }

        //SetY1
        double SetY1
        {
            set;
        }

        //SetX2
        double SetX2
        {
            set;
        }

        //SetY2
        double SetY2
        {
            set;
        }

        //Draw
        void Draw(IGraphics graphics);

        //Copy
        Shape Copy();
    }
}
