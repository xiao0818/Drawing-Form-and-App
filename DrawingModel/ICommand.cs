namespace DrawingModel
{
    interface ICommand
    {
        //Execute
        void Execute();

        //UnExecute
        void ExecuteBack();
    }
}
