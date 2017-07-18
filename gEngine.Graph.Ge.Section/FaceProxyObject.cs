using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Ge.Section
{
    public class FaceProxyObject : Object
    {
        public gTopology.Face Face { get; set; }

        public SectionInfo SectionInfo { get; set; }
    }
}
