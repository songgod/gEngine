using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Commands
{
    public static class BasicCommands
    {
        static BasicCommands()
        {
            NewBasicLayerCommand = new RoutedUICommand("NewBasicLayerCommand", "NewBasicLayerCommand", typeof(BasicCommands));
            NewLineCommand = new RoutedUICommand("NewLineCommand", "NewLineCommand", typeof(BasicCommands));
            NewBoundaryCommand = new RoutedUICommand("NewFillLayerCommand", "NewFillLayerCommand", typeof(BasicCommands));
            NewRectCommand = new RoutedUICommand("NewRectCommand", "NewRectCommand", typeof(BasicCommands));
            NewPolyLineCommand = new RoutedUICommand("NewPolyLineCommand", "NewPolyLineCommand", typeof(BasicCommands));
            NewBezierLineCommand = new RoutedUICommand("NewBezierLineCommand", "NewBezierLineCommand", typeof(BasicCommands));
            NewCompressCommand = new RoutedUICommand("NewCompressCommand", "NewCompressCommand", typeof(BasicCommands));
            NewScaleRuleCommand = new RoutedUICommand("NewScaleRuleCommand", "NewScaleRuleCommand", typeof(BasicCommands));
            PrintPngCommand = new RoutedUICommand("PrintPngCommand", "PrintPngCommand", typeof(BasicCommands));
        }
        public static RoutedCommand NewBasicLayerCommand { get; set; }

        public static RoutedCommand NewLineCommand { get; set; }

        public static RoutedCommand NewBoundaryCommand { get; set; }

        public static RoutedCommand NewRectCommand { get; set; }

        public static RoutedCommand NewScaleRuleCommand { get; set; }

        public static RoutedCommand PrintPngCommand { get; set; }

        public static RoutedCommand NewPolyLineCommand { get; set; }

        public static RoutedCommand NewBezierLineCommand { get; set; }

        public static RoutedCommand NewCompressCommand { get; set; }
    }
}
