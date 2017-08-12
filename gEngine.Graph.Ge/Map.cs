using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Graph.Ge
{
    public class Map : Base, IMap
    {
        public Map()
        {
            Layers = new ILayers();
        }

        public string Type
        {
            get
            {
                return "Ge";
            }
        }

        public ILayers Layers
        {
            get { return (ILayers)GetValue(LayersProperty); }
            set { SetValue(LayersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Layers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayersProperty =
            DependencyProperty.Register("Layers", typeof(ILayers), typeof(Map));
    }
}
