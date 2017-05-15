using gEngine.Commands;
using gEngine.Graph.Ge;
using gEngine.Graph.Interface;
using gEngine.Project.Controls;
using gEngine.Util.Ge.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Project.Ge.Basic.Commands
{
    public class NewCommonLayerCommand : CommandBinding
    {
        public NewCommonLayerCommand()
        {
            Command = BasicCommands.NewCommonLayerCommand;
            Executed += NewCommonLayerCommand_Executed;
            CanExecute += NewCommonLayerCommand_CanExecute;
        }

        private void NewCommonLayerCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.MapsControl==null)
                return;

            e.CanExecute = true;
            e.Handled = true;
        }

        private void NewCommonLayerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.MapsControl == null)
                return;

            IMap map = pc.Project.NewMap("Ge", "Common");

            CommonLayerCreator sc = new CommonLayerCreator();
            Layer layer = sc.Create();
            map.Layers.Add(layer);
        }
    }
}
