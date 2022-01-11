namespace DrawingModel
{
    public class DrawingState : IState
    {
        Model _model;
        Shape _hint = null;
        bool _isPressed = false;
        bool _isMoved = false;
        ShapeFactory _shapeFactory = new ShapeFactory();
        public DrawingState(Model model)
        {
            _model = model;
            _model.ResetSelection();
        }

        //PressedPointer
        public void PressedPointer(double pointX, double pointY)
        {
            _hint = null;
            _hint = _shapeFactory.CreateShape(_model.ShapeFlag);
            _hint.X1 = pointX;
            _hint.Y1 = pointY;
            _isPressed = true;
        }

        //MovedPointer
        public void MovedPointer(double pointX, double pointY)
        {
            if (_isPressed == true)
            {
                _isMoved = true;
                _hint.X2 = pointX;
                _hint.Y2 = pointY;
            }
        }

        //ReleasedPointer
        public void ReleasedPointer(double pointX, double pointY)
        {
            if (_isPressed == true && _isMoved == true)
            {
                _isPressed = false;
                _hint.X2 = pointX;
                _hint.Y2 = pointY;
                _model.ExecuteCommand(new DrawCommand(_model, _hint.Copy()));
                _model.SetPointerState();
            }
            else
                _model.SetDrawingState();
        }

        //Hint
        public Shape Hint
        {
            get
            {
                return _hint;
            }
        }

        //StateFlag
        public StateFlag StateFlag
        {
            get
            {
                return StateFlag.DrawingState;
            }
        }
    }
}
