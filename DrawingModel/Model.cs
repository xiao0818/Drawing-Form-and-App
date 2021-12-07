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
        public void PressedPointer(double pointX, double pointY, int shapeFlag)
        {
            if (shapeFlag == (int)ShapeFlag.Rectangle)
            {
                _hint = new Rectangle();
            }
            else if (shapeFlag == (int)ShapeFlag.Ellipse)
            {
                _hint = new Ellipse();
            }
            if (pointX > 0 && pointY > 0)
            {
                _firstPointX = pointX;
                _firstPointY = pointY;
                _hint.SetX1 = _firstPointX;
                _hint.SetY1 = _firstPointY;
                _isPressed = true;
            }
        }

        //PointerMoved
        public void MovedPointer(double pointX, double pointY)
        {
            if (_isPressed)
            {
                _hint.SetX2 = pointX;
                _hint.SetY2 = pointY;
                NotifyModelChanged();
            }
        }

        //PointerReleased
        public void ReleasedPointer(double pointX, double pointY)
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
