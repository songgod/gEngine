using gEngine.Data.Interface;
using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Data.Ge
{
    public class DBWellLayer : DBBase, IDBWellLayer
    {
        public string BoundaryName
        {
            get { return (string)GetValue(BoundaryNameProperty); }
            set { SetValue(BoundaryNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoundaryNameProperty =
            DependencyProperty.Register("BoundaryName", typeof(string), typeof(DBWellLayer));

        public double TopDepth
        {
            get { return (double)GetValue(TopDepthProperty); }
            set { SetValue(TopDepthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopDepthProperty =
            DependencyProperty.Register("TopDepth", typeof(double), typeof(DBWellLayer));

        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(double), typeof(DBWellLayer));
    }

    public class DBWellLayers : ObservedCollection<IDBWellLayer>, IDBWellLayers
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
    }
}
