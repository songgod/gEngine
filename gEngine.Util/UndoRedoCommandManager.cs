using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace gEngine.Util
{
    public class UndoRedoCommandManager
    {
        public Stack<IUndoRedoCommand> UndoCommands { get; set; }
        public Stack<IUndoRedoCommand> RedoCommands { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UndoRedoCommandManager()
        {
            UndoCommands = new Stack<IUndoRedoCommand>();
            RedoCommands = new Stack<IUndoRedoCommand>();
        }


        public void AddCommand(IUndoRedoCommand command)
        {
            if (command == null) return;
            if (command is IUndoRedoCommand)
            {
                UndoCommands.Push(command);
                RedoCommands.Clear();
            }
        }

        public void Undo()
        {
            if (UndoCommands.Count > 0)
            {
                IUndoRedoCommand command = UndoCommands.Pop();
                command.Undo();
                RedoCommands.Push(command);
            }
        }

        public void Redo()
        {
            if (RedoCommands.Count > 0)
            {
                IUndoRedoCommand command = RedoCommands.Pop();
                command.Redo();
                UndoCommands.Push(command);
            }
        }

        public void Clear()
        {
            ClearUndoCommands();
            ClearRedoCommands();
        }

        public void ClearUndoCommands()
        {
            UndoCommands.Clear();
        }

        public void ClearRedoCommands()
        {
            RedoCommands.Clear();
        }
    }
}
