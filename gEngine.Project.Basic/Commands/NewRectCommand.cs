using gEngine.Commands;
using gEngine.Graph.Interface;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Basic;
using gEngine.Project.Controls;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Project.Ge.Basic.Commands
{
    class NewRectCommand : CommandBinding
    {
        public NewRectCommand()
        {
            Command = BasicCommands.NewRectCommand;
            Executed += NewRectCommand_Executed;
            CanExecute += NewRectCommand_CanExecute;
        }

        private void NewRectCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            e.CanExecute = pc != null &&
                pc.Project.GetActiveMap() != null &&
                pc.Project.GetActiveMap().Layers.CurrentLayer != null &&
                pc.Project.GetActiveMap().Layers.CurrentLayer.Type == "Basic";
            e.Handled = true;
        }

        private void NewRectCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            ILayer layer = pc.Project.GetActiveMap().Layers.CurrentLayer;
            if (layer.Manipulator == "DrawRectObjectManipulator")
                layer.Manipulator = null;
            else
                layer.Manipulator = "DrawRectObjectManipulator";
        }
    }
}
