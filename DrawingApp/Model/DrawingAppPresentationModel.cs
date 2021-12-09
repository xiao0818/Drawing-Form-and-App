﻿using DrawingModel;

namespace DrawingApp
{
    public class DrawingAppPresentationModel
    {
        public event DrawingAppPresentationModelChangedEventHandler _drawingAppPresentationModelChanged;
        public delegate void DrawingAppPresentationModelChangedEventHandler();
        bool _isRectangleButtonEnabled = true;
        bool _isEllipseButtonEnabled = true;
        int _shapeFlag = (int)ShapeFlag.Null;
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

        public int GetShapeFlag
        {
            get
            {
                return _shapeFlag;
            }
        }

        //HandleRectangleButtonClick
        public void HandleRectangleButtonClick()
        {
            _isRectangleButtonEnabled = false;
            _isEllipseButtonEnabled = true;
            _shapeFlag = (int)ShapeFlag.Rectangle;
        }

        //HandleEllipseButtonClick
        public void HandleEllipseButtonClick()
        {
            _isRectangleButtonEnabled = true;
            _isEllipseButtonEnabled = false;
            _shapeFlag = (int)ShapeFlag.Ellipse;
        }

        //HandleClearButtonClick
        public void HandleClearButtonClick()
        {
            _isRectangleButtonEnabled = true;
            _isEllipseButtonEnabled = true;
            _shapeFlag = (int)ShapeFlag.Null;
        }

        //HandleCanvasPointerReleased
        public void HandleCanvasPointerReleased()
        {
            _isRectangleButtonEnabled = true;
            _isEllipseButtonEnabled = true;
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

        //NotifyModelChanged
        private void NotifyModelChanged()
        {
            if (_drawingAppPresentationModelChanged != null)
            {
                _drawingAppPresentationModelChanged();
            }
        }
    }
}
