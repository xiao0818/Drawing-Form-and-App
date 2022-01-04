using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class ClearCommandTests
    {
        Model model;
        Rectangle rectangle;
        Ellipse ellipse;
        List<Shape> shapes;
        ClearCommand clearCommand;

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            model = new Model();
            rectangle = new Rectangle();
            ellipse = new Ellipse();
            shapes = new List<Shape>();
            shapes.Add(rectangle);
            shapes.Add(ellipse);
            clearCommand = new ClearCommand(model, shapes);
        }

        //ExecuteTest
        [TestMethod()]
        public void ExecuteTest()
        {
            clearCommand.ExecuteBack();
            clearCommand.Execute();
            Assert.AreEqual(0, model.Shapes.Count);
        }

        //ExecuteBackTest
        [TestMethod()]
        public void ExecuteBackTest()
        {
            clearCommand.ExecuteBack();
            Assert.AreEqual(2, model.Shapes.Count);
            Assert.AreEqual(ShapeFlag.Rectangle, model.Shapes[0].GetShape);
            Assert.AreEqual(ShapeFlag.Ellipse, model.Shapes[1].GetShape);
        }
    }
}