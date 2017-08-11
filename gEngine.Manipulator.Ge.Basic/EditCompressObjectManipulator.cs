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
    public delegate void CompressChanged(double left, double top, double w, double h);

    public class EditCompressAdoner : Canvas
    {
        public event CompressChanged OnCompressChanged;

        #region Property

        private int NodeCount { get; set; }

        public PointCollection pc { get; set; }

        public double Left { get; set; }

        public double Top { get; set; }

        public double W { get; set; }

        public double H { get; set; }

        public EditCompressAdoner() { }

        #endregion

        #region Method

        public void Select(PointCollection pc)
        {
            Clear();

            if (pc == null || pc.Count == 0)
                return;

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

            double minX = ps.OrderBy(p => p.X).ElementAt(0).X;
            double minY = ps.OrderBy(p => p.Y).ElementAt(0).Y;

            double maxX = ps.OrderByDescending(p => p.X).ElementAt(0).X;
            double maxY = ps.OrderByDescending(p => p.Y).ElementAt(0).Y;

            PathGeometry pgTrack = new PathGeometry();
            RectangleGeometry rg = new RectangleGeometry() { Rect = new Rect(new Point(minX, minY), new Point(maxX, maxY)) };
            Track.Data = rg;
            Children.Add(Track);
            return Track;
        }

        private void CreateNodes(Path track)
        {
            RectangleGeometry rgTrack = track.Data as RectangleGeometry;

            List<Tuple<Point, Tuple<VerticalAlignment, HorizontalAlignment>>> DragPts = new List<Tuple<Point, Tuple<VerticalAlignment, HorizontalAlignment>>>();

            DragPts.Add(new Tuple<Point, Tuple<VerticalAlignment, HorizontalAlignment>>
                ((new Point(rgTrack.Rect.X, rgTrack.Rect.Y)), new Tuple<VerticalAlignment, HorizontalAlignment>(VerticalAlignment.Top, HorizontalAlignment.Left)));
            DragPts.Add(new Tuple<Point, Tuple<VerticalAlignment, HorizontalAlignment>>
                ((new Point(rgTrack.Rect.X, rgTrack.Rect.Y + rgTrack.Rect.Height)), new Tuple<VerticalAlignment, HorizontalAlignment>(VerticalAlignment.Bottom, HorizontalAlignment.Left)));
            DragPts.Add(new Tuple<Point, Tuple<VerticalAlignment, HorizontalAlignment>>
                ((new Point(rgTrack.Rect.X + rgTrack.Rect.Width, rgTrack.Rect.Y)), new Tuple<VerticalAlignment, HorizontalAlignment>(VerticalAlignment.Top, HorizontalAlignment.Right)));
            DragPts.Add(new Tuple<Point, Tuple<VerticalAlignment, HorizontalAlignment>>
                ((new Point(rgTrack.Rect.X + rgTrack.Rect.Width, rgTrack.Rect.Y + rgTrack.Rect.Height)), new Tuple<VerticalAlignment, HorizontalAlignment>(VerticalAlignment.Bottom, HorizontalAlignment.Right)));

            NodeCount = DragPts.Count;

            for (int i = 0; i < DragPts.Count; ++i)
            {
                Path control = new Path() { VerticalAlignment = DragPts[i].Item2.Item1, HorizontalAlignment = DragPts[i].Item2.Item2, Stroke = new SolidColorBrush() { Color = Colors.Black }, StrokeThickness = 1.0, Fill = new SolidColorBrush() { Color = Colors.PaleVioletRed } };
                control.Name = string.Format("C{0:G}", i);
                control.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                control.MouseMove += Control_MouseMove;
                control.MouseLeftButtonUp += Control_MouseLeftButtonUp;
                EllipseGeometry eg = new EllipseGeometry() { RadiusX = 2, RadiusY = 2, Center = DragPts[i].Item1 };
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

        private EllipseGeometry SetNodePos(int index, Point p)
        {
            Path node = GetNode(index);

            if (node == null)
                return null;

            EllipseGeometry eg = node.Data as EllipseGeometry;
            eg.Center = p;
            return eg;
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
                EllipseGeometry eg = SetNodePos(id, mouse_pt);
                path.Data = eg;
                path.CaptureMouse();

                double nW = -1, nH = -1, nTop = -1, nLeft = -1;

                switch (path.VerticalAlignment)
                {
                    case VerticalAlignment.Bottom:
                        nH = H + offset.Y;
                        nTop = Top;
                        break;
                    case VerticalAlignment.Top:
                        nH = H + (offset.Y * -1);
                        nTop = Top + offset.Y;
                        break;
                    default:
                        break;
                }

                switch (path.HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        nW = W + (offset.X * -1);
                        nLeft = Left + offset.X;
                        break;
                    case HorizontalAlignment.Right:
                        nW = W + offset.X;
                        nLeft = Left;
                        break;
                    default:
                        break;
                }

                if (OnCompressChanged != null)
                    OnCompressChanged.Invoke(nLeft, nTop, nW, nH);

                e.Handled = true;
            }
        }

        private void Control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path path = sender as Path;
            if (path != null)
                path.ReleaseMouseCapture();
            e.Handled = true;
        }

        #endregion
    }

    public class EditCompressObjectManipulator : ObjectManipulator
    {
        #region Property

        private RectToCompassConverter _instance;
        public RectToCompassConverter RectToCompassConverter
        {
            get
            {
                _instance = RectToCompassConverter.CreateInstance();
                return _instance;
            }
        }

        public EditCompressAdoner EditCompressAdoner
        {
            get; private set;
        }

        #endregion

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

            EditCompressAdoner.OnCompressChanged += EditCompressAdoner_OnCompressChanged;
            EditCompressAdoner.pc = (PointCollection) RectToCompassConverter.Convert(values, null, null, null);
            EditCompressAdoner.Select(EditCompressAdoner.pc);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            MapControl mc = this.AssociatedObject.Owner.Owner;
            mc.EditLayer.Children.Remove(EditCompressAdoner);
        }

        #region Event

        private void EditCompressAdoner_OnCompressChanged(double left, double top, double w, double h)
        {
            if (this.AssociatedObject == null)
                return;

            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;
            if (mc == null)
                return;

            ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Width = w;
            ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Height = h;
            ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Left = left;
            ((gEngine.Graph.Ge.Basic.Comprass) oc.DataContext).Top = top;

            EditCompressAdoner.W = w;
            EditCompressAdoner.H = h;
            EditCompressAdoner.Left = left;
            EditCompressAdoner.Top = top;

            object[] values = new object[] { left, top, w, h };

            EditCompressAdoner.pc = (PointCollection) RectToCompassConverter.Convert(values, null, null, null);
            EditCompressAdoner.Select(EditCompressAdoner.pc);
        }

        #endregion
    }

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
