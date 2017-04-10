using gEngine.Data.Interface;
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
            List<string> names = Project.Single.DBFactory.WellLocationsNames;
            if (names.Count == 0)
                return false;

            return true;
        }

        public override void Execute(object parameter)
        {
            List<string> names = Project.Single.DBFactory.WellLocationsNames;
            if (names.Count == 0)
                return;

            IDBWellLocations wls = Project.Single.DBFactory.GetWellLocations(names[0]);
            gEngine.Graph.Ge.Map map = new gEngine.Graph.Ge.Map() { Name = "Plane" };
            PlaneLayerCreator pc = new PlaneLayerCreator();
            gEngine.Graph.Ge.Layer layer = pc.CreateWellLocationLayer(wls);
            map.Layers.Add(layer);
            Project.Single.Maps.Add(map);
            Project.Single.OpenMaps.Add(map);
        }
    }
}
