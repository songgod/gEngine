using gEngine.Data.Interface;
using gEngine.Graph.Ge.Column;
using gEngine.Graph.Interface;
using System;
using System.Linq;
using System.Windows.Media;

namespace gEngine.Util.Ge.Column
{
    public class WellCreator
    {
        public IObjects Create(IDBWell db)
        {
            if (db == null)
                return null;

            IObjects res = new IObjects();

            Well well = new Well() { Name = db.Name };
            well.Depths = new Utility.ObsDoubles(db.Depths);

            for (int i = 0; i < db.Columns.Count; i++)
            {
                string name = db.Columns[i].Item1;
                WellColumn c = new WellColumn() { Name = db.Columns[i].Item1, Owner = well, MathType=Enums.MathType.DEFAULT };
                c.Values = new Utility.ObsDoubles(db.Columns[i].Item2);
                c.Color = Colors.Red;
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
