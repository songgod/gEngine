using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Util
{
    public class CommandManager
    {
        private Stack<ICommand> redostack;
        private Stack<ICommand> undostack;

        public CommandManager()
        {
            redostack = new Stack<ICommand>();
            undostack = new Stack<ICommand>();
        }

        public int MaxCount { get; set; }
        

        void AddCommand(ICommand command)
        {
            if (command == null)
                return;

            command.Do();
            undostack.Push(command);
            redostack.Clear();
        }

        void Clear()
        {
            redostack.Clear();
            undostack.Clear();
        }

        void Undo()
        {
            if (undostack.Count == 0)
                return;
            ICommand cmd = undostack.Pop();
            redostack.Push(cmd);
            cmd.UnDo();
        }

        void Redo()
        {
            if (redostack.Count == 0)
                return;
            ICommand cmd = redostack.Pop();
            undostack.Push(cmd);
            cmd.Do();
        }
    }
}
