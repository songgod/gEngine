using gEngine.Commands;
using gEngine.Manipulator;
using gEngine.Project.Controls;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Project.Commands
{
    public class SelectObjectCommand:CommandBinding
    {
        public SelectObjectCommand()
        {
            Command = StartCommands.SelectObjectCommand;
            Executed += SelectObjectCommand_Executed;
            CanExecute += SelectObjectCommand_CanExecute;
        }

        private void SelectObjectCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            e.CanExecute = pc != null && pc.MapsControl.ActiveMapControl != null;
            e.Handled = true;
        }

        private void SelectObjectCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null)
                return;

            MapsControl tc = pc.MapsControl;
            MapControl mc = tc.ActiveMapControl;

            if (mc == null)
                return;


            if (ManipulatorSetter.IsContainManipulator(typeof(SelectObjectManipulator), mc))
                ManipulatorSetter.RemoveManipulator(typeof(SelectObjectManipulator), mc);
            else
            {
                SelectObjectManipulator mpl = gEngine.Manipulator.Registry.CreateManipulator("SelectObjectManipulator") as SelectObjectManipulator;
                if (mpl == null)
                    return;
                ManipulatorSetter.AddManipulator(mpl, mc);
                mpl.OnSelectObject += pc.Mpl_OnSelectObject;

            }


            e.Handled = true;
        }
    }
}
