using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static gEngine.Graph.Ge.Column.Enums;

namespace gEngine.Graph.Ge.Column
{
    public class WellLogColumn : WellColumn
    {
        public WellLogColumn()
        {
            Values = new ObsDoubles();
        }

        public ObsDoubles Values
        {
            get { return (ObsDoubles) GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Values.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register("Values", typeof(ObsDoubles), typeof(WellLogColumn));

        public MathType MathType
        {
            get { return (MathType) GetValue(MathTypProperty); }
            set { SetValue(MathTypProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MathTyp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MathTypProperty =
            DependencyProperty.Register("MathTyp", typeof(MathType), typeof(WellLogColumn));
    }
}
