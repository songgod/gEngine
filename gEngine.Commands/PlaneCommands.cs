using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Commands
{
    public static class PlaneCommands
    {
        static PlaneCommands()
        {
            NewPlaneMapCommand = new RoutedUICommand("NewPlaneMapCommand", "NewPlaneMapCommand", typeof(PlaneCommands));
            NewPlaneLineCommand = new RoutedUICommand("NewPlaneLineCommand", "NewPlaneLineCommand", typeof(PlaneCommands));
        }

        public static RoutedCommand NewPlaneMapCommand { get; set; }

        public static RoutedCommand NewPlaneLineCommand { get; set; }
    }
}
