using System;
using System.Collections.Generic;

namespace DrawingModel
{
    class CommandManager
    {
        Stack<ICommand> undo = new Stack<ICommand>();
        Stack<ICommand> redo = new Stack<ICommand>();

        //Execute
        public void Execute(ICommand cmd)
        {
            cmd.Execute();
            // push command 進 undo stack
            undo.Push(cmd);
            // 清除redo stack
            redo.Clear();
        }

        //Undo
        public void Undo()
        {
            if (undo.Count <= 0)
                throw new Exception("Cannot Undo exception\n");
            ICommand cmd = undo.Pop();
            if(cmd.GetShape != ShapeFlag.DotRectangle)
            {
                redo.Push(cmd);
            }
            cmd.UnExecute();
        }

        //Redo
        public void Redo()
        {
            if (redo.Count <= 0)
                throw new Exception("Cannot Redo exception\n");
            ICommand cmd = redo.Pop();
            undo.Push(cmd);
            cmd.Execute();
        }

        public bool IsRedoEnabled
        {
            get
            {
                return redo.Count != 0;
            }
        }

        public bool IsUndoEnabled
        {
            get
            {
                return undo.Count != 0;
            }
        }
    }
}
