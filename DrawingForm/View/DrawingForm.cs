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
        Button _line = new Button();
        Button _clear = new Button();
        Label _label = new Label();
        int _shapeFlag = (int)ShapeFlag.Null;
        const int HEIGHT = 40;
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
            _ellipse.Location = new Point(450, LOCATION_Y);
            _ellipse.Click += HandleEllipseButtonClick;
            Controls.Add(_ellipse);
            //
            // prepare line button
            //
            _line.Text = "Line";
            _line.AutoSize = false;
            _line.Height = HEIGHT;
            _line.Width = WIDTH;
            _line.Location = new Point(800, LOCATION_Y);
            _line.Click += HandleLineButtonClick;
            Controls.Add(_line);
            //
            // prepare clear button
            //
            _clear.Text = "Clear";
            _clear.AutoSize = false;
            _clear.Height = HEIGHT;
            _clear.Width = WIDTH;
            _clear.Location = new Point(1150, LOCATION_Y);
            _clear.Click += HandleClearButtonClick;
            Controls.Add(_clear);
            //
            // prepare label
            //
            _label.Name = "_label";
            _label.Text = "";
            _label.Height = HEIGHT;
            _label.Width = 2 * WIDTH;
            _label.Location = new Point(1100, 660);
            _label.BackColor = Color.White;
            Controls.Add(_label);
            //
            // prepare canvas
            //
            _canvas.Name = "_canvas";
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
            _line.Enabled = _drawingFormPresentationModel.IsLineButtonEnable;
            _shapeFlag = _drawingFormPresentationModel.GetShapeFlag;
        }

        //HandleEllipseButtonClick
        public void HandleEllipseButtonClick(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.HandleEllipseButtonClick();
            _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
            _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
            _line.Enabled = _drawingFormPresentationModel.IsLineButtonEnable;
            _shapeFlag = _drawingFormPresentationModel.GetShapeFlag;
        }

        //HandleLineButtonClick
        public void HandleLineButtonClick(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.HandleLineButtonClick();
            _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
            _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
            _line.Enabled = _drawingFormPresentationModel.IsLineButtonEnable;
            _shapeFlag = _drawingFormPresentationModel.GetShapeFlag;
        }

        //HandleClearButtonClick
        public void HandleClearButtonClick(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.Clear();
            _drawingFormPresentationModel.HandleClearButtonClick();
            _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
            _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
            _line.Enabled = _drawingFormPresentationModel.IsLineButtonEnable;
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
                _line.Enabled = _drawingFormPresentationModel.IsLineButtonEnable;
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
