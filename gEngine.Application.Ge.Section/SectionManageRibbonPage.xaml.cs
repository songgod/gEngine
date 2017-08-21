using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
using gEngine.Application;
using gEngine.Graph.Ge.Column;
using gEngine.Graph.Interface;
using gEngine.Util.Ge.Section;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<int> LongitudinalProportion
        {
            get { return (ObservableCollection<int>) GetValue(LongitudinalProportionProperty); }
            set { SetValue(LongitudinalProportionProperty, value); }
        }

        public static readonly DependencyProperty LongitudinalProportionProperty =
            DependencyProperty.Register("LongitudinalProportion", typeof(ObservableCollection<int>), typeof(SectionManageRibbonPage));

        public LayerControl LayerControl
        {
            get { return (LayerControl) GetValue(LayerControlProperty); }
            set { SetValue(LayerControlProperty, value); }
        }

        public static readonly DependencyProperty LayerControlProperty =
           DependencyProperty.Register("LayerControl", typeof(LayerControl), typeof(SectionManageRibbonPage));

        #endregion

        #region Method

        private void BindLongitudinalProportion()
        {
            SectionSetEntity sse = new SectionSetEntity();
            LongitudinalProportion = new ObservableCollection<int>(sse.LongitudinalProportion);
        }

        #endregion

        #region Event

        private void eLongitudinalProportion_EditValueChanged(object sender, RoutedEventArgs e)
        {
            if (ProjectControl.MapsControl.Items.Count.Equals(0))
                return;
            if (ProjectControl.MapsControl.ActiveMapControl.ActiveLayerControl == null)
                return;

            LayerControl lc = ProjectControl.MapsControl.ActiveMapControl.ActiveLayerControl;
            ILayer layer = lc.LayerContext;

            if (layer.Type != "Section")
                return;

            if (layer.Objects.Count.Equals(0))
                return;

            if (typeof(Well) != layer.Objects[0].GetType())
                return;

            int LongitudinalProportion = Int32.Parse(eLongitudinalProportion.EditValue.ToString());
            foreach (Well well in lc.LayerContext.Objects)
            {
                well.LongitudinalProportion = LongitudinalProportion;
            }
        }

        #endregion
    }
}
