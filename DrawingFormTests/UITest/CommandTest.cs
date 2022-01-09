using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace DrawingForm.Tests
{
    [TestClass()]
    public class CommandTest
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

        //UndoTest
        [TestMethod]
        public void UndoTest()
        {
            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop(-100, -100, 100, 100);
            _robot.ClickButton("Undo");
            _robot.ClickPosition(0, 0);
            _robot.AssertLabelText("_label", "Selected : None");
        }

        //MoveTest
        [TestMethod]
        public void MovingTest()
        {
            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop(-100, -100, 100, 100);
            _robot.ClickButton("Undo");
            _robot.ClickButton("Redo");
            _robot.ClickPosition(0, 0);
            _robot.AssertLabelText("_label", "Selected : Rectangle (" + (centerX - 100).ToString() + ", " + (centerY - 100).ToString() + ", " + (centerX + 100).ToString() + ", " + (centerY + 100).ToString() + ")");
        }
    }
}
