using gEngine.Data.Ge.Txt;
using gEngine.Graph.Ge;
using gEngine.Util.Ge.Column;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using gEngine.View;
using gEngine.Util.Ge.Section;
using gEngine.Manipulator.Ge.Section;
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
        #endregion

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

            IObjects objs = wc.Create(tw);
            foreach (IObject obj in objs)
            {
                layer.Objects.Add(obj);
            }
            map.Layers.Add(layer);

            Binding bd = new Binding("Layers") { Source = map };
            mc.SetBinding(ItemsControl.ItemsSourceProperty, bd);
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ManipulatorSetter.SetManipulator(new DrawCurveManipulator(), mc.GetLayerControl(0));
        }
    }
}
