using gEngine.Commands;
using gEngine.Graph.Interface;
using gEngine.Project.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace gEngine.Project.Commands
{
    class SaveProjectCommand : CommandBinding
    {
        public SaveProjectCommand()
        {
            Command = ProjectCommands.SaveCommand;
            CanExecute += SaveCommand_CanExecute;
            Executed += SaveCommand_Executed;
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.Project == null)
                return;
            if (pc.Project.Maps.Count == 0 || pc.Project.DBSource == null)
                return;
            e.CanExecute = true;
            e.Handled = true;
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.Project == null)
                return;
            if (pc.Project.Maps.Count == 0 || pc.Project.DBSource == null)
                return;

            if (pc.Project.Save())
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存失败！");
            }

            e.Handled = true;
        }
    }
}
