using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace DrawingForm.Tests.UITest
{
    [TestClass()]
    public class DrawTest
    {
        Robot _robot;
        private string targetAppPath;
        private const string DRAWING_FORM = "DrawingForm";

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

        //DrawSnowman
        [TestMethod]
        public void DrawSnowman()
        {
            _robot.ClickButton("Ellipse");
            _robot.DragAndDrop("_canvas", -150, 25, 150, 325);
            _robot.ClickButton("Ellipse");
            _robot.DragAndDrop("_canvas", -100, -150, 100, 50);
            _robot.ClickButton("Ellipse");
            _robot.DragAndDrop("_canvas", -60, -100, -20, -60);
            _robot.ClickButton("Ellipse");
            _robot.DragAndDrop("_canvas", 20, -100, 60, -60);

            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop("_canvas", -120, -225, 120, -120);
            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop("_canvas", -150, -125, 150, -110);
            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop("_canvas", -20, -30, 20, 10);
            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop("_canvas", -160, -50, -150, 175);
            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop("_canvas", 150, -50, 160, 175);
            _robot.ClickButton("Clear");
            _robot.Sleep(1);
        }
    }
}
