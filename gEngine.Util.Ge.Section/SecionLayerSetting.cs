using gEngine.Graph.Ge.Column;
using gEngine.Graph.Ge.Section;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Util.Ge.Section
{
    public class SecionLayerSetting
    {
        public SectionLayer SectionLayer { get; set; }

        public SecionLayerSetting() { }
        public SecionLayerSetting(SectionLayer layer)
        {
            SectionLayer = layer;
        }
        public void setSectionLayerWellVerticalScale(int s)
        {
            if (SectionLayer == null)
                return;
            
            if (SectionLayer.Objects.Count.Equals(0))
                return;

            foreach (IObject obj in SectionLayer.Objects)
            {
                if (typeof(Well) == obj.GetType())
                {
                    ((Well)obj).LongitudinalProportion = s;
                }
            }
        }

        public int getSectionLayerWellVerticalScale()
        {
            if (SectionLayer == null)
                return 0;
            if (SectionLayer.Objects.Count.Equals(0))
                return 0;

            foreach (IObject obj in SectionLayer.Objects)
            {
                if (typeof(Well) == obj.GetType())
                    return ((Well)obj).LongitudinalProportion;
            }

            return 0;
        }
    }
}
