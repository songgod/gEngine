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
    public class NewFillLayerCommand : CommandBinding
    {
        public NewFillLayerCommand()
        {
            Command = BasicCommands.NewFillLayerCommand;
            Executed += NewFillLayerCommand_Executed;
            CanExecute += NewFillLayerCommand_CanExecute;
        }

        private void NewFillLayerCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.MapsControl == null)
                return;

            e.CanExecute = true;
            e.Handled = true;
        }

        private void NewFillLayerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.MapsControl == null)
                return;

            IMap map = pc.Project.NewMap("Ge", "Fill");

            //IManipulatorBase mp = Registry.CreateManipulator("DrawBoundaryObjectManipulator");
            //ManipulatorSetter.SetManipulator(mp, mc.GetLayerControl(0));
            FillLayerCreator fl = new FillLayerCreator();
            Layer layer = fl.Create();
            map.Layers.Add(layer);
        }
    }
}
