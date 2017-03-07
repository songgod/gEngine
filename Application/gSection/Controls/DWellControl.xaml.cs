using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Util.Ge.Column;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using gEngine.View;
using System.Windows.Interactivity;

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
            get { return lyControl; }
            set { lyControl = (LayerControl)value; }
        }

        Behavior<UIElement> IView.ManipulatorBehavior
        {
            get;set;
        }
        #endregion

        public DWellControl()
        {
            InitializeComponent();
            InitWell();
        }

        private void InitWell()
        {
            Layer layer = new Layer();

            TxtWell tw = new TxtWell() { TxtFile = "D:\\Data\\MulWellColumnDataNew.txt" };
            WellCreator wc = new WellCreator();
            layer.Objects = wc.Create(tw);

            Binding bd = new Binding("Objects") { Source = layer };
            lyControl.SetBinding(ItemsControl.ItemsSourceProperty, bd);
        }
    }
}
