using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class DrawCommandTests
    {
        Model model;
        Rectangle rectangle;
        DrawCommand drawCommand;

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            model = new Model();
            rectangle = new Rectangle();
            drawCommand = new DrawCommand(model, rectangle);
        }

        //ExecuteTest
        [TestMethod()]
        public void ExecuteTest()
        {
            drawCommand.Execute();
            Assert.AreEqual(1, model.GetShapes.Count);
            Assert.AreEqual(ShapeFlag.Rectangle, model.GetShapes[0].GetShape);
        }

        //ExecuteBackTest
        [TestMethod()]
        public void ExecuteBackTest()
        {
            drawCommand.Execute();
            drawCommand.ExecuteBack();
            Assert.AreEqual(0, model.GetShapes.Count);
        }
    }
}