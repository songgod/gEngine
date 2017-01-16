﻿using gEngine.Graph.Interface;
using gEngine.Utility;
using System.Windows;
using System.Windows.Media;
using static gEngine.Graph.Interface.Enums;

namespace gEngine.Graph.Ge
{
    public class WellColumn : Object, IWellColumn
    {
        public WellColumn()
        {
            Values = new ObsDoubles();
        }



        public IWell Owner
        {
            get { return (IWell)GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Owner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.Register("Owner", typeof(IWell), typeof(WellColumn));



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

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(WellColumn));
    }
}
