﻿using DrawingModel;
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

        //GetShapes
        public List<Shape> GetShapes
        {
            get
            {
                return _model.Shapes;
            }
        }

        //HandleRectangleButtonClick
        public void HandleRectangleButtonClick()
        {
            _model.ResetSelection();
            _isRectangleButtonEnabled = false;
            _isEllipseButtonEnabled = true;
            _isLineButtonEnabled = true;
            _model.ShapeFlag = ShapeFlag.Rectangle;
            _model.SetDrawingState();
        }

        //HandleEllipseButtonClick
        public void HandleEllipseButtonClick()
        {
            _model.ResetSelection();
            _isRectangleButtonEnabled = true;
            _isEllipseButtonEnabled = false;
            _isLineButtonEnabled = true;
            _model.ShapeFlag = ShapeFlag.Ellipse;
            _model.SetDrawingState();
        }

        //HandleLineButtonClick
        public void HandleLineButtonClick()
        {
            _model.ResetSelection();
            _isRectangleButtonEnabled = true;
            _isEllipseButtonEnabled = true;
            _isLineButtonEnabled = false;
            _model.ShapeFlag = ShapeFlag.Line;
            _model.SetDrawingLineState();
        }

        //HandleClearButtonClick
        public void HandleClearButtonClick()
        {
            _model.ResetSelection();
            _isRectangleButtonEnabled = true;
            _isEllipseButtonEnabled = true;
            _isLineButtonEnabled = true;
            _model.ShapeFlag = ShapeFlag.Null;
            _model.SetPointerState();
        }

        //PointerPressed
        public void PressedPointer(double pointX, double pointY)
        {
            _model.PressedPointer(pointX, pointY);
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
            if (_model.GetStateFlag() == StateFlag.DrawingState)
            {
                _isLineButtonEnabled = true;
            }
            else if (_model.GetStateFlag() == StateFlag.DrawingLineState)
            {
                _isRectangleButtonEnabled = true;
                _isEllipseButtonEnabled = true;
            }
            else
            {
                _isRectangleButtonEnabled = true;
                _isEllipseButtonEnabled = true;
                _isLineButtonEnabled = true;
            }
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

        //GetTarget
        public DotRectangle GetTarget()
        {
            return _model.GetTarget();
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
