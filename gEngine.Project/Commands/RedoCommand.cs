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

            e.CanExecute = pc!=null &&
                pc.Project!=null &&
                pc.Project.GetActiveMap()!=null &&
                pc.Project.GetActiveMap().UndoRedoCmdMgr.CanRedo();
            e.Handled = true;
        }

        private void RedoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            pc.Project.GetActiveMap().UndoRedoCmdMgr.Redo();
            e.Handled = true;
        }
    }
}
