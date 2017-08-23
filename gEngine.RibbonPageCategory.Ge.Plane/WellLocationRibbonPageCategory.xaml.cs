﻿using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Plane;
using gEngine.Symbol;
using gEngine.Symbol.gesym;
using gEngine.View;
using gEngine.View.Ge;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interactivity;
using DevExpress.Xpf.Ribbon;
using gEngine.Application;

namespace gEngine.Application.Ge.Plane
{
    /// <summary>
    /// WellLocationRibbonPageCategory.xaml 的交互逻辑
    /// </summary>
    public partial class WellLocationRibbonPageCategory : GeRibbonPageCategory
    {
        public WellLocationRibbonPageCategory()
        {
            InitializeComponent();
            this.DataContext = new Graph.Ge.Plane.WellLocation();
        }

        public override Type SupportType
        {
            get
            {
                return typeof(WellLocation);
            }
        }
    }
}
