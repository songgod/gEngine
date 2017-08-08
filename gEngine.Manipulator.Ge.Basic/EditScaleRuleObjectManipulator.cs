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
    public class EditScaleRuleObjectManipulator : ObjectManipulator
    {
        #region Property

        public Thumb th1 { get; set; }
      
        public Point location { get; set; }


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
            double top = ((gEngine.Graph.Ge.Basic.ScaleRule)oc.DataContext).Top;
            double left = ((gEngine.Graph.Ge.Basic.ScaleRule)oc.DataContext).Left;
            int ScaleNumber = ((gEngine.Graph.Ge.Basic.ScaleRule)oc.DataContext).ScaleNumber;
            int ScaleSpace = ((gEngine.Graph.Ge.Basic.ScaleRule)oc.DataContext).ScaleSpace;
            double ScaleHeight = ((gEngine.Graph.Ge.Basic.ScaleRule)oc.DataContext).ScaleHeight;

            Style style = new Style();
            var factory = new FrameworkElementFactory(typeof(Rectangle));
            factory.SetValue(Shape.FillProperty, Brushes.Transparent);
            factory.SetValue(Shape.StrokeProperty, new SolidColorBrush(Color.FromRgb(120, 162, 239)));
            factory.SetValue(Shape.CursorProperty, Cursors.SizeAll);
            factory.SetValue(Shape.StrokeThicknessProperty, 1.0);
            var controlTemplate = new ControlTemplate { TargetType = typeof(Thumb), VisualTree = factory };
            style.Setters.Add(new Setter() { Property = Thumb.TemplateProperty, Value = new ControlTemplate { TargetType = typeof(Thumb), VisualTree = factory } });
            style.Setters.Add(new Setter() { Property = Rectangle.WidthProperty, Value = 5.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.HeightProperty, Value = 5.0 });


            th1 = new Thumb() { Style = style };
            Canvas.SetTop(th1, top );
            Canvas.SetLeft(th1, left-10);
            th1.Width = (ScaleNumber * ScaleSpace / 10 * 38 + left + 20 - th1.Width / 2) - (left - 10 - th1.Width / 2);
            th1.Height = (ScaleHeight / 10 * 38 + top + mc.Lp2DP(ScaleHeight) + 20) - (top - th1.Height / 2);
            th1.DragDelta += th1_DragDelta;
            mc.EditLayer.Children.Add(th1);
            

            
        }

       
        protected override void OnDetaching()
        {
            base.OnDetaching();
            LayerControl lc = this.AssociatedObject.Owner;
            MapControl mc = lc.Owner;
            if (mc == null)
                return;
            mc.EditLayer.Children.Clear();
            th1 = null;

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

            //((gEngine.Graph.Ge.Basic.ScaleRule)oc.DataContext).ScaleHeight = TrackAdorner.Height-20;
            ((gEngine.Graph.Ge.Basic.ScaleRule)oc.DataContext).Top += e.VerticalChange;
            ((gEngine.Graph.Ge.Basic.ScaleRule)oc.DataContext).Left = Canvas.GetLeft(mth) +10;

        }
       


        #endregion

    }
    public class EditScaleRuleObjectFactory : IObjectManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "EditScaleRuleObjectManipulator";
            }
        }

        public Type SupportIObjectType
        {
            get
            {
                return typeof(gEngine.Graph.Ge.Basic.ScaleRule);
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            EditScaleRuleObjectManipulator dm = new EditScaleRuleObjectManipulator();
            return dm;
        }
    }
}
