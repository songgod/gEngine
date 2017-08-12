using gEngine.Graph.Ge.Basic;
using gEngine.Graph.Interface;
using gEngine.Graph.Rw.Ge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Rw.Ge.Basic
{
    public class RWBasicLayer : RWLayerBase
    {
        public override string SupportType
        {
            get
            {
                return "Basic";
            }
        }

        public override ILayer CreateLayer()
        {
            return new BasicLayer();
        }
    }
}
