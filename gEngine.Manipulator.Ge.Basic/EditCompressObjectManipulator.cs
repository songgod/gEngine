using gEngine.Utility;
using gEngine.View;
using gEngine.View.Ge.Basic;
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

namespace gEngine.Manipulator.Ge.Basic
{
    public delegate void CompressChanged(Point pt);

    public class EditCompressAdoner : Canvas
    {
        public event CompressChanged OnCompressChanged;

        private int NodeCount { get; set; }

        public PointCollection pc { get; set; }

        public double Left { get; set; }

        public double Top { get; set; }

        public double W { get; set; }

        public double H { get; set; }

        public EditCompressAdoner() { }


        #region Method

        public void Select(PointCollection pc)
        {
            Clear();

            if (pc == null || pc.Count == 0)
                return;

            NodeCount = pc.Count;
            Path Track = CreateRect(pc);
            CreateNodes(Track);
        }

        private void Clear()
        {
            Children.Clear();
            NodeCount = 0;
        }

        private Path CreateRect(PointCollection ps)
        {
            Path Track = new Path() { Stroke = new SolidColorBrush() { Color = Colors.Black }, StrokeThickness = 1 };

            Track = new Path() { Stroke = new SolidColorBrush() { Color = Colors.Black }, StrokeThickness = 1 };

            double minX = ps.OrderBy(p => p.X).ElementAt(0).X;
            double minY = ps.OrderBy(p => p.Y).ElementAt(0).Y;

            double maxX = ps.OrderByDescending(p => p.X).ElementAt(0).X;
            double maxY = ps.OrderByDescending(p => p.Y).ElementAt(0).Y;

            double width = maxX - minX;
            double height = maxY - minY;

            PathGeometry pgTrack = new PathGeometry();
            RectangleGeometry rg = new RectangleGeometry() { Rect = new Rect(minX, minY, width, height) };
            Track.Data = rg;
            Children.Add(Track);
            return Track;
        }

        private void CreateNodes(Path track)
        {
            RectangleGeometry rgTrack = track.Data as RectangleGeometry;

            List<Point> DragPts = new List<Point>();
            DragPts.Add(new Point(rgTrack.Rect.X, rgTrack.Rect.Y));
            DragPts.Add(new Point(rgTrack.Rect.X, rgTrack.Rect.Y + rgTrack.Rect.Height));
            DragPts.Add(new Point(rgTrack.Rect.X + rgTrack.Rect.Width, rgTrack.Rect.Y));
            DragPts.Add(new Point(rgTrack.Rect.X + rgTrack.Rect.Width, rgTrack.Rect.Y + rgTrack.Rect.Height));

            for (int i = 0; i < DragPts.Count; ++i)
            {
                Path control = new Path() { VerticalAlignment = VerticalAlignment.Top, Stroke = new SolidColorBrush() { Color = Colors.Black }, StrokeThickness = 1.0, Fill = new SolidColorBrush() { Color = Colors.PaleVioletRed } };
                control.Name = string.Format("C{0:G}", i);
                control.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                control.MouseMove += Control_MouseMove;
                control.MouseLeftButtonUp += Control_MouseLeftButtonUp;
                EllipseGeometry eg = new EllipseGeometry() { RadiusX = 2, RadiusY = 2, Center = DragPts[i] };
                control.Data = eg;
                Children.Add(control);
            }
        }

        public Point GetNodePos(Path node)
        {
            if (node == null)
                return new Point();
            EllipseGeometry eg = node.Data as EllipseGeometry;
            return eg.Center;
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

            int numoftrackaline = 1;
            return numoftrackaline + index >= Children.Count ? null : Children[numoftrackaline + index] as Path;
        }

