using DevExpress.Xpf.Ribbon;
using gEngine.Commands;
using gEngine.Project.Controls;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace gEngine.Project.Commands
{
    class OpenMapCommand : CommandBinding
    {
        public OpenMapCommand()
        {
            Command = MapCommands.OpenMapCommand;
            CanExecute += OpenMapCommand_CanExecute;
            Executed += OpenMapCommand_Executed;
        }

        private void OpenMapCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void OpenMapCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TextBlock TextBlock = e.Parameter as TextBlock;
            string url = TextBlock.Text;
            ApplicationMenuContentControl ApplicationControl = FindParent.FindVisualParent<ApplicationMenuContentControl>(TextBlock);
            if (ApplicationControl == null)
                return;
            DXRibbonWindow DxWindow = ApplicationControl.DataContext as DXRibbonWindow;
            ProjectControl pc = FindChild.FindVisualChild<ProjectControl>(DxWindow, "prjctrl");
            pc.Project.OpenMap(url);
            ApplicationControl.Close();
            e.Handled = true;
        }
    }
}
