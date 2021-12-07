using DrawingModel;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DrawingApp.View
{
    // Windows Store App的繪圖方式採用"物件"模型(與Windows Forms完全不同)
    // 當繪圖時，必須先建立"圖形物件"，再將"圖形物件"加入畫布的Children，此後該圖形就會被畫出來
    // 由於畫布管理其Children，因此有以下優缺點
    //   優點：畫布可以自行處理OnPaint()，而使用者則省掉處理OnPaint()的麻煩
    //   缺點：繪圖時必須先建立"圖形物件"；清除某圖形時，必須刪除Children中對應的物件
    class WindowsStoreGraphicsAdaptor : IGraphics
    {
        Canvas _canvas;

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
            if (x1 > x2)
            {
                double temp = x1;
                x1 = x2;
                x2 = temp;
            }
            if (y1 > y2)
            {
                double temp = y1;
                y1 = y2;
                y2 = temp;
            }
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
            if (x1 > x2)
            {
                double temp = x1;
                x1 = x2;
                x2 = temp;
            }
            if (y1 > y2)
            {
                double temp = y1;
                y1 = y2;
                y2 = temp;
            }
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
    }
}