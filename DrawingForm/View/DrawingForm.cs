using DrawingModel;
using System.Collections.Generic;
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
        ToolStripButton _undo;
        ToolStripButton _redo;
        const int HEIGHT = 40;
        const int WIDTH = 100;
        const int LOCATION_Y = 30;
        const string LABEL_DEFAULT = "Selected : None";
        const string LABEL_HEAD = "Selected : ";
        const string LABEL_COMMA = ", ";
        const string LABEL_LEFT_BRACKET = " (";
        const string LABEL_RIGHT_BRACKET = ")";

        public DrawingForm(DrawingFormPresentationModel drawingFormPresentationModel)
        {
            InitializeComponent();
            ToolStrip toolStrip = new ToolStrip();
            //Controls.Add(ts);
            toolStrip.Parent = this;
            _undo = new ToolStripButton("Undo", null, UndoHandler);
            _undo.Enabled = false;
            toolStrip.Items.Add(_undo);
            _redo = new ToolStripButton("Redo", null, RedoHandler);
            _redo.Enabled = false;
            toolStrip.Items.Add(_redo);
            //
            // prepare button
            //
            _rectangle.Text = "Rectangle";
            _ellipse.Text = "Ellipse";
            _line.Text = "Line";
            _clear.Text = "Clear";
            _rectangle.AutoSize = _ellipse.AutoSize = _line.AutoSize = _clear.AutoSize = false;
            _rectangle.Height = _ellipse.Height = _line.Height = _clear.Height = HEIGHT;
            _rectangle.Width = _ellipse.Width = _line.Width = _clear.Width = WIDTH;
            _rectangle.Location = new Point(100, LOCATION_Y);
            _ellipse.Location = new Point(450, LOCATION_Y);
            _line.Location = new Point(800, LOCATION_Y);
            _clear.Location = new Point(1150, LOCATION_Y);
            _rectangle.Click += HandleRectangleButtonClick;
            _ellipse.Click += HandleEllipseButtonClick;
            _line.Click += HandleLineButtonClick;
            _clear.Click += HandleClearButtonClick;
            Controls.Add(_rectangle);
            Controls.Add(_ellipse);
            Controls.Add(_line);
            Controls.Add(_clear);
            //
            // prepare label
            //
            _label.Name = "_label";
            _label.Text = LABEL_DEFAULT;
            _label.Height = 20;
            _label.Width = 250;
            _label.Location = new Point(1050, 680);
            _label.BackColor = Color.LightYellow;
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
            _drawingFormPresentationModel._drawingFormPresentationModelChanged += UpdateLabel;
            //
            // Use Double buffer
            //
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        //HandleRectangleButtonClick
        public void HandleRectangleButtonClick(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.HandleRectangleButtonClick();
            RefreshButton();
        }

        //HandleEllipseButtonClick
        public void HandleEllipseButtonClick(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.HandleEllipseButtonClick();
            RefreshButton();
        }

        //HandleLineButtonClick
        public void HandleLineButtonClick(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.HandleLineButtonClick();
            RefreshButton();
        }

        //HandleClearButtonClick
        public void HandleClearButtonClick(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.Clear();
            _drawingFormPresentationModel.HandleClearButtonClick();
            RefreshButton();
            RefreshUserInterface();
        }

        //HandleCanvasPointerPressed
        public void HandleCanvasPointerPressed(object sender, MouseEventArgs e)
        {
            _drawingFormPresentationModel.PressedPointer(e.X, e.Y);
            RefreshUserInterface();
        }

        //HandleCanvasPointerMoved
        public void HandleCanvasPointerMoved(object sender, MouseEventArgs e)
        {
            _drawingFormPresentationModel.MovedPointer(e.X, e.Y);
            RefreshUserInterface();
        }

        //HandleCanvasPointerReleased
        public void HandleCanvasPointerReleased(object sender, MouseEventArgs e)
        {
            _drawingFormPresentationModel.ReleasedPointer(e.X, e.Y);
            RefreshButton();
            RefreshUserInterface();
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

        //UndoHandler
        void UndoHandler(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.Undo();
            RefreshUserInterface();
        }

        //RedoHandler
        void RedoHandler(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.Redo();
            RefreshUserInterface();
        }

        //RefreshButton
        void RefreshButton()
        {
            _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
            _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
            _line.Enabled = _drawingFormPresentationModel.IsLineButtonEnable;
        }

        //RefreshUserInterface
        void RefreshUserInterface()
        {
            // 更新redo與undo是否為enabled
            _redo.Enabled = _drawingFormPresentationModel.IsRedoEnabled;
            _undo.Enabled = _drawingFormPresentationModel.IsUndoEnabled;
            Invalidate();
        }

        //TakeLarger
        public double TakeLarge(double number1, double number2)
        {
            if (number1 > number2)
            {
                return number1;
            }
            return number2;
        }

        //TakeSmaller
        public double TakeSmall(double number1, double number2)
        {
            if (number1 < number2)
            {
                return number1;
            }
            return number2;
        }

        //GetShapeWithIndex
        private Shape GetShapeWithIndex(List<Shape> shapes, int index)
        {
            return shapes[shapes.Count - index - 1];
        }

        //GetShapePointX1
        private double GetShapePointX1(Shape shape)
        {
            return shape.X1;
        }

        //GetShapePointY1
        private double GetShapePointY1(Shape shape)
        {
            return shape.Y1;
        }

        //GetShapePointX2
        private double GetShapePointX2(Shape shape)
        {
            return shape.X2;
        }

        //GetShapePointY2
        private double GetShapePointY2(Shape shape)
        {
            return shape.Y2;
        }

        //UpdateLabel
        public void UpdateLabel()
        {
            List<Shape> shapes = _drawingFormPresentationModel.GetShapes;
            if (shapes.Count != 0)
            {
                if (GetShapeWithIndex(shapes, 0).GetShape == ShapeFlag.DotRectangle)
                {
                    DotRectangle target = _drawingFormPresentationModel.GetTarget();
                    _label.Text = LABEL_HEAD + target.Shape.GetShape + LABEL_LEFT_BRACKET + TakeSmall(GetShapePointX1(target), GetShapePointX2(target)) + LABEL_COMMA + TakeSmall(GetShapePointY1(target), GetShapePointY2(target)) + LABEL_COMMA + TakeLarge(GetShapePointX1(target), GetShapePointX2(target)) + LABEL_COMMA + TakeLarge(GetShapePointY1(target), GetShapePointY2(target)) + LABEL_RIGHT_BRACKET;
                }
                else
                {
                    _label.Text = LABEL_DEFAULT;
                }
            }
            else
            {
                _label.Text = LABEL_DEFAULT;
            }
        }
    }
}
