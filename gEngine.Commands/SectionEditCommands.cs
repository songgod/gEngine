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
            NewCurveFaultCommand = new RoutedUICommand("NewCurveFaultCommand", "NewCurveFaultCommand", typeof(SectionEditCommands));
            NewCurveSandCommand = new RoutedUICommand("NewCurveSandCommand", "NewCurveSandCommand", typeof(SectionEditCommands));
            NewCurveStratumCommand = new RoutedUICommand("NewCurveStratumCommand", "NewCurveStratumCommand", typeof(SectionEditCommands));
            NewLineFaultCommand = new RoutedUICommand("NewLineFaultCommand", "NewLineFaultCommand", typeof(SectionEditCommands));
            NewLineSandCommand = new RoutedUICommand("NewLineSandCommand", "NewLineSandCommand", typeof(SectionEditCommands));
            NewLineStratumCommand = new RoutedUICommand("NewLineStratumCommand", "NewLineStratumCommand", typeof(SectionEditCommands));
            ReplaceLineCommand = new RoutedUICommand("ReplaceLineCommand", "ReplaceLineCommand", typeof(SectionEditCommands));
            NewTrendLineCommand = new RoutedUICommand("NewTrendLineCommand", "NewTrendLineCommand", typeof(SectionEditCommands));
            NewBoundaryCommand = new RoutedUICommand("NewBoundaryCommand", "NewBoundaryCommand", typeof(SectionEditCommands));
        }

        public static RoutedCommand EditLineCommand { get; set; }
        public static RoutedCommand EraseFaceCommand { get; set; }
        public static RoutedCommand EraseLineCommand { get; set; }
        public static RoutedCommand NewCurveFaultCommand { get; set; }
        public static RoutedCommand NewCurveSandCommand { get; set; }
        public static RoutedCommand NewCurveStratumCommand { get; set; }
        public static RoutedCommand NewLineFaultCommand { get; set; }
        public static RoutedCommand NewLineSandCommand { get; set; }
        public static RoutedCommand NewLineStratumCommand { get; set; }
        public static RoutedCommand ReplaceLineCommand { get; set; }
        public static RoutedCommand NewTrendLineCommand { get; set; }
        public static RoutedCommand NewBoundaryCommand { get; set; }

    }
}
