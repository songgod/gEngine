using gEngine.Graph.Ge.Plane;
using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Plane;
using gEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator.Ge.Plane
{
    public class UndoConnectWellCommand : IUndoRedoCommand
    {
        private WellLocationsConnectManipulator _mp;
        private Point _p;
        private string _wellNum;
        public UndoConnectWellCommand(WellLocationsConnectManipulator mp, WellLocation wl)
        {
            _mp = mp;
            _p = new Point(wl.X, wl.Y); 
            _wellNum = wl.WellNum;
        }
        public void Undo()
        {
            _mp.TrackAdorner.Points.Remove(_p);
            if (_mp.TrackAdorner.Points.Count == 1)
                _mp.TrackAdorner.Points.RemoveAt(_mp.TrackAdorner.Points.Count - 1);
            _mp.SelectWellLocations.Remove(_wellNum);
        }
        public void Redo()
        {
            if (_mp.TrackAdorner.Points.Count > 1)
            {
                _mp.TrackAdorner.Points.RemoveAt(_mp.TrackAdorner.Points.Count - 1);
            }
            _mp.TrackAdorner.Points.Add(_p);
            _mp.TrackAdorner.Points.Add(_p);
            _mp.SelectWellLocations.Add(_wellNum);
        }

        
    }
}