        public Point GetNodePos(int index)
        {
            Path node = GetNode(index);
            return GetNodePos(node);
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

        #endregion

        #region Event

        private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path path = sender as Path;
            if (path != null)
                path.CaptureMouse();
            e.Handled = true;
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Path path = sender as Path;
                Point mouse_pt = e.GetPosition(this);
                Point clicked_pt = GetNodePos(path);
                Vector offset = mouse_pt - clicked_pt;
                int id = GetNodeID(path);
                SetNodePos(id, mouse_pt);

                double nW = -1, nH = -1;
                if (id == 0 || id == 1)
                {
                    if (offset.X > 0)
                        nW = W - offset.X;
                    else
                        nW = W + Math.Abs(offset.X);

                    if (id == 0)
                    {
                        if (offset.Y > 0)
                            nH = H - offset.Y;
                        else
                            nH = H + Math.Abs(offset.Y);
                    }
                    else
                    {
                        if (offset.Y > 0)
                            nH = H + offset.Y;
                        else
                            nH = H - Math.Abs(offset.Y);
                    }
                }

                if (id == 2 || id == 3)
                {
                    nW = W + offset.X;

                    if (id == 2)
                    {

                        if (offset.Y > 0)
                            nH = H - offset.Y;
                        else
                            nH = H + Math.Abs(offset.Y);

                        if (nH <= 0)
                            return;
                    }
                    else
                    {
                        nH = H + offset.Y;
                    }
                }

                Point pt = new Point(nW, nH);

                if (OnCompressChanged != null)
                    OnCompressChanged.Invoke(pt);

                e.Handled = true;
            }
        }

        private void Control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path path = sender as Path;
            if (path != null)
                path.ReleaseMouseCapture();

            //int index = GetNodeID(path);
            //Point p = GetNodePos(index);

