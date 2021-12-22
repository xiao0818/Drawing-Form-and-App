using DrawingModel;
using System.Collections.Generic;

namespace DrawingForm
{
    public class DrawingFormPresentationModel
    {
        public event DrawingFormPresentationModelChangedEventHandler _drawingFormPresentationModelChanged;
        public delegate void DrawingFormPresentationModelChangedEventHandler();
        bool _isRectangleButtonEnabled = true;
        bool _isEllipseButtonEnabled = true;
        bool _isLineButtonEnabled = true;
        int _shapeFlag = (int)ShapeFlag.Null;
        Model _model;
        public DrawingFormPresentationModel(Model model)
        {
            _model = model;
            _model._modelChanged += NotifyModelChanged;
        }

        public bool IsRectangleButtonEnable
        {
            get
            {
                return _isRectangleButtonEnabled;
            }
        }

        public bool IsEllipseButtonEnable
        {
            get
            {
                return _isEllipseButtonEnabled;
            }
        }

        public bool IsLineButtonEnable
        {
            get
            {
                return _isLineButtonEnabled;
            }
        }

        public int GetShapeFlag
        {
            get
            {
                return _shapeFlag;
            }
        }

        //GetShapes
        public List<Shape> GetShapes
        {
            get
            {
                return _model.GetShapes;
            }
        }

        //HandleRectangleButtonClick
        public void HandleRectangleButtonClick()
        {
            _isRectangleButtonEnabled = false;
            _isEllipseButtonEnabled = true;
            _isLineButtonEnabled = true;
            _shapeFlag = (int)ShapeFlag.Rectangle;
        }

        //HandleEllipseButtonClick
        public void HandleEllipseButtonClick()
        {
            _isRectangleButtonEnabled = true;
            _isEllipseButtonEnabled = false;
            _isLineButtonEnabled = true;
            _shapeFlag = (int)ShapeFlag.Ellipse;
        }

        //HandleLineButtonClick
        public void HandleLineButtonClick()
        {
            _isRectangleButtonEnabled = true;
            _isEllipseButtonEnabled = true;
            _isLineButtonEnabled = false;
            _shapeFlag = (int)ShapeFlag.Line;
        }

        //HandleClearButtonClick
        public void HandleClearButtonClick()
        {
            _isRectangleButtonEnabled = true;
            _isEllipseButtonEnabled = true;
            _isLineButtonEnabled = true;
            _shapeFlag = (int)ShapeFlag.Null;
        }

        //HandleCanvasPointerReleased
        public void HandleCanvasPointerReleased()
        {
            _isRectangleButtonEnabled = true;
            _isEllipseButtonEnabled = true;
            _isLineButtonEnabled = true;
            _shapeFlag = (int)ShapeFlag.Null;
        }

        //PointerPressed
        public void PressedPointer(double pointX, double pointY, int shapeFlag)
        {
            _model.PressedPointer(pointX, pointY, shapeFlag);
        }

        //PointerMoved
        public void MovedPointer(double pointX, double pointY)
        {
            _model.MovedPointer(pointX, pointY);
            NotifyModelChanged();
        }

        //PointerReleased
        public void ReleasedPointer(double pointX, double pointY)
        {
            _model.ReleasedPointer(pointX, pointY);
            NotifyModelChanged();
        }

        //Clear
        public void Clear()
        {
            _model.Clear();
            NotifyModelChanged();
        }

        //Draw
        public void Draw(IGraphics graphics)
        {
            _model.Draw(graphics);
        }

        //DeleteShape
        public void DeleteShape()
        {
            _model.DeleteShape();
        }

        //Undo
        public void Undo()
        {
            _model.Undo();
        }

        //Redo
        public void Redo()
        {
            _model.Redo();
        }

        //IsRedoEnabled
        public bool IsRedoEnabled
        {
            get
            {
                return _model.IsRedoEnabled;
            }
        }

        //IsUndoEnabled
        public bool IsUndoEnabled
        {
            get
            {
                return _model.IsUndoEnabled;
            }
        }

        //NotifyModelChanged
        public void NotifyModelChanged()
        {
            if (_drawingFormPresentationModelChanged != null)
            {
                _drawingFormPresentationModelChanged();
            }
        }
    }
}
