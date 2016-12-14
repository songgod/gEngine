using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gEngine.Graph.Interface;
using gEngine.Utility;
using System.Windows;

namespace gEngine.Graph.Ge
{
    public class Well : Object, IWell
    {
        public Well()
        {
            Columns = new IWellColumns();
            Depths = new ObsDoubles();
        }



        public IWellColumns Columns
        {
            get { return (IWellColumns)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(IWellColumns), typeof(Well));




        public ObsDoubles Depths
        {
            get { return (ObsDoubles)GetValue(DepthsProperty); }
            set { SetValue(DepthsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DepthsProperty =
            DependencyProperty.Register("Depths", typeof(ObsDoubles), typeof(Well));
    }
}
