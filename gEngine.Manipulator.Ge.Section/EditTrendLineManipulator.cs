using gEngine.View;
using GraphAlgo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator.Ge.Section
{
    public class EditTrendLineAdoner : Canvas
    {
        public event Action<PointCollection> OnLineChanged;
        private int NodeCount { get; set; }

        public PointCollection pc { get; set; }

        public EditTrendLineAdoner() { }

        #region Method

        public void Select(PointCollection pc)
        {
            Clear();

            if (pc == null || pc.Count == 0)
                return;

            NodeCount = pc.Count;
            Path Track = CreateTrack(pc);
            CreateConnects(Track);
            CreateNodes(Track);
        }

        private void Clear()
        {
            Children.Clear();
            NodeCount = 0;
        }

        private Path CreateTrack(PointCollection ps)
        {
            Path Track = new Path() { Stroke = new SolidColorBrush() { Color = Colors.LightGray }, StrokeThickness = 1, StrokeDashArray = new DoubleCollection() { 2, 3 } };
            Track.MouseLeftButtonDown += Track_MouseLeftButtonDown;
            PathGeometry pgTrack = new PathGeometry();
            PathFigure pfTrack = new PathFigure() { StartPoint = ps[0] };
            PolyBezierSegment psTrack = new PolyBezierSegment() { Points = new PointCollection(ps.Skip(1).Take(ps.Count - 1)) };
            pfTrack.Segments.Add(psTrack);
            pgTrack.Figures.Add(pfTrack);
            Track.Data = pgTrack;
            Children.Add(Track);
            return Track;
        }

        private void CreateConnects(Path track)
        {
            PathGeometry pgTrack = track.Data as PathGeometry;
            PathFigure pfTrack = pgTrack.Figures[0];
            PolyBezierSegment psTrack = pfTrack.Segments[0] as PolyBezierSegment;

            for (int i = 0; i < NodeCount; ++i)
            {
                if (i % 3 == 0)
                    continue;

                System.Windows.Shapes.Line l = new System.Windows.Shapes.Line() { Stroke = new SolidColorBrush() { Color = Colors.LightGray }, StrokeThickness = 1, StrokeDashArray = new DoubleCollection() { 1, 1 } };
                if (i % 3 == 1)
                {
                    PropertyPath ppx1 = new PropertyPath(string.Format("Points.[{0:G}].X", i - 1));
                    PropertyPath ppy1 = new PropertyPath(string.Format("Points.[{0:G}].Y", i - 1));
                    PropertyPath ppx2 = null;
                    PropertyPath ppy2 = null;
                    Binding bdingx1 = new Binding() { Path = ppx1, Source = psTrack, Mode = BindingMode.TwoWay };
                    Binding bdingy1 = new Binding() { Path = ppy1, Source = psTrack, Mode = BindingMode.TwoWay };
                    Binding bdingx2 = null;
                    Binding bdingy2 = null;
                    if (i == 1)
                    {
                        ppx2 = new PropertyPath("StartPoint.X");
                        ppy2 = new PropertyPath("StartPoint.Y");
                        bdingx2 = new Binding() { Path = ppx2, Source = pfTrack, Mode = BindingMode.TwoWay };
                        bdingy2 = new Binding() { Path = ppy2, Source = pfTrack, Mode = BindingMode.TwoWay };
                    }
                    else
                    {
                        ppx2 = new PropertyPath(string.Format("Points.[{0:G}].X", i - 2));
                        ppy2 = new PropertyPath(string.Format("Points.[{0:G}].Y", i - 2));
                        bdingx2 = new Binding() { Path = ppx2, Source = psTrack, Mode = BindingMode.TwoWay };
                        bdingy2 = new Binding() { Path = ppy2, Source = psTrack, Mode = BindingMode.TwoWay };
                    }

                    BindingOperations.SetBinding(l, System.Windows.Shapes.Line.X1Property, bdingx1);
                    BindingOperations.SetBinding(l, System.Windows.Shapes.Line.Y1Property, bdingy1);
                    BindingOperations.SetBinding(l, System.Windows.Shapes.Line.X2Property, bdingx2);
                    BindingOperations.SetBinding(l, System.Windows.Shapes.Line.Y2Property, bdingy2);

                }
                else
                {
                    PropertyPath ppx1 = new PropertyPath(string.Format("Points.[{0:G}].X", i - 1));
                    PropertyPath ppy1 = new PropertyPath(string.Format("Points.[{0:G}].Y", i - 1));
                    PropertyPath ppx2 = new PropertyPath(string.Format("Points.[{0:G}].X", i));
                    PropertyPath ppy2 = new PropertyPath(string.Format("Points.[{0:G}].Y", i));
                    BindingOperations.SetBinding(l, System.Windows.Shapes.Line.X1Property, new Binding() { Path = ppx1, Source = psTrack, Mode = BindingMode.TwoWay });
                    BindingOperations.SetBinding(l, System.Windows.Shapes.Line.Y1Property, new Binding() { Path = ppy1, Source = psTrack, Mode = BindingMode.TwoWay });
                    BindingOperations.SetBinding(l, System.Windows.Shapes.Line.X2Property, new Binding() { Path = ppx2, Source = psTrack, Mode = BindingMode.TwoWay });
                    BindingOperations.SetBinding(l, System.Windows.Shapes.Line.Y2Property, new Binding() { Path = ppy2, Source = psTrack, Mode = BindingMode.TwoWay });
                }
                Children.Add(l);
            }
        }

        private void CreateNodes(Path track)
        {
            PathGeometry pgTrack = track.Data as PathGeometry;
            PathFigure pfTrack = pgTrack.Figures[0];
            PolyBezierSegment psTrack = pfTrack.Segments[0] as PolyBezierSegment;
            for (int i = 0; i < NodeCount; ++i)
            {
                if (i % 3 == 0)
                {
                    Path control = new Path() { Stroke = new SolidColorBrush() { Color = Colors.Black }, StrokeThickness = 1.0, Fill = new SolidColorBrush() { Color = Colors.PaleVioletRed } };
                    control.Name = string.Format("C{0:G}", i);
                    control.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                    control.MouseMove += Control_MouseMove;
                    control.MouseLeftButtonUp += Control_MouseLeftButtonUp;
                    EllipseGeometry eg = new EllipseGeometry() { RadiusX = 5, RadiusY = 5 };
                    if (i == 0)
                    {

                        PropertyPath ppcenter = new PropertyPath("StartPoint");
                        Binding bding = new Binding() { Path = ppcenter, Source = pfTrack, Mode = BindingMode.TwoWay };
                        BindingOperations.SetBinding(eg, EllipseGeometry.CenterProperty, bding);
                    }
                    else
                    {
                        PropertyPath ppcenter = new PropertyPath(string.Format("Points.[{0:G}]", i - 1));
                        Binding bding = new Binding() { Path = ppcenter, Source = psTrack, Mode = BindingMode.TwoWay };
                        BindingOperations.SetBinding(eg, EllipseGeometry.CenterProperty, bding);
                    }
                    control.Data = eg;
                    Children.Add(control);
                }
                else
                {
                    Path ear = new Path() { Stroke = new SolidColorBrush() { Color = Colors.Black }, StrokeThickness = 1.0, Fill = new SolidColorBrush() { Color = Colors.LightBlue } };
                    ear.Name = string.Format("A{0:G}", i);
                    ear.MouseMove += Ear_MouseMove;
                    ear.MouseLeftButtonUp += Ear_MouseLeftButtonUp;
                    ear.MouseLeftButtonDown += Ear_MouseLeftButtonDown;
                    EllipseGeometry eg = new EllipseGeometry() { RadiusX = 2, RadiusY = 2 };
                    PropertyPath ppcenter = new PropertyPath(string.Format("Points.[{0:G}]", i - 1));
                    Binding bding = new Binding() { Path = ppcenter, Source = psTrack, Mode = BindingMode.TwoWay };
                    BindingOperations.SetBinding(eg, EllipseGeometry.CenterProperty, bding);
                    ear.Data = eg;
                    Children.Add(ear);
                }
            }
        }

        public int GetNodeID(Path node)
        {
            if (node == null)
                return -1;
            if (node.Name.Count() == 0)
                return -1;

            int id = -1;
            int.TryParse(node.Name.Substring(1), out id);
            return id;
        }

        private Path GetNode(int index)
        {
            if (index < 0 || index >= NodeCount)
                return null;

            int numoftrackandconnectline = ((NodeCount - 1) / 3) * 2 + 1;
            return numoftrackandconnectline + index >= Children.Count ? null : Children[numoftrackandconnectline + index] as Path;
        }

        private void SetNodePos(Path node, Point p)
        {
            if (node == null)
                return;

            EllipseGeometry eg = node.Data as EllipseGeometry;
            eg.Center = p;
        }

        private void SetNodePos(int index, Point p)
        {
            Path node = GetNode(index);
            SetNodePos(node, p);
        }

        public Point GetNodePos(Path node)
        {
            if (node == null)
                return new Point();
            EllipseGeometry eg = node.Data as EllipseGeometry;
            return eg.Center;
        }

        public Point GetNodePos(int index)
        {
            Path node = GetNode(index);
            return GetNodePos(node);
        }

        public bool IsFirstEar(int index)
        {
            if (index % 3 == 2)
                return true;
            return false;
        }

        #endregion

        #region Event

        private void Track_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PointCollection bezier0 = new PointCollection();
            PointCollection bezier1 = new PointCollection();
            Point p = e.GetPosition(this);
            bool flag = BezierDivision.Division(pc, p, out bezier0, out bezier1);
            if (!flag)
                return;

            if (bezier0.Count <= 0)
                return;

            bezier0.RemoveAt(bezier0.Count - 1);

            pc = new PointCollection(bezier0);
            foreach (Point pt in bezier1)
            {
                pc.Add(pt);
            }

            if (OnLineChanged != null)
                OnLineChanged.Invoke(pc);
            e.Handled = true;
        }

        private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path path = sender as Path;
            if (path != null)
                path.CaptureMouse();
            e.Handled = true;
        }

        private void Control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path path = sender as Path;
            if (path != null)
                path.ReleaseMouseCapture();

            int index = GetNodeID(path);
            Point p = GetNodePos(index);

            pc[index] = p;
            if (OnLineChanged != null)
                OnLineChanged.Invoke(pc);
            e.Handled = true;
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Path path = sender as Path;
                Point p = e.GetPosition(this);
                Vector offset = p - GetNodePos(path);
                int id = GetNodeID(path);
                SetNodePos(id, p);
                SetNodePos(id - 1, GetNodePos(id - 1) + offset);
                SetNodePos(id + 1, GetNodePos(id + 1) + offset);
                e.Handled = true;
            }
        }

        private void Ear_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path path = sender as Path;
            if (path != null)
                path.CaptureMouse();
            e.Handled = true;
        }

        private void Ear_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path path = sender as Path;
            if (path != null)
                path.ReleaseMouseCapture();

            int index = GetNodeID(path);
            Point p = GetNodePos(index);

            pc[index] = p;
            if (OnLineChanged != null)
                OnLineChanged.Invoke(pc);
            e.Handled = true;
        }

        private void Ear_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Path ear = sender as Path;
                Point p = e.GetPosition(this);
                SetNodePos(ear, p);

                if ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.Control)
                {
                    int id = GetNodeID(ear);
                    Path ctrl = null;
                    Path pairear = null;
                    if (IsFirstEar(id))
                    {
                        ctrl = GetNode(id + 1);
                        pairear = GetNode(id + 2);
                    }
                    else
                    {
                        ctrl = GetNode(id - 1);
                        pairear = GetNode(id - 2);
                    }

                    if (pairear != null)
                    {
                        Vector v = GetNodePos(ear) - GetNodePos(ctrl);
                        v.Normalize();
                        double dis = (GetNodePos(pairear) - GetNodePos(ctrl)).Length;
                        Vector vp = v * dis * -1;
                        SetNodePos(pairear, vp + GetNodePos(ctrl));
                    }
                }
                e.Handled = true;
            }
        }

        #endregion
    }

    public class EditTrendLineManipulator : ObjectManipulator
    {
        protected EditTrendLineAdoner EditTrendLineAdoner { get; private set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;
            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;
            if (mc == null)
                return;

            PointCollection ps = ((gEngine.Graph.Ge.Section.TrendLine)oc.DataContext).Points;
            EditTrendLineAdoner = new EditTrendLineAdoner();
            mc.EditLayer.Children.Add(EditTrendLineAdoner);
            EditTrendLineAdoner.OnLineChanged += EditTrendLineAdoner_OnLineChanged;
            EditTrendLineAdoner.pc = ps;
            EditTrendLineAdoner.Select(ps);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            MapControl mc = this.AssociatedObject.Owner.Owner;
            mc.EditLayer.Children.Remove(EditTrendLineAdoner);
        }

        #region Event

        private void EditTrendLineAdoner_OnLineChanged(PointCollection ps)
        {
            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;
            if (mc == null)
                return;

            ((gEngine.Graph.Ge.Section.TrendLine)oc.DataContext).Points = ps;

            EditTrendLineAdoner.Select(ps);
        }

        #endregion
    }

    public class EditTrendLineFactory : IObjectManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "EditTrendLineManipulator";
            }
        }

        public Type SupportIObjectType
        {
            get
            {
                return typeof(gEngine.Graph.Ge.Section.TrendLine);
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            EditTrendLineManipulator em = new EditTrendLineManipulator();
            return em;
        }
    }
}
