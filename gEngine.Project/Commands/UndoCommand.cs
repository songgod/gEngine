using gEngine.Commands;
using gEngine.Project.Controls;
using gEngine.Util;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Project.Commands
{
    public class UndoCommand : CommandBinding
    {
        public UndoCommand()
        {
            Command = StartCommands.UndoCommand;
            Executed += UndoCommand_Executed;
            CanExecute += UndoCommand_CanExecute;
        }

        private void UndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null)
                return;

            MapsControl tc = pc.MapsControl;
            MapControl mc = tc.ActiveMapControl;
            if (mc == null)
                return;

            e.CanExecute = mc.UndoRedoCommandManager.UndoCommands.Count > 0;
            e.Handled = true;
        }

        private void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null)
                return;

            MapsControl tc = pc.MapsControl;
            MapControl mc = tc.ActiveMapControl;
            if (mc == null)
                return;

            mc.UndoRedoCommandManager.Undo();
            e.Handled = true;
        }
    }
}
