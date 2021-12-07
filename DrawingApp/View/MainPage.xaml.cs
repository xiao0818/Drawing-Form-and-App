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
        Model _model;
        IGraphics _igraphics;
        int _shapeFlag = (int)ShapeFlag.Null;

        public MainPage()
        {
            this.InitializeComponent();
            // Model
            _model = new Model();
            // Note: 重複使用_igraphics物件
            _igraphics = new View.WindowsStoreGraphicsAdaptor(_canvas);
            // Events
            _canvas.PointerPressed += HandleCanvasPointerPressed;
            _canvas.PointerReleased += HandleCanvasPointerReleased;
            _canvas.PointerMoved += HandleCanvasPointerMoved;
            _rectangle.Click += HandleRectangleButtonClick;
            _ellipse.Click += HandleEllipseButtonClick;
            _clear.Click += HandleClearButtonClick;
            _model._modelChanged += HandleModelChanged;
        }

        //HandleRectangleButtonClick
        private void HandleRectangleButtonClick(object sender, RoutedEventArgs e)
        {
            _rectangle.IsEnabled = false;
            _ellipse.IsEnabled = true;
            _shapeFlag = (int)ShapeFlag.Rectangle;
        }

        //HandleEllipseButtonClick
        private void HandleEllipseButtonClick(object sender, RoutedEventArgs e)
        {
            _rectangle.IsEnabled = true;
            _ellipse.IsEnabled = false;
            _shapeFlag = (int)ShapeFlag.Ellipse;
        }

        //HandleClearButtonClick
        private void HandleClearButtonClick(object sender, RoutedEventArgs e)
        {
            _model.Clear();
            _rectangle.IsEnabled = true;
            _ellipse.IsEnabled = true;
            _shapeFlag = (int)ShapeFlag.Null;
        }

        //HandleCanvasPointerPressed
        public void HandleCanvasPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (_shapeFlag != (int)ShapeFlag.Null)
            {
                _model.PointerPressed(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y, _shapeFlag);
            }
        }

        //HandleCanvasPointerReleased
        public void HandleCanvasPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (_shapeFlag != (int)ShapeFlag.Null)
            {
                _model.PointerReleased(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
                _rectangle.IsEnabled = true;
                _ellipse.IsEnabled = true;
                _shapeFlag = (int)ShapeFlag.Null;
            }
        }

        //HandleCanvasPointerMoved
        public void HandleCanvasPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (_shapeFlag != (int)ShapeFlag.Null)
            {
                _model.PointerMoved(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
            }
        }

        //HandleModelChanged
        public void HandleModelChanged()
        {
            _model.Draw(_igraphics);
        }
    }
}
