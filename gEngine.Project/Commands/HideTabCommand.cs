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
    public class HideTabCommand : CommandBinding
    {
        public HideTabCommand()
        {
            Command = OperateViewCommands.HideTabCommand;
            Executed += HideTabCommand_Executed;
            CanExecute += HideTabCommand_CanExecute;
        }

        private void HideTabCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void HideTabCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.Parameter as ProjectControl;
            if (pc == null)
                return;
            Project p = pc.Project;
            if (p == null)
                return;

            for (int i = 0; i < pc.MapsControl.MapControlCount; i++)
            {
                View.MapControl mc = pc.MapsControl.GetMapControl(i);
                if (!mc.IsVisible)
                {
                    p.Maps.RemoveAt(i);
                }
            }

            e.Handled = true;
        }
    }
}
