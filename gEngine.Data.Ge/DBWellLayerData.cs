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
    public class DBWellLayerData : DBBase, IDBWellLayerData
    {
        public List<string> WellNames
        {
            get { return (List<string>)GetValue(WellNamesProperty); }
            set { SetValue(WellNamesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WellNamesProperty =
            DependencyProperty.Register("WellNames", typeof(List<string>), typeof(DBWellLayerData));

        public List<Tuple<string, List<string>>> WellLayerDatas
        {
            get { return (List<Tuple<string, List<string>>>)GetValue(WellLayerDatasProperty); }
            set { SetValue(WellLayerDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WellLayerDatasProperty =
            DependencyProperty.Register("WellLayerDatas", typeof(List<Tuple<string, List<string>>>), typeof(DBWellLayerData));
    }

    public class DBWellLayerDatas : ObservedCollection<IDBWellLayerData>, IDBWellLayerDatas
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
