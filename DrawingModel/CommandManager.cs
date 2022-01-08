using System;
using System.Collections.Generic;

namespace DrawingModel
{
    public class CommandManager
    {
        Stack<ICommand> _undo = new Stack<ICommand>();
        Stack<ICommand> _redo = new Stack<ICommand>();
        const string UNDO_EXCEPTION = "Cannot Undo exception\n";
        const string REDO_EXCEPTION = "Cannot Redo exception\n";
        //Execute
        public void Execute(ICommand command)
        {
            command.Execute();
            // push command 進 undo stack
            _undo.Push(command);
            // 清除redo stack
            _redo.Clear();
        }

        //Undo
        public void Undo()
        {
            if (_undo.Count <= 0)
                throw new Exception(UNDO_EXCEPTION);
            ICommand command = _undo.Pop();
            _redo.Push(command);
            command.ExecuteBack();
        }

        //Redo
        public void Redo()
        {
            if (_redo.Count <= 0)
                throw new Exception(REDO_EXCEPTION);
            ICommand command = _redo.Pop();
            _undo.Push(command);
            command.Execute();
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

        //AddCommand
        public void AddCommand(ICommand command)
        {
            _undo.Push(command);
            _redo.Clear();
        }

        //ClearAllCommand
        public void ClearAllCommand()
        {
            _redo.Clear();
            _undo.Clear();
        }
    }
}
