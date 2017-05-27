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
                lco.VisibalityImageName = "Small/eye.png";
                lco.VisibalityImageOpacity = layer.Visible ? 1.0 : 0.2;
                lco.EditImageName= "Small/Pencil.png";
                lco.EditImageOpacity = layer.Editable ? 1.0 : 0.2;
                soureList.Add(lco);

                foreach (IObject obj in layer.Objects)
                {
                    lco = new LayerCtrlObject();
                    lco.Name = obj.Name;
                    lco.VisibalityImageName = "Small/eye.png";
                    lco.VisibalityImageOpacity = obj.Visible ? 1.0 : 0.2;
                    lco.EditImageName = "Small/Pencil.png";
                    lco.EditImageOpacity = obj.Editable ? 1.0 : 0.2;
                    soureList.Add(lco);
                }
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


    }

    public class LayerCtrlObject : DependencyObject
    {
        public string Name { get; set; }

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

    }


}

