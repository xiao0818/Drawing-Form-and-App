using System.Collections.Generic;

namespace DrawingModel
{
    public class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        double _firstPointX;
        double _firstPointY;
        bool _isPressed = false;
        List<Shape> _shapes = new List<Shape>();
        Shape _hint;

        //PointerPressed
        public void PointerPressed(double x, double y, int shapeFlag)
        {
            if (shapeFlag == (int)ShapeFlag.Rectangle)
            {
                _hint = new Rectangle();
            }
            else if (shapeFlag == (int)ShapeFlag.Ellipse)
            {
                _hint = new Ellipse();
            }
            if (x > 0 && y > 0)
            {
                _firstPointX = x;
                _firstPointY = y;
                _hint.SetX1(_firstPointX);
                _hint.SetY1(_firstPointY);
                _isPressed = true;
            }
        }

        //PointerMoved
        public void PointerMoved(double x, double y)
        {
            if (_isPressed)
            {
                _hint.SetX2(x);
                _hint.SetY2(y);
                NotifyModelChanged();
            }
        }

        //PointerReleased
        public void PointerReleased(double x, double y)
        {
            if (_isPressed)
            {
                _isPressed = false;
                _shapes.Add(_hint.Copy());
                NotifyModelChanged();
            }
        }

        //Clear
        public void Clear()
        {
            _isPressed = false;
            _shapes.Clear();
            NotifyModelChanged();
        }

        //Draw
        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (Shape aShape in _shapes)
            {
                aShape.Draw(graphics);
            }
            if (_isPressed)
            {
                _hint.Draw(graphics);
            }
        }

        //NotifyModelChanged
        private void NotifyModelChanged()
        {
            if (_modelChanged != null)
            {
                _modelChanged();
            }
        }
    }
}
