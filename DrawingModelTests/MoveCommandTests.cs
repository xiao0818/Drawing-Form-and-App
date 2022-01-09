using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class MoveCommandTests
    {
        Model model;
        Rectangle rectangle;
        MoveCommand moveCommand;

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            model = new Model();
            rectangle = new Rectangle();
            rectangle.X1 = rectangle.Y1 = 100;
            rectangle.X2 = rectangle.Y2 = 200;
            moveCommand = new MoveCommand(model, rectangle, 100, 100);
        }

        //ExecuteTest
        [TestMethod()]
        public void ExecuteTest()
        {
            moveCommand.Execute();
            Assert.AreEqual(200, rectangle.X1);
            Assert.AreEqual(200, rectangle.Y1);
            Assert.AreEqual(300, rectangle.X2);
            Assert.AreEqual(300, rectangle.Y2);
        }

        //ExecuteBackTest
        [TestMethod()]
        public void ExecuteBackTest()
        {
            moveCommand.Execute();
            moveCommand.ExecuteBack();
            Assert.AreEqual(100, rectangle.X1);
            Assert.AreEqual(100, rectangle.Y1);
            Assert.AreEqual(200, rectangle.X2);
            Assert.AreEqual(200, rectangle.Y2);
        }
    }
}