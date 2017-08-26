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
    public class NewScaleRuleCommand : CommandBinding
    {
        public NewScaleRuleCommand()
        {
            Command = BasicCommands.NewScaleRuleCommand;
            Executed += NewScaleRuleCommand_Executed;
            CanExecute += NewScaleRuleCommand_CanExecute;
        }

        private void NewScaleRuleCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            e.CanExecute = pc != null &&
                pc.Project.GetActiveMap() != null &&
                pc.Project.GetActiveMap().Layers.CurrentLayer != null &&
                pc.Project.GetActiveMap().Layers.CurrentLayer.Type == "Basic";
            e.Handled = true;
        }

        private void NewScaleRuleCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            ILayer layer = pc.Project.GetActiveMap().Layers.CurrentLayer;
            if (layer.Manipulator == "DrawScaleRuleObjectManipulator")
                layer.Manipulator = null;
            else
                layer.Manipulator = "DrawScaleRuleObjectManipulator";
        }
    }
}
