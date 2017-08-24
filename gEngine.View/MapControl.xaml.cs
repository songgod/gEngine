using gEngine.Graph.Interface;
using gEngine.Util;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace gEngine.View
{
    /// <summary>
    /// MapControl.xaml 的交互逻辑
    /// </summary>
    public partial class MapControl : UserControl
    {
        public MapControl()
        {
            InitializeComponent();
            UndoRedoCommandManager = new UndoRedoCommandManager();
            Binding bd = new Binding("MapContext.Layers.CurrentIndex") {  Mode = BindingMode.TwoWay, Source = this };
            BindingOperations.SetBinding(this, SelectIndexProperty, bd);
        }

        public UndoRedoCommandManager UndoRedoCommandManager
        {
            get;set;
        }

        public IMap MapContext
        {
            get { return (IMap)GetValue(MapContextProperty); }
            set { SetValue(MapContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapContext.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapContextProperty =
            DependencyProperty.Register("MapContext", typeof(IMap), typeof(MapControl), new PropertyMetadata(null));



        public int SelectIndex
        {
            get { return (int)GetValue(SelectIndexProperty); }
            set { SetValue(SelectIndexProperty, value); }
        }

        static void OnSelectIndexChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs arg)
        {
            MapControl mc = (MapControl)obj;
            int pos = (int)arg.NewValue;

            mc.ActiveLayerControl = mc.GetLayerControl(pos);
        }

        // Using a DependencyProperty as the backing store for SelectIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectIndexProperty =
            DependencyProperty.Register("SelectIndex", typeof(int), typeof(MapControl), new PropertyMetadata(-1, new PropertyChangedCallback(OnSelectIndexChangedCallback)));



        public LayerControl ActiveLayerControl
        {
            get { return (LayerControl)GetValue(ActiveLayerControlProperty); }
            private set { SetValue(ActiveLayerControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActiveLayerControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActiveLayerControlProperty =
            DependencyProperty.Register("ActiveLayerControl", typeof(LayerControl), typeof(MapControl), new PropertyMetadata(null));



        public int LayerControlCount
        {
            get
            {
                return layeritemscontrol.Items.Count;
            }
        }

        public LayerControl GetLayerControl(int index)
        {
            if (index < 0)
                return null;
            var item = layeritemscontrol.ItemContainerGenerator.ContainerFromIndex(index);
            LayerControl lc = FindChild.FindVisualChild<LayerControl>(item, "layercontrol");
            return lc;
        }

        public Canvas EditLayer
        {
            get
            {
                return EditCanvas;
            }
        }

        public Canvas BackGroundLayer
        {
            get
            {
                return BackGroundCanvas;
            }
        }

        public void FullView()
        {
            Rect r = Rect.Empty;
            for (int i = 0; i < layeritemscontrol.Items.Count; i++)
            {
                LayerControl lc = GetLayerControl(i);
                Rect rect = lc.GetRect();
                r.Union(rect);
            }
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
