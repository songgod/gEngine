﻿using DevExpress.Xpf.Core;
using gEngine.Graph.Interface;
using gEngine.Project.Converter;
using gEngine.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace gEngine.Project.Controls
{
    /// <summary>
    /// MapsControl.xaml 的交互逻辑
    /// </summary>
    public partial class MapsControl : DXTabControl
    {
        public MapsControl()
        {
            InitializeComponent();
            this.View.HideButtonShowMode = HideButtonShowMode.InAllTabs;
            this.DataContext = this;
        }

        public IMaps MapsSource
        {
            get { return (IMaps)GetValue(MapsSourceProperty); }
            set { SetValue(MapsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapsSourceProperty =
            DependencyProperty.Register("MapsSource", typeof(IMaps), typeof(MapsControl));


        public int MapControlCount { get { return Items.Count; } }
        public MapControl ActiveMapControl { get { return GetMapControl(SelectedIndex); } }
        public MapControl GetMapControl(int i)
        {
            if (i < 0 || i >= MapControlCount)
                return null;

            if (i >= VisualTreeHelper.GetChildrenCount(FastRenderPanel))
                return null;

            var chd = VisualTreeHelper.GetChild(FastRenderPanel, i);
            if (VisualTreeHelper.GetChildrenCount(chd) == 0)
                return null;

            return VisualTreeHelper.GetChild(chd, 0) as MapControl;
        }

        public int GetMapControlIndex(MapControl mc)
        {
            if (mc == null)
                return -1;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(FastRenderPanel); i++)
            {
                var chd = VisualTreeHelper.GetChild(FastRenderPanel, i);
                if(VisualTreeHelper.GetChildrenCount(chd)>0 
                    && VisualTreeHelper.GetChild(chd, 0) == mc)
                    return i;
            }
            return -1;
        }
    }
}
