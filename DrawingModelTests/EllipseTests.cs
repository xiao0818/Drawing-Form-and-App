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
            ellipse.X1 = 50;
            ellipse.Y1 = 100;
            ellipse.X2 = 150;
            ellipse.Y2 = 200;
            Assert.AreEqual(ellipse.X1, ellipse.Copy().X1);
            Assert.AreEqual(ellipse.Y1, ellipse.Copy().Y1);
            Assert.AreEqual(ellipse.X2, ellipse.Copy().X2);
            Assert.AreEqual(ellipse.Y2, ellipse.Copy().Y2);
        }
    }
}