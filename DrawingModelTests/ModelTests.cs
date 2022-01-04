using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class ModelTests
    {
        Model model;

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            model = new Model();
        }

        //PressedPointerTestForShape
        [TestMethod()]
        public void PressedPointerTestForShape()
        {
            model.ShapeFlag = ShapeFlag.Rectangle;
            model.PressedPointer(10, 20, null);
            Assert.AreEqual(10, model.FirstPointX);
            Assert.AreEqual(20, model.FirstPointY);
            Assert.AreEqual(10, model.ShapeHint.X1);
            Assert.AreEqual(20, model.ShapeHint.Y1);
            Assert.IsTrue(model.IsPressed);
        }

        //PressedPointerTestForLine
        [TestMethod()]
        public void PressedPointerTestForLine()
        {
            model.ShapeFlag = ShapeFlag.Line;
            model.PressedPointer(10, 20, null);
            Assert.AreEqual(10, model.FirstPointX);
            Assert.AreEqual(20, model.FirstPointY);
            Assert.AreEqual(10, model.LineHint.X1);
            Assert.AreEqual(20, model.LineHint.Y1);
            Assert.IsTrue(model.IsPressed);
        }

        //PressedPointerTestFail
        [TestMethod()]
        public void PressedPointerTestFail()
        {
            model.PressedPointer(-10, -20, null);
            Assert.AreEqual(0, model.FirstPointX);
            Assert.AreEqual(0, model.FirstPointY);
            Assert.IsFalse(model.IsPressed);
        }

        //MovedPointerTestForShape
        [TestMethod()]
        public void MovedPointerTestForShape()
        {
            model.ShapeFlag = ShapeFlag.Rectangle;
            model.PressedPointer(10, 20, null);
            model.MovedPointer(30, 40);
            Assert.AreEqual(30, model.ShapeHint.X2);
            Assert.AreEqual(40, model.ShapeHint.Y2);
        }

        //MovedPointerTestForLine
        [TestMethod()]
        public void MovedPointerTestForLine()
        {
            model.ShapeFlag = ShapeFlag.Line;
            model.PressedPointer(10, 20, null);
            model.MovedPointer(30, 40);
            Assert.AreEqual(30, model.LineHint.X2);
            Assert.AreEqual(40, model.LineHint.Y2);
        }

        //MovedPointerTestFail
        [TestMethod()]
        public void MovedPointerTestFail()
        {
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.MovedPointer(30, 40);
            Assert.IsFalse(isNotifyObserverWork);
        }

        //ReleasedPointerTestForShape
        [TestMethod()]
        public void ReleasedPointerTestForShape()
        {
            model.ShapeFlag = ShapeFlag.Rectangle;
            model.PressedPointer(10, 20, null);
            model.ReleasedPointer(30, 40, null);
            Assert.AreEqual(1, model.Shapes.Count);
            Assert.AreEqual(model.ShapeHint.X1, model.Shapes[0].X1);
            Assert.AreEqual(model.ShapeHint.Y1, model.Shapes[0].Y1);
            Assert.AreEqual(model.ShapeHint.X2, model.Shapes[0].X2);
            Assert.AreEqual(model.ShapeHint.Y2, model.Shapes[0].Y2);
            Assert.IsFalse(model.IsPressed);
        }

        //ReleasedPointerTestForLine
        [TestMethod()]
        public void ReleasedPointerTestForLine()
        {
            model.ShapeFlag = ShapeFlag.Line;
            model.PressedPointer(10, 20, null);
            model.ReleasedPointer(30, 40, null);
            Assert.AreEqual(1, model.Shapes.Count);
            Assert.AreEqual(model.LineHint.X1, model.Shapes[0].X1);
            Assert.AreEqual(model.LineHint.Y1, model.Shapes[0].Y1);
            Assert.AreEqual(model.LineHint.X2, model.Shapes[0].X2);
            Assert.AreEqual(model.LineHint.Y2, model.Shapes[0].Y2);
            Assert.IsFalse(model.IsPressed);
        }

        //ReleasedPointerTestFail
        [TestMethod()]
        public void ReleasedPointerTestFail()
        {
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.ReleasedPointer(30, 40, null);
            Assert.IsFalse(isNotifyObserverWork);
        }

        //ClearTest
        [TestMethod()]
        public void ClearTest()
        {
            model.ShapeFlag = ShapeFlag.Rectangle;
            model.PressedPointer(10, 20, null);
            model.ReleasedPointer(30, 40, null);
            model.Clear();
            Assert.AreEqual(0, model.Shapes.Count);
            Assert.IsFalse(model.IsPressed);
        }

        //DrawShapeTest
        [TestMethod()]
        public void DrawShapeTest()
        {
            Rectangle rectangle = new Rectangle();
            model.DrawShape(rectangle);
            Assert.AreEqual(1, model.Shapes.Count);
        }

        //ClearAllTest
        [TestMethod()]
        public void ClearAllTest()
        {
            Rectangle rectangle = new Rectangle();
            Ellipse ellipse = new Ellipse();
            model.DrawShape(rectangle);
            model.DrawShape(ellipse);
            model.ClearAll();
            Assert.AreEqual(0, model.Shapes.Count);
        }

        //DeleteShapeTest
        [TestMethod()]
        public void DeleteShapeTest()
        {
            Rectangle rectangle = new Rectangle();
            model.DrawShape(rectangle);
            model.DeleteShape();
            Assert.AreEqual(0, model.Shapes.Count);
        }

        //UndoTest
        [TestMethod()]
        public void UndoTest()
        {
            model.ShapeFlag = ShapeFlag.Rectangle;
            model.PressedPointer(10, 20, null);
            model.ReleasedPointer(30, 40, null);
            model.Undo();
            Assert.AreEqual(0, model.Shapes.Count);
            Assert.IsFalse(model.IsUndoEnabled);
            Assert.IsTrue(model.IsRedoEnabled);
        }

        //RedoTest
        [TestMethod()]
        public void RedoTest()
        {
            model.ShapeFlag = ShapeFlag.Rectangle;
            model.PressedPointer(10, 20, null);
            model.ReleasedPointer(30, 40, null);
            model.Undo();
            model.Redo();
            Assert.AreEqual(1, model.Shapes.Count);
            Assert.AreEqual(model.ShapeHint.X1, model.Shapes[0].X1);
            Assert.AreEqual(model.ShapeHint.Y1, model.Shapes[0].Y1);
            Assert.AreEqual(model.ShapeHint.X2, model.Shapes[0].X2);
            Assert.AreEqual(model.ShapeHint.Y2, model.Shapes[0].Y2);
            Assert.IsTrue(model.IsUndoEnabled);
            Assert.IsFalse(model.IsRedoEnabled);
        }

        //PressedCancelTest
        [TestMethod()]
        public void PressedCancelTest()
        {
            model.PressedPointer(10, 20, null);
            model.PressedCancel();
            Assert.IsFalse(model.GetIsPressed);
        }

        //NotifyModelChangedTest
        [TestMethod()]
        public void NotifyModelChangedTest()
        {
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.NotifyModelChanged();
            Assert.IsTrue(isNotifyObserverWork);
        }

        //NotifyModelChangedNullTest
        [TestMethod()]
        public void NotifyModelChangedNullTest()
        {
            bool isNotifyObserverWork = false;
            model.NotifyModelChanged();
            Assert.IsFalse(isNotifyObserverWork);
        }
    }
}