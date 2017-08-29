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
    public class SelectObjectCommand : CommandBinding
    {
        public SelectObjectCommand()
        {
            Command = OperateViewCommands.SelectObjectCommand;
            Executed += SelectObjectCommand_Executed;
            CanExecute += SelectObjectCommand_CanExecute;
        }

        private void SelectObjectCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            e.CanExecute = pc != null &&
                pc.Project != null && pc.Project.GetActiveMap() != null;
            e.Handled = true;
        }

        private void SelectObjectCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc.Project.GetActiveMap().Manipulator == "MapControlZoomPanSelect")
                pc.Project.GetActiveMap().Manipulator = "MapControlZoomPan";
            else
                pc.Project.GetActiveMap().Manipulator = "MapControlZoomPanSelect";
            e.Handled = true;
        }
    }
}
