﻿using gEngine.Data.Ge;
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

        public Layer CreateSectionLayer(IDBSource db, Stack<WellLocation> wellLocs, string horizonName, string discreteName)
        {
            Layer layer = new SectionLayer();

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
                    layer.Objects.Insert(0, well);
                }
            }
            return layer;
        }

        public Layer CreateSectionLayer(IDBSource db, Stack<WellLocation> wellLocs, string horizonName, string discreteName, Well WellTpl)
        {
            Layer layer = new SectionLayer();

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

                    Well newWell = CreateWellByTpl(WellTpl, well) as Well;
                    //layer.Objects.Insert(0, well);
                    layer.Objects.Insert(0, newWell);
                }
            }
            return layer;
        }

        public IObject CreateWellByTpl(IObject tplObject, IObject destObject)
        {
            Well Well = destObject as Well;
            Well destWell = tplObject.DeepClone() as Well;

            destWell.Name = Well.Name;
            destWell.Location = Well.Location;
            destWell.LongitudinalProportion = Well.LongitudinalProportion;
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
