using DrawingModel;
using System;
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
        bool _isSelectMode = true;
        ShapeFlag _shapeFlag = ShapeFlag.Null;
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
            _isSelectMode = false;
            ResetSelection();
        }

        //HandleEllipseButtonClick
        public void HandleEllipseButtonClick(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.HandleEllipseButtonClick();
            RefreshButton();
            _isSelectMode = false;
            ResetSelection();
        }

        //HandleLineButtonClick
        public void HandleLineButtonClick(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.HandleLineButtonClick();
            RefreshButton();
            _isSelectMode = false;
            ResetSelection();
        }

        //HandleClearButtonClick
        public void HandleClearButtonClick(object sender, System.EventArgs e)
        {
            ResetSelection();
            _drawingFormPresentationModel.Clear();
            _drawingFormPresentationModel.HandleClearButtonClick();
            RefreshButton();
            _isSelectMode = true;
            RefreshUserInterface();
        }

        //HandleCanvasPointerPressed
        public void HandleCanvasPointerPressed(object sender, MouseEventArgs e)
        {
            if (_shapeFlag != ShapeFlag.Null)
            {
                if (_shapeFlag == ShapeFlag.Line)
                {
                    if (IsInShape(e.X, e.Y) != null)
                    {
                        _drawingFormPresentationModel.PressedPointer(e.X, e.Y, IsInShape(e.X, e.Y));
                    }
                }
                else
                {
                    _drawingFormPresentationModel.PressedPointer(e.X, e.Y, null);
                }
                _isSelectMode = false;
                ResetSelection();
            }
            RefreshUserInterface();
        }

        //HandleCanvasPointerMoved
        public void HandleCanvasPointerMoved(object sender, MouseEventArgs e)
        {
            if (_shapeFlag != ShapeFlag.Null)
            {
                _drawingFormPresentationModel.MovedPointer(e.X, e.Y);
            }
            RefreshUserInterface();
        }

        //HandleCanvasPointerReleased
        public void HandleCanvasPointerReleased(object sender, MouseEventArgs e)
        {
            this.ResetSelection();
            if (_isSelectMode == true)
            {
                HandleCanvasPointerReleasedForSelected(e);
            }
            else
            {
                if (_shapeFlag != ShapeFlag.Null)
                {
                    HandleCanvasPointerReleasedForShapes(e);
                }
            }
        }

        //HandleCanvasPointerReleasedForSelected
        private void HandleCanvasPointerReleasedForSelected(MouseEventArgs e)
        {
            List<Shape> shapes = _drawingFormPresentationModel.GetShapes;
            for (int index = 0; index < shapes.Count; index++)
            {
                Shape aShape = GetShapeWithIndex(shapes, index);
                if (((GetShapePointX1(aShape) <= e.X && GetShapePointX2(aShape) >= e.X) || (GetShapePointX1(aShape) >= e.X && GetShapePointX2(aShape) <= e.X)) && ((GetShapePointY1(aShape) <= e.Y && GetShapePointY2(aShape) >= e.Y) || (GetShapePointY1(aShape) >= e.Y && GetShapePointY2(aShape) <= e.Y)))
                {
                    HandleCanvasPointerReleasedForSelectedTrue(aShape);
                    break;
                }
            }
        }

        //HandleCanvasPointerReleasedForSelectedTrue
        private void HandleCanvasPointerReleasedForSelectedTrue(Shape aShape)
        {
            DotRectangle dotRectangle = new DotRectangle();
            dotRectangle.Shape = aShape;
            _drawingFormPresentationModel.DrawShape(dotRectangle);
            if (aShape.GetShape == ShapeFlag.Line)
            {
                _label.Text = LABEL_HEAD + aShape.GetShape + LABEL_LEFT_BRACKET + aShape.X1 + LABEL_COMMA + aShape.Y1 + LABEL_COMMA + aShape.X2 + LABEL_COMMA + aShape.Y2 + LABEL_RIGHT_BRACKET;
            }
            else
            {
                _label.Text = LABEL_HEAD + aShape.GetShape + LABEL_LEFT_BRACKET + TakeSmall(aShape.X1, aShape.X2) + LABEL_COMMA + TakeSmall(aShape.Y1, aShape.Y2) + LABEL_COMMA + TakeLarge(aShape.X1, aShape.X2) + LABEL_COMMA + TakeLarge(aShape.Y1, aShape.Y2) + LABEL_RIGHT_BRACKET;
            }
        }

        //HandleCanvasPointerReleasedForShapes
        private void HandleCanvasPointerReleasedForShapes(MouseEventArgs e)
        {
            if (_shapeFlag == ShapeFlag.Line)
            {
                HandleCanvasPointerReleasedForLine(e);
            }
            else
            {
                HandleCanvasPointerReleasedForOtherShapes(e);
            }
            RefreshButton();
            RefreshUserInterface();
        }

        //HandleCanvasPointerReleasedForLine
        private void HandleCanvasPointerReleasedForLine(MouseEventArgs e)
        {
            if (_drawingFormPresentationModel.GetIsPressed == true)
            {
                Shape isInShapes = IsInShape(e.X, e.Y);
                if (isInShapes != null)
                {
                    _drawingFormPresentationModel.ReleasedPointer(e.X, e.Y, isInShapes);
                    _isSelectMode = true;
                }
                else
                {
                    PressCancel();
                }
            }
        }

        //HandleCanvasPointerReleasedForOtherShapes
        private void HandleCanvasPointerReleasedForOtherShapes(MouseEventArgs e)
        {
            _drawingFormPresentationModel.ReleasedPointer(e.X, e.Y, null);
            _isSelectMode = true;
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
            this.ResetSelection();
            _drawingFormPresentationModel.Undo();
            RefreshUserInterface();
        }

        //RedoHandler
        void RedoHandler(object sender, System.EventArgs e)
        {
            this.ResetSelection();
            _drawingFormPresentationModel.Redo();
            RefreshUserInterface();
        }

        //RefreshButton
        void RefreshButton()
        {
            _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
            _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
            _line.Enabled = _drawingFormPresentationModel.IsLineButtonEnable;
            _shapeFlag = _drawingFormPresentationModel.GetShapeFlag;
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

        //ReserSelection
        public void ResetSelection()
        {
            List<Shape> shapes = _drawingFormPresentationModel.GetShapes;
            if (shapes.Count != 0)
            {
                if (GetShapeWithIndex(shapes, 0).GetShape == ShapeFlag.DotRectangle)
                {
                    ClearSelection();
                    _label.Text = LABEL_DEFAULT;
                }
            }
        }

        //IsInShape
        public Shape IsInShape(double pointX, double pointY)
        {
            List<Shape> shapes = _drawingFormPresentationModel.GetShapes;
            for (int index = 0; index < shapes.Count; index++)
            {
                Shape aShape = GetShapeWithIndex(shapes, index);
                if ((aShape.GetShape != ShapeFlag.Line) && (((GetShapePointX1(aShape) <= pointX && GetShapePointX2(aShape) >= pointX) || (GetShapePointX1(aShape) >= pointX && GetShapePointX2(aShape) <= pointX)) && ((GetShapePointY1(aShape) <= pointY && GetShapePointY2(aShape) >= pointY) || (GetShapePointY1(aShape) >= pointY && GetShapePointY2(aShape) <= pointY))))
                {
                    return aShape;
                }
            }
            return null;
        }

        //PressCancel
        private void PressCancel()
        {
            _drawingFormPresentationModel.PressedCancel();
        }

        //ClearSelection
        private void ClearSelection()
        {
            _drawingFormPresentationModel.DeleteShape();
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
    }
}
