using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Util
{
    public interface IView
    {
        FrameworkElement FullScreenObject { get; set; }
    }
}
