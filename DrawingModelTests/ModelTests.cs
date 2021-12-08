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

        //PressedPointerTestForRectangle
        [TestMethod()]
        public void PressedPointerTestForRectangle()
        {
            model.PressedPointer(10, 20, (int)ShapeFlag.Rectangle);
            Assert.AreEqual(10, model.GetFirstPointX);
            Assert.AreEqual(20, model.GetFirstPointY);
            Assert.AreEqual(10, model.GetHint.X1);
            Assert.AreEqual(20, model.GetHint.Y1);
            Assert.IsTrue(model.GetIsPressed);
        }

        //PressedPointerTestForEllipse
        [TestMethod()]
        public void PressedPointerTestForEllipse()
        {
            model.PressedPointer(10, 20, (int)ShapeFlag.Ellipse);
            Assert.AreEqual(10, model.GetFirstPointX);
            Assert.AreEqual(20, model.GetFirstPointY);
            Assert.AreEqual(10, model.GetHint.X1);
            Assert.AreEqual(20, model.GetHint.Y1);
            Assert.IsTrue(model.GetIsPressed);
        }

        //PressedPointerTestFail
        [TestMethod()]
        public void PressedPointerTestFail()
        {
            model.PressedPointer(-10, -20, (int)ShapeFlag.Null);
            Assert.AreEqual(0, model.GetFirstPointX);
            Assert.AreEqual(0, model.GetFirstPointY);
            Assert.IsFalse(model.GetIsPressed);
        }

        //MovedPointerTest
        [TestMethod()]
        public void MovedPointerTest()
        {
            model.PressedPointer(10, 20, (int)ShapeFlag.Rectangle);
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.MovedPointer(30, 40);
            Assert.AreEqual(30, model.GetHint.X2);
            Assert.AreEqual(40, model.GetHint.Y2);
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

        //ReleasedPointerTest
        [TestMethod()]
        public void ReleasedPointerTest()
        {
            model.PressedPointer(10, 20, (int)ShapeFlag.Rectangle);
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.ReleasedPointer(30, 40);
            Assert.AreEqual(10, model.GetHint.X1);
            Assert.AreEqual(20, model.GetHint.Y1);
            Assert.AreEqual(30, model.GetHint.X2);
            Assert.AreEqual(40, model.GetHint.Y2);
            Assert.AreEqual(1, model.GetShapes.Count);
            Assert.AreEqual(model.GetHint.X1, model.GetShapes[0].X1);
            Assert.AreEqual(model.GetHint.Y1, model.GetShapes[0].Y1);
            Assert.AreEqual(model.GetHint.X2, model.GetShapes[0].X2);
            Assert.AreEqual(model.GetHint.Y2, model.GetShapes[0].Y2);
            Assert.IsFalse(model.GetIsPressed);
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
            model.ReleasedPointer(30, 40);
            Assert.IsFalse(isNotifyObserverWork);
        }

        //ClearTest
        [TestMethod()]
        public void ClearTest()
        {
            model.PressedPointer(10, 20, (int)ShapeFlag.Rectangle);
            model.ReleasedPointer(30, 40);
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.Clear();
            Assert.AreEqual(0, model.GetShapes.Count);
            Assert.IsFalse(model.GetIsPressed);
            Assert.IsTrue(isNotifyObserverWork);
        }

        //DrawTest
        [TestMethod()]
        public void DrawTest()
        {
            Assert.Fail();
        }

        //NotifyModelChanged
        [TestMethod()]
        public void NotifyModelChanged()
        {
            bool isNotifyObserverWork = false;
            model._modelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            model.NotifyModelChanged();
            Assert.IsTrue(isNotifyObserverWork);
        }
    }
}