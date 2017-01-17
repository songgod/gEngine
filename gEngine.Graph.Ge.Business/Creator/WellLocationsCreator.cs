﻿using gEngine.Data.Interface;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static gEngine.Graph.Ge.Enums;

namespace gEngine.Graph.Ge.Business.Creator
{
    public class WellLocationsCreator : ICreator
    {
        public IObjects Create(IDBBase db)
        {
            if (db == null)
                return null;

            IDBWellLocations dbwl = db as IDBWellLocations;
            if (dbwl == null)
                return null;

            IObjects res = new IObjects();

            foreach (IDBWellLocation item in dbwl)
            {
                WellLocation wl = new WellLocation();
                wl.Name = item.Name;
                wl.WellNum = item.Name;
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
            }
            
            return res;
        }

        public Type ProcessType()
        {
            return typeof(IDBWellLocations);
        }
    }
}
