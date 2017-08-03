using gEngine.Graph.Interface;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace gEngine.View
{
    /// <summary>
    /// ObjectControl.xaml 的交互逻辑
    /// </summary>
    public partial class ObjectControl : ContentControl
    {
        public ObjectControl()
        {
            InitializeComponent();
            Binding bd = new Binding("Visible") { Converter = new BooleanToVisibilityConverter(), Mode = BindingMode.TwoWay };
            BindingOperations.SetBinding(this, VisibilityProperty, bd);
            Binding bdselect = new Binding("IsSeclected") { Mode = BindingMode.TwoWay };
            BindingOperations.SetBinding(this, IsSelectedProperty, bdselect);
        }

        public LayerControl Owner
        {
            get
            {
                return FindParent.FindVisualParent<LayerControl>(this);
            }
        } 
        
        public IObject ObjectContext
        {
            get
            {
                return FindContext.Find<IObject>(this);
            }
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        static void IsSelectedPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs arg)
        {
            //ObjectControl oc = (ObjectControl)obj;
            //bool isSelected = (bool)arg.NewValue;
            //if(isSelected)
            //{
            //    string dir = Directory.GetCurrentDirectory();
            //    string qstr = dir + "\\gEngine.Manipulator.dll";
            //    Assembly ass = Assembly.Load(qstr);

            //}
            //else
            //{

            //}
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(ObjectControl), new PropertyMetadata(false,new PropertyChangedCallback(IsSelectedPropertyChanged)));
    }
}
