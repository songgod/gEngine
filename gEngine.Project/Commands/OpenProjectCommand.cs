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
            BackstageViewControl BackstageViewControl = e.Parameter as BackstageViewControl;
            ApplicationMenuContentControl ApplicationControl = FindParent.FindVisualParent<ApplicationMenuContentControl>(BackstageViewControl);
            DXRibbonWindow DxWindow = ApplicationControl.DataContext as DXRibbonWindow;
            ProjectControl pc = FindChild.FindVisualChild<ProjectControl>(DxWindow, "prjctrl");
           
            if (pc == null || pc.Project == null)
                return;

            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Filter = "工程文件|*.Gepro";

            if (OpenFileDialog.ShowDialog() == true)
            {
                if (OpenFileDialog.FileName.Equals(pc.Project.Url))
                {
                    MessageBox.Show("您选择的工区已经打开！");
                    return;
                }

                if (pc.Project.OpenMapList(OpenFileDialog.FileName))
                {
                    MessageBox.Show("打开成功");
                    pc.Project.WriteRecentProject(OpenFileDialog.FileName);
                    BackstageViewControl.SelectedTabIndex = 1;
                }
                else
                {
                    MessageBox.Show("打开失败");
                }
            }
            else
            {
                MessageBox.Show("取消打开");
            }

            e.Handled = true;
        }
    }
}
