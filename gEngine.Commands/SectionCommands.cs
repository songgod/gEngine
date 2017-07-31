using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Commands
{
    public static class SectionCommands
    {
        static SectionCommands()
        {
            NewSectionMapCommand = new RoutedUICommand("NewSectionMapCommand", "NewSectionMapCommand", typeof(SectionCommands));
            SaveTemplateCommand = new RoutedUICommand("保存模板", "SaveTemplateCommand", typeof(SectionCommands));
            ChangeTemplateCommand = new RoutedUICommand("更换模板", "ChangeTemplateCommand", typeof(SectionCommands));
        }

        public static RoutedCommand NewSectionMapCommand { get; set; }

        public static RoutedCommand SaveTemplateCommand { get; set; }
        public static RoutedCommand ChangeTemplateCommand { get; set; }
    }
}
