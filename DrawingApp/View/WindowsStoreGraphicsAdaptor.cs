using DrawingModel;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DrawingApp
{
    // Windows Store App的繪圖方式採用"物件"模型(與Windows Forms完全不同)
    // 當繪圖時，必須先建立"圖形物件"，再將"圖形物件"加入畫布的Children，此後該圖形就會被畫出來
    // 由於畫布管理其Children，因此有以下優缺點
    //   優點：畫布可以自行處理OnPaint()，而使用者則省掉處理OnPaint()的麻煩
    //   缺點：繪圖時必須先建立"圖形物件"；清除某圖形時，必須刪除Children中對應的物件
    public class WindowsStoreGraphicsAdaptor : IGraphics
    {
        Canvas _canvas;
        const int CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL = 6;
        const int CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER = 12;
        const int THICKNESS = 3;
        const int DASH_PIXEL = 2;

        public WindowsStoreGraphicsAdaptor(Canvas canvas)
        {
            _canvas = canvas;
        }

        //ClearAll
        public void ClearAll()
        {
            // 清除Children也就清除畫布
            _canvas.Children.Clear();
        }

        //DrawRectangle
        public void DrawRectangle(double x1, double y1, double x2, double y2)
        {
            // 先建立圖形物件
            Windows.UI.Xaml.Shapes.Rectangle rectangle = new Windows.UI.Xaml.Shapes.Rectangle();
            rectangle.Margin = new Windows.UI.Xaml.Thickness(x1, y1, 0, 0);
            rectangle.Width = x2 - x1;
            rectangle.Height = y2 - y1;
            rectangle.Stroke = new SolidColorBrush(Colors.Black);
            rectangle.StrokeThickness = 1;
            rectangle.Fill = new SolidColorBrush(Colors.Yellow);
            // 將圖形物件加入Children
            _canvas.Children.Add(rectangle);
        }

        //DrawEllipse
        public void DrawEllipse(double x1, double y1, double x2, double y2)
        {
            // 先建立圖形物件
            Windows.UI.Xaml.Shapes.Ellipse ellipse = new Windows.UI.Xaml.Shapes.Ellipse();
            ellipse.Margin = new Windows.UI.Xaml.Thickness(x1, y1, 0, 0);
            ellipse.Width = x2 - x1;
            ellipse.Height = y2 - y1;
            ellipse.Stroke = new SolidColorBrush(Colors.Black);
            ellipse.StrokeThickness = 1;
            ellipse.Fill = new SolidColorBrush(Colors.Orange);
            // 將圖形物件加入Children
            _canvas.Children.Add(ellipse);
        }

        //DrawLine
        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            // 先建立圖形物件
            Windows.UI.Xaml.Shapes.Line line = new Windows.UI.Xaml.Shapes.Line();
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;
            line.Stroke = new SolidColorBrush(Colors.Black);
            // 將圖形物件加入Children
            _canvas.Children.Add(line);
        }

        //DrawDotRectangle
        public void DrawDotRectangle(double x1, double y1, double x2, double y2)
        {
            // 先建立圖形物件
            Windows.UI.Xaml.Shapes.Rectangle dotRectangle = new Windows.UI.Xaml.Shapes.Rectangle();
            dotRectangle.Margin = new Windows.UI.Xaml.Thickness(x1, y1, 0, 0);
            dotRectangle.Width = x2 - x1;
            dotRectangle.Height = y2 - y1;
            dotRectangle.Stroke = new SolidColorBrush(Colors.Red);
            dotRectangle.StrokeThickness = THICKNESS;
            dotRectangle.StrokeDashArray = new DoubleCollection()
            { 
                DASH_PIXEL, 1, 1, 1
            };
            // 將圖形物件加入Children
            _canvas.Children.Add(dotRectangle);
            _canvas.Children.Add(AngleCircle(x1 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, y1 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL));
            _canvas.Children.Add(AngleCircle(x1 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, y2 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL));
            _canvas.Children.Add(AngleCircle(x2 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, y1 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL));
            _canvas.Children.Add(AngleCircle(x2 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, y2 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL));
        }

        //AngleCircle
        private Windows.UI.Xaml.Shapes.Ellipse AngleCircle(double pointX, double pointY)
        {
            Windows.UI.Xaml.Shapes.Ellipse ellipse = new Windows.UI.Xaml.Shapes.Ellipse();
            ellipse.Margin = new Windows.UI.Xaml.Thickness(pointX, pointY, 0, 0);
            ellipse.Width = CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER;
            ellipse.Height = CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER;
            ellipse.Stroke = new SolidColorBrush(Colors.Black);
            ellipse.StrokeThickness = 1;
            ellipse.Fill = new SolidColorBrush(Colors.White);
            return ellipse;
        }
    }
}