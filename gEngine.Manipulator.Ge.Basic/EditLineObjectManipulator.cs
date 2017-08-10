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
    public class EditLineObjectManipulator : ObjectManipulator
    {
        #region Property

        public Rectangle TrackAdorner1 { get; set; }
        public Rectangle TrackAdorner2 { get; set; }

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
            Point p1 = ((gEngine.Graph.Ge.Basic.Line) oc.DataContext).Start;
            Point p2 = ((gEngine.Graph.Ge.Basic.Line) oc.DataContext).End;

            Style style = new Style();
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeProperty, Value = new SolidColorBrush(Color.FromRgb(120, 162, 239)) });
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeThicknessProperty, Value = 1.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.FillProperty, Value = Brushes.White });
            style.Setters.Add(new Setter() { Property = Rectangle.CursorProperty, Value = Cursors.Hand });
            style.Setters.Add(new Setter() { Property = Rectangle.WidthProperty, Value = 5.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.HeightProperty, Value = 5.0 });

            TrackAdorner1 = new Rectangle() { Style = style };
            TrackAdorner2 = new Rectangle() { Style = style };

            Canvas.SetLeft(TrackAdorner1, p1.X - TrackAdorner1.Width / 2);
            Canvas.SetTop(TrackAdorner1, p1.Y - TrackAdorner1.Height / 2);
            Canvas.SetLeft(TrackAdorner2, p2.X - TrackAdorner2.Width / 2);
            Canvas.SetTop(TrackAdorner2, p2.Y - TrackAdorner2.Height / 2);

            mc.EditLayer.Children.Add(TrackAdorner1);
            mc.EditLayer.Children.Add(TrackAdorner2);

            this.TrackAdorner1.MouseMove += TrackAdo1_MouseMove;
            this.TrackAdorner2.MouseMove += TrackAdo2_MouseMove;
            this.TrackAdorner1.MouseLeftButtonUp += TrackAdo1_MouseLeftButtonUp;
            this.TrackAdorner2.MouseLeftButtonUp += TrackAdo2_MouseLeftButtonUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            LayerControl lc = this.AssociatedObject.Owner;
            MapControl mc = lc.Owner;
            if (mc == null)
                return;
            mc.EditLayer.Children.Clear();
            this.TrackAdorner1.MouseMove -= TrackAdo1_MouseMove;
            this.TrackAdorner1.MouseLeftButtonUp -= TrackAdo1_MouseLeftButtonUp;
            this.TrackAdorner2.MouseMove -= TrackAdo2_MouseMove;
            this.TrackAdorner2.MouseLeftButtonUp -= TrackAdo2_MouseLeftButtonUp;
        }

        #region Event

        private void TrackAdo1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.AssociatedObject == null)
                return;

            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;

            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.TrackAdorner1.CaptureMouse();
                Point p = mc.Dp2LP(e.GetPosition(mc));
                Canvas.SetLeft(TrackAdorner1, p.X - TrackAdorner1.Width / 2);
                Canvas.SetTop(TrackAdorner1, p.Y - TrackAdorner1.Height / 2);
                ((gEngine.Graph.Ge.Basic.Line) oc.DataContext).Start = p;
            }
        }

        private void TrackAdo2_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.AssociatedObject == null)
                return;

            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;

            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.TrackAdorner2.CaptureMouse();
                Point p = mc.Dp2LP(e.GetPosition(mc));
                Canvas.SetLeft(TrackAdorner2, p.X - TrackAdorner2.Width / 2);
                Canvas.SetTop(TrackAdorner2, p.Y - TrackAdorner2.Height / 2);
                ((gEngine.Graph.Ge.Basic.Line) oc.DataContext).End = p;
            }
        }

        private void TrackAdo1_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            this.TrackAdorner1.ReleaseMouseCapture();
        }

        private void TrackAdo2_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            this.TrackAdorner2.ReleaseMouseCapture();
        }

        #endregion
    }

    public class EditLineObjectFactory : IObjectManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "EditLineObjectManipulator";
            }
        }

        public Type SupportIObjectType
        {
            get
            {
                return typeof(gEngine.Graph.Ge.Basic.Line);
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            EditLineObjectManipulator em = new EditLineObjectManipulator();
            return em;
        }
    }
}
