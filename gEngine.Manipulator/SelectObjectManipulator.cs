using gEngine.Graph.Interface;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator
{
    public delegate void SelectObjectDel(ObjectControl oc);
    public class SelectObjectManipulator : LayerManipulator
    {
        public event SelectObjectDel OnSelectObject;

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;

            MapControl mc = this.AssociatedObject.Owner;
            if (mc == null)
                return;


            mc.MouseLeftButtonDown += mc_MouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.AssociatedObject == null)
                return;

            MapControl mc = this.AssociatedObject.Owner;
            if (mc == null)
                return;

            mc.MouseLeftButtonDown -= mc_MouseLeftButtonDown;
        }

        private void mc_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MapControl mc = this.AssociatedObject.Owner;
            LayerControl lc = this.AssociatedObject;
            ILayer layer = lc.DataContext as ILayer;

            Point pt = e.GetPosition(mc);
            VisualTreeHelper.HitTest(mc, null,
                new HitTestResultCallback(MyHitTestResult), new PointHitTestParameters(pt));

        }
        private HitTestResultBehavior MyHitTestResult(HitTestResult hr)
        {
            DependencyObject p = hr.VisualHit;
            while (p != null)
            {
                ObjectControl oc = p as ObjectControl;
                if (oc != null)
                {
                    IObject obj = oc.DataContext as IObject;
                    if (obj != null)
                    {
                        obj.IsSelected = !obj.IsSelected;
                        IManipulatorBase mb = gEngine.Manipulator.Registry.CreateManipulator(obj);
                        if (obj.IsSelected)
                            ManipulatorSetter.SetManipulator(mb, oc);
                        else
                            ManipulatorSetter.ClearManipulator(oc);

                        if (OnSelectObject != null)
                        {
                            OnSelectObject.Invoke(oc);
                        }
                    }
                    return HitTestResultBehavior.Stop;
                    //break;
                }
                p = VisualTreeHelper.GetParent(p);
            }
            return HitTestResultBehavior.Continue;
        }
    }

    public class SOMFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "SelectObjectManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new SelectObjectManipulator();
        }
    }

    public class OutlineAdorner : Adorner
    {
        public OutlineAdorner(UIElement adornedElement) : base(adornedElement)
        {
        }

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            Rect adornedElementRect = new Rect(this.AdornedElement.DesiredSize);
            Pen renderPen = new Pen(new SolidColorBrush(Colors.Red), 3.0);
            renderPen.DashStyle = new DashStyle(new DoubleCollection() { 2, 2 }, 0);

            drawingContext.DrawRectangle(null, renderPen, adornedElementRect);

        }
    }



}
