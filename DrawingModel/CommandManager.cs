using System;
using System.Collections.Generic;

namespace DrawingModel
{
    class CommandManager
    {
        Stack<ICommand> _undo = new Stack<ICommand>();
        Stack<ICommand> _redo = new Stack<ICommand>();
        const string UNDO_EXCEPTION = "Cannot Undo exception\n";
        const string REDO_EXCEPTION = "Cannot Redo exception\n";

        //Execute
        public void Execute(ICommand commandManager)
        {
            commandManager.Execute();
            // push command 進 undo stack
            _undo.Push(commandManager);
            // 清除redo stack
            _redo.Clear();
        }

        //Undo
        public void Undo()
        {
            if (_undo.Count <= 0)
                throw new Exception(UNDO_EXCEPTION);
            ICommand commandManager = _undo.Pop();
            if (commandManager.GetShape != ShapeFlag.DotRectangle)
            {
                _redo.Push(commandManager);
            }
            commandManager.ExecuteBack();
        }

        //Redo
        public void Redo()
        {
            if (_redo.Count <= 0)
                throw new Exception(REDO_EXCEPTION);
            ICommand commandManager = _redo.Pop();
            _undo.Push(commandManager);
            commandManager.Execute();
        }

        public bool IsRedoEnabled
        {
            get
            {
                return _redo.Count != 0;
            }
        }

        public bool IsUndoEnabled
        {
            get
            {
                return _undo.Count != 0;
            }
        }
    }
}
