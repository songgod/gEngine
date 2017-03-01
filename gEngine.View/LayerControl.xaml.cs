using System.Windows.Controls;

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
        
    }
}
