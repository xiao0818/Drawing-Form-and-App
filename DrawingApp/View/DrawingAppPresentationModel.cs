﻿using DrawingModel;
using System.Collections.Generic;

namespace DrawingApp
{
    public class DrawingAppPresentationModel
    {
        public event DrawingAppPresentationModelChangedEventHandler _drawingAppPresentationModelChanged;
        public delegate void DrawingAppPresentationModelChangedEventHandler();
        bool _isRectangleButtonEnabled = true;
        bool _isEllipseButtonEnabled = true;
        bool _isLineButtonEnabled = true;
        Model _model;
        public DrawingAppPresentationModel(Model model)
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
                return _model.GetShapes;
            }
        }

        //HandleRectangleButtonClick
        public void HandleRectangleButtonClick()
        {
            _isRectangleButtonEnabled = false;
            _isEllipseButtonEnabled = true;
            _isLineButtonEnabled = true;
            _model.ShapeFlag = ShapeFlag.Rectangle;
            _model.ToDrawMode();
        }

        //HandleEllipseButtonClick
        public void HandleEllipseButtonClick()
        {
            _isRectangleButtonEnabled = true;
            _isEllipseButtonEnabled = false;
            _isLineButtonEnabled = true;
            _model.ShapeFlag = ShapeFlag.Ellipse;
            _model.ToDrawMode();
        }

        //HandleLineButtonClick
        public void HandleLineButtonClick()
        {
            _isRectangleButtonEnabled = true;
            _isEllipseButtonEnabled = true;
            _isLineButtonEnabled = false;
            _model.ShapeFlag = ShapeFlag.Line;
            _model.ToDrawMode();
        }

        //HandleClearButtonClick
        public void HandleClearButtonClick()
        {
            _isRectangleButtonEnabled = true;
            _isEllipseButtonEnabled = true;
            _isLineButtonEnabled = true;
            _model.ShapeFlag = ShapeFlag.Null;
            _model.ToSelectMode();
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
            _isRectangleButtonEnabled = true;
            _isEllipseButtonEnabled = true;
            _isLineButtonEnabled = _model.GetIsSelectMode();
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
            if (_drawingAppPresentationModelChanged != null)
            {
                _drawingAppPresentationModelChanged();
            }
        }
    }
}
