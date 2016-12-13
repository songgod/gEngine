using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Ge
{
    public class Map : Base, IMap
    {
        public Map()
        {
            Layers = new ILayers();
        }

        private ILayers locallayers;

        public ILayers Layers
        {
            get
            {
                return locallayers;
            }

            set
            {
                locallayers = value;
                RaiseProertyChanged("Layers");
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name=value;
                RaiseProertyChanged("Name");
            }
        }
    }
}
