using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator.Ge.Basic
{
    public class EditPolyLineObjectManipulator : ObjectManipulator
    {
        #region Property

        public List<Rectangle> TrackAdorner { get; private set; }

        public int NodeCount { get; private set; }

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
            PointCollection pc = ((gEngine.Graph.Ge.Basic.PolyLine) oc.DataContext).Points;
            NodeCount = pc.Count;
            Style style = new Style();
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeProperty, Value = new SolidColorBrush(Color.FromRgb(120, 162, 239)) });
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeThicknessProperty, Value = 1.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.FillProperty, Value = Brushes.White });
            style.Setters.Add(new Setter() { Property = Rectangle.CursorProperty, Value = Cursors.Hand });
            style.Setters.Add(new Setter() { Property = Rectangle.WidthProperty, Value = 5.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.HeightProperty, Value = 5.0 });

            TrackAdorner = new List<Rectangle>();
            int index = 0;
            foreach (Point pt in pc)
            {
                Rectangle R_TrackAdo = new Rectangle { Name = "_" + index.ToString(), Style = style };
                Canvas.SetLeft(R_TrackAdo, pt.X - R_TrackAdo.Width / 2);
                Canvas.SetTop(R_TrackAdo, pt.Y - R_TrackAdo.Height / 2);
                mc.EditLayer.Children.Add(R_TrackAdo);
                TrackAdorner.Add(R_TrackAdo);
                TrackAdorner[index].MouseMove += TrackAdo_MouseMove;
                TrackAdorner[index].MouseLeftButtonUp += TrackAdo_MouseLeftButtonUp;
                index++;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            LayerControl lc = this.AssociatedObject.Owner;
            MapControl mc = lc.Owner;
            if (mc == null)
                return;
            mc.EditLayer.Children.Clear();
            for (int index = 0; index < NodeCount; index++)
            {
                this.TrackAdorner[index].MouseMove -= TrackAdo_MouseMove;
                this.TrackAdorner[index].MouseLeftButtonUp -= TrackAdo_MouseLeftButtonUp;
            }
        }

        #region Event

        private void TrackAdo_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.AssociatedObject == null)
                return;

            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;

            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Rectangle clickedRect = e.OriginalSource as Rectangle;
                if (clickedRect == null)
                    return;
                int index = Int32.Parse(clickedRect.Name.Substring(1));
                this.TrackAdorner[index].CaptureMouse();
                Point p = mc.Dp2LP(e.GetPosition(mc));
                if (index.Equals(NodeCount - 1))
                {
                    int i = index-1;
                    Canvas.SetLeft(TrackAdorner[i], p.X - TrackAdorner[index].Width / 2);
                    Canvas.SetTop(TrackAdorner[i], p.Y - TrackAdorner[index].Height / 2);
                    ((gEngine.Graph.Ge.Basic.PolyLine) oc.DataContext).Points[i] = p;
                }
                Canvas.SetLeft(TrackAdorner[index], p.X - TrackAdorner[index].Width / 2);
                Canvas.SetTop(TrackAdorner[index], p.Y - TrackAdorner[index].Height / 2);
                ((gEngine.Graph.Ge.Basic.PolyLine) oc.DataContext).Points[index] = p;
            }
        }

        private void TrackAdo_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            Rectangle clickedRect = e.OriginalSource as Rectangle;
            if (clickedRect == null)
                return;
            int index = Int32.Parse(clickedRect.Name.Substring(1));
            this.TrackAdorner[index].ReleaseMouseCapture();
        }

        #endregion
    }

    public class EditPolyLineObjectFactory : IObjectManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "EditPolyLineObjectManipulator";
            }
        }

        public Type SupportIObjectType
        {
            get
            {
                return typeof(gEngine.Graph.Ge.Basic.PolyLine);
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            EditPolyLineObjectManipulator em = new EditPolyLineObjectManipulator();
            return em;
        }
    }
}
