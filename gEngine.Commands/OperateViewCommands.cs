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
            OpenMapsSelectCommand = new RoutedUICommand("OpenMapsSelectCommand", "OpenMapsSelectCommand", typeof(OperateViewCommands));
        }

        public static RoutedCommand FullViewCommand { get; set; }
        public static RoutedCommand SetLayerVisibleCommand { get; set; }
        public static RoutedCommand SetLayerEditableCommand { get; set; }
        /// <summary>
        /// 切换Map命令
        /// </summary>
        public static RoutedCommand OpenMapsSelectCommand { get; set; }
    }
}
