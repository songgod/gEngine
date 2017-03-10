using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Util.Ge.Column;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using gEngine.View;
using System.Windows.Interactivity;
using GPTDxWPFRibbonApplication1.ViewModels;
using gEngine.Util.Ge.Section;
using gEngine.Graph.Interface;

namespace GPTDxWPFRibbonApplication1.Controls
{
    /// <summary>
    /// DWellControl.xaml 的交互逻辑
    /// </summary>
    public partial class DWellControl : UserControl, IView
    {
        #region IView接口实现
        FrameworkElement IView.FullScreenObject
        {
            get { return mc; }
            set { mc = (MapControl)value; }
        }

        Behavior<UIElement> IView.ManipulatorBehavior
        {
            get;set;
        }
        #endregion

        public NewSectionSetViewModel Vm
        {
            get;
            set;
        }

        public DWellControl()
        {
            InitializeComponent();
            InitWell();
        }

        private void InitWell()
        {
            Map map = new Map();
            Layer layer = SectionLayerCreator.CreateSectionLayer();
            TxtWell tw = new TxtWell() { TxtFile = "D:\\Data\\MulWellColumnDataNew.txt" };
            WellCreator wc = new WellCreator();
            IObjects objs= wc.Create(tw);
            foreach (var obj in objs)
            {
                layer.Objects.Add(obj);
            }
            map.Layers.Add(layer);
            Binding bd = new Binding("Layers") { Source = map };
            mc.SetBinding(ItemsControl.ItemsSourceProperty, bd);
        }
    }
}
