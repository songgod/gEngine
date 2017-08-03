using gEngine.Commands;
using gEngine.Graph.Ge;
using gEngine.Graph.Interface;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Basic;
using gEngine.Project.Controls;
using gEngine.Util.Ge.Basic;
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

        private void NewLineCommand_Executed(object sender, ExecutedRoutedEventArgs e)
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

            DrawLineObjectManipulator dm = gEngine.Manipulator.Registry.CreateManipulator("DrawLineObjectManipulator") as DrawLineObjectManipulator;
            if (dm == null)
                return;
            ManipulatorSetter.SetManipulator(dm, lc);
        }
    }
}
