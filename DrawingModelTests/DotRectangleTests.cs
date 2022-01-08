﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class DotRectangleTests
    {
        DotRectangle dotRectangle;

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            dotRectangle = new DotRectangle();
        }

        //ShapeTest
        [TestMethod()]
        public void ShapeTest()
        {
            Rectangle rectangle = new Rectangle();
            dotRectangle.Shape = rectangle;
            SetShape(rectangle);
            Assert.AreEqual(rectangle.X1, dotRectangle.X1);
            Assert.AreEqual(rectangle.Y1, dotRectangle.Y1);
            Assert.AreEqual(rectangle.X2, dotRectangle.X2);
            Assert.AreEqual(rectangle.Y2, dotRectangle.Y2);
            Assert.AreEqual(ShapeFlag.Rectangle, dotRectangle.Shape.ShapeFlag);
        }

        //CopyTestForNoShapeReference
        [TestMethod()]
        public void CopyTestForNoShapeReference()
        {
            dotRectangle.X1 = 10;
            dotRectangle.Y1 = 20;
            dotRectangle.X2 = 30;
            dotRectangle.Y2 = 40;
            Shape dotRectangleCopy = dotRectangle.Copy();
            Assert.AreEqual(dotRectangle.X1, dotRectangleCopy.X1);
            Assert.AreEqual(dotRectangle.Y1, dotRectangleCopy.Y1);
            Assert.AreEqual(dotRectangle.X2, dotRectangleCopy.X2);
            Assert.AreEqual(dotRectangle.Y2, dotRectangleCopy.Y2);
        }
        //CopyTestForNoShapeReference
        [TestMethod()]
        public void CopyTestForShapeReference()
        {
            Rectangle rectangle = new Rectangle();
            dotRectangle.Shape = rectangle;
            SetShape(rectangle);
            Shape dotRectangleCopy = dotRectangle.Copy();
            Assert.AreEqual(rectangle.X1, dotRectangleCopy.X1);
            Assert.AreEqual(rectangle.Y1, dotRectangleCopy.Y1);
            Assert.AreEqual(rectangle.X2, dotRectangleCopy.X2);
            Assert.AreEqual(rectangle.Y2, dotRectangleCopy.Y2);
        }

        //GetShapeTest
        [TestMethod()]
        public void GetShapeTest()
        {
            Assert.AreEqual(ShapeFlag.DotRectangle, dotRectangle.ShapeFlag);
        }

        //TakeLargeTest1
        [TestMethod()]
        public void TakeLargeTest1()
        {
            dotRectangle.X1 = 10;
            dotRectangle.X2 = 30;
            Assert.AreEqual(30, dotRectangle.TakeLarge(dotRectangle.X1, dotRectangle.X2));
        }

        //TakeLargeTest2
        [TestMethod()]
        public void TakeLargeTest2()
        {
            dotRectangle.X1 = 30;
            dotRectangle.X2 = 10;
            Assert.AreEqual(30, dotRectangle.TakeLarge(dotRectangle.X1, dotRectangle.X2));
        }

        //TakeSmallTest1
        [TestMethod()]
        public void TakeSmallTest1()
        {
            dotRectangle.X1 = 10;
            dotRectangle.X2 = 30;
            Assert.AreEqual(10, dotRectangle.TakeSmall(dotRectangle.X1, dotRectangle.X2));
        }

        //TakeSmallTest2
        [TestMethod()]
        public void TakeSmallTest2()
        {
            dotRectangle.X1 = 30;
            dotRectangle.X2 = 10;
            Assert.AreEqual(10, dotRectangle.TakeSmall(dotRectangle.X1, dotRectangle.X2));
        }

        //setShape
        private void SetShape(Shape shape)
        {
            shape.X1 = 10;
            shape.Y1 = 20;
            shape.X2 = 30;
            shape.Y2 = 40;
        }
    }
}