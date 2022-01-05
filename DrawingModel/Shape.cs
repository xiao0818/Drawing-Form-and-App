namespace DrawingModel
{
    public interface Shape
    {
        //SetX1
        double X1
        {
            get;
            set;
        }

        //SetY1
        double Y1
        {
            get;
            set;
        }

        //SetX2
        double X2
        {
            get;
            set;
        }

        //SetY2
        double Y2
        {
            get;
            set;
        }

        //Draw
        void Draw(IGraphics graphics);

        //Copy
        Shape Copy();

        //GetShape
        ShapeFlag GetShape
        {
            get;
        }

        //Save
        string Save();
    }
}
