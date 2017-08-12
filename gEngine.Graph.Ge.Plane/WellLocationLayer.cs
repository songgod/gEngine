using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Ge.Plane
{
    public class WellLocationLayer : Layer
    {
        public WellLocationLayer()
        {

        }

        public override string Type
        {
            get
            {
                return "WellLocation";
            }
        }
    }
}
