﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            SetShape1(rectangle);
            Assert.AreEqual(rectangle.X1, line.Shape1.X1);
            Assert.AreEqual(rectangle.Y1, line.Shape1.Y1);
            Assert.AreEqual(rectangle.X2, line.Shape1.X2);
            Assert.AreEqual(rectangle.Y2, line.Shape1.Y2);
            Assert.AreEqual(ShapeFlag.Rectangle, line.Shape1.ShapeFlag);
        }

        //Shape2Test
        [TestMethod()]
        public void Shape2Test()
        {
            Ellipse ellipse = new Ellipse();
            line.Shape2 = ellipse;
            SetShape2(ellipse);
            Assert.AreEqual(ellipse.X1, line.Shape2.X1);
            Assert.AreEqual(ellipse.Y1, line.Shape2.Y1);
            Assert.AreEqual(ellipse.X2, line.Shape2.X2);
            Assert.AreEqual(ellipse.Y2, line.Shape2.Y2);
            Assert.AreEqual(ShapeFlag.Ellipse, line.Shape2.ShapeFlag);
        }

        //CopyTestForNoShapeReference
        [TestMethod()]
        public void CopyTestForNoShapeReference()
        {
            line.X1 = 10;
            line.Y1 = 20;
            line.X2 = 30;
            line.Y2 = 40;
            Rectangle rectangle1 = new Rectangle();
            line.Shape1 = rectangle1;
            SetShape1(rectangle1);
            Rectangle rectangle2 = new Rectangle();
            line.Shape2 = rectangle2;
            SetShape2(rectangle2);
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
            Assert.AreEqual(ShapeFlag.Line, line.ShapeFlag);
        }

        //SetPointToShapeCenterTest
        [TestMethod()]
        public void SetPointToShapeCenterTest()
        {
            line.X1 = 10;
            line.Y1 = 20;
            line.X2 = 30;
            line.Y2 = 40;
            Ellipse ellipse1 = new Ellipse();
            line.Shape1 = ellipse1;
            SetShape1(ellipse1);
            Ellipse ellipse2 = new Ellipse();
            line.Shape2 = ellipse2;
            SetShape2(ellipse2);
            line.SetPointToShapeCenter();
            Assert.AreEqual((ellipse1.X1 + ellipse1.X2) / 2, line.X1);
            Assert.AreEqual((ellipse1.Y1 + ellipse1.Y2) / 2, line.Y1);
            Assert.AreEqual((ellipse2.X1 + ellipse2.X2) / 2, line.X2);
            Assert.AreEqual((ellipse2.Y1 + ellipse2.Y2) / 2, line.Y2);
        }

        //setShape1
        private void SetShape1(Shape shape)
        {
            shape.X1 = 400;
            shape.Y1 = 300;
            shape.X2 = 200;
            shape.Y2 = 100;
        }

        //setShape2
        private void SetShape2(Shape shape)
        {
            shape.X1 = 100;
            shape.Y1 = 200;
            shape.X2 = 300;
            shape.Y2 = 400;
        }
    }
}