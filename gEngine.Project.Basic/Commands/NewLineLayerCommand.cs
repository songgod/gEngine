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
    public class NewLineLayerCommand: CommandBinding
    {
        public NewLineLayerCommand()
        {
            Command = BasicCommands.NewLineLayerCommand;
            Executed += NewLineLayerCommand_Executed;
            CanExecute += NewLineLayerCommand_CanExecute;
        }

        private void NewLineLayerCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.MapsControl == null)
                return;

            e.CanExecute = true;
            e.Handled = true;
        }

        private void NewLineLayerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.MapsControl == null)
                return;

            IMap map = pc.Project.NewMap("Ge", "Line");

            //IManipulatorBase mp = Registry.CreateManipulator("DrawLineObjectManipulator");
            //ManipulatorSetter.SetManipulator(mp, mc.GetLayerControl(0));
            LineLayerCreator sc = new LineLayerCreator();
            Layer layer = sc.Create();
            map.Layers.Add(layer);
        }
    }
}
