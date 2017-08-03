using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator.Ge.Basic
{
    public class EditRectObjectManipulator : ObjectManipulator
    {
        #region Property

        public Thumb th1 { get; set; }
        public Thumb th2 { get; set; }
        public Thumb th3 { get; set; }
        public Thumb th4 { get; set; }


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
            double height = ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Height;
            double width = ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Width;
            double top = ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Top;
            double left = ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Left;


            Style style = new Style();
            var factory = new FrameworkElementFactory(typeof(Rectangle));
            factory.SetValue(Shape.FillProperty, Brushes.White);
            factory.SetValue(Shape.StrokeProperty, new SolidColorBrush(Color.FromRgb(120, 162, 239)));
            factory.SetValue(Shape.CursorProperty, Cursors.Hand);
            factory.SetValue(Shape.StrokeThicknessProperty, 1.0);
            var controlTemplate = new ControlTemplate { TargetType = typeof(Thumb), VisualTree = factory };
            style.Setters.Add(new Setter() { Property = Thumb.TemplateProperty, Value = new ControlTemplate { TargetType = typeof(Thumb), VisualTree = factory } });
            style.Setters.Add(new Setter() { Property = Rectangle.WidthProperty, Value = 5.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.HeightProperty, Value = 5.0 });

            th1 = new Thumb() { Style = style };
            Canvas.SetTop(th1, top - th1.Height / 2);
            Canvas.SetLeft(th1, left - th1.Width / 2);

            th2 = new Thumb() { Style = style };
            Canvas.SetTop(th2, top - th2.Height / 2);
            Canvas.SetLeft(th2, left + width - th2.Width / 2);

            th3 = new Thumb() { Style = style };
            Canvas.SetTop(th3, top + height - th3.Height / 2);
            Canvas.SetLeft(th3, left - th3.Width / 2);

            th4 = new Thumb() { Style = style };
            Canvas.SetTop(th4, top + height - th4.Height / 2);
            Canvas.SetLeft(th4, left + width - th4.Width / 2);


            mc.EditLayer.Children.Add(th1);
            mc.EditLayer.Children.Add(th2);
            mc.EditLayer.Children.Add(th3);
            mc.EditLayer.Children.Add(th4);
            //mc.EditLayer.Children.Add(TrackAdorner2);

            th1.DragDelta += th1_DragDelta;
            th2.DragDelta += th2_DragDelta;
            th3.DragDelta += th3_DragDelta;
            th4.DragDelta += th4_DragDelta;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            LayerControl lc = this.AssociatedObject.Owner;
            MapControl mc = lc.Owner;
            if (mc == null)
                return;
            mc.EditLayer.Children.Clear();
            th1=th2=th3=th4 = null;
         
        }

        #region Event

        private void th1_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.AssociatedObject == null)
                return;
            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;
            Thumb mth = (Thumb)sender;
            Point p = mc.Dp2LP(new Point(Canvas.GetLeft(mth) + e.HorizontalChange, Canvas.GetTop(mth)+e.VerticalChange));

            //Thumb mth = (Thumb)sender;
            //double y = Canvas.GetTop(mth) + e.VerticalChange;
            //double x = Canvas.GetLeft(mth) + e.HorizontalChange;
            Canvas.SetTop(mth, Canvas.GetTop(mth) + e.VerticalChange);
            Canvas.SetLeft(mth, Canvas.GetLeft(mth) + e.HorizontalChange);
            //((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Top = p.Y + th1.Width / 2;
            //((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Left = p.X + th1.Width / 2;
            //((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Width += p.Y + th1.Width / 2;
            //((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Height += p.X + th1.Width / 2;
        }
        private void th2_DragDelta(object sender, DragDeltaEventArgs e)
        {
            //if (this.AssociatedObject == null)
            //    return;

            //if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            //{
            //    ObjectControl oc = this.AssociatedObject;
            //    Point p = e.GetPosition(oc);
            //    Canvas.SetLeft(TrackAdorner1, p.X - TrackAdorner1.Width / 2);
            //    Canvas.SetTop(TrackAdorner1, p.Y - TrackAdorner1.Height / 2);
            //    ((gEngine.Graph.Ge.Basic.Line)oc.DataContext).Start = p;
            //}
        }
        private void th3_DragDelta(object sender, DragDeltaEventArgs e)
        {
            //if (this.AssociatedObject == null)
            //    return;

            //if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            //{
            //    ObjectControl oc = this.AssociatedObject;
            //    Point p = e.GetPosition(oc);
            //    Canvas.SetLeft(TrackAdorner1, p.X - TrackAdorner1.Width / 2);
            //    Canvas.SetTop(TrackAdorner1, p.Y - TrackAdorner1.Height / 2);
            //    ((gEngine.Graph.Ge.Basic.Line)oc.DataContext).Start = p;
            //}
        }
        private void th4_DragDelta(object sender, DragDeltaEventArgs e)
        {
            //if (this.AssociatedObject == null)
            //    return;

            //if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            //{
            //    ObjectControl oc = this.AssociatedObject;
            //    Point p = e.GetPosition(oc);
            //    Canvas.SetLeft(TrackAdorner1, p.X - TrackAdorner1.Width / 2);
            //    Canvas.SetTop(TrackAdorner1, p.Y - TrackAdorner1.Height / 2);
            //    ((gEngine.Graph.Ge.Basic.Line)oc.DataContext).Start = p;
            //}
        }


        #endregion

      
    }
    public class EditRectObjectFactory : IObjectManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "EditRectObjectManipulator";
            }
        }

        public Type SupportIObjectType
        {
            get
            {
                return typeof(gEngine.Graph.Ge.Basic.Rect);
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            EditRectObjectManipulator dm = new EditRectObjectManipulator();
            return dm;
        }
    }
}
