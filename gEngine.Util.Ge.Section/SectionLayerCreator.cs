using gEngine.Data.Ge;
using gEngine.Data.Ge.Txt;
using gEngine.Data.Interface;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Column;
using gEngine.Graph.Ge.Plane;
using gEngine.Graph.Ge.Section;
using gEngine.Graph.Interface;
using gEngine.Util.Ge.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace gEngine.Util.Ge.Section
{
    public class SectionLayerCreator
    {
        public Layer CreateSectionLayer()
        {
            Layer layer = new SectionLayer();
            return layer;
        }

        public Layer CreateSectionLayer(IDBSource db, Stack<WellLocation> wellLocs, string horizonName, string discreteName, SectionSetEntity sse)
        {
            Layer layer = new SectionLayer();

            WellCreator wc = new WellCreator();

            var wlocs = wellLocs.OrderBy(o => o.X);

            // 井曲线数据
            IDBWells wells = new DBWells();
            foreach (WellLocation wellLoc in wlocs)
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
                    well.LongitudinalProportion = sse.SLongitudinalProportion;
                    well.HorizontalProportion = sse.SHorizontalProportion;

                    #region 计算起-止深度

                    var tempTop_Horizon = horizons.Horizons.Where(x => x.LayerNumber.Equals(sse.SelTopCw));
                    var tempBottom_Horizon = horizons.Horizons.Where(x => x.LayerNumber.Equals(sse.SelBottomCw));

                    if (tempTop_Horizon.Count().Equals(0) || tempTop_Horizon.ElementAt(0).Top_MeasuredDepth < 0)
                    {
                        tempTop_Horizon = horizons.Horizons.OrderBy(x => x.Top_MeasuredDepth).Where(x => !string.IsNullOrEmpty( x.LayerNumber) && x.Top_MeasuredDepth > 0);
                    }
                    
                    if (tempBottom_Horizon.Count().Equals(0) || tempBottom_Horizon.ElementAt(0).Top_MeasuredDepth < 0)
                    {
                        tempBottom_Horizon = horizons.Horizons.OrderByDescending(x => x.Top_MeasuredDepth).Where(x => !string.IsNullOrEmpty(x.LayerNumber) && x.Top_MeasuredDepth > 0);
                    }

                    if (tempTop_Horizon.Count().Equals(0) || tempBottom_Horizon.Count().Equals(0))
                        continue;

                    IDBHorizon Top_Horizon = tempTop_Horizon.ElementAt(0);
                    IDBHorizon Bottom_Horizon = tempBottom_Horizon.ElementAt(0);

                    well.TopDepth = Top_Horizon.Top_MeasuredDepth - sse.TopYs;
                    well.BottomDepth = Bottom_Horizon.Top_MeasuredDepth + Bottom_Horizon.MeasuredThickness + sse.BottomYs;

                    #endregion

                    string tplName = sse.SelTplName;
                    if (string.IsNullOrEmpty(tplName))
                    {
                        layer.Objects.Add(well);
                    }
                    else
                    {
                        IObject WellTpl = gEngine.Graph.Tpl.Ge.Registry.GetTemplate(typeof(Well), tplName);
                        Well newWell = CreateWellByTpl(WellTpl, well) as Well;
                        layer.Objects.Add(newWell);
                    }
                }
            }

            SetWellLocation(layer, wlocs);
            return layer;
        }

        private void SetWellLocation(Layer layer, IOrderedEnumerable<WellLocation> wlocs)
        {
            double firstWlLoc = 0;
            int wellWidth = 0;
            foreach (Well well in layer.Objects)
            {
                var wl = wlocs.Where(x => x.Name.Equals(well.Name));
                double xAxis = wl.ElementAt(0).X;
                if (firstWlLoc == 0)
                {
                    firstWlLoc = xAxis;
                    well.Location = (xAxis - firstWlLoc) * Graph.Ge.Column.Enums.PerMilePx / well.HorizontalProportion;
                }
                else
                {
                    well.Location = (xAxis - firstWlLoc) * Graph.Ge.Column.Enums.PerMilePx / well.HorizontalProportion + wellWidth;
                }
                wellWidth += well.LstColumns.Sum(x => x[0].Width);
            }
        }

        public IObject CreateWellByTpl(IObject tplObject, IObject destObject)
        {
            Well Well = destObject as Well;
            Well destWell = tplObject.DeepClone() as Well;

            destWell.Name = Well.Name;
            destWell.TopDepth = Well.TopDepth;
            destWell.BottomDepth = Well.BottomDepth;
            foreach (WellColumns Wellcolumns in destWell.LstColumns)
            {
                foreach (WellColumn WellColumn in Wellcolumns)
                {
                    IEnumerator<WellColumns> wcols = (from x in Well.LstColumns.ToList() where 1 == 1 && (from y in x where y.Name == WellColumn.Name select y).Any() select x).GetEnumerator();
                    while (wcols.MoveNext())
                    {
                        var res = wcols.Current.Where(x => x.Name.Equals(WellColumn.Name));
                        if (res.Count() > 0)
                        {
                            if (WellColumn is WellLogColumn)
                            {
                                WellLogColumn logColumn = (WellLogColumn) res.ElementAt(0);
                                ((WellLogColumn) WellColumn).Values = new Utility.ObsDoubles(logColumn.Values);
                            }
                            else if (WellColumn is WellDepth)
                            {
                                WellDepth wellDepth = (WellDepth) res.ElementAt(0);
                                ((WellDepth) WellColumn).Depths = new Utility.ObsDoubles(wellDepth.Depths);
                            }
                            else if (WellColumn is WellSegmentColumn)
                            {
                                WellSegmentColumn segColumn = (WellSegmentColumn) res.ElementAt(0);
                                ((WellSegmentColumn) WellColumn).Segments = segColumn.Segments;
                            }
                            else { }

                            break;
                        }
                    }
                }
            }

            return destWell;
        }
    }
}
