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
        const string LABEL_DEFAULT = "Selected : None";
        const string LABEL_HEAD = "Selected : ";
        const string LABEL_COMMA = ", ";
        const string LABEL_LEFT_BRACKET = " (";
        const string LABEL_RIGHT_BRACKET = ")";

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
            _save.Click += HandleSaveButtonClick;
            _load.Click += HandleLoadButtonClick;
            _undo.Click += UndoHandler;
            _redo.Click += RedoHandler;
            _drawingAppPresentationModel._drawingAppPresentationModelChanged += HandleModelChanged;
        }

        //HandleRectangleButtonClick
        private void HandleRectangleButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.HandleRectangleButtonClick();
            RefreshButton();
        }

        //HandleEllipseButtonClick
        private void HandleEllipseButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.HandleEllipseButtonClick();
            RefreshButton();
        }

        //HandleLineButtonClick
        private void HandleLineButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.HandleLineButtonClick();
            RefreshButton();
        }

        //HandleClearButtonClick
        private void HandleClearButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.HandleClearButtonClick();
            RefreshButton();
            RefreshUserInterface();
        }

        //HandleCanvasPointerPressed
        public void HandleCanvasPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            _drawingAppPresentationModel.PressedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
            RefreshUserInterface();
        }

        //HandleCanvasPointerMoved
        public void HandleCanvasPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            _drawingAppPresentationModel.MovedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
            RefreshUserInterface();
        }

        //HandleCanvasPointerReleased
        public void HandleCanvasPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            _drawingAppPresentationModel.ReleasedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
            RefreshButton();
            RefreshUserInterface();
        }

        //HandleModelChanged
        public void HandleModelChanged()
        {
            _drawingAppPresentationModel.Draw(_iGraphics);
        }

        //UndoHandler
        void UndoHandler(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.Undo();
            RefreshUserInterface();
        }

        //RedoHandler
        void RedoHandler(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.Redo();
            RefreshUserInterface();
        }

        //RefreshButton
        void RefreshButton()
        {
            _rectangle.IsEnabled = _drawingAppPresentationModel.IsRectangleButtonEnable;
            _ellipse.IsEnabled = _drawingAppPresentationModel.IsEllipseButtonEnable;
            _line.IsEnabled = _drawingAppPresentationModel.IsLineButtonEnable;
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

        //UpdateLabel
        public void UpdateLabel()
        {
            List<Shape> shapes = _drawingAppPresentationModel.GetShapes;
            if (shapes.Count != 0)
            {
                if (GetShapeWithIndex(shapes, 0).GetShape == ShapeFlag.DotRectangle)
                {
                    DotRectangle target = _drawingAppPresentationModel.GetTarget();
                    _label.Text = LABEL_HEAD + target.Shape.GetShape + LABEL_LEFT_BRACKET + TakeSmall(GetShapePointX1(target), GetShapePointX2(target)) + LABEL_COMMA + TakeSmall(GetShapePointY1(target), GetShapePointY2(target)) + LABEL_COMMA + TakeLarge(GetShapePointX1(target), GetShapePointX2(target)) + LABEL_COMMA + TakeLarge(GetShapePointY1(target), GetShapePointY2(target)) + LABEL_RIGHT_BRACKET;
                }
                else
                {
                    _label.Text = LABEL_DEFAULT;
                }
            }
            else
            {
                _label.Text = LABEL_DEFAULT;
            }
        }

        //HandleSaveButtonClick
        public void HandleSaveButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.HandleSaveButtonClick();
            RefreshButton();
            RefreshUserInterface();
        }

        //HandleLoadButtonClick
        public void HandleLoadButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.HandleLoadButtonClickAsync();
            RefreshButton();
            RefreshUserInterface();
        }
    }
}
