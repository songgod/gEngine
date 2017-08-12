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
            if (pc == null || pc.MapsControl == null)
                return;

            if (pc.MapsControl.ActiveMapControl == null)
                return;

            MapControl mc = pc.MapsControl.ActiveMapControl;
            if (mc == null)
                return;

            LayerControl layer = mc.ActiveLayerControl;
            if (layer == null)
                return;

            // 这里需要判断一下是不是装饰图层

            e.CanExecute = true;
            e.Handled = true;
        }

        private void NewBoundaryCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.MapsControl == null)
                return;

            if (pc.MapsControl.ActiveMapControl == null)
                return;

            MapControl mc = pc.MapsControl.ActiveMapControl;
            if (mc == null)
                return;

            LayerControl lc = mc.ActiveLayerControl;
            if (lc == null)
                return;

            DrawBoundaryObjectManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawBoundaryObjectManipulator") as DrawBoundaryObjectManipulator;
            if (dm == null)
                return;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
