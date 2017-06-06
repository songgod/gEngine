using DevExpress.Xpf.Ribbon;
using gEngine.Commands;
using gEngine.Project.Controls;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace gEngine.Project.Commands
{
    /// <summary>
    /// 打开最近的项目文档
    /// </summary>
    class OpenRecentProjectCommand : CommandBinding
    {
        public OpenRecentProjectCommand()
        {
            Command = ProjectCommands.OpenRecentCommand;
            CanExecute += OpenRecentCommand_CanExecute;
            Executed += OpenRecentCommand_Executed;
        }

        private void OpenRecentCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void OpenRecentCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TextBlock TextBlock = e.Parameter as TextBlock;
            string url = TextBlock.Text;
            BackstageViewControl BackstageViewControl = FindParent.FindVisualParent<BackstageViewControl>(TextBlock);
            ApplicationMenuContentControl ApplicationControl = FindParent.FindVisualParent<ApplicationMenuContentControl>(BackstageViewControl);
            DXRibbonWindow DxWindow = ApplicationControl.DataContext as DXRibbonWindow;
            ProjectControl pc = FindChild.FindVisualChild<ProjectControl>(DxWindow, "prjctrl");

            if (pc == null || pc.Project == null)
                return;

            if (!pc.Project.IsExistProject(url))
            {
                MessageBox.Show("您选择的工区文件不存在，请选择其它工区文件！");
                return;
            }

            if (url.Equals(pc.Project.Url))
            {
                MessageBox.Show("您选择的工区已经打开！");
                return;
            }

            if (pc.Project.OpenMapList(url))
            {
                MessageBox.Show("打开成功");
                BackstageViewControl.SelectedTabIndex = 1;
            }
            else
            {
                MessageBox.Show("打开失败");
            }

            e.Handled = true;
        }
    }
}
