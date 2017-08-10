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
    public class EditBoundaryObjectManipulator : ObjectManipulator
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
            PointCollection points = ((gEngine.Graph.Ge.Basic.Boundary)oc.DataContext).Points;
           

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
            int i = 0;
            foreach (Point p in points)
            {
                th1 = new Thumb() { Style = style };
                Canvas.SetTop(th1, p.Y -th1.Height/2);
                Canvas.SetLeft(th1, p.X - th1.Width / 2);
                th1.DragDelta += th1_DragDelta;
                th1.Name = "n_"+i.ToString() ;
                //th1.SetBinding(Canvas.TopProperty, new Binding("Y") { Source = p});
                //th1.SetBinding(Canvas.LeftProperty, new Binding("X") { Source = p });
                mc.EditLayer.Children.Add(th1);
                i++;
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
            th1 = th2 = th3 = th4 = null;

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
            Canvas.SetTop(mth, y - th1.Height / 2);
            Canvas.SetLeft(mth, x - th1.Width / 2);

            ((gEngine.Graph.Ge.Basic.Boundary)oc.DataContext).Points[Int32.Parse(mth.Name.Split('_')[1])] = new Point(x, y);
            //((gEngine.Graph.Ge.Basic.Boundary)oc.DataContext).Points = y + th1.Width / 2;
            //((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Left = x + th1.Width / 2;
            //((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Width += (e.HorizontalChange * -1);
            //((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Height += (e.VerticalChange * -1);
            //Canvas.SetTop(th2, y);
            //Canvas.SetLeft(th2, x + ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Width);

            //Canvas.SetTop(th3, y + ((gEngine.Graph.Ge.Basic.Rect)oc.DataContext).Height);
            //Canvas.SetLeft(th3, x);

        }
     

        #endregion


    }
    public class EditBoundaryObjectFactory : IObjectManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "EditBoundaryObjectFactory";
            }
        }

        public Type SupportIObjectType
        {
            get
            {
                return typeof(gEngine.Graph.Ge.Basic.Boundary);
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            EditBoundaryObjectManipulator dm = new EditBoundaryObjectManipulator();
            return dm;
        }
    }
}
