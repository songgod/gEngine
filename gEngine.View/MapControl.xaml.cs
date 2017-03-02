using System.Windows.Controls;
using System.Windows.Media;

namespace gEngine.View
{
    /// <summary>
    /// MapControl.xaml 的交互逻辑
    /// </summary>
    public partial class MapControl : ItemsControl
    {
        public MapControl()
        {
            InitializeComponent();
        }

        public int LayerControlCount
        {
            get
            {
                return Items.Count;
            }
        }

        public LayerControl GetLayerControl(int index)
        {
            var item = ItemContainerGenerator.ContainerFromIndex(index);
            LayerControl lc = FindChild.FindVisualChild<LayerControl>(item, "layercontrol");
            return lc;
        }

        public void FullView()
        {
            System.Windows.Rect r = new System.Windows.Rect();
            for (int i = 0; i < Items.Count; i++)
            {
                LayerControl lc = GetLayerControl(i);
                System.Windows.Rect rect = VisualTreeHelper.GetDescendantBounds(lc.Root);
                if (i == 0)
                    r = rect;
                else
                    r.Union(rect);
            }
            for (int i = 0; i < Items.Count; i++)
            {
                LayerControl lc = GetLayerControl(i);
                ViewUtil.ZoomtoExtent(lc.Root, r);
            }
        }
    }
}
