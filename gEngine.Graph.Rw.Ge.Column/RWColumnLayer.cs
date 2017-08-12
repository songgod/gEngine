using gEngine.Graph.Ge.Column;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Rw.Ge.Column
{
    public class RWColumnLayer : RWLayerBase
    {
        public override string SupportType
        {
            get
            {
                return "Column";
            }
        }

        public override ILayer CreateLayer()
        {
            return new ColumnLayer();
        }
    }
}
