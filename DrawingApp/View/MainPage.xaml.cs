using DrawingModel;
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
        IGraphics _igraphics;
        int _shapeFlag = (int)ShapeFlag.Null;

        public MainPage()
        {
            this.InitializeComponent();
            _drawingAppPresentationModel = new DrawingAppPresentationModel(new Model());
            // Note: 重複使用_igraphics物件
            _igraphics = new View.WindowsStoreGraphicsAdaptor(_canvas);
            // Events
            _canvas.PointerPressed += HandleCanvasPointerPressed;
            _canvas.PointerReleased += HandleCanvasPointerReleased;
            _canvas.PointerMoved += HandleCanvasPointerMoved;
            _rectangle.Click += HandleRectangleButtonClick;
            _ellipse.Click += HandleEllipseButtonClick;
            _clear.Click += HandleClearButtonClick;
            _drawingAppPresentationModel._drawingAppPresentationModelChanged += HandleModelChanged;
        }

        //HandleRectangleButtonClick
        private void HandleRectangleButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.HandleRectangleButtonClick();
            _rectangle.IsEnabled = _drawingAppPresentationModel.IsRectangleButtonEnable;
            _ellipse.IsEnabled = _drawingAppPresentationModel.IsEllipseButtonEnable;
            _shapeFlag = _drawingAppPresentationModel.GetShapeFlag;
        }

        //HandleEllipseButtonClick
        private void HandleEllipseButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.HandleEllipseButtonClick();
            _rectangle.IsEnabled = _drawingAppPresentationModel.IsRectangleButtonEnable;
            _ellipse.IsEnabled = _drawingAppPresentationModel.IsEllipseButtonEnable;
            _shapeFlag = _drawingAppPresentationModel.GetShapeFlag;
        }

        //HandleClearButtonClick
        private void HandleClearButtonClick(object sender, RoutedEventArgs e)
        {
            _drawingAppPresentationModel.Clear();
            _drawingAppPresentationModel.HandleClearButtonClick();
            _rectangle.IsEnabled = _drawingAppPresentationModel.IsRectangleButtonEnable;
            _ellipse.IsEnabled = _drawingAppPresentationModel.IsEllipseButtonEnable;
            _shapeFlag = _drawingAppPresentationModel.GetShapeFlag;
        }

        //HandleCanvasPointerPressed
        public void HandleCanvasPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (_shapeFlag != (int)ShapeFlag.Null)
            {
                _drawingAppPresentationModel.PointerPressed(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y, _shapeFlag);
            }
        }

        //HandleCanvasPointerReleased
        public void HandleCanvasPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (_shapeFlag != (int)ShapeFlag.Null)
            {
                _drawingAppPresentationModel.PointerReleased(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
                _drawingAppPresentationModel.HandleCanvasPointerReleased();
                _rectangle.IsEnabled = _drawingAppPresentationModel.IsRectangleButtonEnable;
                _ellipse.IsEnabled = _drawingAppPresentationModel.IsEllipseButtonEnable;
                _shapeFlag = _drawingAppPresentationModel.GetShapeFlag;
            }
        }

        //HandleCanvasPointerMoved
        public void HandleCanvasPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (_shapeFlag != (int)ShapeFlag.Null)
            {
                _drawingAppPresentationModel.PointerMoved(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
            }
        }

        //HandleModelChanged
        public void HandleModelChanged()
        {
            _drawingAppPresentationModel.Draw(_igraphics);
        }
    }
}
