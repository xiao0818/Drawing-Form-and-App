using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class DrawingStateTests
    {
        Model model;
        DrawingState drawingState;

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            model = new Model();
            drawingState = new DrawingState(model);
            model.ShapeFlag = ShapeFlag.Rectangle;
            model.SetDrawingState();
        }

        //PressedPointerTest
        [TestMethod()]
        public void PressedPointerTest()
        {
            drawingState.PressedPointer(100, 100);
            Assert.AreEqual(ShapeFlag.Rectangle, drawingState.Hint.ShapeFlag);
            Assert.AreEqual(100, drawingState.Hint.X1);
            Assert.AreEqual(100, drawingState.Hint.Y1);
        }

        //MovedPointerTestSuccess
        [TestMethod()]
        public void MovedPointerTestSuccess()
        {
            drawingState.PressedPointer(100, 100);
            drawingState.MovedPointer(200, 200);
            Assert.AreEqual(200, drawingState.Hint.X2);
            Assert.AreEqual(200, drawingState.Hint.Y2);
        }

        //MovedPointerTestFail
        [TestMethod()]
        public void MovedPointerTestFail()
        {
            drawingState.MovedPointer(200, 200);
            Assert.IsNull(drawingState.Hint);
        }

        //ReleasedPointerTestSuccess
        [TestMethod()]
        public void ReleasedPointerTestSuccess()
        {
            drawingState.PressedPointer(100, 100);
            drawingState.MovedPointer(200, 200);
            drawingState.ReleasedPointer(300, 300);
            Assert.AreEqual(300, drawingState.Hint.X2);
            Assert.AreEqual(300, drawingState.Hint.Y2);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //ReleasedPointerTestFail
        [TestMethod()]
        public void ReleasedPointerTestFail()
        {
            drawingState.ReleasedPointer(300, 300);
            Assert.AreEqual(StateFlag.DrawingState, model.StateFlag);
        }
    }
}