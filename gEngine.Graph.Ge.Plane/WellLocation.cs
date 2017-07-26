using System;
using System.Windows;
using static gEngine.Graph.Ge.Plane.Enums;

namespace gEngine.Graph.Ge.Plane
{

    public class WellLocation : Object
    {
        public WellLocation()
        {
            PointStyle = new PointStyle() { SymbolLib = "ge", Symbol = "GeEllipsePointSymbol" };
        }

        public string WellNum
        {
            get { return (string)GetValue(WellNumProperty); }
            set { SetValue(WellNumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WellNum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WellNumProperty =
            DependencyProperty.Register("WellNum", typeof(string), typeof(WellLocation));



        public WellCategory WellCategory
        {
            get { return (WellCategory)GetValue(WellCategoryProperty); }
            set { SetValue(WellCategoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WellCategory.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WellCategoryProperty =
            DependencyProperty.Register("WellCategory", typeof(WellCategory), typeof(WellLocation));




        public WellType WellType
        {
            get { return (WellType)GetValue(WellTypeProperty); }
            set { SetValue(WellTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WellType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WellTypeProperty =
            DependencyProperty.Register("WellType", typeof(WellType), typeof(WellLocation));




        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        // Using a DependencyProperty as the backing store for X.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(WellLocation));



        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Y.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(WellLocation));



        public PointStyle PointStyle
        {
            get { return (PointStyle)GetValue(PointStyleProperty); }
            set { SetValue(PointStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PointStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointStyleProperty =
            DependencyProperty.Register("PointStyle", typeof(PointStyle), typeof(WellLocation));
    }
}
