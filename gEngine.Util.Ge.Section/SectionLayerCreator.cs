using gEngine.Data.Ge;
using gEngine.Data.Ge.Txt;
using gEngine.Data.Interface;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Column;
using gEngine.Graph.Ge.Plane;
using gEngine.Graph.Ge.Section;
using gEngine.Util.Ge.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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

        public Layer CreateSectionLayer(IDBSource db, Stack<WellLocation> wellLocs, string horizonName, string discreteName)
        {
            Layer layer = new Layer() { Type = "Section" };

            WellCreator wc = new WellCreator();

            // 井曲线数据
            int WellLocation = 0;
            IDBWells wells = new DBWells();
            foreach (WellLocation wellLoc in wellLocs)
            {
                string name = wellLoc.WellNum;
                IDBWell wl = db.GetWell(name);
                if (wl == null)
                    continue;
                IDBHorizons horizons = db.GetHorizonsByWell(name, horizonName);
                IDBDiscreteDatas discretes = db.GetDiscretesByWell(name, discreteName);
                Well well = wc.Create(wl, horizons, discretes);
                if (well != null)
                {
                    well.LongitudinalProportion = 1500;
                    well.Location = WellLocation;
                    foreach (var item in well.LstColumns)
                    {
                        WellLocation += item[0].Width;
                    }
                    WellLocation += 50;
                    layer.Objects.Add(well);
                }
            }
            layer.Objects.Add(new SectionObject());
            return layer;
        }
    }
}
