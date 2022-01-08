namespace DrawingModel
{
    public class Line : Shape
    {
        double _x1;
        double _y1;
        double _x2;
        double _y2;
        Shape _shape1 = null;
        Shape _shape2 = null;
        const int TWO = 2;
        int _shapeIndex1 = -1;
        int _shapeIndex2 = -1;
        const string SHAPE_TEXT_HEAD = "Line ";
        const string SPACE = " ";
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

        //shapeIndex1
        public int ShapeIndex1
        {
            get
            {
                return _shapeIndex1;
            }
            set
            {
                _shapeIndex1 = value;
            }
        }

        //shapeIndex2
        public int ShapeIndex2
        {
            get
            {
                return _shapeIndex2;
            }
            set
            {
                _shapeIndex2 = value;
            }
        }

        //Draw
        public void Draw(IGraphics graphics)
        {
            SetPointToShapeCenter();
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
            if (_shape1 != null)
            {
                hint.Shape1 = _shape1;
                hint.ShapeIndex1 = _shapeIndex1;
            }
            if (_shape2 != null)
            {
                hint.Shape2 = _shape2;
                hint.ShapeIndex2 = _shapeIndex2;
            }
            return hint;
        }

        //GetShape
        public ShapeFlag ShapeFlag
        {
            get
            {
                return ShapeFlag.Line;
            }
        }

        //SaveText
        public string SaveText
        {
            get
            {
                return SHAPE_TEXT_HEAD + _shapeIndex1.ToString() + SPACE + _shapeIndex2.ToString();
            }
        }

        //SetPointToShapeCenter
        public void SetPointToShapeCenter()
        {
            if (_shape1 != null && _shape2 != null)
            {
                _x1 = (_shape1.X1 + _shape1.X2) / TWO;
                _y1 = (_shape1.Y1 + _shape1.Y2) / TWO;
                _x2 = (_shape2.X1 + _shape2.X2) / TWO;
                _y2 = (_shape2.Y1 + _shape2.Y2) / TWO;
            }
        }
    }
}
