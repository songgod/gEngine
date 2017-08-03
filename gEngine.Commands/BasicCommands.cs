﻿using System;
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
        }
        public static RoutedCommand NewCommonLayerCommand { get; set; }

        public static RoutedCommand NewLineCommand { get; set; }

        public static RoutedCommand NewBoundaryCommand { get; set; }
    }
}
