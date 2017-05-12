using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Commands
{
    public static class SectionEditCommands
    {
        static SectionEditCommands()
        {
            EditLineCommand = new RoutedUICommand("EditLineCommand", "EditLineCommand", typeof(SectionEditCommands));
            EraseFaceCommand = new RoutedUICommand("EraseFaceCommand", "EraseFaceCommand", typeof(SectionEditCommands));
            EraseLineCommand = new RoutedUICommand("EraseLineCommand", "EraseLineCommand", typeof(SectionEditCommands));
            NewCurveCommand = new RoutedUICommand("NewCurveCommand", "NewCurveCommand", typeof(SectionEditCommands));
            NewFaultCommand = new RoutedUICommand("NewFaultCommand", "NewFaultCommand", typeof(SectionEditCommands));
            NewLineCommand = new RoutedUICommand("NewLineCommand", "NewLineCommand", typeof(SectionEditCommands));
            ReplaceLineCommand = new RoutedUICommand("ReplaceLineCommand", "ReplaceLineCommand", typeof(SectionEditCommands));
        }

        public static RoutedCommand EditLineCommand { get; set; }
        public static RoutedCommand EraseFaceCommand { get; set; }
        public static RoutedCommand EraseLineCommand { get; set; }
        public static RoutedCommand NewCurveCommand { get; set; }
        public static RoutedCommand NewFaultCommand { get; set; }
        public static RoutedCommand NewLineCommand { get; set; }
        public static RoutedCommand ReplaceLineCommand { get; set; }
    }
}
