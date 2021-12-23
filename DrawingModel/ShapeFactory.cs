namespace DrawingModel
{
    class ShapeFactory
    {
        public ShapeFactory()
        {
        }

        //CreateShape
        public Shape CreateShape(ShapeFlag shapeFlag)
        {
            if (shapeFlag == ShapeFlag.Rectangle)
            {
                return new Rectangle();
            }
            else if (shapeFlag == ShapeFlag.Ellipse)
            {
                return new Ellipse();
            }
            else
            {
                return new DotRectangle();
            }
        }

        //CreateLine
        public Line CreateLine()
        {
            return new Line();
        }
    }
}
