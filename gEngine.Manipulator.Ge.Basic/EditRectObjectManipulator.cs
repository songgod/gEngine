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

        public Thumb th { get; set; }
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

            Style style1 = new Style();
            var factory1 = new FrameworkElementFactory(typeof(Rectangle));
            factory1.SetValue(Shape.FillProperty, Brushes.Transparent);
            factory1.SetValue(Shape.StrokeProperty, Brushes.Transparent );
            factory1.SetValue(Shape.CursorProperty, Cursors.SizeAll);
            factory1.SetValue(Shape.StrokeThicknessProperty, 1.0);
            var controlTemplate1 = new ControlTemplate { TargetType = typeof(Thumb), VisualTree = factory1 };
            style1.Setters.Add(new Setter() { Property = Thumb.TemplateProperty, Value = new ControlTemplate { TargetType = typeof(Thumb), VisualTree = factory1 } });


            th = new Thumb() { Style = style1 };
            th.Width = width;
            th.Height = height;
            Canvas.SetTop(th, top);
            Canvas.SetLeft(th, left);

            th1 = new Thumb() { Style = style };
            Canvas.SetTop(th1, top - th1.Height / 2);
            Canvas.SetLeft(th1, left - th1.Width / 2);
            th1.VerticalAlignment = VerticalAlignment.Top;
            th1.HorizontalAlignment = HorizontalAlignment.Left;
           
           
            
            th2 = new Thumb() { Style = style };
            Canvas.SetTop(th2, top - th2.Height / 2);
            Canvas.SetLeft(th2, left + width - th2.Width / 2);
            th2.VerticalAlignment = VerticalAlignment.Top;
            th2.HorizontalAlignment = HorizontalAlignment.Right;

            th3 = new Thumb() { Style = style };
            Canvas.SetTop(th3, top + height - th3.Height / 2);
            Canvas.SetLeft(th3, left - th3.Width / 2);
            th3.VerticalAlignment = VerticalAlignment.Bottom;
            th3.HorizontalAlignment = HorizontalAlignment.Left;
          

            th4 = new Thumb() { Style = style };
            Canvas.SetTop(th4, top + height - th4.Height / 2);
            Canvas.SetLeft(th4, left + width - th4.Width / 2);
            th4.VerticalAlignment = VerticalAlignment.Bottom;
            th4.HorizontalAlignment = HorizontalAlignment.Right;

            mc.EditLayer.Children.Add(th);
            mc.EditLayer.Children.Add(th1);
            mc.EditLayer.Children.Add(th2);
            mc.EditLayer.Children.Add(th3);
            mc.EditLayer.Children.Add(th4);
            //mc.EditLayer.Children.Add(TrackAdorner2);

            th.DragDelta += th_DragDelta;
            th1.DragDelta += th1_DragDelta;
            th2.DragDelta += th1_DragDelta;
            th3.DragDelta += th1_DragDelta;
            th4.DragDelta += th1_DragDelta;
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
        private void th_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.AssociatedObject == null)
                return;
            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;
            Thumb mth = (Thumb)sender;
            gEngine.Graph.Ge.Basic.Rect designerItem = ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext);
            double y = Canvas.GetTop(mth) + e.VerticalChange;
            double x = Canvas.GetLeft(mth) + e.HorizontalChange;
            if (designerItem != null)
            {
                Canvas.SetTop(mth, y- th1.Height / 2);
                Canvas.SetLeft(mth, x - th1.Width / 2);
                designerItem.Top = y;
                designerItem.Left = x;
                Canvas.SetTop(th1, designerItem.Top - th1.Height / 2);
                Canvas.SetLeft(th1, designerItem.Left - th1.Width / 2);
                Canvas.SetTop(th2, designerItem.Top - th2.Height / 2);
                Canvas.SetLeft(th2, designerItem.Left + designerItem.Width - th2.Width / 2);
                Canvas.SetTop(th3, designerItem.Top + designerItem.Height - th3.Height / 2);
                Canvas.SetLeft(th3, designerItem.Left - th3.Width / 2);
                Canvas.SetTop(th4, designerItem.Top + designerItem.Height - th4.Height / 2);
                Canvas.SetLeft(th4, designerItem.Left + designerItem.Width - th4.Width / 2);

            }
        }
        private void th1_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.AssociatedObject == null)
                return;
            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;
            Thumb mth = (Thumb)sender;
            gEngine.Graph.Ge.Basic.Rect designerItem = ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext);
            double y = Canvas.GetTop(mth) + e.VerticalChange;
            double x = Canvas.GetLeft(mth) + e.HorizontalChange;
            double deltaVertical, deltaHorizontal;
            double MinHeight = 50;
            double MinWidth = 50;
            if (designerItem != null)
            {
               switch (mth.VerticalAlignment)
               {
                   case VerticalAlignment.Bottom:
                       deltaVertical = Math.Min(-e.VerticalChange, designerItem.Height- MinHeight);

                       designerItem.Height -= deltaVertical;
                       break;
                   case VerticalAlignment.Top:
                       deltaVertical = Math.Min(e.VerticalChange, designerItem.Height- MinHeight);

                       designerItem.Top += deltaVertical;
                       designerItem.Height -= deltaVertical;
                       break;
                   default:
                       break;
               }
           
               switch (mth.HorizontalAlignment)
               {
                   case HorizontalAlignment.Left:
                       deltaHorizontal = Math.Min(e.HorizontalChange, designerItem.Width- MinWidth);
                   
                       designerItem.Left += deltaHorizontal;
                       designerItem.Width -= deltaHorizontal;
                       break;
                   case HorizontalAlignment.Right:
                       deltaHorizontal = Math.Min(-e.HorizontalChange, designerItem.Width- MinWidth);
                       designerItem.Width -= deltaHorizontal;
                       break;
                   default:
                       break;
               }
                  
                Canvas.SetTop(th1, designerItem.Top - th1.Height / 2);
                Canvas.SetLeft(th1, designerItem.Left - th1.Width / 2);
                Canvas.SetTop(th2, designerItem.Top - th2.Height / 2);
                Canvas.SetLeft(th2, designerItem.Left + designerItem.Width - th2.Width / 2);
                Canvas.SetTop(th3, designerItem.Top + designerItem.Height - th3.Height / 2);
                Canvas.SetLeft(th3, designerItem.Left - th3.Width / 2);
                Canvas.SetTop(th4, designerItem.Top + designerItem.Height - th4.Height / 2);
                Canvas.SetLeft(th4, designerItem.Left + designerItem.Width - th4.Width / 2);
            }

            e.Handled = true;

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
