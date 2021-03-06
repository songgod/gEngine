﻿using gTopology;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawCurveManipulator : GraphCurveManipulator
    {   
        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TrackAdorner.Track.Points.Count == 0)
                return;

            gTopology.Graph graph = Graph;
            if (graph == null)
                return;
            
            Topology editor = new Topology(graph);
            editor.LinAddCurve(TrackPoints, Tolerance, false);
            base.MouseLeftButtonUp(sender, e);
        }
    }
}
