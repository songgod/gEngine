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
            Random rdm = new Random();

            if (db == null)
                return null;

            IObjects res = new IObjects();

            for (int j = 0; j < 2; j++)
            {
                Well well = new Well() { Name = db.Name };
                well.Depths = new Utility.ObsDoubles(db.Depths);
                well.LongitudinalProportion = 1500;//纵向比例
                int depthsWidth = 60;// 深度道宽度
                int colsOffset = 0; // 曲线偏移
                int wellColumnWidth = (db.Columns.Count * 60 + depthsWidth) * j;//单井剖面图宽度

                for (int i = 0; i < db.Columns.Count; i++)
                {
                    string name = db.Columns[i].Item1;
                    WellColumn c = new WellColumn() { Name = db.Columns[i].Item1, Owner = well, MathType = Enums.MathType.DEFAULT };
                    c.Values = new Utility.ObsDoubles(db.Columns[i].Item2);
                    //c.Color =  Colors.Red;
                    c.Color = Color.FromRgb((byte)rdm.Next(0, 255), (byte)rdm.Next(0, 255), (byte)rdm.Next(0, 255));
                    // 初步定义深度道显示在第一条曲线右侧
                    if (i.Equals(1))
                    {
                        colsOffset = depthsWidth;
                        well.DepthsOffset = i * 60 + wellColumnWidth + j * 100;
                    }
                    c.Offset = colsOffset + i * 60 + wellColumnWidth + j * 100;
                    well.Columns.Add(c);
                }

                well.Offset = wellColumnWidth + j * 100;
                res.Add(well);
            }
            return res;
        }

        public Type ProcessType()
        {
            return typeof(IDBWell);
        }
    }
}
