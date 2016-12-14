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
    public class DBWell : DBBase, IDBWell
    {
        public List<double> Depths
        {
            get { return (List<double>)GetValue(DepthsProperty); }
            set { SetValue(DepthsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DepthsProperty =
            DependencyProperty.Register("Depths", typeof(List<double>), typeof(DBWell));

        public List<Tuple<string, List<double>>> Columns
        {
            get { return (List<Tuple<string, List<double>>>)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(List<Tuple<string, List<double>>>), typeof(DBWell));
    }

    public class DBWells : ObservedCollection<IDBWell>, IDBWells
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
