using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace gEngine.View
{
    /// <summary>
    /// LayerControl.xaml 的交互逻辑
    /// </summary>
    public partial class LayerControl : ItemsControl
    {
        public LayerControl()
        {
            InitializeComponent();
        }

        public Canvas Root
        {
            get
            {
                return FindChild.FindVisualChild<Canvas>(this, "layerpanel");
            }
        }

        public Rect GetRect()
        {
            return ViewUtil.GetTypeRect<ObjectControl>(Root);
        }

    }
}
