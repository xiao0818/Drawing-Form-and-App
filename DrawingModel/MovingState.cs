namespace DrawingModel
{
    public class MovingState : IState
    {
        Model _model;
        double _movementX = 0;
        double _movementY = 0;
        double _moveInitialX = 0;
        double _moveInitialY = 0;
        bool _isMoving = false;
        DotRectangle _target;
        public MovingState(Model model)
        {
            _model = model;
            _target = _model.GetTarget();
        }

        //PressedPointer
        public void PressedPointer(double pointX, double pointY)
        {
            if (((_target.X1 <= pointX && _target.X2 >= pointX) || (_target.X1 >= pointX && _target.X2 <= pointX)) && ((_target.Y1 <= pointY && _target.Y2 >= pointY) || (_target.Y1 >= pointY && _target.Y2 <= pointY)))
            {
                _moveInitialX = pointX;
                _moveInitialY = pointY;
                _movementX = pointX;
                _movementY = pointY;
                _isMoving = true;
            }
            else
            {
                _model.ResetSelection();
                _model.SetPointerState();
            }
        }

        //MovedPointer
        public void MovedPointer(double pointX, double pointY)
        {
            if (_isMoving == true)
            {
                HandleMove(pointX - _movementX, pointY - _movementY);
                _movementX = pointX;
                _movementY = pointY;
            }
        }

        //ReleasedPointer
        public void ReleasedPointer(double pointX, double pointY)
        {
            if (_isMoving == true)
            {
                _model.AddCommand(new MoveCommand(_model, _target.Shape, pointX - _moveInitialX, pointY - _moveInitialY));
                _isMoving = false;
            }
            else
            {
                _model.SetPointerState();
            }
        }

        //HandleMove
        private void HandleMove(double XChange, double YChange)
        {
            _target.Shape.X1 += XChange;
            _target.Shape.X2 += XChange;
            _target.Shape.Y1 += YChange;
            _target.Shape.Y2 += YChange;
        }

        //GetHint
        public Shape GetHint()
        {
            return null;
        }

        //GetStateFlag
        public StateFlag GetStateFlag()
        {
            return StateFlag.MovingState;
        }
    }
}
