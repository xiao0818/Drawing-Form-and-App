using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class RectangleTests
    {
        Rectangle rectangle;

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            rectangle = new Rectangle();
        }

        //DrawTest
        [TestMethod()]
        public void DrawTest()
        {
            Assert.Fail();
        }

        //CopyTest
        [TestMethod()]
        public void CopyTest()
        {
            rectangle.X1 = 50;
            rectangle.Y1 = 100;
            rectangle.X2 = 150;
            rectangle.Y2 = 200;
            Assert.AreEqual(rectangle.X1, rectangle.Copy().X1);
            Assert.AreEqual(rectangle.Y1, rectangle.Copy().Y1);
            Assert.AreEqual(rectangle.X2, rectangle.Copy().X2);
            Assert.AreEqual(rectangle.Y2, rectangle.Copy().Y2);
        }
    }
}