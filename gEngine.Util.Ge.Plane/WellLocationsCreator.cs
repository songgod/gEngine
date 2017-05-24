using gEngine.Data.Interface;
using gEngine.Graph.Ge.Plane;
using gEngine.Graph.Interface;
using System;
using static gEngine.Graph.Ge.Plane.Enums;

namespace gEngine.Util.Ge.Plane
{
    public class WellLocationsCreator
    {
        public IObjects Create(IDBWellLocations dbwl)
        {
            if (dbwl == null)
                return null;

            IObjects res = new IObjects();

            foreach (IDBWellLocation item in dbwl)
            {
                WellLocation wl = new WellLocation();
                wl.WellNum = item.Name;
                wl.Name = item.Name;
                wl.X = item.x;
                wl.Y = item.y;
                wl.WellCategory = (WellCategory)(item.WellCategory);
                if(item.WellType=="W")
                {
                    wl.WellType = WellType.W;
                }
                else if(item.WellType=="Y")
                {
                    wl.WellType = WellType.Y;
                }
                res.Add(wl);
                //wl.DataTemplate = "WellLocationTemplate";
            }
            
            return res;
        }

        public Type ProcessType()
        {
            return typeof(IDBWellLocations);
        }
    }
}
