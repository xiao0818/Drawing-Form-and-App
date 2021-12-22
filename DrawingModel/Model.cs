using System;
using System.Collections.Generic;
using System.Linq;

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
        CommandManager commandManager = new CommandManager();

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

        //GetHint
        public Shape GetHint
        {
            get
            {
                return _hint;
            }
        }

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
            else if (shapeFlag == (int)ShapeFlag.Line)
            {
                _hint = new Line();
            }
            else if (shapeFlag == (int)ShapeFlag.DotRectangle)
            {
                _hint = new DotRectangle();
            }
            if (pointX > 0 && pointY > 0)
            {
                _firstPointX = pointX;
                _firstPointY = pointY;
                _hint.X1 = _firstPointX;
                _hint.Y1 = _firstPointY;
                _isPressed = true;
            }
        }

        //PointerMoved
        public void MovedPointer(double pointX, double pointY)
        {
            if (_isPressed)
            {
                _hint.X2 = pointX;
                _hint.Y2 = pointY;
                NotifyModelChanged();
            }
        }

        //PointerReleased
        public void ReleasedPointer(double pointX, double pointY)
        {
            if (_isPressed)
            {
                _hint.X2 = pointX;
                _hint.Y2 = pointY;
                _isPressed = false;
                //_shapes.Add(_hint.Copy());
                commandManager.Execute(new DrawCommand(this, _hint.Copy()));
                NotifyModelChanged();
            }
        }

        //Clear
        public void Clear()
        {
            _isPressed = false;
            int count = _shapes.Count();
            for (int index = 0; index < count; index++)
            {
                this.Undo();
            }
            NotifyModelChanged();
        }

        //Draw
        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (Shape aShape in _shapes)
            {
                if(aShape.GetShape == "Line")
                {
                    aShape.Draw(graphics);
                }
            }
            foreach (Shape aShape in _shapes)
            {
                if (aShape.GetShape != "Line")
                {
                    aShape.Draw(graphics);
                }
            }
            if (_isPressed)
            {
                _hint.Draw(graphics);
            }
        }

        //DrawShape
        public void DrawShape(Shape shape)
        {
            _shapes.Add(shape);
        }

        //DeleteShape
        public void DeleteShape()
        {
            _shapes.RemoveAt(_shapes.Count - 1);
        }

        //Undo
        public void Undo()
        {
            commandManager.Undo();
            NotifyModelChanged();
        }

        //Redo
        public void Redo()
        {
            commandManager.Redo();
            NotifyModelChanged();
        }

        public bool IsRedoEnabled
        {
            get
            {
                return commandManager.IsRedoEnabled;
            }
        }

        public bool IsUndoEnabled
        {
            get
            {
                return commandManager.IsUndoEnabled;
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
