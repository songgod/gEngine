using gEngine.Graph.Ge.Plane;
using gEngine.View;
using gTopology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gEngine.Manipulator.Ge.Section
{
    public class DrawWellTieSection : GraphLineManipulator
    {
        public HashSet<string> WellNumList { get; set; }

        public DrawWellTieSection()
        {
            WellNumList = new HashSet<string>();
        }

        #region Override MouseEvents
        public override void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.MouseLeftButtonDown(sender, e);

            Type clickSourceType = e.OriginalSource.GetType();
            if (clickSourceType.Equals(typeof(Path)))
            {
                Path path = (Path)e.OriginalSource;
                Point p = PathToPoint(path);
                Start = p;
                End = p;

                WellLocation well = PathToWellLocation(path);
                if (well != null)
                {
                    SelectPath(path);
                    WellNumList.Add(well.WellNum);
                }
            }
            else
            {
                Start = new Point(-1, -1);
            }
        }


        public override void MouseMove(object sender, MouseEventArgs e)
        {
            base.MouseMove(sender, e);

            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Type clickSourceType = e.OriginalSource.GetType();

                if (clickSourceType.Equals(typeof(Path)))
                {
                    Path path = (Path)e.OriginalSource;
                    Point p = PathToPoint(path);

                    End = p;
                }
                else
                {
                    End = new Point(-1, -1);
                }
            }
        }
        public override void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            gTopology.Graph graph = Graph;
            if (graph == null)
                return;

            Topology editor = new Topology(graph);

            Type clickSourceType = e.OriginalSource.GetType();

            if (Start.X >= 0 && Start.Y >= 0 && End.X >= 0 && End.Y >= 0 && Start != End)
            {
                editor.LinAddLine(Start, End, Tolerance);
                if (clickSourceType.Equals(typeof(Path)))
                {
                    Path path = (Path)e.OriginalSource;
                    Point p = PathToPoint(path);
                    WellLocation well = PathToWellLocation(path);

                    if (well != null)
                    {
                        path.StrokeThickness = 3;
                        path.Stroke = Brushes.Black;
                        WellNumList.Add(well.WellNum);
                    }
                }
                else if (clickSourceType.Equals(typeof(System.Windows.Shapes.Line)))
                {
                    IEnumerable<Path> paths = FindChild.FindVisualChildren<Path>(TrackAdorner.AdornedElement);
                    foreach (Path path in paths)
                    {
                        WellLocation well = PathToWellLocation(path);
                        if (well != null)
                        {
                            if (well.X == End.X && well.Y == End.Y)
                            {
                                path.StrokeThickness = 3;
                                path.Stroke = Brushes.Black;
                                WellNumList.Add(well.WellNum);
                            }
                        }
                    }
                }

            }
            base.MouseLeftButtonUp(sender, e);

        }
        #endregion

        #region Private Method

        private Point PathToPoint(Path path)
        {
            double x = path.RenderTransform.Value.OffsetX;
            double y = path.RenderTransform.Value.OffsetY;
            Point p = new Point(x, y);
            return p;
        }

        private void SelectPath(Path path)
        {
            path.StrokeThickness = 3;
            path.Stroke = Brushes.Black;
        }

        private WellLocation PathToWellLocation(Path path)
        {
            StackPanel stackPanel = VisualTreeHelper.GetParent(path) as StackPanel;
            if (stackPanel == null)
                return null;
            ContentPresenter dataTemplate = VisualTreeHelper.GetParent(stackPanel) as ContentPresenter;
            if (dataTemplate == null)
                return null;
            WellLocation well = dataTemplate.DataContext as WellLocation;
            if (well == null)
                return null;
            return well;
        } 
        #endregion
    }
}
