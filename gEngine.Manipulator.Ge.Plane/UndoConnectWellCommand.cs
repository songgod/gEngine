using gEngine.Manipulator;
using gEngine.Manipulator.Ge.Plane;
using gEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Manipulator.Ge.Plane
{
    public class UndoConnectWellCommand : IUndoRedoCommand
    {
        private WellLocationsConnectManipulator _mp;
        private Point _p;
        private string _wellNum;
        public UndoConnectWellCommand(WellLocationsConnectManipulator mp, Point p,string wellNum)
        {
            _mp = mp;
            _p = p;
            _wellNum = wellNum;
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
