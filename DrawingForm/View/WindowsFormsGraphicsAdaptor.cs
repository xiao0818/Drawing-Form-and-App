using DrawingModel;
using System.Drawing;

namespace DrawingForm
{
    public class WindowsFormsGraphicsAdaptor : IGraphics
    {
        Graphics _graphics;
        const int THICKNESS = 3;
        const int CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL = 4;
        const int CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER = 8;

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

        //DrawLine
        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            _graphics.DrawLine(new Pen(Color.Black, THICKNESS), (float)x1, (float)y1, (float)x2, (float)y2);
        }

        //DrawDotRectangle
        public void DrawDotRectangle(double x1, double y1, double x2, double y2)
        {
            Pen pen = new Pen(Color.Red, THICKNESS);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            _graphics.DrawRectangle(pen, (float)x1, (float)y1, (float)x2 - (float)x1, (float)y2 - (float)y1);
            _graphics.DrawEllipse(new Pen(Color.Black, THICKNESS), (float)x1 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, (float)y1 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER);
            _graphics.FillEllipse(new SolidBrush(Color.White), (float)x1 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, (float)y1 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER);
            _graphics.DrawEllipse(new Pen(Color.Black, THICKNESS), (float)x1 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, (float)y2 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER);
            _graphics.FillEllipse(new SolidBrush(Color.White), (float)x1 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, (float)y2 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER);
            _graphics.DrawEllipse(new Pen(Color.Black, THICKNESS), (float)x2 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, (float)y1 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER);
            _graphics.FillEllipse(new SolidBrush(Color.White), (float)x2 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, (float)y1 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER);
            _graphics.DrawEllipse(new Pen(Color.Black, THICKNESS), (float)x2 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, (float)y2 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER);
            _graphics.FillEllipse(new SolidBrush(Color.White), (float)x2 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, (float)y2 - CIRCLE_FOR_RECTANGLE_ANGLE_PIXEL, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER, CIRCLE_FOR_RECTANGLE_ANGLE_DIAMETER);
        }
    }
}
