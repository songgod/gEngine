using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace gEngine.View
{
    public interface IView
    {
        FrameworkElement FullScreenObject { get; set; }
        Behavior<UIElement> ManipulatorBehavior { get; set; }
    }
}
