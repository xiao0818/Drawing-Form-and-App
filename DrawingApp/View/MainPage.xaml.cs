using DrawingModel;
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

        public MainPage()
        {
            this.InitializeComponent();
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
            _rectangle.IsEnabled = _drawingAppPresentationModel.IsRectangleButtonEnable;
            _ellipse.IsEnabled = _drawingAppPresentationModel.IsEllipseButtonEnable;
            _line.IsEnabled = _drawingAppPresentationModel.IsLineButtonEnable;
            _shapeFlag = _drawingAppPresentationModel.GetShapeFlag;
            _isSelectMode = false;
            this.ResetSelection();
        }

        //HandleEllipseButtonClick
        private void HandleEllipseButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.HandleEllipseButtonClick();
            _rectangle.IsEnabled = _drawingAppPresentationModel.IsRectangleButtonEnable;
            _ellipse.IsEnabled = _drawingAppPresentationModel.IsEllipseButtonEnable;
            _line.IsEnabled = _drawingAppPresentationModel.IsLineButtonEnable;
            _shapeFlag = _drawingAppPresentationModel.GetShapeFlag;
            _isSelectMode = false;
            this.ResetSelection();
        }

        //HandleLineButtonClick
        private void HandleLineButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.HandleLineButtonClick();
            _rectangle.IsEnabled = _drawingAppPresentationModel.IsRectangleButtonEnable;
            _ellipse.IsEnabled = _drawingAppPresentationModel.IsEllipseButtonEnable;
            _line.IsEnabled = _drawingAppPresentationModel.IsLineButtonEnable;
            _shapeFlag = _drawingAppPresentationModel.GetShapeFlag;
            _isSelectMode = false;
            this.ResetSelection();
        }

        //HandleClearButtonClick
        private void HandleClearButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.Clear();
            _drawingAppPresentationModel.HandleClearButtonClick();
            _rectangle.IsEnabled = _drawingAppPresentationModel.IsRectangleButtonEnable;
            _ellipse.IsEnabled = _drawingAppPresentationModel.IsEllipseButtonEnable;
            _line.IsEnabled = _drawingAppPresentationModel.IsLineButtonEnable;
            _shapeFlag = _drawingAppPresentationModel.GetShapeFlag;
            _isSelectMode = true;
            this.ResetSelection();
            RefreshUI();
        }

        //HandleCanvasPointerPressed
        public void HandleCanvasPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (_shapeFlag != ShapeFlag.Null)
            {
                if (_shapeFlag == ShapeFlag.Line)
                {
                    if (IsInShape(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y) != null)
                    {
                        _drawingAppPresentationModel.PressedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y, _shapeFlag, IsInShape(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y));
                    }
                }
                else
                {
                    _drawingAppPresentationModel.PressedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y, _shapeFlag, null);
                }
                _isSelectMode = false;
                this.ResetSelection();
            }
            RefreshUI();
        }

        //HandleCanvasPointerMoved
        public void HandleCanvasPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (_shapeFlag != ShapeFlag.Null)
            {
                _drawingAppPresentationModel.MovedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
            }
            RefreshUI();
        }

        //HandleCanvasPointerReleased
        public void HandleCanvasPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            this.ResetSelection();
            if (_isSelectMode == true)
            {
                List<Shape> shapes = _drawingAppPresentationModel.GetShapes;
                for (int index = 0; index < shapes.Count; index++)
                {
                    Shape aShape = shapes[shapes.Count - index - 1];
                    if (((aShape.X1 <= e.GetCurrentPoint(_canvas).Position.X && aShape.X2 >= e.GetCurrentPoint(_canvas).Position.X) || (aShape.X1 >= e.GetCurrentPoint(_canvas).Position.X && aShape.X2 <= e.GetCurrentPoint(_canvas).Position.X)) && ((aShape.Y1 <= e.GetCurrentPoint(_canvas).Position.Y && aShape.Y2 >= e.GetCurrentPoint(_canvas).Position.Y) || (aShape.Y1 >= e.GetCurrentPoint(_canvas).Position.Y && aShape.Y2 <= e.GetCurrentPoint(_canvas).Position.Y)))
                    {
                        _drawingAppPresentationModel.PressedPointer(aShape.X1, aShape.Y1, ShapeFlag.DotRectangle, null);
                        _drawingAppPresentationModel.ReleasedPointer(aShape.X2, aShape.Y2, null);
                        _label.Text = "Selected : " + aShape.GetShape + " (" + TakeSmall(aShape.X1, aShape.X2) + ", " + TakeSmall(aShape.Y1, aShape.Y2) + ", " + TakeLarge(aShape.X1, aShape.X2) + ", " + TakeLarge(aShape.Y1, aShape.Y2) + ")";
                        break;
                    }
                }
            }
            else
            {
                if (_shapeFlag != ShapeFlag.Null)
                {
                    if (_shapeFlag == ShapeFlag.Line)
                    {
                        if (_drawingAppPresentationModel.GetIsPressed == true)
                        {
                            if (IsInShape(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y) != null)
                            {
                                _drawingAppPresentationModel.ReleasedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y, IsInShape(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y));
                                _drawingAppPresentationModel.HandleCanvasPointerReleased();
                                _rectangle.IsEnabled = _drawingAppPresentationModel.IsRectangleButtonEnable;
                                _ellipse.IsEnabled = _drawingAppPresentationModel.IsEllipseButtonEnable;
                                _line.IsEnabled = _drawingAppPresentationModel.IsLineButtonEnable;
                                _shapeFlag = _drawingAppPresentationModel.GetShapeFlag;
                                RefreshUI();
                                _isSelectMode = true;
                            }
                            else
                            {
                                _drawingAppPresentationModel.PressedCancel();
                            }
                        }
                    }
                    else
                    {
                        _drawingAppPresentationModel.ReleasedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y, null);
                        _drawingAppPresentationModel.HandleCanvasPointerReleased();
                        _rectangle.IsEnabled = _drawingAppPresentationModel.IsRectangleButtonEnable;
                        _ellipse.IsEnabled = _drawingAppPresentationModel.IsEllipseButtonEnable;
                        _line.IsEnabled = _drawingAppPresentationModel.IsLineButtonEnable;
                        _shapeFlag = _drawingAppPresentationModel.GetShapeFlag;
                        RefreshUI();
                        _isSelectMode = true;
                    }
                }
            }
        }

        //HandleModelChanged
        public void HandleModelChanged()
        {
            _drawingAppPresentationModel.Draw(_iGraphics);
        }

        //UndoHandler
        void UndoHandler(object sender, RoutedEventArgs e)
        {
            this.ResetSelection();
            _drawingAppPresentationModel.Undo();
            RefreshUI();
        }

        //RedoHandler
        void RedoHandler(object sender, RoutedEventArgs e)
        {
            this.ResetSelection();
            _drawingAppPresentationModel.Redo();
            RefreshUI();
        }

        //RefreshUI
        void RefreshUI()
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
                return number1;
            }
            return number2;
        }

        //TakeSmaller
        public double TakeSmall(double number1, double number2)
        {
            if (number1 < number2)
            {
                return number1;
            }
            return number2;
        }

        //ReserSelection
        public void ResetSelection()
        {
            List<Shape> shapes = _drawingAppPresentationModel.GetShapes;
            if (shapes.Count != 0)
            {
                if (shapes[shapes.Count - 1].GetShape == ShapeFlag.DotRectangle)
                {
                    _drawingAppPresentationModel.Undo();
                    _label.Text = "Selected : None";
                }
            }
        }

        //IsInShape
        public Shape IsInShape(double x, double y)
        {
            List<Shape> shapes = _drawingAppPresentationModel.GetShapes;
            for (int index = 0; index < shapes.Count; index++)
            {
                Shape aShape = shapes[shapes.Count - index - 1];
                if ((aShape.GetShape != ShapeFlag.Line) && (((aShape.X1 <= x && aShape.X2 >= x) || (aShape.X1 >= x && aShape.X2 <= x)) && ((aShape.Y1 <= y && aShape.Y2 >= y) || (aShape.Y1 >= y && aShape.Y2 <= y))))
                {
                    return aShape;
                }
            }
            return null;
        }
    }
}
