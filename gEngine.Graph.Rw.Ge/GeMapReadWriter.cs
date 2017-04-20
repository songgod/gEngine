using gEngine.Graph.Ge;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Rw.Ge
{
    public class GeMapReadWriter : IMapReadWriter
    {
        public string SupportType
        {
            get
            {
                return "Ge";
            }
        }

        public IMap CreateMap()
        {
            return new Map();
        }

        public IMap ReadMap(string url)
        {
            throw new NotImplementedException();
        }

        public bool WriteMap(string url)
        {
            throw new NotImplementedException();
        }
    }
}
