using gEngine.Commands;
using gEngine.Graph.Ge;
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
    public class NewBoundaryCommand : CommandBinding
    {
        public NewBoundaryCommand()
        {
            Command = BasicCommands.NewBoundaryCommand;
            Executed += NewBoundaryCommand_Executed;
            CanExecute += NewBoundaryCommand_CanExecute;
        }

        private void NewBoundaryCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            e.CanExecute = pc != null &&
                    pc.Project.GetActiveMap() != null &&
                    pc.Project.GetActiveMap().Layers.CurrentLayer != null &&
                    pc.Project.GetActiveMap().Layers.CurrentLayer.Type == "Basic";
            e.Handled = true;
        }

        private void NewBoundaryCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            ILayer layer = pc.Project.GetActiveMap().Layers.CurrentLayer;
            if (layer.Manipulator == "DrawBoundaryObjectManipulator")
                layer.Manipulator = null;
            else
                layer.Manipulator = "DrawBoundaryObjectManipulator";
        }
    }
}
