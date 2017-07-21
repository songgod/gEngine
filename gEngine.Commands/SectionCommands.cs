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
        }

        public static RoutedCommand NewSectionMapCommand { get; set; }

        public static RoutedCommand SaveTemplateCommand { get; set; }
    }
}
