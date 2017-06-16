using gEngine.Commands;
using gEngine.Project.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Project.Commands
{
    class OpenDBSourceCommand : CommandBinding
    {
        public OpenDBSourceCommand()
        {
            Command = ProjectCommands.OpenDBSourceCommand;
            CanExecute += OpenDBSourceCommand_CanExecute;
            Executed += OpenDBSourceCommand_Executed;
        }

        private void OpenDBSourceCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void OpenDBSourceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.Project == null)
                return;

            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Filter = "文本文件|*.Txt";
            if (OpenFileDialog.ShowDialog() == true)
            {
                pc.Project.OpenDBSource(OpenFileDialog.FileName);
            }

            e.Handled = true;
        }
    }
}
