using DrawingModel;
using System.Drawing;

namespace DrawingForm
{
    public class WindowsFormsGraphicsAdaptor : IGraphics
    {
        Graphics _graphics;
        const int THICKNESS = 3;

        public WindowsFormsGraphicsAdaptor(Graphics graphics)
        {
            this._graphics = graphics;
        }

        //GetGraphics
        public Graphics GetGraphics
        {
            get
            {
                return _graphics;
            }
        }

        //ClearAll
        public void ClearAll()
        {
            // OnPaint時會自動清除畫面，因此不需實作
        }

        //DrawRectangle
        public void DrawRectangle(double x1, double y1, double x2, double y2)
        {
            _graphics.DrawRectangle(new Pen(Color.Black, THICKNESS), (float)x1, (float)y1, (float)x2 - (float)x1, (float)y2 - (float)y1);
            _graphics.FillRectangle(new SolidBrush(Color.Yellow), (float)x1, (float)y1, (float)x2 - (float)x1, (float)y2 - (float)y1);
        }

        //DrawEllipse
        public void DrawEllipse(double x1, double y1, double x2, double y2)
        {
            _graphics.DrawEllipse(new Pen(Color.Black, THICKNESS), (float)x1, (float)y1, (float)x2 - (float)x1, (float)y2 - (float)y1);
            _graphics.FillEllipse(new SolidBrush(Color.Orange), (float)x1, (float)y1, (float)x2 - (float)x1, (float)y2 - (float)y1);
        }
    }
}
