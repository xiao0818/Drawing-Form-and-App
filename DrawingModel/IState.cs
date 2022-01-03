namespace DrawingModel
{
    public interface IState
    {
        //PressedPointer
        void PressedPointer();

        //MovedPointer
        void MovedPointer();

        //ReleasedPointer
        void ReleasedPointer();
    }
}
