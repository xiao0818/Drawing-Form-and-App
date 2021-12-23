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
            else if (shapeFlag == ShapeFlag.Line)
            {
                return new Line();
            }
            else
            {
                return new DotRectangle();
            }
        }
    }
}
