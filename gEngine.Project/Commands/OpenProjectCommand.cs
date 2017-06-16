using DevExpress.Xpf.Ribbon;
using gEngine.Commands;
using gEngine.Project.Controls;
using gEngine.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Project.Commands
{
    class OpenProjectCommand : CommandBinding
    {
        public OpenProjectCommand()
        {
            Command = ProjectCommands.OpenCommand;
            CanExecute += OpenCommand_CanExecute;
            Executed += OpenCommand_Executed;
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.Parameter as ProjectControl;
            if (pc == null || pc.Project == null)
                return;
            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Filter = "工程文件|*.Gepro";

            if (OpenFileDialog.ShowDialog() == true)
            {
                pc.Project = new Project();
                pc.Project.Open(OpenFileDialog.FileName);
                RecentProject.Write(OpenFileDialog.FileName);
            }
            else
            {
                MessageBox.Show("取消打开");
            }

            e.Handled = true;
        }
    }
}
