namespace DrawingModel
{
    public class MovingState : IState
    {
        Model _model;
        public MovingState(Model model)
        {
            _model = model;
        }

        //PressedPointer
        public void PressedPointer()
        {

        }

        //MovedPointer
        public void MovedPointer()
        {

        }

        //ReleasedPointer
        public void ReleasedPointer()
        {

        }
    }
}
