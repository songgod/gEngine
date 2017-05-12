using gEngine.Commands;
using gEngine.Data.Interface;
using gEngine.Graph.Interface;
using gEngine.Project.Commands;
using gEngine.Project.Controls;
using gEngine.Util.Ge.Plane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gEngine.Project.Plane
{
    public class NewPlaneMapCommand : CommandBinding
    {
        public NewPlaneMapCommand()
        {
            Command = PlaneCommands.NewPlaneMapCommand;
            Executed += NewPlaneMapCommand_Executed; ;
            CanExecute += NewPlaneMapCommand_CanExecute; ;
        }

        private void NewPlaneMapCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.Project == null || pc.Project.DBSource == null)
                return;

            List<string> names = pc.Project.DBSource.WellLocationsNames;
            if (names.Count == 0)
                return;
            e.CanExecute = true;
            e.Handled = true;
        }

        private void NewPlaneMapCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pctrl = e.OriginalSource as ProjectControl;
            if (pctrl == null || pctrl.Project == null || pctrl.Project.DBSource == null)
                return;

            List<string> names = pctrl.Project.DBSource.WellLocationsNames;
            if (names.Count == 0)
                return;

            //弹出对话框，选择信息？

            IDBWellLocations wls = pctrl.Project.DBSource.GetWellLocations(names[0]);
            PlaneLayerCreator pc = new PlaneLayerCreator();
            gEngine.Graph.Ge.Layer layer = pc.CreateWellLocationLayer(wls);

            IMap map = pctrl.Project.NewMap("Ge", "Plane");
            map.Layers.Add(layer);
            e.Handled = true;
        }
    }
}
