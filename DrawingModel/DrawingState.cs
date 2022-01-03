namespace DrawingModel
{
    public class DrawingState : IState
    {
        Model _model;
        public DrawingState(Model model)
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
