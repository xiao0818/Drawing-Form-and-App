using System.Collections.Generic;

namespace DrawingModel
{
    public class PointerState : IState
    {
        Model _model;
        public PointerState(Model model)
        {
            _model = model;
        }

        //PressedPointer
        public void PressedPointer(double pointX, double pointY)
        {

        }

        //MovedPointer
        public void MovedPointer(double pointX, double pointY)
        {

        }

        //ReleasedPointer
        public void ReleasedPointer(double pointX, double pointY)
        {
            _model.ResetSelection();
            List<Shape> shapes = _model.Shapes;
            for (int index = 0; index < shapes.Count; index++)
            {
                Shape aShape = shapes[shapes.Count - index - 1];
                if (((aShape.X1 <= pointX && aShape.X2 >= pointX) || (aShape.X1 >= pointX && aShape.X2 <= pointX)) && ((aShape.Y1 <= pointY && aShape.Y2 >= pointY) || (aShape.Y1 >= pointY && aShape.Y2 <= pointY)))
                {
                    DotRectangle dotRectangle = new DotRectangle();
                    dotRectangle.Shape = aShape;
                    _model.DrawDotRectangle(dotRectangle);
                    _model.SetMovingState();
                    break;
                }
            }
        }

        //Hint
        public Shape Hint
        {
            get
            {
                return null;
            }
        }

        //StateFlag
        public StateFlag StateFlag
        {
            get
            {
                return StateFlag.PointerState;
            }
        }
    }
}
