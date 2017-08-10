using gEngine.Commands;
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
    public class NewBezierLineCommand: CommandBinding
    {
        public NewBezierLineCommand()
        {
            Command = BasicCommands.NewBezierLineCommand;
            Executed += NewBezierLineCommand_Executed;
            CanExecute += NewBezierLineCommand_CanExecute;
        }

        private void NewBezierLineCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
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

            e.CanExecute = true;
            e.Handled = true;
        }

        private void NewBezierLineCommand_Executed(object sender, ExecutedRoutedEventArgs e)
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


            DrawBezierLineObjectManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawBezierLineObjectManipulator") as DrawBezierLineObjectManipulator;
            if (dm == null)
                return;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
