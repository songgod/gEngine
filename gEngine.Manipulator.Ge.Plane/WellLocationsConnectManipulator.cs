using gEngine.Graph.Ge;
using gEngine.Graph.Ge.Plane;
using gEngine.Manipulator;
using gEngine.Util;
using gEngine.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator.Ge.Plane
{
    public delegate void FinishSelectWellLocations(Stack<WellLocation> wellLocs);

    public class WellLocationsConnectManipulator : PolyLineManipulator
    {

        #region 属性
        public Stack<WellLocation> SelectWellLocations { get; set; }
        MapControl _mc;
        public event FinishSelectWellLocations OnFinishSelect;
        #endregion

        #region 构造函数

        public WellLocationsConnectManipulator(MapControl mc)
        {
            SelectWellLocations = new Stack<WellLocation>();
            _mc = mc;
            _mc.Focus();
            _mc.KeyDown += _mc_KeyDown;
        }

        private void _mc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                if (SelectWellLocations.Count <= 0)
                    return;
                WellLocation wl=SelectWellLocations.Pop();
                this.TrackAdorner.Points.Remove(new Point(wl.X,wl.Y));
                if (this.TrackAdorner.Points.Count == 1)
                    this.TrackAdorner.Points.RemoveAt(this.TrackAdorner.Points.Count - 1);
            }
        }
        #endregion

        #region 事件


        /// <summary>
        /// 覆盖父类左键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WellLocation wl = GetClickWell(e);
            if (wl != null&& !SelectWellLocations.Contains(wl))
            {
                SelectWellLocations.Push(wl);
                DrawLine(wl);//这里没有调用父类事件base.MouseLeftButtonUp(sender, e)，因为该事件不能定位井中心
                //AddUndoCommand(e, wl);
            }
        }

        /// <summary>
        /// 覆盖父类鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void MouseMove(object sender, MouseEventArgs e)
        {
            base.MouseMove(sender, e);
        }

        protected override void MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OnFinishSelect != null && SelectWellLocations.Count != 0)
            {
                OnFinishSelect.Invoke(SelectWellLocations);
            }

            SelectWellLocations.Clear();
            //ClearCommands();
            base.MouseRightButtonUp(sender, e);
        }


        #endregion

        #region 方法

        private WellLocation GetClickWell(MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this.AssociatedObject);
            HitTestResult hr = VisualTreeHelper.HitTest(this.AssociatedObject, p);
            if (hr == null || hr.VisualHit == null)
                return null;
            Shape sp = hr.VisualHit as Shape;
            if (sp == null)
                return null;
            WellLocation wl = sp.DataContext as WellLocation;
            if (wl == null)
                return null;
            return wl;
        }

        /// <summary>
        /// 井连线
        /// </summary>
        /// <param name="wl"></param>
        private void DrawLine(WellLocation wl)
        {
            if (this.TrackAdorner.Points.Count > 1)
            {
                this.TrackAdorner.Points.RemoveAt(this.TrackAdorner.Points.Count - 1);
            }
            Point p = new Point(wl.X, wl.Y); //定位井中心
            this.TrackAdorner.Points.Add(p);
            this.TrackAdorner.Points.Add(p);
        }

        #endregion
    }

    public class WLCMFactory : IManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "WellLocationsConnectManipulator";
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new WellLocationsConnectManipulator(param as MapControl);
        }
    }
}
