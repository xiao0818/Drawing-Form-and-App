using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class DrawingLineStateTests
    {
        Model model;
        DrawingLineState drawingLineState;
        Rectangle rectangle;
        Ellipse ellipse;
        Line line;

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            model = new Model();
            drawingLineState = new DrawingLineState(model);
            rectangle = new Rectangle();
            ellipse = new Ellipse();
            line = new Line();
            rectangle.X1 = rectangle.Y1 = 100;
            rectangle.X2 = rectangle.Y2 = 200;
            ellipse.X1 = ellipse.Y1 = 500;
            ellipse.X2 = ellipse.Y2 = 400;
            model.DrawShape(rectangle);
            model.DrawShape(ellipse);
            line.Shape1 = rectangle;
            line.Shape2 = ellipse;
            model.DrawShape(line);
            model.SetDrawingLineState();
        }

        //PressedPointerTestSuccess
        [TestMethod()]
        public void PressedPointerTestSuccess()
        {
            drawingLineState.PressedPointer(150, 150);
            Assert.AreEqual(150, ((Line)drawingLineState.Hint).X1);
            Assert.AreEqual(150, ((Line)drawingLineState.Hint).Y1);
            Assert.AreEqual(ShapeFlag.Rectangle, ((Line)drawingLineState.Hint).Shape1.ShapeFlag);
            Assert.AreEqual(100, ((Line)drawingLineState.Hint).Shape1.X1);
            Assert.AreEqual(100, ((Line)drawingLineState.Hint).Shape1.Y1);
            Assert.AreEqual(200, ((Line)drawingLineState.Hint).Shape1.X2);
            Assert.AreEqual(200, ((Line)drawingLineState.Hint).Shape1.Y2);
            Assert.AreEqual(0, ((Line)drawingLineState.Hint).ShapeIndex1);
        }

        //PressedPointerTestFail
        [TestMethod()]
        public void PressedPointerTestFail()
        {
            drawingLineState.PressedPointer(150, 300);
            Assert.IsNull(drawingLineState.Hint);
        }

        //MovedPointerTestSuccess
        [TestMethod()]
        public void MovedPointerTestSuccess()
        {
            drawingLineState.PressedPointer(150, 150);
            drawingLineState.MovedPointer(300, 300);
            Assert.AreEqual(300, ((Line)drawingLineState.Hint).X2);
            Assert.AreEqual(300, ((Line)drawingLineState.Hint).Y2);
        }

        //MovedPointerTestFail
        [TestMethod()]
        public void MovedPointerTestFail()
        {
            drawingLineState.MovedPointer(300, 300);
            Assert.IsNull(drawingLineState.Hint);
        }

        //ReleasedPointerTestSuccess
        [TestMethod()]
        public void ReleasedPointerTestSuccess()
        {
            drawingLineState.PressedPointer(150, 150);
            drawingLineState.MovedPointer(300, 300);
            drawingLineState.ReleasedPointer(450, 450);
            Assert.AreEqual(450, ((Line)drawingLineState.Hint).X2);
            Assert.AreEqual(450, ((Line)drawingLineState.Hint).Y2);
            Assert.AreEqual(ShapeFlag.Ellipse, ((Line)drawingLineState.Hint).Shape2.ShapeFlag);
            Assert.AreEqual(500, ((Line)drawingLineState.Hint).Shape2.X1);
            Assert.AreEqual(500, ((Line)drawingLineState.Hint).Shape2.Y1);
            Assert.AreEqual(400, ((Line)drawingLineState.Hint).Shape2.X2);
            Assert.AreEqual(400, ((Line)drawingLineState.Hint).Shape2.Y2);
            Assert.AreEqual(1, ((Line)drawingLineState.Hint).ShapeIndex2);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //ReleasedPointerTestFail1
        [TestMethod()]
        public void ReleasedPointerTestFail1()
        {
            drawingLineState.PressedPointer(150, 150);
            drawingLineState.MovedPointer(300, 300);
            drawingLineState.ReleasedPointer(300, 300);
            Assert.AreEqual(StateFlag.DrawingLineState, model.StateFlag);
        }

        //ReleasedPointerTestFail2
        [TestMethod()]
        public void ReleasedPointerTestFail2()
        {
            drawingLineState.ReleasedPointer(450, 450);
            Assert.AreEqual(StateFlag.DrawingLineState, model.StateFlag);
        }
    }
}