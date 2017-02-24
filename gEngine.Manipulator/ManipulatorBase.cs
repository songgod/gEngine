using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace gEngine.Manipulator
{
    public class ManipulatorBase : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;

            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
            this.AssociatedObject.MouseRightButtonDown += AssociatedObject_MouseRightButtonDown;
            this.AssociatedObject.MouseRightButtonUp += AssociatedObject_MouseRightButtonUp;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.AssociatedObject == null)
                return;
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
            this.AssociatedObject.MouseRightButtonDown -= AssociatedObject_MouseRightButtonDown;
            this.AssociatedObject.MouseRightButtonUp -= AssociatedObject_MouseRightButtonUp;
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove(sender, e);
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MouseLeftButtonUp(sender, e);
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MouseLeftButtonDown(sender, e);
        }

        private void AssociatedObject_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            MouseRightButtonUp(sender, e);
        }

        private void AssociatedObject_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MouseRightButtonDown(sender, e);
        }

        public virtual void MouseMove(object sender, MouseEventArgs e) { }
        public virtual void MouseLeftButtonUp(object sender, MouseButtonEventArgs e) { }
        public virtual void MouseLeftButtonDown(object sender, MouseButtonEventArgs e) { }
        public virtual void MouseRightButtonDown(object sender, MouseButtonEventArgs e) { }
        public virtual void MouseRightButtonUp(object sender, MouseButtonEventArgs e) { }
    }
}
