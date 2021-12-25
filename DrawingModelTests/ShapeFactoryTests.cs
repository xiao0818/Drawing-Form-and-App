using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class ShapeFactoryTests
    {
        ShapeFactory shapeFactory;

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            shapeFactory = new ShapeFactory();
        }

        //CreateShapeTestForRectangle
        [TestMethod()]
        public void CreateShapeTestForRectangle()
        {
            Assert.AreEqual(ShapeFlag.Rectangle, shapeFactory.CreateShape(ShapeFlag.Rectangle).GetShape);
        }

        //CreateShapeTestForEllipse
        [TestMethod()]
        public void CreateShapeTestForEllipse()
        {
            Assert.AreEqual(ShapeFlag.Ellipse, shapeFactory.CreateShape(ShapeFlag.Ellipse).GetShape);
        }

        //CreateShapeTestForDotRectangle
        [TestMethod()]
        public void CreateShapeTestForDotRectangle()
        {
            Assert.AreEqual(ShapeFlag.DotRectangle, shapeFactory.CreateShape(ShapeFlag.DotRectangle).GetShape);
        }

        //CreateLineTest
        [TestMethod()]
        public void CreateLineTest()
        {
            Assert.AreEqual(ShapeFlag.Line, shapeFactory.CreateLine.GetShape);
        }
    }
}