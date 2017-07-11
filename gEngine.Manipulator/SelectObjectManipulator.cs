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
    public delegate void SelectObjectDel(IObject iobject);
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
            IMap map = mc.DataContext as IMap;
            string mapName = map.Name;

            LayerControl lc = this.AssociatedObject;
            ILayer layer = lc.DataContext as ILayer;

            HitTestResult hr = VisualTreeHelper.HitTest(this.AssociatedObject, e.GetPosition(this.AssociatedObject));
            if (hr == null || hr.VisualHit == null)
                return;
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

                        //IEnumerable<Path> paths = FindChild.FindVisualChildren<Path>(oc);
                        //foreach (Path pt in paths)
                        //{
                        //    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(pt);
                        //    OutlineAdorner conrner = new OutlineAdorner(pt);
                        //    adornerLayer.Add(conrner);

                        //    if (OnSelectObject != null)
                        //    {
                        //        OnSelectObject.Invoke(obj);
                        //    }
                        //}

                        IManipulatorBase mb = gEngine.Manipulator.Registry.CreateManipulator(obj);
                        if (obj.IsSelected)
                            ManipulatorSetter.SetManipulator(mb, oc);
                        else
                            ManipulatorSetter.RemoveManipulator(mb, oc);


                        break;
                    }
                }
                p = VisualTreeHelper.GetParent(p);
            }

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
