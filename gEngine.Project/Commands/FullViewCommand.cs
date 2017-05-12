using gEngine.Commands;
using gEngine.Project.Controls;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Project.Commands
{
    public class FullViewCommand : CommandBinding
    {
        public FullViewCommand()
        {
            Command = OperateViewCommands.FullViewCommand;
            Executed += FullViewCommand_Executed;
            CanExecute += FullViewCommand_CanExecute;
        }

        private void FullViewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            e.CanExecute = pc!=null && pc.MapsControl.ActiveMapControl!=null;
            e.Handled = true;
        }

        private void FullViewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc != null && pc.MapsControl.ActiveMapControl!=null)
            {
                pc.MapsControl.ActiveMapControl.FullView();
            }
            e.Handled = true;
        }
    }
}
