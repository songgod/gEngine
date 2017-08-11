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
        }

        public void BuildListViewSource()
        {
            if (MapSource == null)
            {
                lbLayers.ItemsSource = null;
                return;
            }  
            List<LayerCtrlObject> soureList = new List<LayerCtrlObject>();
            ILayers layers = this.MapSource.Layers;
            foreach (ILayer layer in layers)
            {
                LayerCtrlObject lco = new LayerCtrlObject();
                lco.Name = layer.Name;
                lco.NewLayerImageName = "Small/newlayer.png";
                lco.NewLayerImageOpacity = layer.NewLayer ? 1.0 : 0.2;

                lco.NewUniversallyLayerImageName = "Small/NewUniversallyLayer.png";
                lco.NewUniversallyLayerImageOpacity = layer.NewLayer ? 1.0 : 0.2;
                lco.NewWellLocationLayerImageName = "Small/NewWellLocationLayer.png";
                lco.NewWellLocationLayerImageOpacity = layer.NewLayer ? 1.0 : 0.2;
                lco.NewSectionMapLayerImageName = "Small/NewSectionMapLayer.png";
                lco.NewSectionMapLayerImageOpacity = layer.NewLayer ? 1.0 : 0.2;
                lco.EmptyLayerImageName = "Small/emptylayer.png";
                lco.EmptyLayerImageOpacity = layer.NewLayer ? 1.0 : 0.2;


                lco.VisibalityImageName = "Small/eye.png";
                lco.VisibalityImageOpacity = layer.Visible ? 1.0 : 0.2;
                lco.EditImageName= "Small/Pencil.png";
                lco.EditImageOpacity = layer.Editable ? 1.0 : 0.2;
                lco.LayerOpacity = layer.Opacity;
                lco.DeleteImageName = "Small/delete.png";
                lco.DeleteImageOpacity = layer.Delete ? 1.0 : 0.2;
                soureList.Add(lco);

                //foreach (IObject obj in layer.Objects)
                //{
                //    lco = new LayerCtrlObject();
                //    lco.Name = obj.Name;
                //    lco.VisibalityImageName = "Small/eye.png";
                //    lco.VisibalityImageOpacity = obj.Visible ? 1.0 : 0.2;
                //    lco.EditImageName = "Small/Pencil.png";
                //    lco.EditImageOpacity = obj.Editable ? 1.0 : 0.2;
                //    lco.LayerOpacity = obj.Opacity;
                //    soureList.Add(lco);
                //}
            }
            lbLayers.ItemsSource = soureList;
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

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

    }

    public class LayerCtrlObject : DependencyObject
    {
        public string Name { get; set; }
        public string NewLayerImageName { get; set; }
        public double NewLayerImageOpacity
        {
            get
            {
                return (double)GetValue(NewLayerImageOpacityProperty);
            }
            set
            {
                SetValue(NewLayerImageOpacityProperty, value);
            }
        }
        public static readonly DependencyProperty NewLayerImageOpacityProperty =
            DependencyProperty.Register("NewLayerImageOpacity", typeof(double), typeof(LayerCtrlObject));


        public string NewUniversallyLayerImageName { get; set; }
        public double NewUniversallyLayerImageOpacity
        {
            get
            {
                return (double)GetValue(NewUniversallyLayerImageOpacityProperty);
            }
            set
            {
                SetValue(NewUniversallyLayerImageOpacityProperty, value);
            }
        }
        public static readonly DependencyProperty NewUniversallyLayerImageOpacityProperty =
            DependencyProperty.Register("NewUniversallyLayerImageOpacity", typeof(double), typeof(LayerCtrlObject));

        public string NewWellLocationLayerImageName { get; set; }
        public double NewWellLocationLayerImageOpacity
        {
            get
            {
                return (double)GetValue(NewWellLocationLayerImageOpacityProperty);
            }
            set
            {
                SetValue(NewWellLocationLayerImageOpacityProperty, value);
            }
        }
        public static readonly DependencyProperty NewWellLocationLayerImageOpacityProperty =
            DependencyProperty.Register("NewWellLocationLayerImageOpacity", typeof(double), typeof(LayerCtrlObject));

        public string NewSectionMapLayerImageName { get; set; }
        public double NewSectionMapLayerImageOpacity
        {
            get
            {
                return (double)GetValue(NewSectionMapLayerImageOpacityProperty);
            }
            set
            {
                SetValue(NewSectionMapLayerImageOpacityProperty, value);
            }
        }
        public static readonly DependencyProperty NewSectionMapLayerImageOpacityProperty =
            DependencyProperty.Register("NewSectionMapLayerImageOpacity", typeof(double), typeof(LayerCtrlObject));

        public string VisibalityImageName { get; set; }

        public double VisibalityImageOpacity
        {
            get
            {
                return (double)GetValue(VisibalityImageOpacityProperty);
            }
            set
            {
                SetValue(VisibalityImageOpacityProperty, value);
            }
        }

        public static readonly DependencyProperty VisibalityImageOpacityProperty =
            DependencyProperty.Register("VisibalityImageOpacity", typeof(double), typeof(LayerCtrlObject));

        public string EditImageName { get; set; }
        public double EditImageOpacity
        {
            get
            {
                return (double)GetValue(EditImageOpacityProperty);
            }
            set
            {
                SetValue(EditImageOpacityProperty, value);
            }
        }
        public static readonly DependencyProperty EditImageOpacityProperty =
            DependencyProperty.Register("EditImageOpacity", typeof(double), typeof(LayerCtrlObject));


        public string DeleteImageName { get; set; }

        public double DeleteImageOpacity
        {
            get
            {
                return (double)GetValue(DeleteImageOpacityProperty);
            }
            set
            {
                SetValue(DeleteImageOpacityProperty, value);
            }
        }

        public static readonly DependencyProperty DeleteImageOpacityProperty =
            DependencyProperty.Register("DeleteImageOpacity", typeof(double), typeof(LayerCtrlObject));

        public string EmptyLayerImageName { get; set; }

        public double EmptyLayerImageOpacity
        {
            get
            {
                return (double)GetValue(EmptyLayerImageOpacityProperty);
            }
            set
            {
                SetValue(EmptyLayerImageOpacityProperty, value);
            }
        }

        public static readonly DependencyProperty EmptyLayerImageOpacityProperty =
            DependencyProperty.Register("EmptyLayerImageOpacity", typeof(double), typeof(LayerCtrlObject));

        public double LayerOpacity
        {
            get
            {
                return (double)GetValue(LayerOpacityProperty);
            }
            set
            {
                SetValue(LayerOpacityProperty, value);
            }
        }

        public static readonly DependencyProperty LayerOpacityProperty =
            DependencyProperty.Register("LayerOpacity", typeof(double), typeof(LayerCtrlObject));

       

    }


}

