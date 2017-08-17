using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.View
{
    public class ViewUtil
    { 
        public static void ZoomtoExtent(FrameworkElement elm, Rect rect)
        {
            if (elm == null)
                return;

            double width = elm.ActualWidth;
            double height = elm.ActualHeight;
            if (width <= 0 || height <= 0)
                return;

            int xr = elm.RenderTransform.Value.M11 < 0 ? -1 : 1;
            int yr = -1;// elm.RenderTransform.Value.M22 < 0 ? -1 : 1;

            Matrix mscale = new Matrix();
            double sx = width / rect.Width;
            double sy = height / rect.Height;
            double s = sx < sy ? sx : sy;
            double cx = (rect.Left + rect.Right) / 2;
            double cy = (rect.Top + rect.Bottom) / 2;
            mscale.ScaleAt(s * xr, s * yr, cx, cy);

            Matrix mtrans = new Matrix();
            double xoffset = width / 2 - cx;
            double yoffset = height / 2 - cy;
            mtrans.Translate(xoffset, yoffset);

            Matrix m = Matrix.Identity;
            m = mtrans * m;
            Point p0 = m.Transform(rect.BottomLeft);
            m = mscale * m;
            Point p1 = m.Transform(rect.BottomLeft);
            elm.RenderTransform = new MatrixTransform(m);
        }

        public static void FullView(FrameworkElement elm)
        {
            if (elm == null)
                return;

            System.Windows.Rect rect = VisualTreeHelper.GetDescendantBounds(elm);
            if (rect.Width == 0 || rect.Height == 0)
                return;

            ZoomtoExtent(elm, rect);


        }

        public static Rect GetBoundsWithoutInvalid(Visual elm)
        {
            Rect res = Rect.Empty;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(elm); i++)
            {
                Visual child = VisualTreeHelper.GetChild(elm, i) as Visual;
                if (child!=null)
                {
                    Rect rect = GetBoundsWithoutInvalid(child);
                    if(rect.IsEmpty==false)
                    {
                        res.Union(rect);
                    }
                    
                }
            }
            Rect rect2 = VisualTreeHelper.GetDescendantBounds(elm);
            if(rect2.Width <= 0.0 || rect2.Height <= 0.0)
            {
                rect2 = Rect.Empty;
            }
            res.Union(rect2);
            return res;
        }

        public static Rect GetTypeRect<Type>(DependencyObject obj, bool bRender=true)
            where Type : Visual
        {
            Rect res = Rect.Empty;

            Type t = obj as Type;
            if (t != null)
            {
                Rect rect2 = VisualTreeHelper.GetDescendantBounds(t);
                if (rect2.Width <= 0.0 || rect2.Height <= 0.0)
                {
                    rect2 = Rect.Empty;
                }
                res.Union(rect2);
            }

            
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                Visual child = VisualTreeHelper.GetChild(obj, i) as Visual;
                if (child != null)
                {
                    Rect rect = GetTypeRect<Type>(child, bRender);
                    if (rect.IsEmpty == false)
                    {
                        res.Union(rect);
                    }
                }
            }
            return res;
        }
    }
}
