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
        public Well Create(IDBWell db)
        {
            if (db == null)
                return null;
            

            Well well = new Well() { Name = db.Name };
            well.Depths = new Utility.ObsDoubles(db.Depths);
            //for (int i = 0; i < db.Columns.Count; i++)//暂时为了演示效果，只显示两条曲线 2017-3-14
            for (int i = 0; i < 2; i++)
            {
                string name = db.Columns[i].Item1;
                WellColumn c = new WellColumn() { Name = db.Columns[i].Item1, Owner = well, MathType = Enums.MathType.DEFAULT };
                c.Values = new Utility.ObsDoubles(db.Columns[i].Item2);
                well.Columns.Add(c);
            }

            return well;
        }
    }
}
