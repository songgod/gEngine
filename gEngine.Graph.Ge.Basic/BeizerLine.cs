using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Graph.Ge.Basic
{
    public class BeizerLine : Object
    {


        public PointCollection Points
        {
            get { return (PointCollection)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Points.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(PointCollection), typeof(BeizerLine), new PropertyMetadata(new PointCollection()));



        public LineStyle LinStyle
        {
            get { return (LineStyle)GetValue(LinStyleProperty); }
            set { SetValue(LinStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LinStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinStyleProperty =
            DependencyProperty.Register("LinStyle", typeof(LineStyle), typeof(BeizerLine), new PropertyMetadata(new LineStyle()));


    }
}
