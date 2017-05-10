﻿using gEngine.Data.Interface;
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

        public Well Create(IDBWell db, IDBHorizons horizons, IDBDiscreteDatas discretes)
        {
            if (db == null)
                return null;

            Random rdm = new Random();

            Well well = new Well() { Name = db.Name };

            WellColumns WellColumns = null;

            for (int i = 0; i < db.Columns.Count; i++)
            {
                WellColumns = new WellColumns();
                string name = db.Columns[i].Item1;
                WellLogColumn logColumn = new WellLogColumn() { Name = db.Columns[i].Item1, Owner = well, MathType = Enums.MathType.DEFAULT };
                logColumn.Values = new Utility.ObsDoubles(db.Columns[i].Item2);
                logColumn.Color = Color.FromRgb((byte) rdm.Next(0, 255), (byte) rdm.Next(0, 255), (byte) rdm.Next(0, 255));
                logColumn.Width = RoadWidth;
                WellColumns.Add(logColumn);
                well.LstColumns.Add(WellColumns);
            }

            WellColumns = new WellColumns();
            WellDepth WDepth = new WellDepth() { Name = "深度", Owner = well };
            WDepth.Depths = new Utility.ObsDoubles(db.Depths);
            WDepth.Color = Colors.Black;
            WDepth.Width = RoadWidth;
            WellColumns.Add(WDepth);
            well.LstColumns.Add(WellColumns);

            WellColumns = new WellColumns();
            if (discretes.DiscreteDatas.Count > 0)
            {
                WellSegmentColumn segmentColumn = new WellSegmentColumn() { Owner = well, Width = RoadWidth, Name = "二类砂岩" };
                for (int i = 0; i < discretes.DiscreteDatas.Count; i++)
                {
                    WellSegmentColumn.Segment segment = new WellSegmentColumn.Segment();
                    segment.Top = discretes.DiscreteDatas[i].Top_MeasuredDepth;
                    segment.Bottom = discretes.DiscreteDatas[i].MeasuredThickness;
                    segment.Color = Colors.Green;
                    segmentColumn.Segments.Add(segment);
                }
                WellColumns.Add(segmentColumn);
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
                WellColumns.Add(segmentColumn);
            }

            well.LstColumns.Add(WellColumns);
            return well;
        }
    }
}
