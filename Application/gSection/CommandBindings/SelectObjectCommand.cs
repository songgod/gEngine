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

            //mc.EditLayer.Width=0;

            if (mc == null)
                return;

            LayerControl lc = mc.ActiveLayerControl;
            if (lc == null)
                return;

            //关联后台数据
            //ILayer layer = lc.DataContext as ILayer;

            //IEnumerable<ObjectControl> objectControls = FindChild.FindVisualChildren<ObjectControl>(lc);
            //foreach (ObjectControl objectCtrl in objectControls)
            //{
            //    IEnumerable<Path> paths = FindChild.FindVisualChildren<Path>(objectCtrl);
            //    IObject iobject = objectCtrl.DataContext as IObject;
            //    foreach (Path path in paths)
            //    {
            //        Binding bd = new Binding("IsSelected") { /*Converter = new BooleanToVisibilityConverter(), Mode = BindingMode.TwoWay */};
            //        bd.Source = iobject;
            //        bd.Converter = new IsSelectedConverter();
            //        path.SetBinding(Path.StrokeThicknessProperty, bd);
            //    }
            //}

            SelectObjectManipulator mpl = gEngine.Manipulator.Registry.CreateManipulator("SelectObjectManipulator") as SelectObjectManipulator;
            if (mpl == null)
                return;
            ManipulatorSetter.SetManipulator(mpl, lc);
            mpl.OnSelectObject += mw.Mpl_OnSelectObject;
            e.Handled = true;
        }
    }
}
