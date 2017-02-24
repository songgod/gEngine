using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Ge.Section
{
    public class SectionObject : Object
    {
        public SectionObject()
        {
            TopGraph = new gTopology.Graph();
        }

        public gTopology.Graph TopGraph { get; set; }
    }
}
