using gEngine.Data.Interface;
using gEngine.Graph.Ge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Util.Ge.Plane
{
    public class PlaneLayerCreator : IToolBase
    {
        public string Name
        {
            get
            {
                return "PlaneLayerCreator";
            }
        }

        public Layer CreateWellLocationLayer()
        {
            return new Layer() { Type = "WellPlane" };
        }

        public Layer CreateWellLocationLayer(IDBWellLocations dbwl)
        {
            Layer layer = new Layer() { Name = dbwl.Name, Type = "WellPlane" };
            WellLocationsCreator c = new WellLocationsCreator();
            layer.Objects = c.Create(dbwl);
            return layer;
        }
    }
}
