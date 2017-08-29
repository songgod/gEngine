using gEngine.Graph.Interface;
using gEngine.Util;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace gEngine.View
{
    public delegate void SelectLayerChanged(MapControl mc, int index);
    public delegate void MapManipulatorChanged(MapControl mc, string manipulator);
    /// <summary>
    /// MapControl.xaml 的交互逻辑
    /// </summary>
    public partial class MapControl : UserControl
    {
        const string BackGroundCanvasName = "PART_BackGroundCanvas";
        const string EditCanvasName = "PART_EditCanvas";
        const string MapPanelName = "PART_MapPanel";
        const string LayerControlName = "PART_Layercontrol";

        public MapControl()
        {
            InitializeComponent();
            Binding bdviewmatrix = new Binding("MapContext.ViewMatrix") { Source = this,Mode=BindingMode.TwoWay };
            BindingOperations.SetBinding(this, ViewMatrixProperty, bdviewmatrix);
            Binding bdsellyrindex = new Binding("MapContext.Layers.CurrentIndex") { Source = this, Mode = BindingMode.TwoWay };
            BindingOperations.SetBinding(this, SelectLayerIndexProperty, bdsellyrindex);
            Binding bdmanipulator = new Binding("MapContext.Manipulator") { Source = this, Mode = BindingMode.TwoWay };
            BindingOperations.SetBinding(this, ManipulatorProperty, bdmanipulator);
        }

        public IMap MapContext
        {
            get { return (IMap)GetValue(MapContextProperty); }
            set { SetValue(MapContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapContext.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapContextProperty =
            DependencyProperty.Register("MapContext", typeof(IMap), typeof(MapControl), new PropertyMetadata(null));



        public string Manipulator
        {
            get { return (string)GetValue(ManipulatorProperty); }
            set { SetValue(ManipulatorProperty, value); }
        }

        public static event MapManipulatorChanged OnManipulatorChanged;
        static void OnManipulatorChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs arg)
        {
            MapControl mc = (MapControl)obj;
            string manipulator = (string)arg.NewValue;

            if (OnManipulatorChanged != null)
                OnManipulatorChanged.Invoke(mc, manipulator);
        }

        // Using a DependencyProperty as the backing store for Manipulator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ManipulatorProperty =
            DependencyProperty.Register("Manipulator", typeof(string), typeof(MapControl), new PropertyMetadata("",new PropertyChangedCallback(OnManipulatorChangedCallback)));


        public int SelectLayerIndex
        {
            get { return (int)GetValue(SelectLayerIndexProperty); }
            set { SetValue(SelectLayerIndexProperty, value); }
        }

        public static event SelectLayerChanged OnSelectLayerChanged;
        static void OnSelectLayerChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs arg)
        {
            MapControl mc = (MapControl)obj;
            int index = (int)arg.NewValue;

            if (OnSelectLayerChanged != null)
                OnSelectLayerChanged.Invoke(mc, index);
        }

        // Using a DependencyProperty as the backing store for SelectLayerIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectLayerIndexProperty =
            DependencyProperty.Register("SelectLayerIndex", typeof(int), typeof(MapControl), new PropertyMetadata(-1, new PropertyChangedCallback(OnSelectLayerChangedCallback)));




        public Matrix ViewMatrix
        {
            get { return (Matrix)GetValue(ViewMatrixProperty); }
            set { SetValue(ViewMatrixProperty, value); }
        }

        static void ViewMatrixPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs arg)
        {
            MapControl mc = (MapControl)obj;
            Matrix m = (Matrix)arg.NewValue;
            if (m.IsIdentity)
                mc.FullView();
            else
            {
                MatrixTransform ft = new MatrixTransform(m);
                mc.root.RenderTransform = ft;
            }
        }

        // Using a DependencyProperty as the backing store for ViewMatrix.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewMatrixProperty =
            DependencyProperty.Register("ViewMatrix", typeof(Matrix), typeof(MapControl), new PropertyMetadata(Matrix.Identity, new PropertyChangedCallback(ViewMatrixPropertyChanged)));



        public Canvas EditLayer
        {
            get
            {
                return PART_BackGroundCanvas;
            }
        }

        public Canvas BackGroundLayer
        {
            get
            {
                return PART_EditCanvas;
            }
        }

        public void FullView()
        {
            Rect r = ViewUtil.GetTypeRect<ObjectControl>(root);
            if (r.IsEmpty)
                return;

            ViewUtil.ZoomtoExtent(root, r);
        }

        public void Zoom(Point center, Vector scale)
        {
            Transform t = root.RenderTransform;
            ScaleTransform st = new ScaleTransform() { CenterX = center.X, CenterY = center.Y, ScaleX = scale.X, ScaleY = scale.Y };
            Matrix mt = t.Value;
            Matrix mst = st.Value;
            Matrix m = mst * mt;
            MatrixTransform ft = new MatrixTransform(m);
            root.RenderTransform = ft;
        }

        public void Move(Vector trans)
        {
            Transform t = root.RenderTransform;

            TranslateTransform tt = new TranslateTransform() { X = trans.X, Y = trans.Y };
            Matrix mt = t.Value;
            Matrix mtt = tt.Value;
            Matrix m = mtt * mt;
            MatrixTransform ft = new MatrixTransform(m);
            root.RenderTransform = ft;
        }

        public Point Dp2LP(Point p)
        {
            return TranslatePoint(p, root);
        }

        public Vector Dp2LP(Vector v)
        {
            Point p0 = TranslatePoint(new Point(0,0), root);
            Point p1 = TranslatePoint(new Point(v.X, v.Y), root);
            return p1 - p0;
        }

        public double Dp2LP(double v)
        {
            Point p0 = new Point(0, 0);
            Point p1 = new Point(v, 0);
            p0 = TranslatePoint(p0, root);
            p1 = TranslatePoint(p1, root);
            return (p1-p0).Length;
        }
        public Point Lp2Dp(Point p)
        {
            return root.TranslatePoint(p, this);
        }

        public Vector Lp2Dp(Vector v)
        {
            Point p0 = root.TranslatePoint(new Point(0, 0), this);
            Point p1 = root.TranslatePoint(new Point(v.X, v.Y), this);
            return p1 - p0;
        }

        public double Lp2DP(double v)
        {
            Point p0 = new Point(0, 0);
            Point p1 = new Point(v, 0);
            p0 = root.TranslatePoint(p0, this);
            p1 = root.TranslatePoint(p1, this);
            return (p1 - p0).Length;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FullView();
        }
    }
}
