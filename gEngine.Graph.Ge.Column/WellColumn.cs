using gEngine.Utility;
using System.Windows;
using System.Windows.Media;
using static gEngine.Graph.Ge.Column.Enums;

namespace gEngine.Graph.Ge.Column
{
    public class WellColumn : Object
    {
        public Well Owner
        {
            get { return (Well) GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Owner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.Register("Owner", typeof(Well), typeof(WellColumn));

        public Color Color
        {
            get { return (Color) GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(WellColumn));

        public int Width
        {
            get { return (int) GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(int), typeof(WellColumn));
    }

    public class WellColumns : ObservedCollection<WellColumn> { }
}
