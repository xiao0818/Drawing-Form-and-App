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
        Line _line;
        ShapeFlag _shapeFlag = ShapeFlag.Null;
        ShapeFactory _shapeFactory = new ShapeFactory();
        CommandManager _commandManager = new CommandManager();
        DotRectangle _target;
        bool _isSelectMode = true;
        double _movementX = 0;
        double _movementY = 0;
        bool _isMoving = false;
        double _moveInitialX = 0;
        double _moveInitialY = 0;

        //GetFirstPointX
        public double GetFirstPointX
        {
            get
            {
                return _firstPointX;
            }
        }

        //GetFirstPointY
        public double GetFirstPointY
        {
            get
            {
                return _firstPointY;
            }
        }

        //GetIsPressed
        public bool IsPressed
        {
            get
            {
                return _isPressed;
            }
        }

        //GetShapes
        public List<Shape> GetShapes
        {
            get
            {
                return _shapes;
            }
        }

        //SetShapes
        public List<Shape> SetShapes
        {
            set
            {
                _shapes = value;
            }
        }

        //GetShapeHint
        public Shape GetShapeHint
        {
            get
            {
                return _hint;
            }
        }

        //GetLineHint
        public Shape GetLineHint
        {
            get
            {
                return _line;
            }
        }

        //ShapeFlag
        public ShapeFlag ShapeFlag
        {
            set
            {
                _shapeFlag = value;
            }
        }

        //PointerPressed
        public void PressedPointer(double pointX, double pointY)
        {
            _hint = null;
            _line = null;
            if (pointX > 0 && pointY > 0)
            {
                if (_shapes.Count != 0 && _shapeFlag == ShapeFlag.Null)
                {
                    if (_shapes[_shapes.Count - 1].GetShape == ShapeFlag.DotRectangle)
                    {
                        if (((_target.X1 <= pointX && _target.X2 >= pointX) || (_target.X1 >= pointX && _target.X2 <= pointX)) && ((_target.Y1 <= pointY && _target.Y2 >= pointY) || (_target.Y1 >= pointY && _target.Y2 <= pointY)))
                        {
                            _moveInitialX = pointX;
                            _moveInitialY = pointY;
                            _movementX = pointX;
                            _movementY = pointY;
                            _isMoving = true;
                        }
                        else
                        {
                            ResetSelection();
                        }
                    }
                }
                else if (_shapeFlag != ShapeFlag.Null)
                {
                    _firstPointX = pointX;
                    _firstPointY = pointY;
                    if (_shapeFlag == ShapeFlag.Line)
                    {
                        if (IsInShape(pointX, pointY) != null)
                        {
                            _line = _shapeFactory.CreateLine;
                            _line.X1 = _firstPointX;
                            _line.Y1 = _firstPointY;
                            _line.Shape1 = IsInShape(pointX, pointY);
                            _isPressed = true;
                        }
                    }
                    else
                    {
                        _hint = _shapeFactory.CreateShape(_shapeFlag);
                        _hint.X1 = _firstPointX;
                        _hint.Y1 = _firstPointY;
                        _isPressed = true;
                    }
                    _isSelectMode = false;
                    ResetSelection();
                }
            }
        }

        //PointerMoved
        public void MovedPointer(double pointX, double pointY)
        {
            if (_isMoving == true)
            {
                HandleMove(pointX - _movementX, pointY - _movementY);
                _movementX = pointX;
                _movementY = pointY;
            }
            else if (_shapeFlag != ShapeFlag.Null)
            {
                if (_isPressed == true)
                {
                    if (_shapeFlag == ShapeFlag.Line)
                    {
                        _line.X2 = pointX;
                        _line.Y2 = pointY;
                    }
                    else
                    {
                        _hint.X2 = pointX;
                        _hint.Y2 = pointY;
                    }
                }
            }
            NotifyModelChanged();
        }

        //PointerReleased
        public void ReleasedPointer(double pointX, double pointY)
        {
            if (_isMoving == true)
            {
                _commandManager.AddCommand(new MoveCommand(this, _target.Shape, pointX - _moveInitialX, pointY - _moveInitialY));
                _isMoving = false;
            }
            else if (_isSelectMode == true)
            {
                ResetSelection();
                for (int index = 0; index < _shapes.Count; index++)
                {
                    Shape aShape = _shapes[_shapes.Count - index - 1];
                    if (((aShape.X1 <= pointX && aShape.X2 >= pointX) || (aShape.X1 >= pointX && aShape.X2 <= pointX)) && ((aShape.Y1 <= pointY && aShape.Y2 >= pointY) || (aShape.Y1 >= pointY && aShape.Y2 <= pointY)))
                    {
                        DotRectangle dotRectangle = new DotRectangle();
                        dotRectangle.Shape = aShape;
                        DrawDotRectangle(dotRectangle);
                        break;
                    }
                }
            }
            else
            {
                ResetSelection();
                if (_shapeFlag != ShapeFlag.Null)
                {
                    if (_shapeFlag == ShapeFlag.Line)
                    {
                        if (_isPressed == true)
                        {
                            Shape isInShapes = IsInShape(pointX, pointY);
                            if (isInShapes != null)
                            {
                                if(_line.Shape1 != isInShapes)
                                {
                                    _isPressed = false;
                                    _line.X2 = pointX;
                                    _line.Y2 = pointY;
                                    _line.Shape2 = isInShapes;
                                    _line.SetPointToShapeCenter();
                                    _commandManager.Execute(new DrawCommand(this, _line.Copy()));
                                    _isSelectMode = true;
                                    _shapeFlag = ShapeFlag.Null;
                                    NotifyModelChanged();
                                }
                                else
                                {
                                    _isPressed = false;
                                }
                            }
                            else
                            {
                                _isPressed = false;
                            }
                        }
                    }
                    else
                    {
                        if (_isPressed)
                        {
                            _isPressed = false;
                            _hint.X2 = pointX;
                            _hint.Y2 = pointY;
                            _isPressed = false;
                            _commandManager.Execute(new DrawCommand(this, _hint.Copy()));
                            NotifyModelChanged();
                        }
                        _shapeFlag = ShapeFlag.Null;
                        _isSelectMode = true;
                    }
                }
            }
        }

        //Clear
        public void Clear()
        {
            _isPressed = false;
            _commandManager.Execute(new ClearCommand(this, _shapes));
            NotifyModelChanged();
        }

        //Draw
        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (Shape aShape in _shapes)
            {
                if (aShape.GetShape == ShapeFlag.Line)
                {
                    DrawShapes(aShape, graphics);
                }
            }
            foreach (Shape aShape in _shapes)
            {
                if (aShape.GetShape != ShapeFlag.Line)
                {
                    DrawShapes(aShape, graphics);
                }
            }
            DrawHint(graphics);
        }

        //DrawShapes
        private void DrawShapes(Shape aShape, IGraphics graphics)
        {
            aShape.Draw(graphics);
        }

        //DrawHint
        private void DrawHint(IGraphics graphics)
        {
            if (_isPressed)
            {
                if (_shapeFlag == ShapeFlag.Line)
                {
                    _line.Draw(graphics);
                }
                else
                {
                    _hint.Draw(graphics);
                }
            }
        }

        //DrawShape
        public void DrawShape(Shape shape)
        {
            _shapes.Add(shape);
            NotifyModelChanged();
        }

        //ClearAll
        public void ClearAll()
        {
            _shapes.Clear();
            NotifyModelChanged();
        }

        //DeleteShape
        public void DeleteShape()
        {
            _shapes.RemoveAt(_shapes.Count - 1);
            NotifyModelChanged();
        }

        //Undo
        public void Undo()
        {
            _commandManager.Undo();
            NotifyModelChanged();
        }

        //Redo
        public void Redo()
        {
            _commandManager.Redo();
            NotifyModelChanged();
        }

        public bool IsRedoEnabled
        {
            get
            {
                return _commandManager.IsRedoEnabled;
            }
        }

        public bool IsUndoEnabled
        {
            get
            {
                return _commandManager.IsUndoEnabled;
            }
        }

        //DrawDotRectangle
        private void DrawDotRectangle(DotRectangle shape)
        {
            _target = shape;
            _shapes.Add(shape);
            NotifyModelChanged();
        }

        //HandleMove
        private void HandleMove(double XChange, double YChange)
        {
            _target.Shape.X1 += XChange;
            _target.Shape.X2 += XChange;
            _target.Shape.Y1 += YChange;
            _target.Shape.Y2 += YChange;
            NotifyModelChanged();
        }

        //ToDrawMode
        public void ToDrawMode()
        {
            _isSelectMode = false;
        }

        //ToSelectMode
        public void ToSelectMode()
        {
            _isSelectMode = true;
        }

        //IsInShape
        private Shape IsInShape(double pointX, double pointY)
        {
            for (int index = 0; index < _shapes.Count; index++)
            {
                Shape aShape = _shapes[_shapes.Count - index - 1];
                if ((aShape.GetShape != ShapeFlag.Line) && (((aShape.X1 <= pointX && aShape.X2 >= pointX) || (aShape.X1 >= pointX && aShape.X2 <= pointX)) && ((aShape.Y1 <= pointY && aShape.Y2 >= pointY) || (aShape.Y1 >= pointY && aShape.Y2 <= pointY))))
                {
                    return aShape;
                }
            }
            return null;
        }

        //ReserSelection
        private void ResetSelection()
        {
            if (_shapes.Count != 0)
            {
                if (_shapes[_shapes.Count - 1].GetShape == ShapeFlag.DotRectangle)
                {
                    DeleteShape();
                }
            }
        }

        //GetTarget
        public DotRectangle GetTarget()
        {
            return _target;
        }

        //GetIsSelectMode
        public bool GetIsSelectMode()
        {
            return _isSelectMode;
        }

        //NotifyModelChanged
        public void NotifyModelChanged()
        {
            if (_modelChanged != null)
            {
                _modelChanged();
            }
        }
    }
}
