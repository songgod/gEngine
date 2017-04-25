﻿using System;
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
using System.Globalization;
using DevExpress.Xpf.Core;
using gEngine.View;
using gSection.ViewModel;
using gEngine.Graph.Interface;

namespace gSection.View
{
    /// <summary>
    /// TabControl.xaml 的交互逻辑
    /// </summary>
    public partial class TabControl : DXTabControl
    {
        public TabControl()
        {
            InitializeComponent();

            this.View.HideButtonShowMode = HideButtonShowMode.InAllTabs;
            this.TabHidden += TabControl_TabHidden;
            Project.Single.OpenMaps.OnItemAdded += OpenMaps_OnItemAdded;
            
        }

        private void TabControl_TabHidden(object sender, TabControlTabHiddenEventArgs e)
        {
            Project.Single.OpenMaps.RemoveAt(e.TabIndex);
        }

        private void OpenMaps_OnItemAdded(int aIndex, IMap aItem)
        {
            this.SelectItem(aIndex);
        }

        public int MapControlCount
        {
            get
            {
                return Items.Count;
            }
        }

        public MapControl GetMapControl(int i)
        {
            if (i < 0 || i >= MapControlCount)
                return null;

            var chd = VisualTreeHelper.GetChild(FastRenderPanel, SelectedIndex);
            return VisualTreeHelper.GetChild(chd, 0) as MapControl;
        }

        public int ActiveMapControlIndex
        {
            get
            {
                return SelectedIndex;
            }
            set
            {
                SelectedIndex = value;
            }
        }

        public MapControl ActiveMapControl
        {
            get
            {
                return GetMapControl(ActiveMapControlIndex);
            }
        }

        
    }
}
