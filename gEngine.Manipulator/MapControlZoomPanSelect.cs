using gEngine.Graph.Interface;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace gEngine.Manipulator
{
    public class MapControlZoomPanSelect : MapControlZoomPan
    {
        private ObjectControl SelectObjectControl;

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;
            
            this.AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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

        private void ClearSelect()
        {
            gEngine.Graph.Interface.Utility.ClearSelect(this.AssociatedObject.MapContext);
        }

        private HitTestFilterBehavior HitTestFilterCallback(DependencyObject potentialHitTestTarget)
        {
            Canvas editcanvas = potentialHitTestTarget as Canvas;
            if (editcanvas != null && editcanvas.Name == "EditCanvas")
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

        protected override void OnDetaching()
        {
            ClearSelect();
            base.OnDetaching();
            this.AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
        }
    }

    public class MCZPSMFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "MapControlZoomPanSelect";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            MapControlZoomPanSelect m = new MapControlZoomPanSelect();
            return m;
        }
    }
}
