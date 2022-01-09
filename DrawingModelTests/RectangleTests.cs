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

        //CopyTest
        [TestMethod()]
        public void CopyTest()
        {
            rectangle.X1 = 10;
            rectangle.Y1 = 20;
            rectangle.X2 = 30;
            rectangle.Y2 = 40;
            Shape rectangleCopy = rectangle.Copy();
            Assert.AreEqual(rectangle.X1, rectangleCopy.X1);
            Assert.AreEqual(rectangle.Y1, rectangleCopy.Y1);
            Assert.AreEqual(rectangle.X2, rectangleCopy.X2);
            Assert.AreEqual(rectangle.Y2, rectangleCopy.Y2);
        }

        //ShapeFlagTest
        [TestMethod()]
        public void ShapeFlagTest()
        {
            Assert.AreEqual(ShapeFlag.Rectangle, rectangle.ShapeFlag);
        }

        //SaveTextTest
        [TestMethod()]
        public void SaveTextTest()
        {
            rectangle.X1 = 10;
            rectangle.Y1 = 20;
            rectangle.X2 = 30;
            rectangle.Y2 = 40;
            Assert.AreEqual("Rectangle 10 20 30 40", rectangle.SaveText);
        }

        //TakeLargeTest1
        [TestMethod()]
        public void TakeLargeTest1()
        {
            rectangle.X1 = 10;
            rectangle.X2 = 20;
            Assert.AreEqual(20, rectangle.TakeLarge(rectangle.X1, rectangle.X2));
        }

        //TakeLargeTest2
        [TestMethod()]
        public void TakeLargeTest2()
        {
            rectangle.X1 = 20;
            rectangle.X2 = 10;
            Assert.AreEqual(20, rectangle.TakeLarge(rectangle.X1, rectangle.X2));
        }

        //TakeSmallTest1
        [TestMethod()]
        public void TakeSmallTest1()
        {
            rectangle.X1 = 10;
            rectangle.X2 = 20;
            Assert.AreEqual(10, rectangle.TakeSmall(rectangle.X1, rectangle.X2));
        }

        //TakeSmallTest2
        [TestMethod()]
        public void TakeSmallTest2()
        {
            rectangle.X1 = 20;
            rectangle.X2 = 10;
            Assert.AreEqual(10, rectangle.TakeSmall(rectangle.X1, rectangle.X2));
        }
    }
}