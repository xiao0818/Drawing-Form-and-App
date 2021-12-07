using DrawingModel;
using System.Drawing;

namespace DrawingForm
{
    class WindowsFormsGraphicsAdaptor : IGraphics
    {
        Graphics _graphics;

        public WindowsFormsGraphicsAdaptor(Graphics graphics)
        {
            this._graphics = graphics;
        }

        //ClearAll
        public void ClearAll()
        {
            // OnPaint時會自動清除畫面，因此不需實作
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
            _graphics.DrawRectangle(new Pen(Color.Black, 3), (float)x1, (float)y1, (float)x2 - (float)x1, (float)y2 - (float)y1);
            _graphics.FillRectangle(new SolidBrush(Color.Yellow), (float)x1, (float)y1, (float)x2 - (float)x1, (float)y2 - (float)y1);
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
            _graphics.DrawEllipse(new Pen(Color.Black, 3), (float)x1, (float)y1, (float)x2 - (float)x1, (float)y2 - (float)y1);
            _graphics.FillEllipse(new SolidBrush(Color.Orange), (float)x1, (float)y1, (float)x2 - (float)x1, (float)y2 - (float)y1);
        }
    }
}
