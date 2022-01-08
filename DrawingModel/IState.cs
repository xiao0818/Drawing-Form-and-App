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

        //Hint
        Shape Hint
        {
            get;
        }

        //StateFlag
        StateFlag StateFlag
        {
            get;
        }
    }
}
