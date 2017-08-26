using DevExpress.Xpf.Core;
using gEngine.Graph.Interface;
using gEngine.Project.Converter;
using gEngine.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.Project.Controls
{
    /// <summary>
    /// MapsControl.xaml 的交互逻辑
    /// </summary>
    public partial class MapsControl : DXTabControl
    {
        public MapsControl()
        {
            InitializeComponent();
            this.View.HideButtonShowMode = HideButtonShowMode.InAllTabs;
            this.View.RemoveTabItemsOnHiding = true;
        }

        public IMaps MapsSource
        {
            get { return (IMaps)GetValue(MapsSourceProperty); }
            set { SetValue(MapsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapsSourceProperty =
            DependencyProperty.Register("MapsSource", typeof(IMaps), typeof(MapsControl));
    }
}

