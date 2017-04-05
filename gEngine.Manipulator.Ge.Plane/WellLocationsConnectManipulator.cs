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
    public delegate void FinishSelectWellLocations(HashSet<string> names);
    public delegate void DrawWellLineDelegate(IUndoRedoCommand urc);

    public class WellLocationsConnectManipulator : PolyLineManipulator, IUndoRedoCommand
    {
        #region 属性
        public bool IsStopMove { get; set; }
        public HashSet<string> SelectWellLocations { get; set; }
        public Stack<string> UndoSelectWellLocationstHistory { get; set; }
        public HashSet<Point> WellPointList { get; set; }
        public Stack<Point> UndoWellPointListHistory { get; set; }

        public string Title
        {
            get; set;
        }

        public event FinishSelectWellLocations OnFinishSelect;
        public event DrawWellLineDelegate OnDrawWellLine;
        #endregion

        #region 构造函数

        public WellLocationsConnectManipulator()
        {
            InitPara();
        }
        public void InitPara()
        {
            SelectWellLocations = new HashSet<string>();
            UndoSelectWellLocationstHistory = new Stack<string>();
            WellPointList = new HashSet<Point>();
            UndoWellPointListHistory = new Stack<Point>();
            IsStopMove = false;
        }

        #endregion

        #region 事件
        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
                return;
            LayerControl lc = this.AssociatedObject as LayerControl;
            MapControl mc = FindChild.FindVisualParent<MapControl>(lc);
            mc.MouseMove += Mc_MouseMove;
            mc.MouseLeftButtonUp += Mc_MouseLeftButtonUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.AssociatedObject == null)
                return;
            LayerControl lc = this.AssociatedObject as LayerControl;
            MapControl mc = FindChild.FindVisualParent<MapControl>(lc);
            mc.MouseMove -= Mc_MouseMove;
            mc.MouseLeftButtonUp -= Mc_MouseLeftButtonUp;
        }

        private void Mc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WellLocation wl = GetClickWell(e);
            if (wl != null)
            {
                LayerControl lc = this.AssociatedObject as LayerControl;
                SelectWellLocations.Add(wl.WellNum);
                WellPosition = new Point(wl.X, wl.Y);
                Point p = lc.Root.RenderTransform.Transform(WellPosition);//转换到圆心
                WellPointList.Add(p);

                this.TrackAdorner.RemoveLastPoint();
                this.TrackAdorner.AddPoint(p);
                this.TrackAdorner.AddPoint(p);

                //执行画线事件
                if (OnDrawWellLine != null)
                {
                    UndoWellPointListHistory.Clear();
                    UndoSelectWellLocationstHistory.Clear();
                    OnDrawWellLine.Invoke(this);
                }
            }
        }

        private void Mc_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsStopMove)
                return;
            base.MouseMove(sender, e);
        }


        /// <summary>
        /// 覆盖父类左键事件（不执行）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }
        /// <summary>
        /// 覆盖父类鼠标移动事件（不执行）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void MouseMove(object sender, MouseEventArgs e)
        {
        }

        public override void MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OnFinishSelect != null && SelectWellLocations.Count != 0)
            {
                Point lastPoint = this.TrackAdorner.Track.Points[this.TrackAdorner.Track.Points.Count - 1];
                if (!WellPointList.Contains(lastPoint))
                {
                    this.TrackAdorner.Track.Points.Remove(lastPoint);
                }
                IsStopMove = true;
                OnFinishSelect.Invoke(SelectWellLocations);
            }

            base.MouseRightButtonUp(sender, e);
        }
        #endregion

        #region 方法

        private WellLocation GetClickWell(MouseButtonEventArgs e)
        {
            if (IsStopMove)
                return null;
            LayerControl lc = this.AssociatedObject as LayerControl;
            Point p = e.GetPosition(lc);
            HitTestResult hr = VisualTreeHelper.HitTest(lc, p);
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

        public void Undo()
        {
            string lastWell = SelectWellLocations.Last();

            //1.没有结束画线，lastPoint是多余点
            if (!IsStopMove)
            {
                this.TrackAdorner.Track.Points.Remove(WellPointList.Last());
            }
            //2.结束画线
            else
            {
                this.TrackAdorner.RemoveLastPoint();
            }

            UndoSelectWellLocationstHistory.Push(lastWell);
            UndoWellPointListHistory.Push(WellPointList.Last());
            WellPointList.Remove(WellPointList.Last());
            SelectWellLocations.Remove(lastWell);
        }

        public void Redo()
        {
            Point p = UndoWellPointListHistory.Pop();
            string s = UndoSelectWellLocationstHistory.Pop();

            if (!IsStopMove)
            {
                this.TrackAdorner.Track.Points.Insert(this.TrackAdorner.Track.Points.Count - 1, p);
            }
            else
            {
                this.TrackAdorner.Track.Points.Add(p);
            }

            WellPointList.Add(p);
            SelectWellLocations.Add(s);
        }
        #endregion
    }
}
