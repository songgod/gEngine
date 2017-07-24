using gEngine.Graph.Ge.Plane;
using gEngine.RibbonPageCategory;
using gEngine.RibbonPageCategory.Ge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace gEngine.RibbonPageCategory.Ge.Plane
{
    /// <summary>
    /// WellLocationRibbonPageCategory.xaml 的交互逻辑
    /// </summary>
    public partial class WellLocationRibbonPageCategory : GeRibbonPageCategory
    {
        public WellLocation WellLocation
        {
            get { return (WellLocation)GetValue(WellLocationProperty); }
            set { SetValue(WellLocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Project.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WellLocationProperty =
            DependencyProperty.Register("WellLocation", typeof(WellLocation), typeof(WellLocationRibbonPageCategory));

        public WellLocationRibbonPageCategory()
        {
            InitializeComponent();
            Binding bd = new Binding("DataContext");
            bd.Source = this;
            bd.Mode = BindingMode.TwoWay;//OneWay
            this.SetBinding(WellLocationProperty, bd);

        }
        public override Type SupportType
        {
            get
            {
                return typeof(WellLocation);
            }
        }


        private void BarEditItem_EditValueChanged(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(e.Source.ToString());
            DevExpress.Xpf.Bars.BarEditItem item = e.Source as DevExpress.Xpf.Bars.BarEditItem;
            WellLocation data = item.DataContext as WellLocation;
            double d1=data.PointStyle.Width;
            //double d2 = double.Parse(item.EditValue.ToString());
            //string wedllnum=data.WellNum;
            //data.PointStyle.Width = d2;

           
        }
    }
}
