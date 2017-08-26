using gEngine.Graph.Interface;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.View
{
    public delegate void LayerManipulatorChanged(LayerControl oc, string manipulator);
    /// <summary>
    /// LayerControl.xaml 的交互逻辑
    /// </summary>
    public partial class LayerControl : ItemsControl
    {
        const string LayerPanelName = "PART_LayerPanel";
        const string ObjectControlName = "PART_ObjectControl";
        public LayerControl()
        {
            InitializeComponent();
            Binding bdvisible = new Binding("LayerContext.Visible") { Converter = new BooleanToVisibilityConverter(), Mode=BindingMode.TwoWay, Source=this };
            BindingOperations.SetBinding(this, VisibilityProperty, bdvisible);
            Binding bdmanipulator = new Binding("LayerContext.Manipulator") { Mode = BindingMode.TwoWay, Source = this };
            BindingOperations.SetBinding(this, MainpulatorProperty, bdmanipulator);
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

        public static event LayerManipulatorChanged OnManipulatorChanged;

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

        public MapControl Owner
        {
            get
            {
                return FindParent.FindVisualParent<MapControl>(this);
            }
        }
    }
}
