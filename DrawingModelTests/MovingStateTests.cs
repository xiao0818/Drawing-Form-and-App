using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class MovingStateTests
    {
        Model model;
        Rectangle rectangle;
        PointerState pointerState;
        MovingState movingState;

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            model = new Model();
            rectangle = new Rectangle();
            pointerState = new PointerState(model);
        }

        //PressedPointerTestSuccss1
        [TestMethod()]
        public void PressedPointerTestSuccess1()
        {
            SetUp1();
            movingState.PressedPointer(150, 150);
            Assert.AreEqual(2, model.Shapes.Count);
            Assert.AreEqual(StateFlag.MovingState, model.StateFlag);
        }

        //PressedPointerTestSuccss2
        [TestMethod()]
        public void PressedPointerTestSuccess2()
        {
            SetUp2();
            movingState.PressedPointer(150, 150);
            Assert.AreEqual(2, model.Shapes.Count);
            Assert.AreEqual(StateFlag.MovingState, model.StateFlag);
        }

        //PressedPointerTestFail1
        [TestMethod()]
        public void PressedPointerTestFail1()
        {
            SetUp1();
            movingState.PressedPointer(150, 300);
            Assert.AreEqual(1, model.Shapes.Count);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //PressedPointerTestFail2
        [TestMethod()]
        public void PressedPointerTestFail2()
        {
            SetUp1();
            movingState.PressedPointer(300, 150);
            Assert.AreEqual(1, model.Shapes.Count);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //PressedPointerTestFail3
        [TestMethod()]
        public void PressedPointerTestFail3()
        {
            SetUp1();
            movingState.PressedPointer(300, 300);
            Assert.AreEqual(1, model.Shapes.Count);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //MovedPointerTestSuccess
        [TestMethod()]
        public void MovedPointerTestSuccess()
        {
            SetUp1();
            movingState.PressedPointer(150, 150);
            movingState.MovedPointer(250, 250);
            Assert.AreEqual(200, model.Target.X1);
            Assert.AreEqual(200, model.Target.Y1);
            Assert.AreEqual(300, model.Target.X2);
            Assert.AreEqual(300, model.Target.Y2);
        }

        //MovedPointerTestFail
        [TestMethod()]
        public void MovedPointerTestFail()
        {
            SetUp1();
            movingState.MovedPointer(250, 250);
            Assert.AreEqual(100, model.Target.X1);
            Assert.AreEqual(100, model.Target.Y1);
            Assert.AreEqual(200, model.Target.X2);
            Assert.AreEqual(200, model.Target.Y2);
        }

        //ReleasedPointerTestSuccess
        [TestMethod()]
        public void ReleasedPointerTestSuccess()
        {
            SetUp1();
            movingState.PressedPointer(150, 150);
            movingState.MovedPointer(250, 250);
            movingState.ReleasedPointer(250, 250);
            Assert.AreEqual(2, model.Shapes.Count);
            Assert.AreEqual(StateFlag.MovingState, model.StateFlag);
            Assert.IsTrue(model.IsUndoEnabled);
            Assert.IsFalse(model.IsRedoEnabled);
        }

        //ReleasedPointerTestFail
        [TestMethod()]
        public void ReleasedPointerTestFail()
        {
            SetUp1();
            movingState.ReleasedPointer(250, 250);
            Assert.AreEqual(1, model.Shapes.Count);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
            Assert.IsFalse(model.IsUndoEnabled);
            Assert.IsFalse(model.IsRedoEnabled);
        }

        //HintTest
        [TestMethod()]
        public void HintTest()
        {
            SetUp1();
            Assert.IsNull(movingState.Hint);
        }

        //SetUp1
        private void SetUp1()
        {
            rectangle.X1 = rectangle.Y1 = 100;
            rectangle.X2 = rectangle.Y2 = 200;
            model.DrawShape(rectangle);
            pointerState.ReleasedPointer(150, 150);
            movingState = new MovingState(model);
            model.SetMovingState();
        }

        //SetUp2
        private void SetUp2()
        {
            rectangle.X1 = rectangle.Y1 = 200;
            rectangle.X2 = rectangle.Y2 = 100;
            model.DrawShape(rectangle);
            pointerState.ReleasedPointer(150, 150);
            movingState = new MovingState(model);
            model.SetMovingState();
        }
    }
}