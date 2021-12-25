namespace DrawingModel
{
    public interface ICommand
    {
        //Execute
        void Execute();

        //UnExecute
        void ExecuteBack();
    }
}
