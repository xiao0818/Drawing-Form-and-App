namespace DrawingModel
{
    public class Line : Shape
    {
        double _x1;
        double _y1;
        double _x2;
        double _y2;
        Shape _shape1;
        Shape _shape2;
        const int TWO = 2;

        //X1
        public double X1
        {
            get
            {
                return _x1;
            }
            set
            {
                _x1 = value;
            }
        }

        //Y1
        public double Y1
        {
            get
            {
                return _y1;
            }
            set
            {
                _y1 = value;
            }
        }

        //X2
        public double X2
        {
            get
            {
                return _x2;
            }
            set
            {
                _x2 = value;
            }
        }

        //Y2
        public double Y2
        {
            get
            {
                return _y2;
            }
            set
            {
                _y2 = value;
            }
        }

        //shape1
        public Shape Shape1
        {
            get
            {
                return _shape1;
            }
            set
            {
                _shape1 = value;
            }
        }

        //shape2
        public Shape Shape2
        {
            get
            {
                return _shape2;
            }
            set
            {
                _shape2 = value;
            }
        }

        //Draw
        public void Draw(IGraphics graphics)
        {
            graphics.DrawLine(_x1, _y1, _x2, _y2);
        }

        //Copy
        public Shape Copy()
        {
            Line hint = new Line();
            hint.X1 = _x1;
            hint.Y1 = _y1;
            hint.X2 = _x2;
            hint.Y2 = _y2;
            return hint;
        }

        //GetShape
        public ShapeFlag GetShape
        {
            get
            {
                return ShapeFlag.Line;
            }
        }

        //SetPointToShapeCenter
        public void SetPointToShapeCenter()
        {
            _x1 = (_shape1.X1 + _shape1.X2) / TWO;
            _y1 = (_shape1.Y1 + _shape1.Y2) / TWO;
            _x2 = (_shape2.X1 + _shape2.X2) / TWO;
            _y2 = (_shape2.Y1 + _shape2.Y2) / TWO;
        }
    }
}
