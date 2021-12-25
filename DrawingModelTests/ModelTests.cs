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
            Assert.AreEqual(10, model.GetFirstPointX);
            Assert.AreEqual(20, model.GetFirstPointY);
            Assert.AreEqual(10, model.GetShapeHint.X1);
            Assert.AreEqual(20, model.GetShapeHint.Y1);
            Assert.IsTrue(model.IsPressed);
        }

        //PressedPointerTestForLine
        [TestMethod()]
        public void PressedPointerTestForLine()
        {
            model.ShapeFlag = ShapeFlag.Line;
            model.PressedPointer(10, 20, null);
            Assert.AreEqual(10, model.GetFirstPointX);
            Assert.AreEqual(20, model.GetFirstPointY);
            Assert.AreEqual(10, model.GetLineHint.X1);
            Assert.AreEqual(20, model.GetLineHint.Y1);
            Assert.IsTrue(model.IsPressed);
        }

        //PressedPointerTestFail
        [TestMethod()]
        public void PressedPointerTestFail()
        {
            model.PressedPointer(-10, -20, null);
            Assert.AreEqual(0, model.GetFirstPointX);
            Assert.AreEqual(0, model.GetFirstPointY);
            Assert.IsFalse(model.IsPressed);
        }

        //MovedPointerTestForShape
        [TestMethod()]
        public void MovedPointerTestForShape()
        {
            model.ShapeFlag = ShapeFlag.Rectangle;
            model.PressedPointer(10, 20, null);
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.MovedPointer(30, 40);
            Assert.AreEqual(30, model.GetShapeHint.X2);
            Assert.AreEqual(40, model.GetShapeHint.Y2);
            Assert.IsTrue(isNotifyObserverWork);
        }

        //MovedPointerTestForLine
        [TestMethod()]
        public void MovedPointerTestForLine()
        {
            model.ShapeFlag = ShapeFlag.Line;
            model.PressedPointer(10, 20, null);
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.MovedPointer(30, 40);
            Assert.AreEqual(30, model.GetLineHint.X2);
            Assert.AreEqual(40, model.GetLineHint.Y2);
            Assert.IsTrue(isNotifyObserverWork);
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
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.ReleasedPointer(30, 40, null);
            Assert.AreEqual(10, model.GetShapeHint.X1);
            Assert.AreEqual(20, model.GetShapeHint.Y1);
            Assert.AreEqual(30, model.GetShapeHint.X2);
            Assert.AreEqual(40, model.GetShapeHint.Y2);
            Assert.AreEqual(1, model.GetShapes.Count);
            Assert.AreEqual(model.GetShapeHint.X1, model.GetShapes[0].X1);
            Assert.AreEqual(model.GetShapeHint.Y1, model.GetShapes[0].Y1);
            Assert.AreEqual(model.GetShapeHint.X2, model.GetShapes[0].X2);
            Assert.AreEqual(model.GetShapeHint.Y2, model.GetShapes[0].Y2);
            Assert.IsFalse(model.IsPressed);
            Assert.IsTrue(isNotifyObserverWork);
        }

        //ReleasedPointerTestForLine
        [TestMethod()]
        public void ReleasedPointerTestForLine()
        {
            model.ShapeFlag = ShapeFlag.Line;
            model.PressedPointer(10, 20, null);
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.ReleasedPointer(30, 40, null);
            Assert.AreEqual(10, model.GetLineHint.X1);
            Assert.AreEqual(20, model.GetLineHint.Y1);
            Assert.AreEqual(30, model.GetLineHint.X2);
            Assert.AreEqual(40, model.GetLineHint.Y2);
            Assert.AreEqual(1, model.GetShapes.Count);
            Assert.AreEqual(model.GetLineHint.X1, model.GetShapes[0].X1);
            Assert.AreEqual(model.GetLineHint.Y1, model.GetShapes[0].Y1);
            Assert.AreEqual(model.GetLineHint.X2, model.GetShapes[0].X2);
            Assert.AreEqual(model.GetLineHint.Y2, model.GetShapes[0].Y2);
            Assert.IsFalse(model.IsPressed);
            Assert.IsTrue(isNotifyObserverWork);
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
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.Clear();
            Assert.AreEqual(0, model.GetShapes.Count);
            Assert.IsFalse(model.IsPressed);
            Assert.IsTrue(isNotifyObserverWork);
        }

        //DrawShapeTest
        [TestMethod()]
        public void DrawShapeTest()
        {
            Rectangle rectangle = new Rectangle();
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.DrawShape(rectangle);
            Assert.AreEqual(1, model.GetShapes.Count);
            Assert.IsTrue(isNotifyObserverWork);
        }

        //ClearAllTest
        [TestMethod()]
        public void ClearAllTest()
        {
            Rectangle rectangle = new Rectangle();
            Ellipse ellipse = new Ellipse();
            model.DrawShape(rectangle);
            model.DrawShape(ellipse);
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.ClearAll();
            Assert.AreEqual(0, model.GetShapes.Count);
            Assert.IsTrue(isNotifyObserverWork);
        }

        //DeleteShapeTest
        [TestMethod()]
        public void DeleteShapeTest()
        {
            Rectangle rectangle = new Rectangle();
            model.DrawShape(rectangle);
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.DeleteShape();
            Assert.AreEqual(0, model.GetShapes.Count);
            Assert.IsTrue(isNotifyObserverWork);
        }

        //UndoTest
        [TestMethod()]
        public void UndoTest()
        {
            model.ShapeFlag = ShapeFlag.Rectangle;
            model.PressedPointer(10, 20, null);
            model.ReleasedPointer(30, 40, null);
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.Undo();
            Assert.AreEqual(0, model.GetShapes.Count);
            Assert.IsTrue(isNotifyObserverWork);
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
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.Undo();
            model.Redo();
            Assert.AreEqual(1, model.GetShapes.Count);
            Assert.AreEqual(model.GetShapeHint.X1, model.GetShapes[0].X1);
            Assert.AreEqual(model.GetShapeHint.Y1, model.GetShapes[0].Y1);
            Assert.AreEqual(model.GetShapeHint.X2, model.GetShapes[0].X2);
            Assert.AreEqual(model.GetShapeHint.Y2, model.GetShapes[0].Y2);
            Assert.IsTrue(isNotifyObserverWork);
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