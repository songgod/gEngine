using gEngine.Data.Interface;
using gEngine.Graph.Interface;
using gEngine.Util.Ge.Plane;
using gSection.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gSection.Commands.Map
{
    public class NewPlaneMapCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            List<string> names = Project.Single.DBSource.WellLocationsNames;
            if (names.Count == 0)
                return false;

            return true;
        }

        public override void Execute(object parameter)
        {
            List<string> names = Project.Single.DBSource.WellLocationsNames;
            if (names.Count == 0)
                return;

            IDBWellLocations wls = Project.Single.DBSource.GetWellLocations(names[0]);
            PlaneLayerCreator pc = new PlaneLayerCreator();
            gEngine.Graph.Ge.Layer layer = pc.CreateWellLocationLayer(wls);

            IMap map = Project.Single.NewMap("Ge", "Plane");
            map.Layers.Add(layer);
        }
    }
}
