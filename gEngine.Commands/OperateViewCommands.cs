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
            FullViewCommand = new RoutedUICommand("FullViewCommand", "FullViewCommand", typeof(OperateViewCommands));
        }

        public static RoutedCommand FullViewCommand { get; set; }

    }
}
