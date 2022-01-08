using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class CommandManagerTests
    {
        Model model;
        Rectangle rectangle;
        DrawCommand drawCommand;
        CommandManager commandManager;

        //Initialize
        [TestInitialize()]
        public void SetUp()
        {
            model = new Model();
            rectangle = new Rectangle();
            drawCommand = new DrawCommand(model, rectangle);
            commandManager = new CommandManager();
        }

        //ExecuteTest
        [TestMethod()]
        public void ExecuteTest()
        {
            commandManager.Execute(drawCommand);
            Assert.AreEqual(1, model.Shapes.Count);
            Assert.AreEqual(ShapeFlag.Rectangle, model.Shapes[0].ShapeFlag);
            Assert.IsTrue(commandManager.IsUndoEnabled);
            Assert.IsFalse(commandManager.IsRedoEnabled);
        }

        //UndoTestSuccess
        [TestMethod()]
        public void UndoTestSuccess()
        {
            commandManager.Execute(drawCommand);
            commandManager.Undo();
            Assert.AreEqual(0, model.Shapes.Count);
            Assert.IsFalse(commandManager.IsUndoEnabled);
            Assert.IsTrue(commandManager.IsRedoEnabled);
        }

        //UndoTestFail
        [TestMethod()]
        public void UndoTestFail()
        {
            String _exception;
            _exception = "";
            try
            {
                commandManager.Undo();
            }
            catch (Exception exception)
            {
                _exception = exception.Message;
            }
            Assert.AreEqual("Cannot Undo exception\n", _exception);
        }

        //RedoTestSuccess
        [TestMethod()]
        public void RedoTestSuccess()
        {
            commandManager.Execute(drawCommand);
            commandManager.Undo();
            commandManager.Redo();
            Assert.AreEqual(1, model.Shapes.Count);
            Assert.AreEqual(ShapeFlag.Rectangle, model.Shapes[0].ShapeFlag);
            Assert.IsTrue(commandManager.IsUndoEnabled);
            Assert.IsFalse(commandManager.IsRedoEnabled);
        }

        //RedoTestFail
        [TestMethod()]
        public void RedoTestFail()
        {
            String _exception;
            _exception = "";
            try
            {
                commandManager.Redo();
            }
            catch (Exception exception)
            {
                _exception = exception.Message;
            }
            Assert.AreEqual("Cannot Redo exception\n", _exception);
        }
    }
}