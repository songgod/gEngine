﻿using gEngine.Graph.Interface;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace gEngine.View
{
    /// <summary>
    /// LayerControl.xaml 的交互逻辑
    /// </summary>
    public partial class LayerControl : ItemsControl
    {
        public LayerControl()
        {
            InitializeComponent();
        }

        public Canvas Root
        {
            get
            {
                return FindChild.FindVisualChild<Canvas>(this, "layerpanel");
            }
        }

        public MapControl Owner
        {
            get
            {
                return FindParent.FindVisualParent<MapControl>(this);
            }
        }

        public ILayer LayerContext
        {
            get
            {
                return FindContext.Find<ILayer>(this);
            }
        }

        public Rect GetRect()
        {
            return ViewUtil.GetTypeRect<ObjectControl>(Root);
        }

    }
}
