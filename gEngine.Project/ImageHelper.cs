using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace gEngine.Project
{
    internal static class ImageHelper
    {
        static ImageHelper()
        {
            EditableIcon = new BitmapImage(new Uri(@"..\Images\edit.png", UriKind.Relative));
            UnEditableIcon = new BitmapImage(new Uri(@"..\Images\unedit.png", UriKind.Relative));
            VisibleIcon = new BitmapImage(new Uri(@"..\Images\Visible.png", UriKind.Relative));
            InVisibleIcon = new BitmapImage(new Uri(@"..\Images\Invisible.png", UriKind.Relative));
            DelIcon = new BitmapImage(new Uri(@"..\Images\del.png", UriKind.Relative));
        }

        public static BitmapImage EditableIcon { set; get; }
        public static BitmapImage UnEditableIcon { set; get; }
        public static BitmapImage VisibleIcon { set; get; }
        public static BitmapImage InVisibleIcon { set; get; }

        public static BitmapImage DelIcon { get; set; }
    }
}
