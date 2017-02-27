using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Util.Ge.Section
{
    public static class SectionLayerCreator
    {
        public static Layer CreateSectionLayer()
        {
            Layer layer = new Layer();
            layer.Objects.Add(new SectionObject());
            return layer;
        }
    }
}
