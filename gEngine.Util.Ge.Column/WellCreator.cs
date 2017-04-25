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
        public int RoadWidth
        {
            get { return 60; }
        }

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

        public Well Create(IDBWell db, IDBHorizons horizons, IDBDiscreteDatas discretes)
        {
            if (db == null)
                return null;

            Random rdm = new Random();

            Well well = new Well() { Name = db.Name };
            well.Depths = new Utility.ObsDoubles(db.Depths);

            WellDepth WDepth = new WellDepth() { Name = "深度", Owner = well };
            WDepth.Depths = new Utility.ObsDoubles(db.Depths);
            WDepth.Color = Colors.Black;
            WDepth.Width = RoadWidth;
            well.WellColumn_N.Add(WDepth);

            for (int i = 0; i < db.Columns.Count; i++)
            {
                string name = db.Columns[i].Item1;
                WellLogColumn logColumn = new WellLogColumn() { Name = db.Columns[i].Item1, Owner = well, MathType = Enums.MathType.DEFAULT };
                logColumn.Values = new Utility.ObsDoubles(db.Columns[i].Item2);
                logColumn.Color = Color.FromRgb((byte) rdm.Next(0, 255), (byte) rdm.Next(0, 255), (byte) rdm.Next(0, 255));
                logColumn.Width = RoadWidth;
                well.WellColumn_N.Add(logColumn);
            }

            if (horizons.Horizons.Count > 0)
            {
                WellSegmentColumn segmentColumn = new WellSegmentColumn() { Owner = well, Color = Colors.Black, Width = RoadWidth };
                for (int i = 0; i < horizons.Horizons.Count; i++)
                {
                    WellSegmentColumn.Segment segment = new WellSegmentColumn.Segment();
                    segment.Name = horizons.Horizons[i].LayerNumber;
                    segment.Top = horizons.Horizons[i].Top_MeasuredDepth;
                    segment.Bottom = horizons.Horizons[i].MeasuredThickness;
                    segmentColumn.Segments.Add(segment);
                }
                well.WellColumn_N.Add(segmentColumn);
            }

            if (discretes.DiscreteDatas.Count > 0)
            {
                WellSegmentColumn segmentColumn = new WellSegmentColumn() { Owner = well, Color = Colors.Black, Width = RoadWidth };
                for (int i = 0; i < discretes.DiscreteDatas.Count; i++)
                {
                    WellSegmentColumn.Segment segment = new WellSegmentColumn.Segment();
                    segment.Top = discretes.DiscreteDatas[i].Top_MeasuredDepth;
                    segment.Bottom = discretes.DiscreteDatas[i].MeasuredThickness;
                    segment.Color = Colors.Green;
                    segmentColumn.Segments.Add(segment);
                }
                well.WellColumn_N.Add(segmentColumn);
            }

            return well;
        }
    }
}
