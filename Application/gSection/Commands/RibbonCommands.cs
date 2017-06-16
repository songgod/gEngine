using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gSection.Commands
{
    public static class RibbonCommands
    {
        static RibbonCommands()
        {
            SelectObjectCommand = new RoutedUICommand("SelectObjectCommand", "SelectObjectCommand", typeof(RibbonCommands));
        }

        public static RoutedCommand SelectObjectCommand { get; set; }
    }
}
