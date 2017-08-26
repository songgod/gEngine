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
    public class NewLineCommand: CommandBinding
    {
        public NewLineCommand()
        {
            Command = BasicCommands.NewLineCommand;
            Executed += NewLineCommand_Executed;
            CanExecute += NewLineCommand_CanExecute;
        }

        private void NewLineCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            e.CanExecute = pc != null &&
                pc.Project.GetActiveMap() != null &&
                pc.Project.GetActiveMap().Layers.CurrentLayer != null &&
                pc.Project.GetActiveMap().Layers.CurrentLayer.Type == "Basic";
            e.Handled = true;
        }

        private void NewLineCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            ILayer layer = pc.Project.GetActiveMap().Layers.CurrentLayer;
            if (layer.Manipulator == "DrawLineObjectManipulator")
                layer.Manipulator = null;
            else
                layer.Manipulator = "DrawLineObjectManipulator";
        }
    }
}
