using gEngine.Commands;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Basic;
using gEngine.Graph.Interface;
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
    public class NewBasicLayerCommand : CommandBinding
    {
        public NewBasicLayerCommand()
        {
            Command = BasicCommands.NewBasicLayerCommand;
            Executed += NewCommonLayerCommand_Executed;
            CanExecute += NewCommonLayerCommand_CanExecute;
        }

        private void NewCommonLayerCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            e.CanExecute = pc!=null && pc.Project.GetActiveMap()!=null;
            e.Handled = true;
        }

        private void NewCommonLayerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            IMap map = pc.Project.GetActiveMap();

            BasicLayer layer = new BasicLayer();
            map.Layers.Add(layer);
        }
    }
}
