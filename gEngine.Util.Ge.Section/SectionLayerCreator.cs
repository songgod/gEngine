using gEngine.Data.Interface;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Column;
using gEngine.Graph.Ge.Section;
using gEngine.Util.Ge.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Util.Ge.Section
{
    public class SectionLayerCreator : IToolBase
    {
        public string Name
        {
            get
            {
                return "SectionLayerCreator";
            }
        }

        public Layer CreateSectionLayer()
        {
            Layer layer = new Layer() { Type = "Section" };
            layer.Objects.Add(new SectionObject());
            return layer;
        }

        public Layer CreateSectionLayer(IDBWells wells)
        {
            Layer layer = new Layer() { Type = "Section" };
            layer.Objects.Add(new SectionObject());
            WellCreator wc = new WellCreator();
            foreach (var item in wells)
            {
                Well well = wc.Create(item);
                if (well != null)
                    layer.Objects.Add(well);
            }
            return layer;
        }
    }
}
