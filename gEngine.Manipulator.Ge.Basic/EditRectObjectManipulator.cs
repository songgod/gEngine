using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
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
            factory.SetValue(Shape.CursorProperty, Cursors.Cross);
            factory.SetValue(Shape.StrokeThicknessProperty, 1.0);
            var controlTemplate = new ControlTemplate { TargetType = typeof(Thumb), VisualTree = factory };
            style.Setters.Add(new Setter() { Property = Thumb.TemplateProperty, Value = new ControlTemplate { TargetType = typeof(Thumb), VisualTree = factory } });
            style.Setters.Add(new Setter() { Property = Rectangle.WidthProperty, Value = 5.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.HeightProperty, Value = 5.0 });

           
            th1 = new Thumb() { Style = style };
            Canvas.SetTop(th1, top - th1.Height / 2);
            Canvas.SetLeft(th1, left - th1.Width / 2);
            th1.DragDelta += th1_DragDelta;
            mc.EditLayer.Children.Add(th1);
            
            th2 = new Thumb() { Style = style };
            Canvas.SetTop(th2, top - th2.Height / 2);
            Canvas.SetLeft(th2, left + width - th2.Width / 2);
            //th2.SetBinding(Canvas.TopProperty, new Binding("(Canvas.Top)") { Source = th1 });
            //th2.SetBinding(Canvas.LeftProperty, new Binding("(Canvas.Left)") { Source = th1 });

            th3 = new Thumb() { Style = style };
            Canvas.SetTop(th3, top + height - th3.Height / 2);
            Canvas.SetLeft(th3, left - th3.Width / 2);
          

            th4 = new Thumb() { Style = style };
            Canvas.SetTop(th4, top + height - th4.Height / 2);
            Canvas.SetLeft(th4, left + width - th4.Width / 2);

           
           
            mc.EditLayer.Children.Add(th2);
            mc.EditLayer.Children.Add(th3);
            mc.EditLayer.Children.Add(th4);
            //mc.EditLayer.Children.Add(TrackAdorner2);

           
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
           
            double y = Canvas.GetTop(mth) + e.VerticalChange;
            double x = Canvas.GetLeft(mth) + e.HorizontalChange;
            Canvas.SetTop(mth, y);
            Canvas.SetLeft(mth, x);
            
            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Top = y + th1.Width / 2;
            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Left = x + th1.Width / 2;
            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Width += (e.HorizontalChange*-1);
            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Height += (e.VerticalChange*-1) ;
            Canvas.SetTop(th2, y);
            Canvas.SetLeft(th2, x+ ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Width);

            Canvas.SetTop(th3, y + ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Height);
            Canvas.SetLeft(th3, x);

        }
        private void th2_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.AssociatedObject == null)
                return;
            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;
            Thumb mth = (Thumb)sender;

            double y = Canvas.GetTop(mth) + e.VerticalChange;
            double x = Canvas.GetLeft(mth) + e.HorizontalChange;
            Canvas.SetTop(mth, y);
            Canvas.SetLeft(mth, x);

            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Top = y + th2.Width / 2;
            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Left = x-((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Width + th2.Width / 2;
            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Width -= (e.HorizontalChange * -1);
            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Height += (e.VerticalChange * -1);
            Canvas.SetTop(th1, y );
            Canvas.SetLeft(th1, x - ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Width);

            Canvas.SetTop(th4, y + ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Height);
            Canvas.SetLeft(th4, x);
        }
        private void th3_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.AssociatedObject == null)
                return;
            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;
            Thumb mth = (Thumb)sender;

            double y = Canvas.GetTop(mth) + e.VerticalChange;
            double x = Canvas.GetLeft(mth) + e.HorizontalChange;
            Canvas.SetTop(mth, y);
            Canvas.SetLeft(mth, x);

            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Top = y - ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Height + th2.Width / 2;
            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Left = x  + th2.Width / 2;
            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Width += (e.HorizontalChange * -1);
            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Height -= (e.VerticalChange * -1);
            Canvas.SetTop(th1, y);
            Canvas.SetLeft(th1, x + ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Width);

            Canvas.SetTop(th4, y - ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Height);
            Canvas.SetLeft(th4, x);
        }
        private void th4_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.AssociatedObject == null)
                return;
            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;
            Thumb mth = (Thumb)sender;

            double y = Canvas.GetTop(mth) + e.VerticalChange;
            double x = Canvas.GetLeft(mth) + e.HorizontalChange;
            Canvas.SetTop(mth, y);
            Canvas.SetLeft(mth, x);

            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Top = y - ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Height + th2.Width / 2;
            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Left = x - ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Width+ th2.Width / 2;
            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Width -= (e.HorizontalChange * -1);
            ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Height -= (e.VerticalChange * -1);
            Canvas.SetTop(th2, y);
            Canvas.SetLeft(th2, x - ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Width);

            Canvas.SetTop(th3, y - ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Height);
            Canvas.SetLeft(th3, x);
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
