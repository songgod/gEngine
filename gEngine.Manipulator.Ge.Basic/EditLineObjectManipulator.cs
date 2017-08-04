﻿using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator.Ge.Basic
{
    public class EditLineObjectManipulator : ObjectManipulator
    {
        #region Property

        public Rectangle TrackAdorner1 { get; set; }
        public Rectangle TrackAdorner2 { get; set; }

        public Rectangle _hitElement { get; private set; }

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
            Point p1 = ((gEngine.Graph.Ge.Basic.Line) oc.DataContext).Start;
            Point p2 = ((gEngine.Graph.Ge.Basic.Line) oc.DataContext).End;

            Style style = new Style();
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeProperty, Value = new SolidColorBrush(Color.FromRgb(120, 162, 239)) });
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeThicknessProperty, Value = 1.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.FillProperty, Value = Brushes.White });
            style.Setters.Add(new Setter() { Property = Rectangle.CursorProperty, Value = Cursors.Hand });
            style.Setters.Add(new Setter() { Property = Rectangle.WidthProperty, Value = 5.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.HeightProperty, Value = 5.0 });

            TrackAdorner1 = new Rectangle() { Name = "Start", Style = style };
            TrackAdorner2 = new Rectangle() { Name = "End", Style = style };

            Canvas.SetLeft(TrackAdorner1, p1.X - TrackAdorner1.Width / 2);
            Canvas.SetTop(TrackAdorner1, p1.Y - TrackAdorner1.Height / 2);
            Canvas.SetLeft(TrackAdorner2, p2.X - TrackAdorner2.Width / 2);
            Canvas.SetTop(TrackAdorner2, p2.Y - TrackAdorner2.Height / 2);

            mc.EditLayer.Children.Add(TrackAdorner1);
            mc.EditLayer.Children.Add(TrackAdorner2);

            mc.MouseMove += Mc_MouseMove;
            mc.MouseLeftButtonDown += Mc_MouseLeftButtonDown;
            mc.MouseLeftButtonUp += Mc_MouseLeftButtonUp;
        }

        protected override void OnDetaching()
        {
            LayerControl lc = this.AssociatedObject.Owner;
            MapControl mc = lc.Owner;
            if (mc == null)
                return;
            mc.EditLayer.Children.Clear();
            mc.MouseMove -= Mc_MouseMove;
            mc.MouseLeftButtonDown -= Mc_MouseLeftButtonDown;
            mc.MouseLeftButtonUp -= Mc_MouseLeftButtonUp;
        }

        #region Event

        private void Mc_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.AssociatedObject == null)
                return;

            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;

            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                if (_hitElement!=null)
                {
                    Point p = mc.Dp2LP(e.GetPosition(mc));
                    if (_hitElement.Name.Equals("Start"))
                    {
                        Canvas.SetLeft(TrackAdorner1, p.X - TrackAdorner1.Width / 2);
                        Canvas.SetTop(TrackAdorner1, p.Y - TrackAdorner1.Height / 2);
                        ((gEngine.Graph.Ge.Basic.Line) oc.DataContext).Start = p;
                    }
                    if (_hitElement.Name.Equals("End"))
                    {
                        Canvas.SetLeft(TrackAdorner2, p.X - TrackAdorner2.Width / 2);
                        Canvas.SetTop(TrackAdorner2, p.Y - TrackAdorner2.Height / 2);
                        ((gEngine.Graph.Ge.Basic.Line) oc.DataContext).End = p;
                    }
                }
            }
        }

        private void Mc_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            if (this.AssociatedObject == null)
                return;

            ObjectControl oc = this.AssociatedObject;
            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;

            Point pt = e.GetPosition(mc);

            VisualTreeHelper.HitTest(mc, null, new HitTestResultCallback(MyHitTestResult),
                new PointHitTestParameters(pt));
        }

        private HitTestResultBehavior MyHitTestResult(HitTestResult hr)
        {
            DependencyObject p = hr.VisualHit;
            while (p != null)
            {
                Rectangle rect = p as Rectangle;
                if (rect != null)
                {
                    _hitElement = rect;
                    return HitTestResultBehavior.Stop;
                }
                p = VisualTreeHelper.GetParent(p);
            }
            return HitTestResultBehavior.Continue;
        }

        private void Mc_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            _hitElement = null;
        }

        #endregion
    }

    public class EditLineObjectFactory : IObjectManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "EditLineObjectManipulator";
            }
        }

        public Type SupportIObjectType
        {
            get
            {
                return typeof(gEngine.Graph.Ge.Basic.Line);
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            EditLineObjectManipulator dm = new EditLineObjectManipulator();
            return dm;
        }
    }
}
