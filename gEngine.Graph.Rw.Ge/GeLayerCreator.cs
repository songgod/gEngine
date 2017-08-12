using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Rw.Ge
{
    public class GeLayerCreator : ILayerCreator
    {
        public List<string> LayerTypes
        {
            get
            {
                Registry.LoadLocalRW();
                return Registry.DicLayerRW.Keys.ToList();
            }
        }

        public string MapType
        {
            get
            {
                return "Ge";
            }
        }

        public ILayer CreateLayer(string type)
        {
            return Registry.GetLayerRW(type).CreateLayer();
        }
    }
}
