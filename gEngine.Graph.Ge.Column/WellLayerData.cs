using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Graph.Ge.Column
{
    public class WellLayerData : Object
    {
        public WellLayerData()
        {

        }

        public Well Owner
        {
            get { return (Well)GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Owner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.Register("Owner", typeof(Well), typeof(WellLayerData));

        public List<string> WellLayerDatas
        {
            get { return (List<string>)GetValue(WellLayerDatasProperty); }
            set { SetValue(WellLayerDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WellLayerDatasProperty =
            DependencyProperty.Register("WellLayerDatas", typeof(List<string>), typeof(WellLayerData));

        public int Offset
        {
            get { return (int)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(int), typeof(WellLayerData));
    }

    public class WellLayerDatas : ObservedCollection<WellLayerData> { }
}
