using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Graph.Ge.Basic
{
    public class Line : Object
    {
        public Point Start
        {
            get { return (Point)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Start.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(Point), typeof(Line), new PropertyMetadata(new Point()));



        public Point End
        {
            get { return (Point)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for End.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register("End", typeof(Point), typeof(Line), new PropertyMetadata(new Point()));




        public LineStyle LinStyle
        {
            get { return (LineStyle)GetValue(LinStyleProperty); }
            set { SetValue(LinStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LinStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinStyleProperty =
            DependencyProperty.Register("LinStyle", typeof(LineStyle), typeof(Line), new PropertyMetadata(new NormalLineStyle()));
    }
}
