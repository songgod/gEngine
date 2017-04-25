using gEngine.Data.Ge;
using gEngine.Data.Ge.Txt;
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

        public Layer CreateSectionLayer(IDBWells wells)
        {
            Layer layer = new Layer() { Type = "Section" };

            WellCreator wc = new WellCreator();
            Random rdm = new Random();
            int wellCount = 0;
            int WellWidthSum = 0;
            foreach (var item in wells)
            {
                Well well = wc.Create(item);
                if (well != null)
                {
                    well.LongitudinalProportion = 1500;//vm.SLongitudinalProportion == 0 ? 1500 : vm.SLongitudinalProportion;//纵向比例
                    int depthsWidth = 60;// 深度道宽度
                    int colsOffset = 0; // 曲线偏移
                    well.Location = wellCount == 0 ? 0 : WellWidthSum;
                    WellWidthSum += (well.Columns.Count * 60 + depthsWidth) + 50;
                    for (int i = 0; i < well.Columns.Count; i++)
                    {
                        // 初步定义深度道显示在第一条曲线右侧
                        if (i.Equals(1))
                        {
                            colsOffset = depthsWidth;
                            well.DepthsOffset = i * 60;
                        }
                        well.Columns[i].Color = Color.FromRgb((byte) rdm.Next(0, 255), (byte) rdm.Next(0, 255), (byte) rdm.Next(0, 255));
                        well.Columns[i].Offset = colsOffset + i * 60;
                    }


                    wellCount++;
                    layer.Objects.Add(well);
                }

            }
            return layer;
        }

        public Layer CreateSectionLayer(IDBFactory db, HashSet<string> names, string horizonName, string discreteName)
        {
            Layer layer = new Layer() { Type = "Section" };

            WellCreator wc = new WellCreator();

            // 井曲线数据
            int WellLocation = 0;
            IDBWells wells = new DBWells();
            foreach (string name in names)
            {
                IDBWell wl = db.GetWell(name);
                IDBHorizons horizons = db.GetHorizonsByWell(name, horizonName);
                IDBDiscreteDatas discretes = db.GetDiscretesByWell(name, discreteName);
                Well well = wc.Create(wl, horizons, discretes);
                if (well != null)
                {
                    well.LongitudinalProportion = 1500;
                    well.Location = WellLocation;
                    foreach (var item in well.WellColumn_N)
                    {
                        WellLocation += item.Width;
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
