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
        private static UndoRedoCommandManager _instance;
        private static readonly object syn = new object();

        public Dictionary<UIElement, Stack<IUndoRedoCommand>> DicUndoCommands { get; set; }
        public Dictionary<UIElement, Stack<IUndoRedoCommand>> DicRedoCommands { get; set; }

        /// <summary>
        /// 把类的构造函数访问权限设置为private，则该类不能在外界被new了
        /// </summary>
        private UndoRedoCommandManager()
        {
            DicUndoCommands = new Dictionary<UIElement, Stack<IUndoRedoCommand>>();
            DicRedoCommands = new Dictionary<UIElement, Stack<IUndoRedoCommand>>();
        }

        public static UndoRedoCommandManager CreateInstance()
        {
            if (_instance == null)
            {
                lock (syn)
                {
                    if (_instance == null)
                    {
                        _instance = new UndoRedoCommandManager();
                    }
                }
            }
            return _instance;
        }

        public void AddCommand(UIElement mc, IUndoRedoCommand command)
        {
            if (command == null) return;
            if (command is IUndoRedoCommand)
            {
                if (!DicUndoCommands.IsContainsKey(mc))
                {
                    DicUndoCommands.Add(mc, new Stack<IUndoRedoCommand>());
                }
                if (!DicRedoCommands.IsContainsKey(mc))
                {
                    DicRedoCommands.Add(mc, new Stack<IUndoRedoCommand>());
                }
                DicUndoCommands[mc].Push(command);
                DicRedoCommands[mc].Clear();
            }
        }

        public void Undo(UIElement mc)
        {
            if (!DicUndoCommands.IsContainsKey(mc))
                return;
            if (DicUndoCommands[mc].Count > 0)
            {
                IUndoRedoCommand command = DicUndoCommands[mc].Pop();
                command.Undo();
                DicRedoCommands[mc].Push(command);
            }
        }

        public void Redo(UIElement mc)
        {
            if (!DicRedoCommands.IsContainsKey(mc))
                return;
            if (DicRedoCommands[mc].Count > 0)
            {
                IUndoRedoCommand command = DicRedoCommands[mc].Pop();
                command.Redo();
                DicUndoCommands[mc].Push(command);
            }
        }

        public void Clear(UIElement mc)
        {
            ClearUndoCommands(mc);
            ClearRedoCommands(mc);
        }

        public void ClearUndoCommands(UIElement mc)
        {
            if (DicUndoCommands.IsContainsKey(mc))
            {
                DicUndoCommands[mc].Clear();
            }
        }

        public void ClearRedoCommands(UIElement mc)
        {
            if (DicRedoCommands.IsContainsKey(mc))
            {
                DicRedoCommands[mc].Clear();
            }
        }

    }
}
