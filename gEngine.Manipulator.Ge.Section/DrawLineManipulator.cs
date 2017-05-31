﻿using gEngine.Graph.Ge.Section;
using gTopology;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawLineManipulator : LineManipulator
    {
        protected GraphUtil graphutil = null;
        protected override void OnAttached()
        {
            base.OnAttached();
            graphutil = new GraphUtil(this.AssociatedObject);
        }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            gTopology.Graph graph = graphutil.Graph;
            if (graph == null)
                return;

            Topology editor = new Topology(graph);
            Point start = new Point() { X = this.TrackAdorner.X1, Y = this.TrackAdorner.Y1 };
            Point end = new Point() { X = this.TrackAdorner.X2, Y = this.TrackAdorner.Y2 };
            editor.LinAddLine(start, end, graphutil.Tolerance, LineType);

            base.MouseLeftButtonUp(sender, e);
        }

        public int LineType { get; set; }
    }

    public class DLMFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "DrawLineManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new DrawLineManipulator() { LineType = (int)param };
        }
    }
}
