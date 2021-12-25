using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class LineTests
    {
        Line line;

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            line = new Line();
        }

        //Shape1Test
        [TestMethod()]
        public void Shape1Test()
        {
            Rectangle rectangle = new Rectangle();
            line.Shape1 = rectangle;
            rectangle.X1 = 10;
            rectangle.Y1 = 20;
            rectangle.X2 = 30;
            rectangle.Y2 = 40;
            Assert.AreEqual(rectangle.X1, line.Shape1.X1);
            Assert.AreEqual(rectangle.Y1, line.Shape1.Y1);
            Assert.AreEqual(rectangle.X2, line.Shape1.X2);
            Assert.AreEqual(rectangle.Y2, line.Shape1.Y2);
            Assert.AreEqual(ShapeFlag.Rectangle, line.Shape1.GetShape);
        }

        //Shape2Test
        [TestMethod()]
        public void Shape2Test()
        {
            Ellipse ellipse = new Ellipse();
            line.Shape2 = ellipse;
            ellipse.X1 = 10;
            ellipse.Y1 = 20;
            ellipse.X2 = 30;
            ellipse.Y2 = 40;
            Assert.AreEqual(ellipse.X1, line.Shape2.X1);
            Assert.AreEqual(ellipse.Y1, line.Shape2.Y1);
            Assert.AreEqual(ellipse.X2, line.Shape2.X2);
            Assert.AreEqual(ellipse.Y2, line.Shape2.Y2);
            Assert.AreEqual(ShapeFlag.Ellipse, line.Shape2.GetShape);
        }

        //CopyTestForNoShapeReference
        [TestMethod()]
        public void CopyTestForNoShapeReference()
        {
            line.X1 = 10;
            line.Y1 = 20;
            line.X2 = 30;
            line.Y2 = 40;
            Rectangle rectangle = new Rectangle();
            line.Shape1 = rectangle;
            rectangle.X1 = 400;
            rectangle.Y1 = 300;
            rectangle.X2 = 200;
            rectangle.Y2 = 100;
            Ellipse ellipse = new Ellipse();
            line.Shape2 = ellipse;
            ellipse.X1 = 100;
            ellipse.Y1 = 200;
            ellipse.X2 = 300;
            ellipse.Y2 = 400;
            Shape lineCopy = line.Copy();
            Assert.AreEqual(line.X1, lineCopy.X1);
            Assert.AreEqual(line.Y1, lineCopy.Y1);
            Assert.AreEqual(line.X2, lineCopy.X2);
            Assert.AreEqual(line.Y2, lineCopy.Y2);
        }

        //CopyTestForShapeReference
        [TestMethod()]
        public void CopyTestForShapeReference()
        {
            line.X1 = 10;
            line.Y1 = 20;
            line.X2 = 30;
            line.Y2 = 40;
            Shape lineCopy = line.Copy();
            Assert.AreEqual(line.X1, lineCopy.X1);
            Assert.AreEqual(line.Y1, lineCopy.Y1);
            Assert.AreEqual(line.X2, lineCopy.X2);
            Assert.AreEqual(line.Y2, lineCopy.Y2);
        }

        //GetShapeTest
        [TestMethod()]
        public void GetShapeTest()
        {
            Assert.AreEqual(ShapeFlag.Line, line.GetShape);
        }

        //SetPointToShapeCenterTest
        [TestMethod()]
        public void SetPointToShapeCenterTest()
        {
            line.X1 = 10;
            line.Y1 = 20;
            line.X2 = 30;
            line.Y2 = 40;
            Rectangle rectangle = new Rectangle();
            line.Shape1 = rectangle;
            rectangle.X1 = 400;
            rectangle.Y1 = 300;
            rectangle.X2 = 200;
            rectangle.Y2 = 100;
            Ellipse ellipse = new Ellipse();
            line.Shape2 = ellipse;
            ellipse.X1 = 100;
            ellipse.Y1 = 200;
            ellipse.X2 = 300;
            ellipse.Y2 = 400;
            line.SetPointToShapeCenter();
            Assert.AreEqual((rectangle.X1 + rectangle.X2) / 2, line.X1);
            Assert.AreEqual((rectangle.Y1 + rectangle.Y2) / 2, line.Y1);
            Assert.AreEqual((ellipse.X1 + ellipse.X2) / 2, line.X2);
            Assert.AreEqual((ellipse.Y1 + ellipse.Y2) / 2, line.Y2);
        }
    }
}