using gEngine.Graph.Interface;
using gEngine.Manipulator;
using gEngine.Project.Controls;
using gEngine.View;
using gSection.Commands;
using gSection.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace gSection.CommandBindings
{
    public class SelectObjectCommand : CommandBinding
    {
        public SelectObjectCommand()
        {
            Command = RibbonCommands.SelectObjectCommand;
            Executed += SelectObjectCommand_Executed;
            CanExecute += SelectObjectCommand_CanExecute;
        }

        private void SelectObjectCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            MainWindow mw = e.OriginalSource as MainWindow;
            ProjectControl pc = mw.ProjectControl;
            e.CanExecute = pc != null && pc.MapsControl.ActiveMapControl != null;
            e.Handled = true;
        }

        private void SelectObjectCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mw = e.OriginalSource as MainWindow;

            ProjectControl pc = mw.ProjectControl;
            if (pc == null)
                return;

            MapsControl tc = pc.MapsControl;
            MapControl mc = tc.ActiveMapControl;

            if (mc == null)
                return;


            if(ManipulatorSetter.IsContainManipulator(typeof(SelectObjectManipulator),mc))
                ManipulatorSetter.RemoveManipulator(typeof(SelectObjectManipulator), mc);
            else
            {
                SelectObjectManipulator mpl = gEngine.Manipulator.Registry.CreateManipulator("SelectObjectManipulator") as SelectObjectManipulator;
                if (mpl == null)
                    return;
                ManipulatorSetter.AddManipulator(mpl, mc);
                mpl.OnSelectObject += mw.Mpl_OnSelectObject;

            }


            e.Handled = true;
        }
    }
}
