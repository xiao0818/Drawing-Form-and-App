namespace DrawingModel
{
    public interface IState
    {
        //PressedPointer
        void PressedPointer(double pointX, double pointY);

        //MovedPointer
        void MovedPointer(double pointX, double pointY);

        //ReleasedPointer
        void ReleasedPointer(double pointX, double pointY);

        //GetHint
        Shape GetHint();

        //GetStateFlag
        StateFlag GetStateFlag();
    }
}
