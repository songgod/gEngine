using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Graph.Ge.Column
{
    public class WellSegmentColumn : WellColumn_N
    {
        public WellSegmentColumn()
        {
            Segments = new List<Segment>();
        }
       public List<Segment> Segments
        {
            get { return (List<Segment>) GetValue(SegmentsProperty); }
            set { SetValue(SegmentsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SegmentsProperty =
            DependencyProperty.Register("Segments", typeof(List<Segment>), typeof(WellSegmentColumn));

        public class Segment : DependencyObject
        {
            public double Top
            {
                get { return (double) GetValue(TopProperty); }
                set { SetValue(TopProperty, value); }
            }

            // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty TopProperty =
                DependencyProperty.Register("Top", typeof(double), typeof(Segment));

            public double Bottom
            {
                get { return (double) GetValue(BottomProperty); }
                set { SetValue(BottomProperty, value); }
            }

            // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty BottomProperty =
                DependencyProperty.Register("Bottom", typeof(double), typeof(Segment));

            public string Name
            {
                get { return (string) GetValue(NameProperty); }
                set { SetValue(NameProperty, value); }
            }

            // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty NameProperty =
                DependencyProperty.Register("Name", typeof(string), typeof(Segment));

            public Color Color
            {
                get { return (Color) GetValue(ColorProperty); }
                set { SetValue(ColorProperty, value); }
            }

            // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty ColorProperty =
                DependencyProperty.Register("Color", typeof(Color), typeof(Segment));

        }
    }
}