            //if (OnLineChanged != null)
            //    OnLineChanged.Invoke(pc);
            e.Handled = true;
        }

        #endregion
    }

    public class EditCompressObjectManipulator : ObjectManipulator
    {
        public EditCompressAdoner EditCompressAdoner
        {
            get; private set;
        }

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

            EditCompressAdoner = new EditCompressAdoner();
            mc.EditLayer.Children.Add(EditCompressAdoner);
            EditCompressAdoner.Left = ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Left;
            EditCompressAdoner.Top = ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Top;
            EditCompressAdoner.W = ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Width;
            EditCompressAdoner.H = ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Height;

            object[] values = new object[] { EditCompressAdoner.Left, EditCompressAdoner.Top, EditCompressAdoner.W, EditCompressAdoner.H };
            RectToCompassConverter comConverter = new RectToCompassConverter();
            EditCompressAdoner.OnCompressChanged += EditCompressAdoner_OnCompressChanged;
            EditCompressAdoner.pc = (PointCollection) comConverter.Convert(values, null, null, null);
            EditCompressAdoner.Select(EditCompressAdoner.pc);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            MapControl mc = this.AssociatedObject.Owner.Owner;
            mc.EditLayer.Children.Remove(EditCompressAdoner);
        }

        #region Event

        private void EditCompressAdoner_OnCompressChanged(Point pt)
        {
            if (this.AssociatedObject == null)
                return;

            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;
            if (mc == null)
                return;

            ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Width = pt.X;
            ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Height = pt.Y;

            EditCompressAdoner.W = pt.X;
            EditCompressAdoner.H = pt.Y;

            object[] values = new object[] { EditCompressAdoner.Left, EditCompressAdoner.Top, pt.X, pt.Y };
            RectToCompassConverter comConverter = new RectToCompassConverter();
            EditCompressAdoner.pc = (PointCollection) comConverter.Convert(values, null, null, null);
            EditCompressAdoner.Select(EditCompressAdoner.pc);
        }

        #endregion
    }


    //public delegate void CompressChanged(Point pt);

    //public class EditCompressAdoner : Canvas
    //{
    //    public event CompressChanged OnCompressChanged;

    //    private int NodeCount { get; set; }

    //    public PointCollection pc { get; set; }

    //    public double Left { get; set; }

    //    public double Top { get; set; }

    //    public double W { get; set; }

    //    public double H { get; set; }

    //    public EditCompressAdoner() { }

    //    #region Method

    //    public void Select(PointCollection pc)
    //    {
    //        Clear();

    //        if (pc == null || pc.Count == 0)
    //            return;

    //        NodeCount = pc.Count;
    //        Path Track = CreateTrack(pc);
    //        CreateNodes(Track);
    //    }

    //    private void Clear()
    //    {
    //        Children.Clear();
    //        NodeCount = 0;
    //    }

    //    private Path CreateTrack(PointCollection ps)
    //    {
    //        Path Track = new Path() { Stroke = new SolidColorBrush() { Color = Colors.LightGray }, StrokeThickness = 1, StrokeDashArray = new DoubleCollection() { 2, 3 } };
    //        //Track.MouseLeftButtonDown += Track_MouseLeftButtonDown;
    //        PathGeometry pgTrack = new PathGeometry();
    //        PathFigure pfTrack = new PathFigure() { StartPoint = ps[0], IsClosed = true };
    //        PolyLineSegment psTrack = new PolyLineSegment() { Points = new PointCollection(ps.Skip(1).Take(ps.Count - 1)) };
    //        pfTrack.Segments.Add(psTrack);
    //        pgTrack.Figures.Add(pfTrack);

    //        Track.Data = pgTrack;
    //        Children.Add(Track);
    //        return Track;
    //    }

    //    private void CreateNodes(Path track)
    //    {
    //        PathGeometry pgTrack = track.Data as PathGeometry;
    //        PathFigure pfTrack = pgTrack.Figures[0];
    //        PolyLineSegment psTrack = pfTrack.Segments[0] as PolyLineSegment;
    //        for (int i = 0; i < NodeCount; ++i)
    //        {
    //            Path control = new Path() { Stroke = new SolidColorBrush() { Color = Colors.Black }, StrokeThickness = 1.0, Fill = new SolidColorBrush() { Color = Colors.PaleVioletRed } };
    //            control.Name = string.Format("C{0:G}", i);
    //            control.MouseLeftButtonDown += Control_MouseLeftButtonDown;
    //            control.MouseMove += Control_MouseMove;
    //            control.MouseLeftButtonUp += Control_MouseLeftButtonUp;
    //            EllipseGeometry eg = new EllipseGeometry() { RadiusX = 2, RadiusY = 2 };
    //            if (i == 0)
    //            {
    //                PropertyPath ppcenter = new PropertyPath("StartPoint");
    //                Binding bding = new Binding() { Path = ppcenter, Source = pfTrack, Mode = BindingMode.TwoWay };
    //                BindingOperations.SetBinding(eg, EllipseGeometry.CenterProperty, bding);
    //            }
    //            else
    //            {
    //                PropertyPath ppcenter = new PropertyPath(string.Format("Points.[{0:G}]", i - 1));
    //                Binding bding = new Binding() { Path = ppcenter, Source = psTrack, Mode = BindingMode.TwoWay };
    //                BindingOperations.SetBinding(eg, EllipseGeometry.CenterProperty, bding);
    //            }
    //            control.Data = eg;
    //            Children.Add(control);
    //        }
    //    }

    //    public Point GetNodePos(Path node)
    //    {
    //        if (node == null)
    //            return new Point();
    //        EllipseGeometry eg = node.Data as EllipseGeometry;
    //        return eg.Center;
    //    }

    //    public int GetNodeID(Path node)
    //    {
    //        if (node == null)
    //            return -1;
    //        if (node.Name.Count() == 0)
    //            return -1;

    //        int id = -1;
    //        int.TryParse(node.Name.Substring(1), out id);
    //        return id;
    //    }

    //    private Path GetNode(int index)
    //    {
    //        if (index < 0 || index >= NodeCount)
    //            return null;

    //        int numoftrackaline = 1;
    //        return numoftrackaline + index >= Children.Count ? null : Children[numoftrackaline + index] as Path;
    //    }

    //    public Point GetNodePos(int index)
    //    {
    //        Path node = GetNode(index);
    //        return GetNodePos(node);
    //    }

    //    private void SetNodePos(Path node, Point p)
    //    {
    //        if (node == null)
    //            return;

    //        EllipseGeometry eg = node.Data as EllipseGeometry;
    //        eg.Center = p;
    //    }

    //    private void SetNodePos(int index, Point p)
    //    {
    //        Path node = GetNode(index);
    //        SetNodePos(node, p);
    //    }

    //    #endregion

    //    #region Event

    //    private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    //    {
    //        Path path = sender as Path;
    //        if (path != null)
    //            path.CaptureMouse();
    //        e.Handled = true;
    //    }

    //    private void Control_MouseMove(object sender, MouseEventArgs e)
    //    {
    //        if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
    //        {
    //            Path path = sender as Path;
    //            Point mouse_pt = e.GetPosition(this);
    //            Point clicked_pt = GetNodePos(path);
    //            Vector offset = mouse_pt - clicked_pt;
    //            int id = GetNodeID(path);
    //            SetNodePos(id, mouse_pt);
    //            Point pt = new Point(W + offset.X, H + offset.Y);

    //            if (OnCompressChanged != null)
    //                OnCompressChanged.Invoke(pt);

    //            e.Handled = true;
    //        }
    //    }

    //    private void Control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    //    {
    //        Path path = sender as Path;
    //        if (path != null)
    //            path.ReleaseMouseCapture();

    //        //int index = GetNodeID(path);
    //        //Point p = GetNodePos(index);

    //        //if (OnLineChanged != null)
    //        //    OnLineChanged.Invoke(pc);
    //        e.Handled = true;
    //    }

    //    #endregion
    //}

    //public class EditCompressObjectManipulator : ObjectManipulator
    //{
    //    public EditCompressAdoner EditCompressAdoner
    //    {
    //        get; private set;
    //    }

    //    protected override void OnAttached()
    //    {
    //        base.OnAttached();
    //        if (this.AssociatedObject == null)
    //            return;
    //        ObjectControl oc = this.AssociatedObject;
    //        LayerControl lc = oc.Owner;
    //        MapControl mc = lc.Owner;
    //        if (mc == null)
    //            return;

    //        EditCompressAdoner = new EditCompressAdoner();
    //        mc.EditLayer.Children.Add(EditCompressAdoner);
    //        EditCompressAdoner.Left = ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Left;
    //        EditCompressAdoner.Top = ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Top;
    //        EditCompressAdoner.W = ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Width;
    //        EditCompressAdoner.H = ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Height;

    //        object[] values = new object[] { EditCompressAdoner.Left, EditCompressAdoner.Top, EditCompressAdoner.W, EditCompressAdoner.H };
    //        RectToCompassConverter comConverter = new RectToCompassConverter();
    //        EditCompressAdoner.OnCompressChanged += EditCompressAdoner_OnCompressChanged;
    //        EditCompressAdoner.pc = (PointCollection) comConverter.Convert(values, null, null, null);
    //        EditCompressAdoner.Select(EditCompressAdoner.pc);
    //    }

    //    protected override void OnDetaching()
    //    {
    //        base.OnDetaching();
    //        MapControl mc = this.AssociatedObject.Owner.Owner;
    //        mc.EditLayer.Children.Remove(EditCompressAdoner);
    //    }

    //    #region Event

    //    private void EditCompressAdoner_OnCompressChanged(Point pt)
    //    {
    //        if (this.AssociatedObject == null)
    //            return;

    //        ObjectControl oc = this.AssociatedObject;
    //        LayerControl lc = oc.Owner;
    //        MapControl mc = lc.Owner;
    //        if (mc == null)
    //            return;

    //        ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Width = pt.X;
    //        ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Height = pt.Y;

    //        EditCompressAdoner.W = pt.X;
    //        EditCompressAdoner.H = pt.Y;

    //        object[] values = new object[] { EditCompressAdoner.Left, EditCompressAdoner.Top, pt.X, pt.Y };
    //        RectToCompassConverter comConverter = new RectToCompassConverter();
    //        EditCompressAdoner.pc = (PointCollection) comConverter.Convert(values, null, null, null);
    //        EditCompressAdoner.Select(EditCompressAdoner.pc);
    //    }

    //    #endregion
    //}

    public class EditCompressObjectFactory : IObjectManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "EditCompressObjectManipulator";
            }
        }

        public Type SupportIObjectType
        {
            get
            {
                return typeof(gEngine.Graph.Ge.Basic.Comprass);
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            EditCompressObjectManipulator em = new EditCompressObjectManipulator();
            return em;
        }
    }
}
