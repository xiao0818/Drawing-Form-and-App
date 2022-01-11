using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace DrawingForm.Tests
{
    [TestClass()]
    public class DrawTest
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

        //Draw
        [TestMethod]
        public void DrawingTest()
        {
            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop(-100, 100, 100, 300);
            _robot.ClickPosition(0, 200);
            //_robot.AssertLabelText("_label", "Selected : Rectangle (" + (centerX - 100).ToString() + ", " + (centerY + 100).ToString() + ", " + (centerX + 100).ToString() + ", " + (centerY + 300).ToString() + ")");

            _robot.ClickButton("Ellipse");
            _robot.DragAndDrop(-100, -200, 100, 0);
            _robot.ClickPosition(0, -100);
            _robot.DragAndDrop(0, -100, 0, -200);
            _robot.ClickPosition(0, -200);
            //_robot.AssertLabelText("_label", "Selected : Ellipse (" + (centerX - 100).ToString() + ", " + (centerY - 300).ToString() + ", " + (centerX + 100).ToString() + ", " + (centerY - 100).ToString() + ")");

            _robot.ClickButton("Line");
            _robot.DragAndDrop(0, -200, 0, 200);
            _robot.ClickButton("Undo");
            _robot.ClickButton("Redo");
            _robot.ClickPosition(0, 0);
            //_robot.AssertLabelText("_label", "Selected : Line (" + (centerX).ToString() + ", " + (centerY - 200).ToString() + ", " + (centerX).ToString() + ", " + (centerY + 200).ToString() + ")");

            _robot.ClickButton("Ellipse");
            _robot.DragAndDrop(-300, -200, -100, 0);
            _robot.ClickPosition(-200, -100);
            //_robot.AssertLabelText("_label", "Selected : Ellipse (" + (centerX - 300).ToString() + ", " + (centerY - 200).ToString() + ", " + (centerX - 100).ToString() + ", " + (centerY).ToString() + ")");

            _robot.ClickButton("Line");
            _robot.DragAndDrop(-200, -100, 0, 200);
            _robot.ClickPosition(-100, 50);
            //_robot.AssertLabelText("_label", "Selected : Line (" + (centerX - 200).ToString() + ", " + (centerY - 100).ToString() + ", " + (centerX).ToString() + ", " + (centerY + 200).ToString() + ")");

            _robot.ClickButton("Ellipse");
            _robot.DragAndDrop(100, -200, 300, 0);
            _robot.ClickPosition(200, -100);
            //_robot.AssertLabelText("_label", "Selected : Ellipse (" + (centerX + 100).ToString() + ", " + (centerY - 200).ToString() + ", " + (centerX + 300).ToString() + ", " + (centerY).ToString() + ")");

            _robot.ClickButton("Line");
            _robot.DragAndDrop(200, -100, 0, 200);
            _robot.ClickPosition(100, 50);
            //_robot.AssertLabelText("_label", "Selected : Line (" + (centerX + 200).ToString() + ", " + (centerY - 100).ToString() + ", " + (centerX + 0).ToString() + ", " + (centerY + 200).ToString() + ")");

            _robot.ClickButton("Save");
            _robot.Sleep(15);
            _robot.ClickButton("Load");
            _robot.Sleep(15);
            _robot.ClickButton("Clear");
            _robot.ClickPosition(0, 0);
            _robot.AssertLabelText("_label", "Selected : None");
        }
    }
}