using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Commands
{
    public static class UndoRedoCommands
    {
        static UndoRedoCommands()
        {
            UndoCommand = new RoutedUICommand("UndoCommand", "UndoCommand", typeof(UndoRedoCommands));
            RedoCommand = new RoutedUICommand("RedoCommand", "RedoCommand", typeof(UndoRedoCommands));
        }

        public static RoutedCommand UndoCommand { get; set; }
        public static RoutedCommand RedoCommand { get; set; }
    }
}
