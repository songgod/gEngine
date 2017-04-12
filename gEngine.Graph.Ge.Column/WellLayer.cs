using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Graph.Ge.Column
{
    public class WellLayer : Object
    {
        public WellLayer()
        {
            BoundaryNames = new List<string>();
            TopDepths = new ObsDoubles();
            Thickness = new ObsDoubles();
        }

        public Well Owner
        {
            get { return (Well)GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Owner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.Register("Owner", typeof(Well), typeof(WellLayer));

        public List<string> BoundaryNames
        {
            get { return (List<string>)GetValue(BoundaryNameProperty); }
            set { SetValue(BoundaryNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoundaryNameProperty =
            DependencyProperty.Register("BoundaryName", typeof(List<string>), typeof(WellLayer));

        public ObsDoubles TopDepths
        {
            get { return (ObsDoubles)GetValue(TopDepthProperty); }
            set { SetValue(TopDepthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopDepthProperty =
            DependencyProperty.Register("TopDepth", typeof(ObsDoubles), typeof(WellLayer));

        public ObsDoubles Thickness
        {
            get { return (ObsDoubles)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(ObsDoubles), typeof(WellLayer));

        public int Offset
        {
            get { return (int)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(int), typeof(WellLayer));
    }

    public class WellLayers : ObservedCollection<WellLayer> { }
}
