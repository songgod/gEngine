using DevExpress.Xpf.Bars;
using gEngine.Graph.Interface;
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
            List<string> ls = gEngine.Graph.Interface.Registry.GetLayerTypes("Ge");
            foreach (string str in ls)
            {
                BarButtonItem btn = new BarButtonItem();
                btn.Content = str;
                btn.ItemClick += Btn_ItemClick;
                barSubItem.Items.Add(btn);
            }
           
        }
        public bool isEye = false;
        public bool isEdit = false;
        private void Btn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MapSource == null) return;
            BarButtonItem btn = (BarButtonItem)sender;
            ILayer layer = gEngine.Graph.Interface.Registry.CreateLayer("Ge", btn.Content.ToString());
            layer.Name = btn.Content.ToString();
            MapSource.Layers.Add(layer);
        }

        public IMap MapSource
        {
            get
            {
                return (IMap)GetValue(MapSourceProperty);
            }
            set
            {

                SetValue(MapSourceProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for MapSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapSourceProperty =
            DependencyProperty.Register("MapSource", typeof(IMap), typeof(LayerMgrControl));

        private void eyeImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            if (image.Source == ImageHelper.VisibleIcon)
                image.Source = ImageHelper.InVisibleIcon;
            else
                image.Source = ImageHelper.VisibleIcon;
        }

        private void editImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            if (image.Source == ImageHelper.EditableIcon)
                image.Source = ImageHelper.UnEditableIcon;
            else
                image.Source = ImageHelper.EditableIcon;
        }

        private void btnDel_Click(object sender, ItemClickEventArgs e)
        {
            if(lbLayers.SelectedIndex>-1)
                MapSource.Layers.RemoveAt(lbLayers.SelectedIndex);
        }

        private void btnEmpty_Click(object sender, ItemClickEventArgs e)
        {
            if (lbLayers.SelectedIndex > -1)
            {
                MapSource.Layers[lbLayers.SelectedIndex].Objects.Clear();
            }
               
        }

        private void btnEye_Click(object sender, ItemClickEventArgs e)
        {
            if (MapSource == null) return;
            foreach (ILayer layer in MapSource.Layers)
            {
                layer.Visible = isEye;
            }
            isEye = !isEye;
                
        }

        private void btnEdit_Click(object sender, ItemClickEventArgs e)
        {
            if (MapSource == null) return;
            foreach (ILayer layer in MapSource.Layers)
            {
                layer.Editable = isEdit;
            }
            isEdit = !isEdit;
        }


    }
}

