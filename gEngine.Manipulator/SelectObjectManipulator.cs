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

            IEnumerable<ObjectControl> listOc = FindChild.FindVisualChildren<ObjectControl>(lc);
            foreach (ObjectControl oc in listOc)
            {
                IObject o = oc.DataContext as IObject;
                o.IsSelected = false;
                IEnumerable<Path> paths = FindChild.FindVisualChildren<Path>(oc);
                foreach (Path pt in paths)
                {
                    //1.清空所选项
                    AdornerLayer adorLayer = AdornerLayer.GetAdornerLayer(pt);
                    Adorner[] ads = adorLayer.GetAdorners(pt);
                    if (ads != null)
                    {
                        for (int i = ads.Length - 1; i >= 0; i--)
                        {
                            adorLayer.Remove(ads[i]);
                        }
                    }

                    //2.给选择的Path设置选中状态
                    Point p1 = e.GetPosition(this.AssociatedObject);
                    HitTestResult hr = VisualTreeHelper.HitTest(this.AssociatedObject, p1);
                    if (hr == null || hr.VisualHit == null)
                        continue;
                    FrameworkElement element = hr.VisualHit as FrameworkElement;
                    if (element.DataContext == pt.DataContext)
                    {
                        AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(pt);
                        OutlineAdorner conrner = new OutlineAdorner(pt);
                        adornerLayer.Add(conrner);
                        o.IsSelected = true;

                        if (OnSelectObject != null)
                        {
                            OnSelectObject.Invoke(o);
                        }
                    }
                }
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
