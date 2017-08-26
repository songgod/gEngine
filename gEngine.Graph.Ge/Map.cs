using gEngine.Graph.Interface;
using gEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace gEngine.Graph.Ge
{
    
    public class Map : Base, IMap
    {
        public Map()
        {
            Layers = new ILayers();
            UndoRedoCmdMgr = new UndoRedoCommandManager();
        }

        public string Type
        {
            get
            {
                return "Ge";
            }
        }

        public UndoRedoCommandManager UndoRedoCmdMgr { get; set; }

        public string Manipulator
        {
            get { return (string)GetValue(ManipulatorProperty); }
            set { SetValue(ManipulatorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Manipulator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ManipulatorProperty =
            DependencyProperty.Register("Manipulator", typeof(string), typeof(Map), new PropertyMetadata(""));



        public Matrix ViewMatrix
        {
            get { return (Matrix)GetValue(ViewMatrixProperty); }
            set { SetValue(ViewMatrixProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewMatrix.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewMatrixProperty =
            DependencyProperty.Register("ViewMatrix", typeof(Matrix), typeof(Map), new PropertyMetadata(Matrix.Identity));



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
