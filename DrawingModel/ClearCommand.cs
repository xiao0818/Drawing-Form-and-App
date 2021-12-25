using System.Collections.Generic;

namespace DrawingModel
{
    public class ClearCommand : ICommand
    {
        List<Shape> _shapes;
        Model _model;
        public ClearCommand(Model model, List<Shape> shapes)
        {
            _model = model;
            _shapes = new List<Shape>(shapes);
        }

        //Execute
        public void Execute()
        {
            _model.ClearAll();
        }

        //UnExecute
        public void ExecuteBack()
        {
            _model.SetShapes = _shapes;
        }
    }
}
