using gEngine.Graph.Ge.Basic;
using gEngine.Manipulator;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace gEngine.Manipulator.Ge.Basic
{
    public delegate void FinishDrawBoundaryDelegate(Boundary border);
    public class BoundaryManipulator: PolyLineManipulator
    {
        public Boundary BoundaryObj { get; set; }
        public event FinishDrawBoundaryDelegate OnFinishDrawBoundary;

        public BoundaryManipulator()
        {
            BoundaryObj = new Boundary();
        }
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            base.MouseLeftButtonUp(sender,e);
        }

        protected override void MouseMove(object sender, MouseEventArgs e)
        {
            base.MouseMove(sender, e);
        }

        protected override void MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(this.TrackAdorner.Points.Count>0)
            {
                this.TrackAdorner.Points.RemoveAt(this.TrackAdorner.Points.Count - 1);
                BoundaryObj.Points = new PointCollection(this.TrackAdorner.Points);
                BoundaryObj.Stroke = new SolidColorBrush(Colors.Black);
                BoundaryObj.Fill = new SolidColorBrush(Colors.Transparent);
                BoundaryObj.StrokeThickness = 1.0;
                if (OnFinishDrawBoundary != null )
                {
                    OnFinishDrawBoundary.Invoke(BoundaryObj);
                }
                base.MouseRightButtonUp(sender, e);
            }
        }
    }

    public class BoundaryManipulatorFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "BoundaryManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new BoundaryManipulator();
        }
    }
}
