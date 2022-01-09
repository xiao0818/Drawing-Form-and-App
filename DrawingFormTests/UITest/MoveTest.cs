using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace DrawingForm.Tests
{
    [TestClass()]
    public class MoveTest
    {
        Robot _robot;
        private string targetAppPath;
        private const string DRAWING_FORM = "DrawingForm";
        const int centerX = 674;
        const int centerY = 364;

        //Initialize
        [TestInitialize]
        public void SetUp()
        {
            var projectName = "DrawingForm";
            string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            targetAppPath = Path.Combine(solutionPath, projectName, "bin", "Debug", "DrawingForm.exe");
            _robot = new Robot(targetAppPath, DRAWING_FORM);
        }

        //TearDown
        [TestCleanup]
        public void TearDown()
        {
            _robot.CleanUp();
        }

        //MoveTest
        [TestMethod]
        public void MovingTest()
        {
            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop(-100, -100, 100, 100);
            _robot.ClickPosition(0, 0);
            _robot.DragAndDrop(0, 0, 100, 100);
            _robot.ClickPosition(100, 100);
            _robot.AssertLabelText("_label", "Selected : Rectangle (" + (centerX).ToString() + ", " + (centerY).ToString() + ", " + (centerX + 200).ToString() + ", " + (centerY + 200).ToString() + ")");
        }
    }
}
