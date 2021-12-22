﻿namespace DrawingModel
{
    class DrawCommand : ICommand
    {
        Shape _shape;
        Model _model;
        public DrawCommand(Model model, Shape shape)
        {
            _model = model;
            _shape = shape;
        }

        //Execute
        public void Execute()
        {
            _model.DrawShape(_shape);
        }

        //UnExecute
        public void UnExecute()
        {
            _model.DeleteShape();
        }

        //GetShape
        public string GetShape
        {
            get
            {
                return _shape.GetShape;
            }
        }
    }
}
