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
        public static FullViewCommand FullViewCommand { get; set; }
        public static ScaleRuleCommand ScaleRuleCommand { get; set; }
        static CommandsOperateView()
        {
            FullViewCommand = new FullViewCommand();
            ScaleRuleCommand = new ScaleRuleCommand();
        }

        public static RoutedCommand FullViewCommand { get; set; }

    }
}
