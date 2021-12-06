namespace DrawingModel
{
    public interface IGraphics
    {
        void ClearAll();
        void DrawRectangle(double x1, double y1, double x2, double y2);
        void DrawEllipse(double x1, double y1, double x2, double y2);
    }
}