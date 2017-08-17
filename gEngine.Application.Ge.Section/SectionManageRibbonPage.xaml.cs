using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
using gEngine.Application;
using gEngine.View;
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

namespace gEngine.Application.Ge.Section
{
    /// <summary>
    /// SectionManageRibbonPage.xaml 的交互逻辑
    /// </summary>
    public partial class SectionManageRibbonPage : GeRibbonPage
    {
        public SectionManageRibbonPage()
        {
            InitializeComponent();
            this.Name = "SectionManageRibbonPage";
            BindLongitudinalProportion();

            this.DataContext = this;
        }

        #region Property

        public List<int> LongitudinalProportion
        {
            get { return (List<int>) GetValue(LongitudinalProportionProperty); }
            set { SetValue(LongitudinalProportionProperty, value); }
        }

        public static readonly DependencyProperty LongitudinalProportionProperty =
            DependencyProperty.Register("LongitudinalProportion", typeof(List<int>), typeof(SectionManageRibbonPage));

        #endregion

        private void BindLongitudinalProportion()
        {
            LongitudinalProportion = new List<int>();
            LongitudinalProportion.Add(100);
            LongitudinalProportion.Add(200);
            LongitudinalProportion.Add(500);
            LongitudinalProportion.Add(1000);
            LongitudinalProportion.Add(1500);
            LongitudinalProportion.Add(2000);
        }
    }
}
