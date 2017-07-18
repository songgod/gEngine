﻿using gEngine.Graph.Interface;
using System.Windows.Controls;
using System.Windows.Data;

namespace gEngine.View
{
    /// <summary>
    /// ObjectControl.xaml 的交互逻辑
    /// </summary>
    public partial class ObjectControl : ContentControl
    {
        public ObjectControl()
        {
            InitializeComponent();
            Binding bd = new Binding("Visible") { Converter = new BooleanToVisibilityConverter(), Mode = BindingMode.TwoWay };
            BindingOperations.SetBinding(this, VisibilityProperty, bd);
        }

        public LayerControl Owner
        {
            get
            {
                return FindParent.FindVisualParent<LayerControl>(this);
            }
        } 
        
        public IObject ObjectContext
        {
            get
            {
                return FindContext.Find<IObject>(this);
            }
        }  
    }
}
