using DrawingModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingForm.Tests.UnitTest
{
    [TestClass()]
    public class DrawingFormPresentationModelTests
    {
        DrawingFormPresentationModel drawingFormPresentationModel;
        Model model; 

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            model = new Model();
            drawingFormPresentationModel = new DrawingFormPresentationModel(model);
        }

        //HandleRectangleButtonClickTest
        [TestMethod()]
        public void HandleRectangleButtonClickTest()
        {
            drawingFormPresentationModel.HandleRectangleButtonClick();
            Assert.IsFalse(drawingFormPresentationModel.IsRectangleButtonEnable);
            Assert.IsTrue(drawingFormPresentationModel.IsEllipseButtonEnable);
            Assert.AreEqual((int)ShapeFlag.Rectangle, drawingFormPresentationModel.GetShapeFlag);
        }

        //HandleEllipseButtonClickTest
        [TestMethod()]
        public void HandleEllipseButtonClickTest()
        {
            drawingFormPresentationModel.HandleEllipseButtonClick();
            Assert.IsTrue(drawingFormPresentationModel.IsRectangleButtonEnable);
            Assert.IsFalse(drawingFormPresentationModel.IsEllipseButtonEnable);
            Assert.AreEqual((int)ShapeFlag.Ellipse, drawingFormPresentationModel.GetShapeFlag);
        }

        //HandleClearButtonClickTest
        [TestMethod()]
        public void HandleClearButtonClickTest()
        {
            drawingFormPresentationModel.HandleClearButtonClick();
            Assert.IsTrue(drawingFormPresentationModel.IsRectangleButtonEnable);
            Assert.IsTrue(drawingFormPresentationModel.IsEllipseButtonEnable);
            Assert.AreEqual((int)ShapeFlag.Null, drawingFormPresentationModel.GetShapeFlag);
        }

        //HandleCanvasPointerReleasedTest
        [TestMethod()]
        public void HandleCanvasPointerReleasedTest()
        {
            drawingFormPresentationModel.HandleCanvasPointerReleased();
            Assert.IsTrue(drawingFormPresentationModel.IsRectangleButtonEnable);
            Assert.IsTrue(drawingFormPresentationModel.IsEllipseButtonEnable);
            Assert.AreEqual((int)ShapeFlag.Null, drawingFormPresentationModel.GetShapeFlag);
        }

        //PressedPointerTest
        [TestMethod()]
        public void PressedPointerTest()
        {
            drawingFormPresentationModel.PressedPointer(10, 20, (int)ShapeFlag.Rectangle);
            Assert.AreEqual(10, model.GetFirstPointX);
            Assert.AreEqual(20, model.GetFirstPointY);
            Assert.AreEqual(10, model.GetHint.X1);
            Assert.AreEqual(20, model.GetHint.Y1);
            Assert.IsTrue(model.IsPressed);
        }

        //MovedPointerTest
        [TestMethod()]
        public void MovedPointerTest()
        {
            drawingFormPresentationModel.PressedPointer(10, 20, (int)ShapeFlag.Rectangle);
            bool isNotifyObserverWork = false;
            drawingFormPresentationModel._drawingFormPresentationModelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            drawingFormPresentationModel.MovedPointer(30, 40);
            Assert.AreEqual(30, model.GetHint.X2);
            Assert.AreEqual(40, model.GetHint.Y2);
            Assert.IsTrue(isNotifyObserverWork);
            Assert.IsTrue(isNotifyObserverWork);
        }

        //ReleasedPointerTest
        [TestMethod()]
        public void ReleasedPointerTest()
        {
            drawingFormPresentationModel.PressedPointer(10, 20, (int)ShapeFlag.Rectangle);
            bool isNotifyObserverWork = false;
            drawingFormPresentationModel._drawingFormPresentationModelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            drawingFormPresentationModel.ReleasedPointer(30, 40);
            Assert.AreEqual(10, model.GetHint.X1);
            Assert.AreEqual(20, model.GetHint.Y1);
            Assert.AreEqual(30, model.GetHint.X2);
            Assert.AreEqual(40, model.GetHint.Y2);
            Assert.AreEqual(1, model.GetShapes.Count);
            Assert.AreEqual(model.GetHint.X1, model.GetShapes[0].X1);
            Assert.AreEqual(model.GetHint.Y1, model.GetShapes[0].Y1);
            Assert.AreEqual(model.GetHint.X2, model.GetShapes[0].X2);
            Assert.AreEqual(model.GetHint.Y2, model.GetShapes[0].Y2);
            Assert.IsFalse(model.IsPressed);
            Assert.IsTrue(isNotifyObserverWork);
        }

        //ClearTest
        [TestMethod()]
        public void ClearTest()
        {
            drawingFormPresentationModel.PressedPointer(10, 20, (int)ShapeFlag.Rectangle);
            drawingFormPresentationModel.ReleasedPointer(30, 40);
            bool isNotifyObserverWork = false;
            drawingFormPresentationModel._drawingFormPresentationModelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            drawingFormPresentationModel.Clear();
            Assert.AreEqual(0, model.GetShapes.Count);
            Assert.IsFalse(model.IsPressed);
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
            drawingFormPresentationModel._drawingFormPresentationModelChanged += () =>
            {
                isNotifyObserverWork = true;
            };
            drawingFormPresentationModel.NotifyModelChanged();
            Assert.IsTrue(isNotifyObserverWork);
        }

        //NotifyModelChangedNull
        [TestMethod()]
        public void NotifyModelChangedNull()
        {
            bool isNotifyObserverWork = false;
            drawingFormPresentationModel.NotifyModelChanged();
            Assert.IsFalse(isNotifyObserverWork);
        }
    }
}