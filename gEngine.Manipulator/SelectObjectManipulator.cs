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
    public class SelectObjectManipulator : MapManipulator
    {
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
}
