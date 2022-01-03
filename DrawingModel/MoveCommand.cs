using System.Collections.Generic;

namespace DrawingModel
{
    public class MoveCommand : ICommand
    {
        Model _model;
        Shape _shape;
        double _movementX;
        double _movementY;
        public MoveCommand(Model model, Shape shape, double movementX, double movementY)
        {
            _model = model;
            _shape = shape;
            _movementX = movementX;
            _movementY = movementY;
        }

        //Execute
        public void Execute()
        {
            _shape.X1 += _movementX;
            _shape.X2 += _movementX;
            _shape.Y1 += _movementY;
            _shape.Y2 += _movementY;
        }

        //UnExecute
        public void ExecuteBack()
        {
            _shape.X1 -= _movementX;
            _shape.X2 -= _movementX;
            _shape.Y1 -= _movementY;
            _shape.Y2 -= _movementY;
        }
    }
}
