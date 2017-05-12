using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Util.Ge.Basic
{
    public class CommonLayerCreator
    {
        public Layer Create()
        {
            Layer layer = new Layer() { Type = "Common" };

            ScaleRule ScaleRule = new ScaleRule();
            ScaleRule.Unit = "米";
            ScaleRule.ScaleNumber = 3;
            ScaleRule.ScaleSpace = 10;
            ScaleRule.ScaleHeight = 3;
            layer.Objects.Add(ScaleRule);
            return layer;
        }
    }
}
