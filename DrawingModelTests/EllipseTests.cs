using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class EllipseTests
    {
        Ellipse ellipse;

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            ellipse = new Ellipse();
        }

        //CopyTest
        [TestMethod()]
        public void CopyTest()
        {
            ellipse.X1 = 10;
            ellipse.Y1 = 20;
            ellipse.X2 = 30;
            ellipse.Y2 = 40;
            Shape ellipseCopy = ellipse.Copy();
            Assert.AreEqual(ellipse.X1, ellipseCopy.X1);
            Assert.AreEqual(ellipse.Y1, ellipseCopy.Y1);
            Assert.AreEqual(ellipse.X2, ellipseCopy.X2);
            Assert.AreEqual(ellipse.Y2, ellipseCopy.Y2);
        }

        //ShapeFlagTest
        [TestMethod()]
        public void ShapeFlagTest()
        {
            Assert.AreEqual(ShapeFlag.Ellipse, ellipse.ShapeFlag);
        }

        //SaveTextTest
        [TestMethod()]
        public void SaveTextTest()
        {
            ellipse.X1 = 10;
            ellipse.Y1 = 20;
            ellipse.X2 = 30;
            ellipse.Y2 = 40;
            Assert.AreEqual("Ellipse 10 20 30 40", ellipse.SaveText);
        }

        //TakeLargeTest1
        [TestMethod()]
        public void TakeLargeTest1()
        {
            ellipse.X1 = 10;
            ellipse.X2 = 20;
            Assert.AreEqual(20, ellipse.TakeLarge(ellipse.X1, ellipse.X2));
        }

        //TakeLargeTest2
        [TestMethod()]
        public void TakeLargeTest2()
        {
            ellipse.X1 = 20;
            ellipse.X2 = 10;
            Assert.AreEqual(20, ellipse.TakeLarge(ellipse.X1, ellipse.X2));
        }

        //TakeSmallTest1
        [TestMethod()]
        public void TakeSmallTest1()
        {
            ellipse.X1 = 10;
            ellipse.X2 = 20;
            Assert.AreEqual(10, ellipse.TakeSmall(ellipse.X1, ellipse.X2));
        }

        //TakeSmallTest2
        [TestMethod()]
        public void TakeSmallTest2()
        {
            ellipse.X1 = 20;
            ellipse.X2 = 10;
            Assert.AreEqual(10, ellipse.TakeSmall(ellipse.X1, ellipse.X2));
        }
    }
}