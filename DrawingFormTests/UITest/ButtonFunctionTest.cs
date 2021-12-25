using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace DrawingForm.Tests
{
    [TestClass()]
    public class ButtonFunctionTest
    {
        Robot _robot;
        private string targetAppPath;
        private const string DRAWING_FORM = "DrawingForm";
        const int centerX = 674;
        const int centerY = 363;

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

        //DrawRectangle
        [TestMethod]
        public void DrawRectangle()
        {
            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop(-100, -100, 100, 100);
            _robot.ClickPosition(0, 0);
            _robot.AssertLabelText("_label", "Selected : Rectangle (" + (centerX - 100).ToString() + ", " + (centerY - 100).ToString() + ", " + (centerX + 100).ToString() + ", " + (centerY + 100).ToString() + ")");
        }

        //DrawEllipse
        [TestMethod]
        public void DrawEllipse()
        {
            _robot.ClickButton("Ellipse");
            _robot.DragAndDrop(-100, -100, 100, 100);
            _robot.ClickPosition(0, 0);
            _robot.AssertLabelText("_label", "Selected : Ellipse (" + (centerX - 100).ToString() + ", " + (centerY - 100).ToString() + ", " + (centerX + 100).ToString() + ", " + (centerY + 100).ToString() + ")");
        }

        //DrawLine
        [TestMethod]
        public void DrawLine()
        {
            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop(-150, -150, -50, -50);
            _robot.ClickButton("Ellipse");
            _robot.DragAndDrop(50, 50, 150, 150);
            _robot.ClickButton("Line");
            _robot.DragAndDrop(-100, -100, 100, 100);
            _robot.ClickPosition(0, 0);
            _robot.AssertLabelText("_label", "Selected : Line (" + (centerX - 100).ToString() + ", " + (centerY - 100).ToString() + ", " + (centerX + 100).ToString() + ", " + (centerY + 100).ToString() + ")");
        }

        //Clear
        [TestMethod]
        public void Clear()
        {
            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop(-100, -100, 100, 100);
            _robot.ClickButton("Ellipse");
            _robot.DragAndDrop(-100, -100, 100, 100);
            _robot.ClickButton("Line");
            _robot.DragAndDrop(-50, -50, 50, 50);
            _robot.ClickButton("Clear");
            _robot.ClickPosition(0, 0);
            _robot.AssertLabelText("_label", "Selected : None");
        }
    }
}