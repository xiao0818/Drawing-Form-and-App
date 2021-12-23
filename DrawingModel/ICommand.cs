namespace DrawingModel
{
    interface ICommand
    {
        //Execute
        void Execute();

        //UnExecute
        void ExecuteBack();

        //GetShape
        ShapeFlag GetShape
        {
            get;
        }
    }
}
