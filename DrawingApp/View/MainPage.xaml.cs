using DrawingModel;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// 空白頁項目範本已記錄在 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x404

namespace DrawingApp
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DrawingAppPresentationModel _drawingAppPresentationModel;
        IGraphics _iGraphics;
        ShapeFlag _shapeFlag = ShapeFlag.Null;
        bool _isSelectMode = true;
        const string LABEL_DEFAULT = "Selected : None";
        const string LABEL_HEAD = "Selected : ";
        const string LABEL_COMMA = ", ";
        const string LABEL_LEFT_BRACKET = " (";
        const string LABEL_RIGHT_BRACKET = ")";
        double _movementX = 0;
        double _movementY = 0;
        bool _isMoving = false;
        double _moveInitialX = 0;
        double _moveInitialY = 0;

        public MainPage()
        {
            InitializeComponent();
            _drawingAppPresentationModel = new DrawingAppPresentationModel(new Model());
            // Note: 重複使用_igraphics物件
            _iGraphics = new WindowsStoreGraphicsAdaptor(_canvas);
            // Events
            _undo.IsEnabled = false;
            _redo.IsEnabled = false;
            _canvas.PointerPressed += HandleCanvasPointerPressed;
            _canvas.PointerReleased += HandleCanvasPointerReleased;
            _canvas.PointerMoved += HandleCanvasPointerMoved;
            _rectangle.Click += HandleRectangleButtonClick;
            _ellipse.Click += HandleEllipseButtonClick;
            _line.Click += HandleLineButtonClick;
            _clear.Click += HandleClearButtonClick;
            _undo.Click += UndoHandler;
            _redo.Click += RedoHandler;
            _drawingAppPresentationModel._drawingAppPresentationModelChanged += HandleModelChanged;
        }

        //HandleRectangleButtonClick
        private void HandleRectangleButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.HandleRectangleButtonClick();
            RefreshButton();
            _isSelectMode = false;
            ResetSelection();
        }

        //HandleEllipseButtonClick
        private void HandleEllipseButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.HandleEllipseButtonClick();
            RefreshButton();
            _isSelectMode = false;
            ResetSelection();
        }

        //HandleLineButtonClick
        private void HandleLineButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.HandleLineButtonClick();
            RefreshButton();
            _isSelectMode = false;
            ResetSelection();
        }

        //HandleClearButtonClick
        private void HandleClearButtonClick(object sender, RoutedEventArgs e)
        {
            ResetSelection();
            _drawingAppPresentationModel.Clear();
            _drawingAppPresentationModel.HandleClearButtonClick();
            RefreshButton();
            _isSelectMode = true;
            RefreshUserInterface();
        }

        //HandleCanvasPointerPressed
        public void HandleCanvasPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            List<Shape> shapes = _drawingAppPresentationModel.GetShapes;
            if (shapes.Count != 0 && _shapeFlag == ShapeFlag.Null)
            {
                if (GetShapeWithIndex(shapes, 0).GetShape == ShapeFlag.DotRectangle)
                {
                    DotRectangle target = (DotRectangle)shapes[shapes.Count - 1];
                    if (((GetShapePointX1(target) <= e.GetCurrentPoint(_canvas).Position.X && GetShapePointX2(target) >= e.GetCurrentPoint(_canvas).Position.X) || (GetShapePointX1(target) >= e.GetCurrentPoint(_canvas).Position.X && GetShapePointX2(target) <= e.GetCurrentPoint(_canvas).Position.X)) && ((GetShapePointY1(target) <= e.GetCurrentPoint(_canvas).Position.Y && GetShapePointY2(target) >= e.GetCurrentPoint(_canvas).Position.Y) || (GetShapePointY1(target) >= e.GetCurrentPoint(_canvas).Position.Y && GetShapePointY2(target) <= e.GetCurrentPoint(_canvas).Position.Y)))
                    {
                        _moveInitialX = e.GetCurrentPoint(_canvas).Position.X;
                        _moveInitialY = e.GetCurrentPoint(_canvas).Position.Y;
                        _movementX = e.GetCurrentPoint(_canvas).Position.X;
                        _movementY = e.GetCurrentPoint(_canvas).Position.Y;
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
                if (_shapeFlag == ShapeFlag.Line)
                {
                    if (IsInShape(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y) != null)
                    {
                        _drawingAppPresentationModel.PressedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y, IsInShape(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y));
                    }
                }
                else
                {
                    _drawingAppPresentationModel.PressedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y, null);
                }
                _isSelectMode = false;
                ResetSelection();
            }
            RefreshUserInterface();
        }

        //HandleCanvasPointerMoved
        public void HandleCanvasPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (_isMoving == true)
            {
                _drawingAppPresentationModel.HandleMove(e.GetCurrentPoint(_canvas).Position.X - _movementX, e.GetCurrentPoint(_canvas).Position.Y - _movementY);
                DotRectangle target = _drawingAppPresentationModel.GetDotRectangle();
                _label.Text = LABEL_HEAD + target.Shape.GetShape + LABEL_LEFT_BRACKET + TakeSmall(target.X1, target.X2) + LABEL_COMMA + TakeSmall(target.Y1, target.Y2) + LABEL_COMMA + TakeLarge(target.X1, target.X2) + LABEL_COMMA + TakeLarge(target.Y1, target.Y2) + LABEL_RIGHT_BRACKET;
                _movementX = e.GetCurrentPoint(_canvas).Position.X;
                _movementY = e.GetCurrentPoint(_canvas).Position.Y;
            }
            else if (_shapeFlag != ShapeFlag.Null)
            {
                _drawingAppPresentationModel.MovedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
            }
            RefreshUserInterface();
        }

        //HandleCanvasPointerReleased
        public void HandleCanvasPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (_isMoving == true)
            {
                _drawingAppPresentationModel.HandleMoveCommand(e.GetCurrentPoint(_canvas).Position.X - _moveInitialX, e.GetCurrentPoint(_canvas).Position.Y - _moveInitialY);
                _isMoving = false;
            }
            else if (_isSelectMode == true)
            {
                this.ResetSelection();
                HandleCanvasPointerReleasedForSelected(e);
            }
            else
            {
                this.ResetSelection();
                if (_shapeFlag != ShapeFlag.Null)
                {
                    HandleCanvasPointerReleasedForShapes(e);
                }
            }
        }

        //HandleCanvasPointerReleasedForSelected
        private void HandleCanvasPointerReleasedForSelected(PointerRoutedEventArgs e)
        {
            List<Shape> shapes = _drawingAppPresentationModel.GetShapes;
            for (int index = 0; index < shapes.Count; index++)
            {
                Shape aShape = GetShapeWithIndex(shapes, index);
                if (((GetShapePointX1(aShape) <= e.GetCurrentPoint(_canvas).Position.X && GetShapePointX2(aShape) >= e.GetCurrentPoint(_canvas).Position.X) || (GetShapePointX1(aShape) >= e.GetCurrentPoint(_canvas).Position.X && GetShapePointX2(aShape) <= e.GetCurrentPoint(_canvas).Position.X)) && ((GetShapePointY1(aShape) <= e.GetCurrentPoint(_canvas).Position.Y && GetShapePointY2(aShape) >= e.GetCurrentPoint(_canvas).Position.Y) || (GetShapePointY1(aShape) >= e.GetCurrentPoint(_canvas).Position.Y && GetShapePointY2(aShape) <= e.GetCurrentPoint(_canvas).Position.Y)))
                {
                    HandleCanvasPointerReleasedForSelectedTrue(aShape);
                    break;
                }
            }
        }

        //HandleCanvasPointerReleasedForSelectedTrue
        private void HandleCanvasPointerReleasedForSelectedTrue(Shape aShape)
        {
            DotRectangle dotRectangle = new DotRectangle();
            dotRectangle.Shape = aShape;
            _drawingAppPresentationModel.DrawDotRectangle(dotRectangle);
            if (aShape.GetShape == ShapeFlag.Line)
            {
                _label.Text = LABEL_HEAD + aShape.GetShape + LABEL_LEFT_BRACKET + Math.Round(aShape.X1, 0) + LABEL_COMMA + Math.Round(aShape.Y1, 0) + LABEL_COMMA + Math.Round(aShape.X2, 0) + LABEL_COMMA + Math.Round(aShape.Y2, 0) + LABEL_RIGHT_BRACKET;
            }
            else
            {
                _label.Text = LABEL_HEAD + aShape.GetShape + LABEL_LEFT_BRACKET + TakeSmall(aShape.X1, aShape.X2) + LABEL_COMMA + TakeSmall(aShape.Y1, aShape.Y2) + LABEL_COMMA + TakeLarge(aShape.X1, aShape.X2) + LABEL_COMMA + TakeLarge(aShape.Y1, aShape.Y2) + LABEL_RIGHT_BRACKET;
            }
        }

        //HandleCanvasPointerReleasedForShapes
        private void HandleCanvasPointerReleasedForShapes(PointerRoutedEventArgs e)
        {
            if (_shapeFlag == ShapeFlag.Line)
            {
                HandleCanvasPointerReleasedForLine(e);
            }
            else
            {
                HandleCanvasPointerReleasedForOtherShapes(e);
            }
            RefreshButton();
            RefreshUserInterface();
        }

        //HandleCanvasPointerReleasedForLine
        private void HandleCanvasPointerReleasedForLine(PointerRoutedEventArgs e)
        {
            if (_drawingAppPresentationModel.GetIsPressed == true)
            {
                Shape isInShapes = IsInShape(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
                if (isInShapes != null)
                {
                    _drawingAppPresentationModel.ReleasedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y, isInShapes);
                    _isSelectMode = true;
                }
                else
                {
                    PressCancel();
                }
            }
        }

        //HandleCanvasPointerReleasedForOtherShapes
        private void HandleCanvasPointerReleasedForOtherShapes(PointerRoutedEventArgs e)
        {
            _drawingAppPresentationModel.ReleasedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y, null);
            _isSelectMode = true;
        }

        //HandleModelChanged
        public void HandleModelChanged()
        {
            _drawingAppPresentationModel.Draw(_iGraphics);
        }

        //UndoHandler
        void UndoHandler(object sender, RoutedEventArgs e)
        {
            ResetSelection();
            _drawingAppPresentationModel.Undo();
            RefreshUserInterface();
        }

        //RedoHandler
        void RedoHandler(object sender, RoutedEventArgs e)
        {
            ResetSelection();
            _drawingAppPresentationModel.Redo();
            RefreshUserInterface();
        }

        //RefreshButton
        void RefreshButton()
        {
            _rectangle.IsEnabled = _drawingAppPresentationModel.IsRectangleButtonEnable;
            _ellipse.IsEnabled = _drawingAppPresentationModel.IsEllipseButtonEnable;
            _line.IsEnabled = _drawingAppPresentationModel.IsLineButtonEnable;
            _shapeFlag = _drawingAppPresentationModel.GetShapeFlag;
        }

        //RefreshUserInterface
        void RefreshUserInterface()
        {
            // 更新redo與undo是否為enabled
            _redo.IsEnabled = _drawingAppPresentationModel.IsRedoEnabled;
            _undo.IsEnabled = _drawingAppPresentationModel.IsUndoEnabled;
            _drawingAppPresentationModel.Draw(_iGraphics);
        }

        //TakeLarger
        public double TakeLarge(double number1, double number2)
        {
            if (number1 > number2)
            {
                return Math.Round(number1, 0);
            }
            return Math.Round(number2, 0);
        }

        //TakeSmaller
        public double TakeSmall(double number1, double number2)
        {
            if (number1 < number2)
            {
                return Math.Round(number1, 0);
            }
            return Math.Round(number2, 0);
        }

        //ReserSelection
        public void ResetSelection()
        {
            List<Shape> shapes = _drawingAppPresentationModel.GetShapes;
            if (shapes.Count != 0)
            {
                if (GetShapeWithIndex(shapes, 0).GetShape == ShapeFlag.DotRectangle)
                {
                    ClearSelection();
                    _label.Text = LABEL_DEFAULT;
                }
            }
        }

        //IsInShape
        public Shape IsInShape(double pointX, double pointY)
        {
            List<Shape> shapes = _drawingAppPresentationModel.GetShapes;
            for (int index = 0; index < shapes.Count; index++)
            {
                Shape aShape = GetShapeWithIndex(shapes, index);
                if ((aShape.GetShape != ShapeFlag.Line) && (((GetShapePointX1(aShape) <= pointX && GetShapePointX2(aShape) >= pointX) || (GetShapePointX1(aShape) >= pointX && GetShapePointX2(aShape) <= pointX)) && ((GetShapePointY1(aShape) <= pointY && GetShapePointY2(aShape) >= pointY) || (GetShapePointY1(aShape) >= pointY && GetShapePointY2(aShape) <= pointY))))
                {
                    return aShape;
                }
            }
            return null;
        }

        //PressCancel
        private void PressCancel()
        {
            _drawingAppPresentationModel.PressedCancel();
        }

        //ClearSelection
        private void ClearSelection()
        {
            _drawingAppPresentationModel.DeleteShape();
        }

        //GetShapeWithIndex
        private Shape GetShapeWithIndex(List<Shape> shapes, int index)
        {
            return shapes[shapes.Count - index - 1];
        }

        //GetShapePointX1
        private double GetShapePointX1(Shape shape)
        {
            return shape.X1;
        }

        //GetShapePointY1
        private double GetShapePointY1(Shape shape)
        {
            return shape.Y1;
        }

        //GetShapePointX2
        private double GetShapePointX2(Shape shape)
        {
            return shape.X2;
        }

        //GetShapePointY2
        private double GetShapePointY2(Shape shape)
        {
            return shape.Y2;
        }
    }
}
