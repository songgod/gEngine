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
            List<object> LsPara = e.Parameter as List<object>;

            if (LsPara == null)
                return;

            if (LsPara[0] == null || LsPara[1] == null || LsPara[2] == null)
                return;

            ProjectControl pc = LsPara[0] as ProjectControl;
            RecentProjectControl rpc = LsPara[1] as RecentProjectControl;
            TextBlock TextBlock = LsPara[2] as TextBlock;
            string ProjectFile = TextBlock.Text;

            if (pc == null || pc.Project == null)
                return;

            if (rpc == null || rpc.Project == null)
                return;

            if (!rpc.Project.IsExistProject(ProjectFile))
            {
                MessageBox.Show("您选择的工区文件不存在，请选择其它工区文件！");
                return;
            }

            pc.Project = new Project();
            pc.Project.OpenDBSource(@"D:\gSectionData.Txt");
            pc.Project.Open(ProjectFile);
            e.Handled = true;
        }
    }
}
