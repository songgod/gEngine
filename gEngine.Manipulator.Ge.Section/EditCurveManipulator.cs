using gTopology;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator.Ge.Section
{
    public class EditCurveAdorner : Adorner
    {
        private Canvas Container { get; set; }
        
        private int NodeCount { get; set; }

        public EditCurveAdorner(UIElement adornedElement) : base(adornedElement)
        {
            Container = new Canvas();
            this.AddVisualChild(Container);
        }

        private void Clear()
        {
            Container.Children.Clear();
            NodeCount = 0;
        }

        private Path CreateTrack(gTopology.Line line)
        {
            Path Track = new Path() { Stroke = new SolidColorBrush() { Color = Colors.LightGray }, StrokeThickness = 1.0, StrokeDashArray = new DoubleCollection() { 2, 3 } };
            Track.MouseLeftButtonDown += Track_MouseLeftButtonDown;
            PathGeometry pgTrack = new PathGeometry();
            PathFigure pfTrack = new PathFigure() { StartPoint = line.StartPoint };
            PolyBezierSegment psTrack = new PolyBezierSegment() { Points = new PointCollection(line.PointsWithoutFirst.ToArray()) };
            pfTrack.Segments.Add(psTrack);
            pgTrack.Figures.Add(pfTrack);
            Track.Data = pgTrack;
            Container.Children.Add(Track);
            return Track;
        }

        private void Track_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.AdornedElement.RaiseEvent(e);
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

                System.Windows.Shapes.Line l = new System.Windows.Shapes.Line() { Stroke = new SolidColorBrush() { Color = Colors.LightGray }, StrokeThickness = 0.5, StrokeDashArray = new DoubleCollection() { 1, 1 } };
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
                Container.Children.Add(l);
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
                    control.MouseMove += Control_MouseMove;
                    control.MouseLeftButtonUp += Control_MouseLeftButtonUp;
                    control.MouseRightButtonUp += Control_MouseRightButtonUp;
                    control.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                    EllipseGeometry eg = new EllipseGeometry() { RadiusX = 10, RadiusY = 10 };
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
                    Container.Children.Add(control);
                }
                else
                {
                    Path ear = new Path() { Stroke = new SolidColorBrush() { Color = Colors.Black }, StrokeThickness = 1.0, Fill = new SolidColorBrush() { Color = Colors.LightBlue } };
                    ear.Name = string.Format("A{0:G}", i);
                    ear.MouseMove += Ear_MouseMove;
                    ear.MouseLeftButtonUp += Ear_MouseLeftButtonUp;
                    ear.MouseRightButtonUp += Ear_MouseRightButtonUp;
                    ear.MouseLeftButtonDown += Ear_MouseLeftButtonDown;
                    EllipseGeometry eg = new EllipseGeometry() { RadiusX = 5, RadiusY = 5 };
                    PropertyPath ppcenter = new PropertyPath(string.Format("Points.[{0:G}]", i - 1));
                    Binding bding = new Binding() { Path = ppcenter, Source = psTrack, Mode = BindingMode.TwoWay };
                    BindingOperations.SetBinding(eg, EllipseGeometry.CenterProperty, bding);
                    ear.Data = eg;
                    Container.Children.Add(ear);
                }
            }
        }

        private void Ear_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.AdornedElement.RaiseEvent(e);
        }

        private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.AdornedElement.RaiseEvent(e);
        }

        private void Ear_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.AdornedElement.RaiseEvent(e);
        }

        private void Control_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.AdornedElement.RaiseEvent(e);
        }

        private void Ear_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.AdornedElement.RaiseEvent(e);
        }

        private void Control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.AdornedElement.RaiseEvent(e);
        }

        private Path GetNode(int index)
        {
            if (index < 0 || index >= NodeCount)
                return null;

            int numoftrackandconnectline = ((NodeCount - 1) / 3) * 2 + 1;
            return numoftrackandconnectline + index >= Container.Children.Count ? null : Container.Children[numoftrackandconnectline + index] as Path;
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

        public bool IsControl(int index)
        {
            if (index % 3 == 0)
                return true;
            return false;
        }

        public bool IsFirstEar(int index)
        {
            if (index % 3 == 2)
                return true;
            return false;
        }

        public bool IsSecondEar(int index)
        {
            if (index % 3 == 1)
                return true;
            return false;
        }

        public bool IsNode(Path node)
        {
            if (node == null)
                return false;
            if (node.Name.Count() == 0)
                return false;

            int id = -1;
            if (int.TryParse(node.Name.Substring(1), out id))
                return true;
            return false;
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

        public bool IsEndNode(int index)
        {
            if (index == 0 || index==NodeCount-1)
                return true;
            return false;
        }

        public void Select(gTopology.Line line)
        {
            Clear();

            if (line == null || line.Points.Count==0)
                return;

            NodeCount = line.Points.Count;
            Path Track = CreateTrack(line);
            CreateConnects(Track);
            CreateNodes(Track);
        }

        private void Ear_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Path ear = sender as Path;
                Point p = e.GetPosition(this.AdornedElement);
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
            }
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Path path = sender as Path;
                Point p = e.GetPosition(this.AdornedElement);
                Vector offset = p - GetNodePos(path);
                SetNodePos(path, p);
                int id = GetNodeID(path);
                SetNodePos(id - 1, GetNodePos(id - 1) + offset);
                SetNodePos(id + 1, GetNodePos(id + 1) + offset);
            }
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return Container;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Container.Arrange(new Rect(finalSize));
            
            return base.ArrangeOverride(finalSize);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Container.Measure(constraint);
            return Container.DesiredSize;
        }
    }

    public class EditCurveManipulator : ManipulatorBase
    {
        private gTopology.Line SelectLine { get; set; }
        protected EditCurveAdorner EditAdorner { get; private set; }

        public Graph Graph
        {
            get
            {
                ContentPresenter p = VisualTreeHelper.GetParent(this.AssociatedObject) as ContentPresenter;
                if (p == null)
                    return null;
                Graph graph = p.DataContext as Graph;
                return graph;
            }
        }

        public double Tolerance
        {
            get
            {
                ContentPresenter p = VisualTreeHelper.GetParent(this.AssociatedObject) as ContentPresenter;
                if (p == null)
                    return 0;

                return CalcTolerance.GetTolerance(p);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;

            EditAdorner = new EditCurveAdorner(this.AssociatedObject);
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);
            adornerLayer.Add(EditAdorner);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);
            adornerLayer.Remove(this.EditAdorner);
        }

        public override void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Graph graph = Graph;
            if (graph == null)
                return;

            Path node = e.OriginalSource as Path;
            if (node != null && EditAdorner.IsNode(node))
                return;

            Topology editor = new Topology(graph);
            Point pos = e.GetPosition(this.AssociatedObject);
            gTopology.Line line = editor.LinHit(pos, Tolerance);
            
            if (line != null)
            {
                if(SelectLine==line)
                {
                    SelectLine = editor.LinAddPoint(SelectLine, pos,Tolerance);
                    EditAdorner.Select(SelectLine);
                }
                else
                {
                    SelectLine = line;
                    EditAdorner.Select(line);
                }
            }
            else
            {
                SelectLine = null;
                EditAdorner.Select(null);
            }
            base.MouseLeftButtonDown(sender, e);
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            base.MouseMove(sender, e);
        }

        public override void MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Graph graph = Graph;
            if (graph == null)
                return;

            if (SelectLine == null)
                return;

            Topology editer = new Topology(graph);

            Path node = e.OriginalSource as Path;
            if (node != null && EditAdorner.IsNode(node))
            {
                int id = EditAdorner.GetNodeID(node);
                if (EditAdorner.IsControl(id))
                {
                    SelectLine = editer.LinRemovePoint(SelectLine, id);
                    EditAdorner.Select(SelectLine);
                }
            }
            base.MouseRightButtonUp(sender, e);
        }

        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Graph graph = Graph;
            if (graph == null)
                return;

            if (SelectLine == null)
                return;

            Topology editer = new Topology(graph);

            Path node = e.OriginalSource as Path;
            if(node!=null && EditAdorner.IsNode(node))
            {
                int id = EditAdorner.GetNodeID(node);
                if(EditAdorner.IsControl(id))
                {
                    SelectLine = editer.LinMoveControlPoint(SelectLine, id, EditAdorner.GetNodePos(node));
                    EditAdorner.Select(SelectLine);
                }
                else
                {
                    if ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.Control)
                    {
                        SelectLine = editer.LinMoveEarPoint(SelectLine, id, EditAdorner.GetNodePos(node),true);
                        EditAdorner.Select(SelectLine);
                    }
                    else
                    {
                        SelectLine = editer.LinMoveEarPoint(SelectLine, id, EditAdorner.GetNodePos(node), false);
                        EditAdorner.Select(SelectLine);
                    }
                }
            }

            base.MouseLeftButtonUp(sender, e);
        }
    }
}
