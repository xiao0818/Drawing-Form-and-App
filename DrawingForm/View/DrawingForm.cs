using DrawingModel;
using System.Drawing;
using System.Windows.Forms;

namespace DrawingForm
{
    public partial class DrawingForm : Form
    {
        DrawingModel.Model _model;
        Panel _canvas = new DoubleBufferedPanel();
        Button rectangle = new Button();
        Button ellipse = new Button();
        Button clear = new Button();
        int shapeFlag = (int)ShapeFlag.Null;

        public DrawingForm(Model model)
        {
            InitializeComponent();
            //
            // prepare clear button
            //
            rectangle.Text = "Rectangle";
            rectangle.AutoSize = false;
            rectangle.Height = 30;
            rectangle.Width = 100;
            rectangle.Location = new Point(100, 10);
            rectangle.Click += HandleRectangleButtonClick;
            Controls.Add(rectangle);
            //
            // prepare clear button
            //
            ellipse.Text = "Ellipse";
            ellipse.AutoSize = false;
            ellipse.Height = 30;
            ellipse.Width = 100;
            ellipse.Location = new Point(358, 10);
            ellipse.Click += HandleEllipseButtonClick;
            Controls.Add(ellipse);
            //
            // prepare clear button
            //
            clear.Text = "Clear";
            clear.AutoSize = false;
            clear.Height = 30;
            clear.Width = 100;
            clear.Location = new Point(616, 10);
            clear.Click += HandleClearButtonClick;
            Controls.Add(clear);
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
            _model = model;
            _model._modelChanged += HandleModelChanged;
        }

        public void HandleRectangleButtonClick(object sender, System.EventArgs e)
        {
            rectangle.Enabled = false;
            ellipse.Enabled = true;
            shapeFlag = (int)ShapeFlag.Rectangle;
        }

        public void HandleEllipseButtonClick(object sender, System.EventArgs e)
        {
            rectangle.Enabled = true;
            ellipse.Enabled = false;
            shapeFlag = (int)ShapeFlag.Ellipse;
        }

        public void HandleClearButtonClick(object sender, System.EventArgs e)
        {
            _model.Clear();
            rectangle.Enabled = true;
            ellipse.Enabled = true;
            shapeFlag = (int)ShapeFlag.Null;
        }

        public void HandleCanvasPointerPressed(object sender, MouseEventArgs e)
        {
            if (shapeFlag != (int)ShapeFlag.Null)
            {
                _model.PointerPressed(e.X, e.Y, shapeFlag);
            }
        }

        public void HandleCanvasPointerReleased(object sender, MouseEventArgs e)
        {
            if (shapeFlag != (int)ShapeFlag.Null)
            {
                _model.PointerReleased(e.X, e.Y);
                rectangle.Enabled = true;
                ellipse.Enabled = true;
                shapeFlag = (int)ShapeFlag.Null;
            }
        }

        public void HandleCanvasPointerMoved(object sender, MouseEventArgs e)
        {
            if (shapeFlag != (int)ShapeFlag.Null)
            {
                _model.PointerMoved(e.X, e.Y);
            }
        }

        public void HandleCanvasPaint(object sender, PaintEventArgs e)
        {
            // e.Graphics物件是Paint事件帶進來的，只能在當次Paint使用
            // 而Adaptor又直接使用e.Graphics，因此，Adaptor不能重複使用
            // 每次都要重新new
            _model.Draw(new PresentationModel.WindowsFormsGraphicsAdaptor(e.Graphics));
        }

        public void HandleModelChanged()
        {
            Invalidate(true);
        }
    }
}
