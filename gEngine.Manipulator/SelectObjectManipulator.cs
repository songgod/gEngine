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
    public class SelectObjectManipulator : MapManipulator
    {
        public event SelectObjectDel OnSelectObject;
        private ObjectControl SelectObjectControl;

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;

            MapControl mc = this.AssociatedObject;
            if (mc == null)
                return;

            
            mc.MouseLeftButtonUp += Mc_MouseLeftButtonUp;
        }

        private void Mc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MapControl mc = this.AssociatedObject;
            ClearSelect();
            Point pt = e.GetPosition(mc);
            SelectObjectControl = null;
            VisualTreeHelper.HitTest(mc, new HitTestFilterCallback(HitTestFilterCallback),
                new HitTestResultCallback(MyHitTestResult), new PointHitTestParameters(pt));
            if (SelectObjectControl == null)
                e.Handled = false;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.AssociatedObject == null)
                return;

            MapControl mc = this.AssociatedObject;
            if (mc == null)
                return;
            
            mc.MouseLeftButtonUp -= Mc_MouseLeftButtonUp;
        }

        private void ClearSelect()
        {
            gEngine.Graph.Interface.Utility.ClearSelect(this.AssociatedObject.MapContext);
            int lccount = this.AssociatedObject.LayerControlCount;
            for (int i = 0; i < lccount; i++)
            {
                LayerControl lc= this.AssociatedObject.GetLayerControl(i);
                int obcount = lc.ObjectControlCount;
                for (int j = 0; j < obcount; j++)
                {
                    ObjectControl oc = lc.GetObjectControl(j);
                    ManipulatorSetter.ClearManipulator(oc);
                }
            }
        }

        private HitTestFilterBehavior HitTestFilterCallback(DependencyObject potentialHitTestTarget)
        {
            Canvas editcanvas = potentialHitTestTarget as Canvas;
            if(editcanvas!=null && editcanvas.Name=="EditCanvas")
            {
                return HitTestFilterBehavior.ContinueSkipSelfAndChildren;
            }

            return HitTestFilterBehavior.Continue;
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
                        SelectObjectControl = oc;
                        obj.IsSelected = true;
                        IManipulatorBase mb = gEngine.Manipulator.Registry.CreateManipulator(obj);
                        ManipulatorSetter.SetManipulator(mb, oc);

                        if (OnSelectObject != null)
                        {
                            OnSelectObject.Invoke(oc);
                        }
                    }
                    return HitTestResultBehavior.Stop;
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
