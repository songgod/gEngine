using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gEngine.Data.Interface;
using System.Windows;
using System.ComponentModel;
using gEngine.Utility;

namespace gEngine.Data.Ge
{
    public class DBWellLocation : DBBase, IDBWellLocation
    {
        public int WellCategory
        {
            get { return (int)GetValue(WellCategoryProperty); }
            set { SetValue(WellCategoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WellCategory.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WellCategoryProperty =
            DependencyProperty.Register("WellCategory", typeof(int), typeof(DBWellLocation));


        public string WellType
        {
            get { return (string)GetValue(WellTypeProperty); }
            set { SetValue(WellTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WellType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WellTypeProperty =
            DependencyProperty.Register("WellType", typeof(string), typeof(DBWellLocation));



        public double x
        {
            get { return (double)GetValue(xProperty); }
            set { SetValue(xProperty, value); }
        }

        // Using a DependencyProperty as the backing store for x.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xProperty =
            DependencyProperty.Register("x", typeof(double), typeof(DBWellLocation));



        public double y
        {
            get { return (double)GetValue(yProperty); }
            set { SetValue(yProperty, value); }
        }

        // Using a DependencyProperty as the backing store for y.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yProperty =
            DependencyProperty.Register("y", typeof(double), typeof(DBWellLocation));
    }

    public class DBWellLocations : ObservedCollection<IDBWellLocation>, IDBWellLocations
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
