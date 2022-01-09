using System.Collections.Generic;

namespace DrawingModel
{
    public class DrawingLineState : IState
    {
        Model _model;
        Line _line = null;
        bool _isPressed = false;
        ShapeFactory _shapeFactory = new ShapeFactory();
        public DrawingLineState(Model model)
        {
            _model = model;
            _model.ResetSelection();
        }

        //PressedPointer
        public void PressedPointer(double pointX, double pointY)
        {
            _line = null;
            if (IsInShape(pointX, pointY) != null)
            {
                _line = _shapeFactory.CreateLine;
                _line.X1 = pointX;
                _line.Y1 = pointY;
                _line.Shape1 = IsInShape(pointX, pointY);
                _line.ShapeIndex1 = GetShapeIndex(_line.Shape1);
                _isPressed = true;
            }
        }

        //MovedPointer
        public void MovedPointer(double pointX, double pointY)
        {
            if (_isPressed == true)
            {
                _line.X2 = pointX;
                _line.Y2 = pointY;
            }
        }

        //ReleasedPointer
        public void ReleasedPointer(double pointX, double pointY)
        {
            if (_isPressed == true)
            {
                Shape isInShapes = IsInShape(pointX, pointY);
                if (isInShapes != null && _line.Shape1 != isInShapes)
                {
                    _line.X2 = pointX;
                    _line.Y2 = pointY;
                    _line.Shape2 = isInShapes;
                    _line.ShapeIndex2 = GetShapeIndex(_line.Shape2);
                    _model.ExecuteCommand(new DrawCommand(_model, _line.Copy()));
                    _model.SetPointerState();
                }
                else
                {
                    _model.SetDrawingLineState();
                }
            }
        }

        //Hint
        public Shape Hint
        {
            get
            {
                return _line;
            }
        }

        //StateFlag
        public StateFlag StateFlag
        {
            get
            {
                return StateFlag.DrawingLineState;
            }
        }

        //IsInShape
        private Shape IsInShape(double pointX, double pointY)
        {
            List<Shape> shapes = _model.Shapes;
            for (int index = 0; index < shapes.Count; index++)
            {
                Shape aShape = shapes[shapes.Count - index - 1];
                if (aShape.ShapeFlag != ShapeFlag.Line)
                {
                    if (((aShape.X1 <= pointX && aShape.X2 >= pointX) || (aShape.X1 >= pointX && aShape.X2 <= pointX)) && ((aShape.Y1 <= pointY && aShape.Y2 >= pointY) || (aShape.Y1 >= pointY && aShape.Y2 <= pointY)))
                    {
                        return aShape;
                    }
                }
            }
            return null;
        }

        //GetShapeIndex
        private int GetShapeIndex(Shape shape)
        {
            int targetIndex = 0;
            List<Shape> shapes = _model.Shapes;
            for (int index = 0; index < shapes.Count; index++)
            {
                Shape aShape = shapes[index];
                if (shape == aShape)
                {
                    targetIndex = index;
                }
            }
            return targetIndex;
        }
    }
}
