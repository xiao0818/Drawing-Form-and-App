using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingModel.Tests.UnitTest
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

        //CopyTest
        [TestMethod()]
        public void CopyTest()
        {
            rectangle.X1 = 10;
            rectangle.Y1 = 20;
            rectangle.X2 = 30;
            rectangle.Y2 = 40;
            Assert.AreEqual(rectangle.X1, rectangle.Copy().X1);
            Assert.AreEqual(rectangle.Y1, rectangle.Copy().Y1);
            Assert.AreEqual(rectangle.X2, rectangle.Copy().X2);
            Assert.AreEqual(rectangle.Y2, rectangle.Copy().Y2);
        }

        //TakeLargeTest1
        [TestMethod()]
        public void TakeLargeTest1()
        {
            rectangle.X1 = 10;
            rectangle.X2 = 30;
            Assert.AreEqual(30, rectangle.TakeLarge(rectangle.X1, rectangle.X2));
        }

        //TakeLargeTest2
        [TestMethod()]
        public void TakeLargeTest2()
        {
            rectangle.X1 = 30;
            rectangle.X2 = 10;
            Assert.AreEqual(30, rectangle.TakeLarge(rectangle.X1, rectangle.X2));
        }

        //TakeSmallTest1
        [TestMethod()]
        public void TakeSmallTest1()
        {
            rectangle.X1 = 10;
            rectangle.X2 = 30;
            Assert.AreEqual(10, rectangle.TakeSmall(rectangle.X1, rectangle.X2));
        }

        //TakeSmallTest2
        [TestMethod()]
        public void TakeSmallTest2()
        {
            rectangle.X1 = 30;
            rectangle.X2 = 10;
            Assert.AreEqual(10, rectangle.TakeSmall(rectangle.X1, rectangle.X2));
        }
    }
}