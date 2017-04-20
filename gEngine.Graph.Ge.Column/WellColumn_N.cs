using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Graph.Ge.Column
{
    public class WellColumn_N : Object
    {
        public Well Owner
        {
            get { return (Well) GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Owner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.Register("Owner", typeof(Well), typeof(WellColumn_N));
    }

    public class WellColumn_Ns: ObservedCollection<WellColumn_N>
    {
    }
}
