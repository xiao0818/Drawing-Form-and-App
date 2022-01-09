using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class ModelTests
    {
        Model model;
        const string FILE_PATH = "\\..\\..\\..\\DrawingModel\\";
        const string SHAPE_FILE_NAME = "Shape.txt";

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            model = new Model();
        }

        //PressedPointerTestSuccess
        [TestMethod()]
        public void PressedPointerTestSuccess()
        {
            model.PressedPointer(150, 150);
            Assert.AreEqual(0, model.Shapes.Count);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //PressedPointerTestFail
        [TestMethod()]
        public void PressedPointerTestFail()
        {
            model.PressedPointer(0, 0);
            Assert.AreEqual(0, model.Shapes.Count);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //MovedPointerTest
        [TestMethod()]
        public void MovedPointerTest()
        {
            model.MovedPointer(150, 150);
            Assert.AreEqual(0, model.Shapes.Count);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //ReleasedPointerTest
        [TestMethod()]
        public void ReleasedPointerTest()
        {
            model.ReleasedPointer(300, 300);
            Assert.AreEqual(0, model.Shapes.Count);
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //ClearTest
        [TestMethod()]
        public void ClearTest()
        {
            Rectangle rectangle = new Rectangle();
            model.DrawShape(rectangle);
            model.Clear();
            Assert.AreEqual(0, model.Shapes.Count);
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
            model.DrawShape(rectangle);
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
            Rectangle rectangle = new Rectangle();
            model.DrawShape(rectangle);
            model.ExecuteCommand(new ClearCommand(model, model.Shapes));
            model.Undo();
            Assert.IsFalse(model.IsUndoEnabled);
            Assert.IsTrue(model.IsRedoEnabled);
            Assert.AreEqual(1, model.Shapes.Count);
        }

        //RedoTest
        [TestMethod()]
        public void RedoTest()
        {
            Rectangle rectangle = new Rectangle();
            model.DrawShape(rectangle);
            model.ExecuteCommand(new ClearCommand(model, model.Shapes));
            model.Undo();
            model.Redo();
            Assert.IsTrue(model.IsUndoEnabled);
            Assert.IsFalse(model.IsRedoEnabled);
            Assert.AreEqual(0, model.Shapes.Count);
        }

        //DrawDotRectangleTest
        [TestMethod()]
        public void DrawDotRectangleTest()
        {
            Rectangle rectangle = new Rectangle();
            rectangle.X1 = rectangle.Y1 = 100;
            rectangle.X2 = rectangle.Y2 = 200;
            model.DrawShape(rectangle);
            DotRectangle dotRectangle = new DotRectangle();
            dotRectangle.Shape = rectangle;
            model.DrawDotRectangle(dotRectangle);
            Assert.AreEqual(2, model.Shapes.Count);
            Assert.AreEqual(model.Target, dotRectangle);
        }

        //ResetSelectionTest
        [TestMethod()]
        public void ResetSelectionTest()
        {
            Rectangle rectangle = new Rectangle();
            rectangle.X1 = rectangle.Y1 = 100;
            rectangle.X2 = rectangle.Y2 = 200;
            model.DrawShape(rectangle);
            DotRectangle dotRectangle = new DotRectangle();
            dotRectangle.Shape = rectangle;
            model.DrawDotRectangle(dotRectangle);
            model.ResetSelection();
            Assert.AreEqual(1, model.Shapes.Count);
        }

        //SetDrawingStateTest
        [TestMethod()]
        public void SetDrawingStateTest()
        {
            model.SetDrawingState();
            Assert.AreEqual(StateFlag.DrawingState, model.StateFlag);
        }

        //SetDrawingLineStateTest
        [TestMethod()]
        public void SetDrawingLineStateTest()
        {
            model.SetDrawingLineState();
            Assert.AreEqual(StateFlag.DrawingLineState, model.StateFlag);
        }

        //SetPointerStateTest
        [TestMethod()]
        public void SetPointerStateTest()
        {
            model.SetPointerState();
            Assert.AreEqual(StateFlag.PointerState, model.StateFlag);
        }

        //SetMovingStateTest
        [TestMethod()]
        public void SetMovingStateTest()
        {
            model.SetMovingState();
            Assert.AreEqual(StateFlag.MovingState, model.StateFlag);
        }

        //AddCommandTest
        [TestMethod()]
        public void AddCommandTest()
        {
            Rectangle rectangle = new Rectangle();
            model.DrawShape(rectangle);
            model.AddCommand(new ClearCommand(model, model.Shapes));
            Assert.IsTrue(model.IsUndoEnabled);
            Assert.IsFalse(model.IsRedoEnabled);
            Assert.AreEqual(1, model.Shapes.Count);
        }

        //ExecuteCommandTest
        [TestMethod()]
        public void ExecuteCommandTest()
        {
            Rectangle rectangle = new Rectangle();
            model.DrawShape(rectangle);
            model.ExecuteCommand(new ClearCommand(model, model.Shapes));
            Assert.IsTrue(model.IsUndoEnabled);
            Assert.IsFalse(model.IsRedoEnabled);
            Assert.AreEqual(0, model.Shapes.Count);
        }

        //SaveTest
        [TestMethod()]
        public void SaveTest()
        {
            Rectangle rectangle = new Rectangle();
            rectangle.X1 = rectangle.Y1 = 100;
            rectangle.X2 = rectangle.Y2 = 200;
            model.DrawShape(rectangle);
            model.Save();
            StreamReader fileReader = new StreamReader(Environment.CurrentDirectory + FILE_PATH + SHAPE_FILE_NAME);
            Assert.AreEqual(rectangle.SaveText + "\r\n", fileReader.ReadToEnd());
        }

        //LoadTest
        [TestMethod()]
        public void LoadTest()
        {
            Rectangle rectangle = new Rectangle();
            rectangle.X1 = rectangle.Y1 = 100;
            rectangle.X2 = rectangle.Y2 = 200;
            model.DrawShape(rectangle);
            model.Save();
            model.Clear();
            model.Load();
            Assert.AreEqual(1, model.Shapes.Count);
            Assert.AreEqual(rectangle.ShapeFlag, model.Shapes[0].ShapeFlag);
            Assert.AreEqual(rectangle.X1, model.Shapes[0].X1);
            Assert.AreEqual(rectangle.Y1, model.Shapes[0].Y1);
            Assert.AreEqual(rectangle.X2, model.Shapes[0].X2);
            Assert.AreEqual(rectangle.Y2, model.Shapes[0].Y2);
            Assert.IsFalse(model.IsUndoEnabled);
            Assert.IsFalse(model.IsRedoEnabled);
        }

        //NotifyModelChangedTestSuccess
        [TestMethod()]
        public void NotifyModelChangedTestSuccess()
        {
            bool isNotifyModelChangedWork = false;
            model._modelChanged += () =>
            {
                isNotifyModelChangedWork = true;
            };
            model.NotifyModelChanged();
            Assert.IsTrue(isNotifyModelChangedWork);
        }

        //NotifyModelChangedTestFail
        [TestMethod()]
        public void NotifyModelChangedTestFail()
        {
            bool isNotifyModelChangedWork = false;
            model.NotifyModelChanged();
            Assert.IsFalse(isNotifyModelChangedWork);
        }
    }
}