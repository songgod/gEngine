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

namespace gEngineTest.MapDxTab
{
/// <summary>
/// TabControl.xaml 的交互逻辑
/// </summary>
public partial class TabControl : DXTabControl
    {
        public TabControl()
        {
            InitializeComponent();
            
        }

        public TabItemControl GetItemControl(int i)
        {
            return (TabItemControl)Items[i];
        }
        
    }
}
