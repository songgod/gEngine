using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Commands
{
    public static class StartCommands
    {
        static StartCommands()
        {
            UndoCommand = new RoutedUICommand("撤销", "UndoCommand", typeof(StartCommands));
            RedoCommand = new RoutedUICommand("重做", "RedoCommand", typeof(StartCommands));

            SelectObjectCommand = new RoutedUICommand("选择", "SelectObjectCommand", typeof(StartCommands));
        }

        public static RoutedCommand UndoCommand { get; set; }
        public static RoutedCommand RedoCommand { get; set; }

        public static RoutedCommand SelectObjectCommand { get; set; }
    }
}
