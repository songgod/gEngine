using gEngine.Commands;
using gEngine.Graph.Ge.Column;
using gEngine.View;
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

namespace gEngine.Manipulator.Ge.Column
{
    public class EditWellManipulator : ObjectManipulator
    {
        public Rectangle TrackAdorner { get; set; }
        public Rectangle TrackAdorner1 { get; set; }
        public Rectangle TrackAdorner2 { get; set; }
        public Rectangle TrackAdorner3 { get; set; }
        public Rectangle TrackAdorner4 { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;
            ObjectControl oc = this.AssociatedObject;

            //oc.MouseRightButtonDown += Oc_MouseRightButtonDown;

            LayerControl lc = oc.Owner;
            MapControl mc = lc.Owner;
            if (mc == null)
                return;

            Style style = new Style();
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeProperty, Value = new SolidColorBrush(Color.FromRgb(120, 162, 239)) });
            style.Setters.Add(new Setter() { Property = Rectangle.StrokeThicknessProperty, Value = 1.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.FillProperty, Value = Brushes.White });
            style.Setters.Add(new Setter() { Property = Rectangle.CursorProperty, Value = Cursors.Hand });
            style.Setters.Add(new Setter() { Property = Rectangle.WidthProperty, Value = 5.0 });
            style.Setters.Add(new Setter() { Property = Rectangle.HeightProperty, Value = 5.0 });
            TrackAdorner = new Rectangle();
            TrackAdorner1 = new Rectangle() { Style = style };
            TrackAdorner2 = new Rectangle() { Style = style };
            TrackAdorner3 = new Rectangle() { Style = style };
            TrackAdorner4 = new Rectangle() { Style = style };

            Path path = FindChild.FindVisualChild<Path>(oc, "pathLogsBorder");
            Rect rect = VisualTreeHelper.GetDescendantBounds(path);

            rect.X = rect.X + ((gEngine.Graph.Ge.Column.Well) oc.DataContext).Location;
            rect.Y = rect.Y;
            Canvas.SetLeft(TrackAdorner1, rect.Left - TrackAdorner1.Width);
            Canvas.SetTop(TrackAdorner1, rect.Top - TrackAdorner1.Height);
            Canvas.SetLeft(TrackAdorner2, rect.Right);
            Canvas.SetTop(TrackAdorner2, rect.Top - TrackAdorner2.Height);
            Canvas.SetLeft(TrackAdorner3, rect.Left - TrackAdorner3.Width);
            Canvas.SetTop(TrackAdorner3, rect.Bottom);
            Canvas.SetLeft(TrackAdorner4, rect.Right);
            Canvas.SetTop(TrackAdorner4, rect.Bottom);

            Canvas.SetLeft(TrackAdorner, rect.Left);
            Canvas.SetTop(TrackAdorner, rect.Top);
            TrackAdorner.StrokeThickness = 1;
            TrackAdorner.Stroke = new SolidColorBrush(Color.FromRgb(120, 162, 239));
            TrackAdorner.Width = rect.Width;
            TrackAdorner.Height = rect.Height;

            mc.EditLayer.Children.Add(TrackAdorner);
            mc.EditLayer.Children.Add(TrackAdorner1);
            mc.EditLayer.Children.Add(TrackAdorner2);
            mc.EditLayer.Children.Add(TrackAdorner3);
            mc.EditLayer.Children.Add(TrackAdorner4);

            mc.MouseRightButtonDown += Mc_MouseRightButtonDown;
        }

        private void Mc_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.AssociatedObject == null)
                return;
            LayerControl lc = this.AssociatedObject.Owner;
            MapControl mc = lc.Owner;

            Point pt = e.GetPosition(mc);
            VisualTreeHelper.HitTest(mc, null,
                            new HitTestResultCallback(MyHitTestResult), new PointHitTestParameters(pt));
        }

        // 应该把右键菜单加到ObjectControl，但是被EditLayer遮挡，暂时先加到EditLayer上 2017-7-21
        private HitTestResultBehavior MyHitTestResult(HitTestResult hr)
        {
            LayerControl lc = this.AssociatedObject.Owner;
            MapControl mc = lc.Owner;
            DependencyObject p = hr.VisualHit;
            while (p != null)
            {
                ObjectControl oc = p as ObjectControl;
                if (oc != null)
                {
                    ContextMenu contextMenu = new ContextMenu();
                    MenuItem saveModel = new MenuItem();
                    saveModel.Header = "保存模板";
                    saveModel.Command = ColumnCommands.SaveTemplateCommand;
                    saveModel.CommandParameter = oc;
                    MenuItem changeModel = new MenuItem();
                    changeModel.Header = "更换模板";
                    changeModel.Command = ColumnCommands.ChangeTemplateCommand;
                    changeModel.CommandParameter = oc;

                    contextMenu.Items.Add(saveModel);
                    contextMenu.Items.Add(changeModel);
                    mc.EditLayer.ContextMenu = contextMenu;
                    return HitTestResultBehavior.Stop;
                }
                p = VisualTreeHelper.GetParent(p);
            }
            return HitTestResultBehavior.Continue;
        }

        private void Oc_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ObjectControl oc = this.AssociatedObject;

            ContextMenu contextMenu = new ContextMenu();
            MenuItem saveModel = new MenuItem();
            saveModel.Header = "保存模板";
            saveModel.Command = ColumnCommands.SaveTemplateCommand;
            saveModel.CommandParameter = oc;
            MenuItem changeModel = new MenuItem();
            changeModel.Command = ColumnCommands.ChangeTemplateCommand;
            changeModel.CommandParameter = oc;
            changeModel.Header = "更换模板";

            contextMenu.Items.Add(saveModel);
            contextMenu.Items.Add(changeModel);
            oc.ContextMenu = contextMenu;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            LayerControl lc = this.AssociatedObject.Owner;
            MapControl mc = lc.Owner;
            if (mc == null)
                return;
            mc.EditLayer.Children.Clear();

        }
    }

    public class EditWellManipulatorFactory : IObjectManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "EditWellManipulator";
            }
        }

        public Type SupportIObjectType
        {
            get
            {
                return typeof(Well);
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new EditWellManipulator();
        }
    }
}
