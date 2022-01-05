﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        List<Shape> _shapes = new List<Shape>();
        ShapeFlag _shapeFlag = ShapeFlag.Null;
        CommandManager _commandManager = new CommandManager();
        DotRectangle _target;
        bool _isSelectMode = true;
        IState _state;
        public Model()
        {
            _state = new PointerState(this);
        }

        //Shapes
        public List<Shape> Shapes
        {
            get
            {
                return _shapes;
            }
            set
            {
                _shapes = value;
            }
        }

        //ShapeFlag
        public ShapeFlag ShapeFlag
        {
            get
            {
                return _shapeFlag;
            }
            set
            {
                _shapeFlag = value;
            }
        }

        //PointerPressed
        public void PressedPointer(double pointX, double pointY)
        {
            if (pointX > 0 && pointY > 0)
            {
                _state.PressedPointer(pointX, pointY);
            }
        }

        //PointerMoved
        public void MovedPointer(double pointX, double pointY)
        {
            _state.MovedPointer(pointX, pointY);
            NotifyModelChanged();
        }

        //PointerReleased
        public void ReleasedPointer(double pointX, double pointY)
        {
            _state.ReleasedPointer(pointX, pointY);
            NotifyModelChanged();
        }

        //Clear
        public void Clear()
        {
            SetPointerState();
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
            Shape hint = _state.GetHint();
            if (hint != null)
            {
                hint.Draw(graphics);
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
            ResetSelection();
            _commandManager.Undo();
            NotifyModelChanged();
        }

        //Redo
        public void Redo()
        {
            ResetSelection();
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
        public void DrawDotRectangle(DotRectangle shape)
        {
            _target = shape;
            _shapes.Add(shape);
            NotifyModelChanged();
        }

        //ReserSelection
        public void ResetSelection()
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

        //SetDrawingState
        public void SetDrawingState()
        {
            _state = new DrawingState(this);
        }

        //SetDrawingLineState
        public void SetDrawingLineState()
        {
            _state = new DrawingLineState(this);
        }

        //SetPointerState
        public void SetPointerState()
        {
            _state = new PointerState(this);
        }

        //SetMovingState
        public void SetMovingState()
        {
            _state = new MovingState(this);
        }

        //AddCommand
        public void AddCommand(ICommand command)
        {
            _commandManager.AddCommand(command);
        }

        //Execute
        public void ExecuteCommand(ICommand command)
        {
            _commandManager.Execute(command);
        }

        //GetSTateFlag
        public StateFlag GetStateFlag()
        {
            return _state.GetStateFlag();
        }

        //Save
        public void Save()
        {
            List<Shape> saveShapes = new List<Shape>(_shapes);
            //StreamWriter str = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Shape.txt");
            StreamWriter str = new StreamWriter(@"C:\Users\kta23\Desktop\Shape.txt");
            foreach (Shape aShape in saveShapes)
            {
                str.WriteLine(aShape.Save());
            }
            str.Close();
        }

        //Load
        public Task<int> Load()
        {
            _commandManager.ClearAllCommand();
            int count = 0;
            StreamReader str = new StreamReader(@"C:\Users\kta23\Desktop\Shape.txt");
            List<Shape> loadShapes = new List<Shape>();
            while (str.EndOfStream == false)
            {
                count++;
                string text = str.ReadLine();
                List<string> textList = text.Split(' ').ToList();
                if (textList[0] == "Rectangle")
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.X1 = Convert.ToDouble(textList[1]);
                    rectangle.Y1 = Convert.ToDouble(textList[2]);
                    rectangle.X2 = Convert.ToDouble(textList[3]);
                    rectangle.Y2 = Convert.ToDouble(textList[4]);
                    loadShapes.Add(rectangle.Copy());
                }
                else if (textList[0] == "Ellipse")
                {
                    Ellipse ellipse = new Ellipse();
                    ellipse.X1 = Convert.ToDouble(textList[1]);
                    ellipse.Y1 = Convert.ToDouble(textList[2]);
                    ellipse.X2 = Convert.ToDouble(textList[3]);
                    ellipse.Y2 = Convert.ToDouble(textList[4]);
                    loadShapes.Add(ellipse.Copy());
                }
                else if (textList[0] == "Line")
                {
                    Line line = new Line();
                    line.ShapeIndex1 = Convert.ToInt32(textList[1]);
                    line.Shape1 = loadShapes[line.ShapeIndex1];
                    line.ShapeIndex2 = Convert.ToInt32(textList[2]);
                    line.Shape2 = loadShapes[line.ShapeIndex2];
                    loadShapes.Add(line.Copy());
                }
            }
            str.Close();
            _shapes = new List<Shape>(loadShapes);
            NotifyModelChanged();
            return Task.FromResult(count);
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
