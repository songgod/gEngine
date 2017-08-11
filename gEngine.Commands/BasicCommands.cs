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
            NewCommonLayerCommand = new RoutedUICommand("NewCommonLayerCommand", "NewCommonLayerCommand", typeof(BasicCommands));
            NewLineCommand = new RoutedUICommand("NewLineCommand", "NewLineCommand", typeof(BasicCommands));
            NewBoundaryCommand = new RoutedUICommand("NewFillLayerCommand", "NewFillLayerCommand", typeof(BasicCommands));
            NewRectCommand = new RoutedUICommand("NewRectCommand", "NewRectCommand", typeof(BasicCommands));
            NewPolyLineCommand = new RoutedUICommand("新建折线", "NewPolyLineCommand", typeof(BasicCommands));
            NewBezierLineCommand = new RoutedUICommand("新建曲线", "NewBezierLineCommand", typeof(BasicCommands));
            NewCompressCommand = new RoutedUICommand("新建指北针", "NewCompressCommand", typeof(BasicCommands));
            NewScaleRuleCommand = new RoutedUICommand("NewScaleRuleCommand", "NewScaleRuleCommand", typeof(BasicCommands));
            PrintPngCommand = new RoutedUICommand("PrintPngCommand", "PrintPngCommand", typeof(BasicCommands));
        }
        public static RoutedCommand NewCommonLayerCommand { get; set; }

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
