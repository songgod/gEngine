using gEngine.Graph.Ge.Plane;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Rw.Ge.Plane
{
    public class RWWellLocationLayer : RWLayerBase
    {
        public override string SupportType
        {
            get
            {
                return "WellLocation";
            }
        }

        public override ILayer CreateLayer()
        {
            return new WellLocationLayer();
        }
    }
}
