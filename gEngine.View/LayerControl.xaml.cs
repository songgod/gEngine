using gEngine.Graph.Interface;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.View
{
    public delegate void ManipulatorChanged(LayerControl oc, string manipulator);
    /// <summary>
    /// LayerControl.xaml 的交互逻辑
    /// </summary>
    public partial class LayerControl : ItemsControl
    {
        public LayerControl()
        {
            InitializeComponent();
            Binding bd = new Binding("LayerContext.Visible") { Converter = new BooleanToVisibilityConverter(), Mode=BindingMode.TwoWay, Source=this };
            BindingOperations.SetBinding(this, VisibilityProperty, bd);
        }



        public ILayer LayerContext
        {
            get { return (ILayer)GetValue(LayerContextProperty); }
            set { SetValue(LayerContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LayerContext.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayerContextProperty =
            DependencyProperty.Register("LayerContext", typeof(ILayer), typeof(LayerControl), new PropertyMetadata(null));



        public string Manipulator
        {
            get { return (string)GetValue(MainpulatorProperty); }
            set { SetValue(MainpulatorProperty, value); }
        }

        public static event ManipulatorChanged OnManipulatorChanged;

        static void OnManipulatorChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs arg)
        {
            LayerControl lc = (LayerControl)obj;
            string manipulator = (string)arg.NewValue;
            
            if (OnManipulatorChanged != null)
                OnManipulatorChanged.Invoke(lc, manipulator);
        }
        // Using a DependencyProperty as the backing store for Mainpulator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainpulatorProperty =
            DependencyProperty.Register("Mainpulator", typeof(string), typeof(LayerControl), new PropertyMetadata("", new PropertyChangedCallback(OnManipulatorChangedCallback)));



        public Canvas Root
        {
            get
            {
                return FindChild.FindVisualChild<Canvas>(this, "layerpanel");
            }
        }

        public MapControl Owner
        {
            get
            {
                return FindParent.FindVisualParent<MapControl>(this);
            }
        }

        public int ObjectControlCount
        {
            get
            {
                return Items.Count;
            }
        }

        public ObjectControl GetObjectControl(int index)
        {
            if (index < 0)
                return null;
            var item = ItemContainerGenerator.ContainerFromIndex(index);
            ObjectControl oc = FindChild.FindVisualChild<ObjectControl>(item, "objectcontrol");
            return oc;
        }

        public Rect GetRect()
        {
            return ViewUtil.GetTypeRect<ObjectControl>(Root);
        }

    }
}
