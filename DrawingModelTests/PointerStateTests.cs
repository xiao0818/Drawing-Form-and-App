using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class PointerStateTests
    {
        Model model;
        PointerState pointerState;
        Rectangle rectangle;

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            model = new Model();
            pointerState = new PointerState(model);
            rectangle = new Rectangle();
            model.SetPointerState();
        }

        //PressedPointerTest
        [TestMethod()]
        public void PressedPointerTest()
        {
            pointerState.PressedPointer(150, 150);
            Assert.AreEqual(0, model.Shapes.Count);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //MovedPointerTest
        [TestMethod()]
        public void MovedPointerTest()
        {
            pointerState.MovedPointer(150, 150);
            Assert.AreEqual(0, model.Shapes.Count);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //ReleasedPointerTestSuccess1
        [TestMethod()]
        public void ReleasedPointerTestSuccess1()
        {
            rectangle.X1 = rectangle.Y1 = 100;
            rectangle.X2 = rectangle.Y2 = 200;
            model.DrawShape(rectangle);
            pointerState.ReleasedPointer(150, 150);
            Assert.AreEqual(2, model.Shapes.Count);
            Assert.AreEqual(StateFlag.MovingState, model.StateFlag);
        }

        //ReleasedPointerTestSuccess2
        [TestMethod()]
        public void ReleasedPointerTestSuccess2()
        {
            rectangle.X1 = rectangle.Y1 = 200;
            rectangle.X2 = rectangle.Y2 = 100;
            model.DrawShape(rectangle);
            pointerState.ReleasedPointer(150, 150);
            Assert.AreEqual(2, model.Shapes.Count);
            Assert.AreEqual(StateFlag.MovingState, model.StateFlag);
        }

        //ReleasedPointerTestFail1
        [TestMethod()]
        public void ReleasedPointerTestFail1()
        {
            rectangle.X1 = rectangle.Y1 = 100;
            rectangle.X2 = rectangle.Y2 = 200;
            model.DrawShape(rectangle);
            pointerState.ReleasedPointer(150, 300);
            Assert.AreEqual(1, model.Shapes.Count);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //ReleasedPointerTestFail2
        [TestMethod()]
        public void ReleasedPointerTestFail2()
        {
            rectangle.X1 = rectangle.Y1 = 100;
            rectangle.X2 = rectangle.Y2 = 200;
            model.DrawShape(rectangle);
            pointerState.ReleasedPointer(300, 150);
            Assert.AreEqual(1, model.Shapes.Count);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //ReleasedPointerTestFail3
        [TestMethod()]
        public void ReleasedPointerTestFail3()
        {
            rectangle.X1 = rectangle.Y1 = 100;
            rectangle.X2 = rectangle.Y2 = 200;
            model.DrawShape(rectangle);
            pointerState.ReleasedPointer(300, 300);
            Assert.AreEqual(1, model.Shapes.Count);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //HintTest
        [TestMethod()]
        public void HintTest()
        {
            Assert.IsNull(pointerState.Hint);
        }
    }
}