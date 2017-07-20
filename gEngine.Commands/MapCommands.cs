using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Commands
{
    public static class MapCommands
    {
        static MapCommands()
        {
            OpenMapCommand = new RoutedUICommand("打开图件", "OpenMapCommand", typeof(MapCommands));
            DeleteMapCommand = new RoutedUICommand("删除图件", "DeleteMapCommand", typeof(MapCommands));
        }

        public static RoutedCommand OpenMapCommand { get; set; }
        public static RoutedCommand DeleteMapCommand { get; set; }
    }
}
