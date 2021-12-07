using DrawingModel;
using System.Drawing;
using System.Windows.Forms;

namespace DrawingForm
{
    public partial class DrawingForm : Form
    {
        DrawingFormPresentationModel _drawingFormPresentationModel;
        Panel _canvas = new DoubleBufferedPanel();
        Button _rectangle = new Button();
        Button _ellipse = new Button();
        Button _clear = new Button();
        int _shapeFlag = (int)ShapeFlag.Null;
        const int HEIGHT = 30;
        const int WIDTH = 100;
        const int LOCATION_Y = 10;

        public DrawingForm(DrawingFormPresentationModel drawingFormPresentationModel)
        {
            InitializeComponent();
            //
            // prepare rectangle button
            //
            _rectangle.Text = "Rectangle";
            _rectangle.AutoSize = false;
            _rectangle.Height = HEIGHT;
            _rectangle.Width = WIDTH;
            _rectangle.Location = new Point(100, LOCATION_Y);
            _rectangle.Click += HandleRectangleButtonClick;
            Controls.Add(_rectangle);
            //
            // prepare ellipse button
            //
            _ellipse.Text = "Ellipse";
            _ellipse.AutoSize = false;
            _ellipse.Height = HEIGHT;
            _ellipse.Width = WIDTH;
            _ellipse.Location = new Point(358, LOCATION_Y);
            _ellipse.Click += HandleEllipseButtonClick;
            Controls.Add(_ellipse);
            //
            // prepare clear button
            //
            _clear.Text = "Clear";
            _clear.AutoSize = false;
            _clear.Height = HEIGHT;
            _clear.Width = WIDTH;
            _clear.Location = new Point(616, LOCATION_Y);
            _clear.Click += HandleClearButtonClick;
            Controls.Add(_clear);
            //
            // prepare canvas
            //
            _canvas.Dock = DockStyle.Fill;
            _canvas.BackColor = Color.LightYellow;
            _canvas.MouseDown += HandleCanvasPointerPressed;
            _canvas.MouseUp += HandleCanvasPointerReleased;
            _canvas.MouseMove += HandleCanvasPointerMoved;
            _canvas.Paint += HandleCanvasPaint;
            // Note: setting "_canvas.DoubleBuffered = true" does not work
            Controls.Add(_canvas);
            //
            // prepare presentation model and model
            //
            _drawingFormPresentationModel = drawingFormPresentationModel;
            _drawingFormPresentationModel._drawingFormPresentationModelChanged += HandleModelChanged;
        }

        //HandleRectangleButtonClick
        public void HandleRectangleButtonClick(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.HandleRectangleButtonClick();
            _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
            _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
            _shapeFlag = _drawingFormPresentationModel.GetShapeFlag;
        }

        //HandleEllipseButtonClick
        public void HandleEllipseButtonClick(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.HandleEllipseButtonClick();
            _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
            _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
            _shapeFlag = _drawingFormPresentationModel.GetShapeFlag;
        }

        //HandleClearButtonClick
        public void HandleClearButtonClick(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.Clear();
            _drawingFormPresentationModel.HandleClearButtonClick();
            _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
            _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
            _shapeFlag = _drawingFormPresentationModel.GetShapeFlag;
        }

        //HandleCanvasPointerPressed
        public void HandleCanvasPointerPressed(object sender, MouseEventArgs e)
        {
            if (_shapeFlag != (int)ShapeFlag.Null)
            {
                _drawingFormPresentationModel.PressedPointer(e.X, e.Y, _shapeFlag);
            }
        }

        //HandleCanvasPointerReleased
        public void HandleCanvasPointerReleased(object sender, MouseEventArgs e)
        {
            if (_shapeFlag != (int)ShapeFlag.Null)
            {
                _drawingFormPresentationModel.ReleasedPointer(e.X, e.Y);
                _drawingFormPresentationModel.HandleCanvasPointerReleased();
                _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
                _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
                _shapeFlag = _drawingFormPresentationModel.GetShapeFlag;
            }
        }

        //HandleCanvasPointerMoved
        public void HandleCanvasPointerMoved(object sender, MouseEventArgs e)
        {
            if (_shapeFlag != (int)ShapeFlag.Null)
            {
                _drawingFormPresentationModel.MovedPointer(e.X, e.Y);
            }
        }

        //HandleCanvasPaint
        public void HandleCanvasPaint(object sender, PaintEventArgs e)
        {
            // e.Graphics物件是Paint事件帶進來的，只能在當次Paint使用
            // 而Adaptor又直接使用e.Graphics，因此，Adaptor不能重複使用
            // 每次都要重新new
            _drawingFormPresentationModel.Draw(new WindowsFormsGraphicsAdaptor(e.Graphics));
        }

        //HandleModelChanged
        public void HandleModelChanged()
        {
            Invalidate(true);
        }
    }
}
