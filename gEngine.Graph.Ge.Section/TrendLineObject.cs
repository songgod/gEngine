using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Graph.Ge.Section
{
    public class TrendLineObject : Object
    {
        public TrendLineObject()
        {

        }

        public ObsPoints Points
        {
            get { return (ObsPoints)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Points.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(ObsPoints), typeof(TrendLineObject));
    }
}
