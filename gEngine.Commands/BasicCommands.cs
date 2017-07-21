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
            NewLineLayerCommand = new RoutedUICommand("NewLineLayerCommand", "NewLineLayerCommand", typeof(BasicCommands));
        }
        public static RoutedCommand NewCommonLayerCommand { get; set; }

        public static RoutedCommand NewLineLayerCommand { get; set; }
    }
}
