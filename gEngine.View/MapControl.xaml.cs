using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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

        /// <summary>
        /// LayerControl外层的AdornerDecorator
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public AdornerDecorator GetLayerControlAdornerDecorator(int index)
        {
            var item = ItemContainerGenerator.ContainerFromIndex(index);
            LayerControl lc = FindChild.FindVisualChild<LayerControl>(item, "layercontrol");
            AdornerDecorator ad = VisualTreeHelper.GetParent(lc) as AdornerDecorator;
            return ad;
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
                //AdornerDecorator转换
                AdornerDecorator ad = GetLayerControlAdornerDecorator(i);
                Matrix m = Matrix.Identity;
                ad.RenderTransform = new MatrixTransform(m);
            }
        }
    }
}
