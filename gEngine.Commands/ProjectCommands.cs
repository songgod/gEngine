using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Commands
{
    public static class ProjectCommands
    {
        static ProjectCommands()
        {
            OpenCommand = new RoutedUICommand("打开工程", "OpenCommand", typeof(ProjectCommands));
            SaveCommand = new RoutedUICommand("保存工程", "SaveCommand", typeof(ProjectCommands));
            SaveAsCommand = new RoutedUICommand("另存为工程", "SaveAsCommand", typeof(ProjectCommands));
            OpenRecentCommand = new RoutedUICommand("打开最近的工程文件", "OpenRecentCommand", typeof(ProjectCommands));
            OpenDBSourceCommand = new RoutedUICommand("设置数据源", "OpenDBSourceCommand", typeof(ProjectCommands));
        }

        public static RoutedCommand OpenCommand { get; set; }
        public static RoutedCommand SaveCommand { get; set; }
        public static RoutedCommand SaveAsCommand { get; set; }
        public static RoutedCommand OpenRecentCommand { get; set; }
        public static RoutedCommand OpenDBSourceCommand { get; set; }
    }
}
