using DrawingModel;

namespace DrawingApp
{
    class DrawingAppPresentationModel
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
        public void PointerPressed(double x, double y, int shapeFlag)
        {
            _model.PointerPressed(x, y, shapeFlag);
        }

        //PointerMoved
        public void PointerMoved(double x, double y)
        {
            _model.PointerMoved(x, y);
            NotifyModelChanged();
        }

        //PointerReleased
        public void PointerReleased(double x, double y)
        {
            _model.PointerReleased(x, y);
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
