using gEngine.Commands;
using gEngine.Project.Controls;
using gEngine.Util;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Project.Commands
{
    public class RedoCommand : CommandBinding
    {
        public RedoCommand()
        {
            Command = StartCommands.RedoCommand;
            Executed += RedoCommand_Executed;
            CanExecute += RedoCommand_CanExecute; ;
        }

        private void RedoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null)
                return;

            MapsControl tc = pc.MapsControl;
            MapControl mc = tc.ActiveMapControl;
            if (mc == null)
                return;

            e.CanExecute = mc.UndoRedoCommandManager.RedoCommands.Count > 0;
            e.Handled = true;
        }

        private void RedoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null)
                return;

            MapsControl tc = pc.MapsControl;
            MapControl mc = tc.ActiveMapControl;
            if (mc == null)
                return;

            mc.UndoRedoCommandManager.Redo();
            e.Handled = true;
        }
    }
}
