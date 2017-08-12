using gEngine.Data.Interface;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Plane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Util.Ge.Plane
{
    public class WellLocationLayerCreator
    {
        public WellLocationLayer CreateWellLocationLayer()
        {
            return new WellLocationLayer();
        }

        public WellLocationLayer CreateWellLocationLayer(IDBWellLocations dbwl)
        {
            WellLocationLayer layer = new WellLocationLayer() { Name = dbwl.Name };
            WellLocationsCreator c = new WellLocationsCreator();
            layer.Objects = c.Create(dbwl);
            return layer;
        }
    }
}
