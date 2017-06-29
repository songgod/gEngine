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


            mc.MouseLeftButtonDown += Oc_MouseLeftButtonDown;
            mc.MouseLeftButtonUp += Mc_MouseLeftButtonUp;
        }

        private void Mc_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MapControl mc = this.AssociatedObject.Owner;
            //mc.EditLayer.Visibility = Visibility.Visible;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.AssociatedObject == null)
                return;

            MapControl mc = this.AssociatedObject.Owner;
            if (mc == null)
                return;

            mc.MouseLeftButtonDown -= Oc_MouseLeftButtonDown;
            mc.MouseLeftButtonUp -= Mc_MouseLeftButtonUp;
        }

        private void Oc_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //1.清空所选项
            MapControl mc = this.AssociatedObject.Owner;
            IMap map = mc.DataContext as IMap;
            string mapName = map.Name;

            LayerControl lc = this.AssociatedObject;
            ILayer layer = lc.DataContext as ILayer;

            //foreach (IObject n in layer.Objects)
            //{
            //    n.IsSelected = false;
            //}
            FrameworkElement element = e.OriginalSource as FrameworkElement;
            IObject obj = element.DataContext as IObject;

            IEnumerable<ObjectControl> listOc = FindChild.FindVisualChildren<ObjectControl>(lc);
            foreach (ObjectControl oc in listOc)
            {
                IObject o = oc.DataContext as IObject;
                o.IsSelected = false;
                IEnumerable<Path> paths = FindChild.FindVisualChildren<Path>(oc);
                foreach (Path pt in paths)
                {
                    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(pt);
                    Adorner[] ads = adornerLayer.GetAdorners(pt);
                    if (ads != null)
                    {
                        for (int i = ads.Length - 1; i >= 0; i--)
                        {
                            adornerLayer.Remove(ads[i]);
                        }
                    }
                }
            }

            if (element is Shape)
            {
                Shape shape = element as Shape;
                double defaultThick = shape.StrokeThickness;
                obj.IsSelected = true;

                //BindingExpression exp = element.GetBindingExpression(Shape.StrokeThicknessProperty);
                //if (exp == null)
                //{
                //    Binding bd = new Binding("IsSelected") { };
                //    bd.Source = obj;
                //    bd.Converter = new IsSelectedConverter();
                //    bd.ConverterParameter = defaultThick;
                //    bd.Mode = BindingMode.OneWay;
                //    element.SetBinding(Shape.StrokeThicknessProperty, bd);
                //}
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(element);
                OutlineAdorner conrner = new OutlineAdorner(element);
                adornerLayer.Add(conrner);
            }

            if (OnSelectObject != null && obj != null)
            {
                OnSelectObject.Invoke(obj);
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

            Pen renderPen = new Pen(new SolidColorBrush(Colors.Red), 2.0);
            renderPen.DashStyle = new DashStyle(new DoubleCollection() { 2,2}, 0);
            
            drawingContext.DrawRectangle(null, renderPen, adornedElementRect);

        }
    }



}
