using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace DrawingForm.Tests.UITest
{
    [TestClass()]
    public class ButtonFunctionTest
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

        //DrawRectangle
        [TestMethod]
        public void DrawRectangle()
        {
            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop(-100, -100, 100, 100);
            _robot.Sleep(1);
        }

        //DrawEllipse
        [TestMethod]
        public void DrawEllipse()
        {
            _robot.ClickButton("Ellipse");
            _robot.DragAndDrop(-100, -100, 100, 100);
            _robot.Sleep(1);
        }

        //Clear
        [TestMethod]
        public void Clear()
        {
            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop(-100, -100, 100, 100);
            _robot.ClickButton("Ellipse");
            _robot.DragAndDrop(-100, -100, 100, 100);
            _robot.ClickButton("Clear");
            _robot.Sleep(1);
        }
    }
}