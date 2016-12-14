using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gEngine.Data.Interface;
using gEngine.Graph.Interface;

namespace gEngine.Graph.Ge.Business.Creator
{
    public class WellCreator : ICreator
    {
        public IObjects Create(IDBBase db)
        {
            if (db == null)
                return null;

            IDBWell dbwl = db as IDBWell;
            if (dbwl == null)
                return null;

            IObjects res = new IObjects();

            Well well = new Well() { Name = dbwl.Name };
            well.Depths = new Utility.ObsDoubles(dbwl.Depths);

            for (int i = 0; i < dbwl.Columns.Count; i++)
            {
                string name = dbwl.Columns[i].Item1;
                WellColumn c = new WellColumn() { Name = dbwl.Columns[i].Item1, Owner = well, MathTyp=Enums.MathType.DEFAULT };
                c.Values = new Utility.ObsDoubles(dbwl.Columns[i].Item2);
                well.Columns.Add(c);
            }
            res.Add(well);
            return res;
        }

        public Type ProcessType()
        {
            return typeof(IDBWell);
        }
    }
}
