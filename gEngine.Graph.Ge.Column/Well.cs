using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Windows;

namespace gEngine.Graph.Ge.Column
{
    public class Well : Object
    {
        public Well()
        {
            LstColumns = new List<LstWellColumns>();
        }

        public List<LstWellColumns> LstColumns
        {
            get { return (List<LstWellColumns>) GetValue(LstColumnsProperty); }
            set { SetValue(LstColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LstColumnsProperty =
            DependencyProperty.Register("LstColumns", typeof(List<LstWellColumns>), typeof(Well));

        public ObsDoubles Depths
        {
            get
            {
                foreach (var lstColumns in LstColumns)
                {
                    foreach (var columns in lstColumns.Columns)
                    {
                       Type type = columns.GetType();
                        if (type.Name.Equals("WellDepth"))
                        {
                            return ((WellDepth) columns).Depths;
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
