using gEngine.Graph.Interface;
using gEngine.View;
using System;
using System.Collections.Generic;
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

namespace gEngine.Project.Controls
{
    /// <summary>
    /// LayerMgrControl.xaml 的交互逻辑
    /// </summary>
    public partial class LayerMgrControl : UserControl
    {
        public LayerMgrControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
      
        public IMap MapSource
        {
            get
            {
                return (IMap) GetValue(MapSourceProperty);
            }
            set
            {

                SetValue(MapSourceProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for MapSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapSourceProperty =
            DependencyProperty.Register("MapSource", typeof(IMap), typeof(LayerMgrControl));

        public MapsControl mc
        {
            get
            {
                return (MapsControl) GetValue(mcProperty);
            }
            set
            {
                SetValue(mcProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for MapSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty mcProperty =
            DependencyProperty.Register("mc", typeof(MapsControl), typeof(LayerMgrControl));

        private void eyeImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image) sender;
            if (image.Source == ImageHelper.VisibleIcon)
                image.Source = ImageHelper.InVisibleIcon;
            else
                image.Source = ImageHelper.VisibleIcon;
        }

        private void editImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image) sender;
            if (image.Source == ImageHelper.EditableIcon)
                image.Source = ImageHelper.UnEditableIcon;
            else
                image.Source = ImageHelper.EditableIcon;
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (lbLayers.SelectedIndex > -1)
                MapSource.Layers.RemoveAt(lbLayers.SelectedIndex);
        }

        private void btnEmpty_Click(object sender, RoutedEventArgs e)
        {
            if (MapSource == null)
                return;
            MapSource.Layers.Clear();
        }

        private void tbName_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Binding bd = new Binding("SelectedIndex");
            bd.Source = this.lbLayers;
            bd.Converter = new ActiveLayerIndexConverter();
            bd.ConverterParameter = mc;
            bd.Mode = BindingMode.OneWay;
            mc.ActiveMapControl.ActiveLayerControl.SetBinding(LayerControl.ItemsSourceProperty,bd);
        }
    }

    public class ActiveLayerIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int index = (int) value;
            if (index < 0)
                return null;
            ((MapsControl) parameter).ActiveMapControl.ActiveLayerControlIndex = index;
            LayerControl lc = ((MapsControl) parameter).ActiveMapControl.ActiveLayerControl;
            return lc.LayerContext;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

