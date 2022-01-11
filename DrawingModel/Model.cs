using GoogleDriveUploader.GoogleDrive;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        IState _state;
        GoogleDriveService _service;
        const string APPLICATION_NAME = "DrawAnywhere";
        const string CLIENT_SECRET_FILE_NAME = "clientSecret.json";
        const string CONTENT_TYPE = "text/txt";
        const string FILE_PATH = "\\..\\..\\..\\";
        const string SHAPE_FILE_NAME = "Shape.txt";
        const string LINE_STRING = "Line";
        const string RECTANGLE_STRING = "Rectangle";
        const string ELLIPSE_STRING = "Ellipse";
        const char SPACE = ' ';
        const int SHAPE_NAME_INDEX = 0;
        const int X_1_INDEX = 1;
        const int Y_1_INDEX = 2;
        const int X_2_INDEX = 3;
        const int Y_2_INDEX = 4;
        const int SHAPE_1_INDEX = 1;
        const int SHAPE_2_INDEX = 2;
        bool _isShapeFileWorking = false;
        public Model()
        {
            _state = new PointerState(this);
        }

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
                _state.PressedPointer(pointX, pointY);
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
                if (aShape.ShapeFlag == ShapeFlag.Line)
                    DrawShapes(aShape, graphics);
            foreach (Shape aShape in _shapes)
                if (aShape.ShapeFlag != ShapeFlag.Line)
                    DrawShapes(aShape, graphics);
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
            Shape hint = _state.Hint;
            if (hint != null)
                hint.Draw(graphics);
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
                if (_shapes[_shapes.Count - 1].ShapeFlag == ShapeFlag.DotRectangle)
                    DeleteShape();
        }

        public DotRectangle Target
        {
            get
            {
                return _target;
            }
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
            _shapeFlag = ShapeFlag.Null;
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

        public StateFlag StateFlag
        {
            get
            {
                return _state.StateFlag;
            }
        }

        public bool IsShapeFileWorking
        {
            get
            {
                return _isShapeFileWorking;
            }
        }

        //Save
        public void Save()
        {
            _isShapeFileWorking = true;
            List<Shape> saveShapes = new List<Shape>(_shapes);
            StreamWriter fileWriter = new StreamWriter(Environment.CurrentDirectory + FILE_PATH + SHAPE_FILE_NAME);
            foreach (Shape aShape in saveShapes)
                fileWriter.WriteLine(aShape.SaveText);
            fileWriter.Close();
            SaveToGoogle();
            _isShapeFileWorking = false;
        }

        //Load
        public void Load()
        {
            LoadFromGoogle();
            _commandManager.ClearAllCommand();
            StreamReader fileReader = new StreamReader(Environment.CurrentDirectory + FILE_PATH + SHAPE_FILE_NAME);
            List<Shape> loadShapes = new List<Shape>();
            while (fileReader.EndOfStream == false)
            {
                string text = fileReader.ReadLine();
                List<string> textList = text.Split(SPACE).ToList();
                LoadForShapes(textList, loadShapes);
            }
            fileReader.Close();
            _shapes = new List<Shape>(loadShapes);
            NotifyModelChanged();
        }

        //LoadForShapes
        private void LoadForShapes(List<string> textList, List<Shape> loadShapes)
        {
            if (textList[SHAPE_NAME_INDEX] != LINE_STRING)
                LoadForNotLine(textList, loadShapes);
            else
            {
                Line line = new Line();
                line.ShapeIndex1 = Convert.ToInt32(textList[SHAPE_1_INDEX]);
                line.Shape1 = loadShapes[line.ShapeIndex1];
                line.ShapeIndex2 = Convert.ToInt32(textList[SHAPE_2_INDEX]);
                line.Shape2 = loadShapes[line.ShapeIndex2];
                loadShapes.Add(line.Copy());
            }
        }

        //LoadForNotLine
        private void LoadForNotLine(List<string> textList, List<Shape> loadShapes)
        {
            Shape shape = null;
            if (textList[SHAPE_NAME_INDEX] == RECTANGLE_STRING)
                shape = new Rectangle();
            else if (textList[SHAPE_NAME_INDEX] == ELLIPSE_STRING)
                shape = new Ellipse();
            shape.X1 = Convert.ToDouble(textList[X_1_INDEX]);
            shape.Y1 = Convert.ToDouble(textList[Y_1_INDEX]);
            shape.X2 = Convert.ToDouble(textList[X_2_INDEX]);
            shape.Y2 = Convert.ToDouble(textList[Y_2_INDEX]);
            loadShapes.Add(shape.Copy());
        }

        //SaveToGoogle
        private void SaveToGoogle()
        {
            _service = new GoogleDriveService(APPLICATION_NAME, Environment.CurrentDirectory + FILE_PATH + CLIENT_SECRET_FILE_NAME);
            Google.Apis.Drive.v2.Data.File foundFile = _service.ListRootFileAndFolder().Find(item => item.Title == SHAPE_FILE_NAME);
            if (foundFile != null)
                _service.DeleteFile(foundFile.Id);
            _service.UploadFile(Environment.CurrentDirectory + FILE_PATH + SHAPE_FILE_NAME, CONTENT_TYPE);
        }

        //LoadFromGoogle
        private void LoadFromGoogle()
        {
            StreamWriter fileWriter = new StreamWriter(Environment.CurrentDirectory + FILE_PATH + SHAPE_FILE_NAME);
            fileWriter.Close();
            _service = new GoogleDriveService(APPLICATION_NAME, Environment.CurrentDirectory + FILE_PATH + CLIENT_SECRET_FILE_NAME);
            Google.Apis.Drive.v2.Data.File selectedFile = _service.ListRootFileAndFolder().Find(item => item.Title == SHAPE_FILE_NAME);
            _service.DownloadFile(selectedFile, Environment.CurrentDirectory + FILE_PATH);
        }

        //NotifyModelChanged
        public void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }
    }
}
