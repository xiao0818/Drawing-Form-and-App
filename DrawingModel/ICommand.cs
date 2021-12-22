namespace DrawingModel
{
    interface ICommand
    {
        //Execute
        void Execute();

        //UnExecute
        void UnExecute();

        //GetShape
        string GetShape
        {
            get;
        }
    }
}
