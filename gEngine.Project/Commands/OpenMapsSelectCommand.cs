using gEngine.Commands;
using gEngine.Project.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Project.Commands
{
    public class OpenMapsSelectCommand: CommandBinding
    {
        public OpenMapsSelectCommand()
        {
            Command = OperateViewCommands.OpenMapsSelectCommand;
            Executed += OpenMapsSelectCommand_Executed;
            CanExecute += OpenMapsSelectCommand_CanExecute;
        }

        private void OpenMapsSelectCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.Parameter as ProjectControl;
            if (pc == null)
                return;
            Project p = pc.Project;
            if (p == null)
                return;
            e.CanExecute = true;
            e.Handled = true;
        }

        private void OpenMapsSelectCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.Parameter as ProjectControl;
            if (pc == null)
                return;
            Project p = pc.Project;
            if (p == null)
                return;
            p.OpenMaps.CurrentIndex = pc.MapsControl.SelectedIndex;

            e.Handled = true;
        }
    }
}
