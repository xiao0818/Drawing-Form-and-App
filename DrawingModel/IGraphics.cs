namespace DrawingModel
{
    public interface IGraphics
    {
        //ClearAll
        void ClearAll();

        //DrawRectangle
        void DrawRectangle(double x1, double y1, double x2, double y2);

        //DrawEllipse
        void DrawEllipse(double x1, double y1, double x2, double y2);

        //DrawLine
        void DrawLine(double x1, double y1, double x2, double y2);
    }
}