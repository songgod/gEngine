using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using gEngine.Graph.Interface;

namespace gEngine.Graph.Ge.Column
{
    public class WellDepth : WellColumn
    {
        public override IObject DeepClone()
        {
            WellDepth wdepth = new WellDepth();
            wdepth.Name = Name;
            wdepth.Color = Color;
            wdepth.Width = Width;
            wdepth.Depths = new Utility.ObsDoubles(Depths);
            return wdepth;
        }
        public ObsDoubles Depths
        {
            get { return (ObsDoubles) GetValue(DepthsProperty); }
            set { SetValue(DepthsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DepthsProperty =
            DependencyProperty.Register("Depths", typeof(ObsDoubles), typeof(WellDepth));
    }
}
