﻿using gEngine.Graph.Interface;
using gEngine.Utility;
using System.Windows;
using static gEngine.Graph.Ge.Enums;

namespace gEngine.Graph.Ge
{
    public class WellColumn : Object
    {
        public WellColumn()
        {
            Values = new ObsDoubles();
        }



        public Well Owner
        {
            get { return (Well)GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Owner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.Register("Owner", typeof(Well), typeof(WellColumn));



        public ObsDoubles Values
        {
            get { return (ObsDoubles)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Values.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register("Values", typeof(ObsDoubles), typeof(WellColumn));




        public MathType MathType
        {
            get { return (MathType)GetValue(MathTypProperty); }
            set { SetValue(MathTypProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MathTyp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MathTypProperty =
            DependencyProperty.Register("MathTyp", typeof(MathType), typeof(WellColumn));
    }

    public class WellColumns : ObservedCollection<WellColumn> { }
}
