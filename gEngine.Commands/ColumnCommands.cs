using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Commands
{
    public static class ColumnCommands
    {
        static ColumnCommands()
        {
            SaveTemplateCommand = new RoutedUICommand("保存模板", "SaveTemplateCommand", typeof(ColumnCommands));
            ChangeTemplateCommand = new RoutedUICommand("更换模板", "ChangeTemplateCommand", typeof(ColumnCommands));
        }

        public static RoutedCommand SaveTemplateCommand { get; set; }
        public static RoutedCommand ChangeTemplateCommand { get; set; }
    }
}
