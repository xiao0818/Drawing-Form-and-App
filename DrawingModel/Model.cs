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
        ShapeFlag _shapeFlag = ShapeFlag.Null;
        Line _line;
        ShapeFactory _shapeFactory = new ShapeFactory();
        CommandManager _commandManager = new CommandManager();

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

        //GetHint
        public Shape GetHint
        {
            get
            {
                return _hint;
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
        public void PressedPointer(double pointX, double pointY, ShapeFlag shapeFlag, Shape shape1)
        {
            _hint = null;
            _line = null;
            if (pointX > 0 && pointY > 0)
            {
                _firstPointX = pointX;
                _firstPointY = pointY;
                if (_shapeFlag == ShapeFlag.Line)
                {
                    _line = _shapeFactory.CreateLine;
                    _line.X1 = _firstPointX;
                    _line.Y1 = _firstPointY;
                    _line.Shape1 = shape1;
                }
                else
                {
                    _hint = _shapeFactory.CreateShape(shapeFlag);
                    _hint.X1 = _firstPointX;
                    _hint.Y1 = _firstPointY;
                }
                _isPressed = true;
            }
        }

        //PointerMoved
        public void MovedPointer(double pointX, double pointY)
        {
            if (_isPressed)
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
                NotifyModelChanged();
            }
        }

        //PointerReleased
        public void ReleasedPointer(double pointX, double pointY, Shape shape2)
        {
            if (_isPressed)
            {
                _isPressed = false;
                if (_shapeFlag == ShapeFlag.Line)
                {
                    _line.X2 = pointX;
                    _line.Y2 = pointY;
                    _line.Shape2 = shape2;
                    _line.SetPointToShapeCenter();
                    _commandManager.Execute(new DrawCommand(this, _line.Copy()));
                }
                else
                {
                    _hint.X2 = pointX;
                    _hint.Y2 = pointY;
                    _isPressed = false;
                    _commandManager.Execute(new DrawCommand(this, _hint.Copy()));
                }
                NotifyModelChanged();
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
                    aShape.Draw(graphics);
                }
            }
            foreach (Shape aShape in _shapes)
            {
                if (aShape.GetShape != ShapeFlag.Line)
                {
                    aShape.Draw(graphics);
                }
            }
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

        //PressedCancel
        public void PressedCancel()
        {
            _isPressed = false;
        }

        //GetIsPressed
        public bool GetIsPressed
        {
            get
            {
                return _isPressed;
            }
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
