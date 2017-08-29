using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Commands
{
    public static class OperateViewCommands
    {
        
        static OperateViewCommands()
        {
            FullViewCommand = new RoutedUICommand("FullViewCommand", "FullViewCommand",typeof(OperateViewCommands));
            SetLayerVisibleCommand = new RoutedUICommand("SetLayerVisibleCommand", "SetLayerVisibleCommand", typeof(OperateViewCommands));
            SetLayerEditableCommand = new RoutedUICommand("SetLayerEditableCommand", "SetLayerEditableCommand", typeof(OperateViewCommands));
            SetLayerDeleteCommand = new RoutedUICommand("SetLayerDeleteCommand", "SetLayerDeleteCommand", typeof(OperateViewCommands));
            SetLayerOpacityCommand = new RoutedUICommand("SetLayerOpacityCommand", "SetLayerOpacityCommand", typeof(OperateViewCommands));
            SelectObjectCommand = new RoutedUICommand("SelectObjectCommand", "SelectObjectCommand", typeof(OperateViewCommands));
        }

        public static RoutedCommand FullViewCommand { get; set; }
        public static RoutedCommand SetLayerVisibleCommand { get; set; }
        public static RoutedCommand SetLayerEditableCommand { get; set; }

        public static RoutedCommand SetLayerDeleteCommand { get; set; }
        public static RoutedCommand SetLayerOpacityCommand { get; set; }
        public static RoutedCommand SelectObjectCommand { get; set; }
        
    }
}
