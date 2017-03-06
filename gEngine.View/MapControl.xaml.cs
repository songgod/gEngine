using System.Windows;
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
            Rect r = Rect.Empty;
            for (int i = 0; i < Items.Count; i++)
            {
                LayerControl lc = GetLayerControl(i);
                Rect rect = lc.GetRect();
                r.Union(rect);
            }
            if (r.IsEmpty)
                return;

            for (int i = 0; i < Items.Count; i++)
            {
                LayerControl lc = GetLayerControl(i);
                ViewUtil.ZoomtoExtent(lc.Root, r);
            }
        }
    }
}
