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
    class SaveAsProjectCommand : CommandBinding
    {
        public SaveAsProjectCommand()
        {
            Command = ProjectCommands.SaveAsCommand;
            CanExecute += SaveAsCommand_CanExecute;
            Executed += SaveAsCommand_Executed;
        }

        private void SaveAsCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.Project == null)
                return;
            if (pc.Project.Maps.Count == 0 || pc.Project.DBSource == null)
                return;
            e.CanExecute = true;
            e.Handled = true;
        }

        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.Project == null)
                return;
            if (pc.Project.Maps.Count == 0 || pc.Project.DBSource == null)
                return;

            SaveFileDialog SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.Filter = "工程文件|*.Gepro";
            if (SaveFileDialog.ShowDialog() == true)
            {
                if (pc.Project.SaveAs(SaveFileDialog.FileName))
                {
                    MessageBox.Show("保存成功！");
                }
                else
                {
                    MessageBox.Show("保存失败！");
                }
            }

            e.Handled = true;
        }
    }
}

