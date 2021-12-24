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
            _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
            _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
            _line.Enabled = _drawingFormPresentationModel.IsLineButtonEnable;
            _shapeFlag = _drawingFormPresentationModel.GetShapeFlag;
            _isSelectMode = false;
            ResetSelection();
        }

        //HandleEllipseButtonClick
        public void HandleEllipseButtonClick(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.HandleEllipseButtonClick();
            _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
            _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
            _line.Enabled = _drawingFormPresentationModel.IsLineButtonEnable;
            _shapeFlag = _drawingFormPresentationModel.GetShapeFlag;
            _isSelectMode = false;
            ResetSelection();
        }

        //HandleLineButtonClick
        public void HandleLineButtonClick(object sender, System.EventArgs e)
        {
            _drawingFormPresentationModel.HandleLineButtonClick();
            _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
            _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
            _line.Enabled = _drawingFormPresentationModel.IsLineButtonEnable;
            _shapeFlag = _drawingFormPresentationModel.GetShapeFlag;
            _isSelectMode = false;
            ResetSelection();
        }

        //HandleClearButtonClick
        public void HandleClearButtonClick(object sender, System.EventArgs e)
        {
            ResetSelection();
            _drawingFormPresentationModel.Clear();
            _drawingFormPresentationModel.HandleClearButtonClick();
            _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
            _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
            _line.Enabled = _drawingFormPresentationModel.IsLineButtonEnable;
            _shapeFlag = _drawingFormPresentationModel.GetShapeFlag;
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
                        _drawingFormPresentationModel.PressedPointer(e.X, e.Y, _shapeFlag, IsInShape(e.X, e.Y));
                    }
                }
                else
                {
                    _drawingFormPresentationModel.PressedPointer(e.X, e.Y, _shapeFlag, null);
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
                List<Shape> shapes = _drawingFormPresentationModel.GetShapes;
                for (int index = 0; index < shapes.Count; index++)
                {
                    Shape aShape = shapes[shapes.Count - index - 1];
                    if (((aShape.X1 <= e.X && aShape.X2 >= e.X) || (aShape.X1 >= e.X && aShape.X2 <= e.X)) && ((aShape.Y1 <= e.Y && aShape.Y2 >= e.Y) || (aShape.Y1 >= e.Y && aShape.Y2 <= e.Y)))
                    {
                        DotRectangle dotRectangle = new DotRectangle();
                        dotRectangle.Shape = aShape;
                        _drawingFormPresentationModel.DrawShape(dotRectangle);
                        _label.Text = "Selected : " + aShape.GetShape + " (" + TakeSmall(aShape.X1, aShape.X2) + ", " + TakeSmall(aShape.Y1, aShape.Y2) + ", " + TakeLarge(aShape.X1, aShape.X2) + ", " + TakeLarge(aShape.Y1, aShape.Y2) + ")";
                        break;
                    }
                }
            }
            else
            {
                if (_shapeFlag != ShapeFlag.Null)
                {
                    if (_shapeFlag == ShapeFlag.Line)
                    {
                        if (_drawingFormPresentationModel.GetIsPressed == true)
                        {
                            if (IsInShape(e.X, e.Y) != null)
                            {
                                _drawingFormPresentationModel.ReleasedPointer(e.X, e.Y, IsInShape(e.X, e.Y));
                                _drawingFormPresentationModel.HandleCanvasPointerReleased();
                                _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
                                _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
                                _line.Enabled = _drawingFormPresentationModel.IsLineButtonEnable;
                                _shapeFlag = _drawingFormPresentationModel.GetShapeFlag;
                                RefreshUserInterface();
                                _isSelectMode = true;
                            }
                            else
                            {
                                _drawingFormPresentationModel.PressedCancel();
                            }
                        }
                    }
                    else
                    {
                        _drawingFormPresentationModel.ReleasedPointer(e.X, e.Y, null);
                        _drawingFormPresentationModel.HandleCanvasPointerReleased();
                        _rectangle.Enabled = _drawingFormPresentationModel.IsRectangleButtonEnable;
                        _ellipse.Enabled = _drawingFormPresentationModel.IsEllipseButtonEnable;
                        _line.Enabled = _drawingFormPresentationModel.IsLineButtonEnable;
                        _shapeFlag = _drawingFormPresentationModel.GetShapeFlag;
                        RefreshUserInterface();
                        _isSelectMode = true;
                    }
                }
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
                if (shapes[shapes.Count - 1].GetShape == ShapeFlag.DotRectangle)
                {
                    _drawingFormPresentationModel.DeleteShape();
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
                Shape aShape = shapes[shapes.Count - index - 1];
                if ((aShape.GetShape != ShapeFlag.Line) && (((aShape.X1 <= pointX && aShape.X2 >= pointX) || (aShape.X1 >= pointX && aShape.X2 <= pointX)) && ((aShape.Y1 <= pointY && aShape.Y2 >= pointY) || (aShape.Y1 >= pointY && aShape.Y2 <= pointY))))
                {
                    return aShape;
                }
            }
            return null;
        }
    }
}
