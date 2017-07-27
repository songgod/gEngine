using gEngine.Graph.Interface;
using gEngine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace gEngine.Graph.Ge.Column
{
    public class Well : Object
    {
        public Well()
        {
            LstColumns = new LstWellColumns();
        }

        public override IObject DeepClone()
        {
            Well well = new Well();
            foreach (WellColumns wcs in LstColumns)
            {
                WellColumns twcs = new WellColumns();
                foreach (WellColumn wc in wcs)
                {
                    WellColumn twc = (WellColumn) (wc.DeepClone());
                    twc.Owner = well;
                    twcs.Add(twc);
                }

                well.LstColumns.Add(twcs);
            }

            well.Name = Name;
            well.Location = Location;
            well.LongitudinalProportion = LongitudinalProportion;
            return well;
        }

        public LstWellColumns LstColumns
        {
            get { return (LstWellColumns) GetValue(LstColumnsProperty); }
            set { SetValue(LstColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LstColumnsProperty =
            DependencyProperty.Register("LstColumns", typeof(LstWellColumns), typeof(Well));

        public ObsDoubles Depths
        {
            get
            {
                foreach (var lstColumns in LstColumns)
                {
                    foreach (var cols in lstColumns)
                    {
                        Type type = cols.GetType();
                        if (type.Name.Equals("WellDepth"))
                        {
                            return ((WellDepth) cols).Depths;
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 每口井位置
        /// </summary>
        public int Location
        {
            get { return (int) GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(int), typeof(Well));

        public int LongitudinalProportion
        {
            get { return (int) GetValue(LongitudinalProportionProperty); }
            set { SetValue(LongitudinalProportionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LongitudinalProportionProperty =
            DependencyProperty.Register("LongitudinalProportion", typeof(int), typeof(Well));
    }
}
