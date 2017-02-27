﻿using gEngine.Utility;
using System;
using System.Windows;

namespace gEngine.Graph.Ge.Column
{
    public class Well : Object
    {
        public Well()
        {
            Columns = new WellColumns();
            Depths = new ObsDoubles();
        }



        public WellColumns Columns
        {
            get { return (WellColumns)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(WellColumns), typeof(Well));




        public ObsDoubles Depths
        {
            get { return (ObsDoubles)GetValue(DepthsProperty); }
            set { SetValue(DepthsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DepthsProperty =
            DependencyProperty.Register("Depths", typeof(ObsDoubles), typeof(Well));
    }
}