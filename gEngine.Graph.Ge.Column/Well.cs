using gEngine.Utility;
using System;
using System.Windows;

namespace gEngine.Graph.Ge.Column
{
    public class Well : Object
    {
        public Well()
        {
            Columns = new WellColumns();
            Depths = new ObsDoubles();
            WellLayers = new WellLayers();
            WellLayerDatas = new WellLayerDatas();
            WellLayerDatasUI = new WellLayerDatas();
        }

        public WellColumns Columns
        {
            get { return (WellColumns)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(WellColumns), typeof(Well));

        public WellLayers WellLayers
        {
            get { return (WellLayers)GetValue(WellLayersProperty); }
            set { SetValue(WellLayersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WellLayersProperty =
            DependencyProperty.Register("WellLayers", typeof(WellLayers), typeof(Well));

        public WellLayerDatas WellLayerDatas
        {
            get { return (WellLayerDatas)GetValue(WellLayerDatasProperty); }
            set { SetValue(WellLayerDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WellLayerDatasProperty =
            DependencyProperty.Register("WellLayerDatas", typeof(WellLayerDatas), typeof(Well));

        public WellLayerDatas WellLayerDatasUI
        {
            get { return (WellLayerDatas)GetValue(WellLayerDatasUIProperty); }
            set { SetValue(WellLayerDatasUIProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WellLayerDatasUIProperty =
            DependencyProperty.Register("WellLayerDatasUI", typeof(WellLayerDatas), typeof(Well));


        public ObsDoubles Depths
        {
            get { return (ObsDoubles)GetValue(DepthsProperty); }
            set { SetValue(DepthsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DepthsProperty =
            DependencyProperty.Register("Depths", typeof(ObsDoubles), typeof(Well));

        /// <summary>
        /// 每口井位置
        /// </summary>
        public int Location
        {
            get { return (int)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(int), typeof(Well));

        /// <summary>
        /// 井深度道位置
        /// </summary>
        public int DepthsOffset
        {
            get { return (int)GetValue(DepthsOffsetProperty); }
            set { SetValue(DepthsOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DepthsOffsetProperty =
            DependencyProperty.Register("DepthsOffset", typeof(int), typeof(Well));

        public int LongitudinalProportion
        {
            get { return (int)GetValue(LongitudinalProportionProperty); }
            set { SetValue(LongitudinalProportionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LongitudinalProportionProperty =
            DependencyProperty.Register("LongitudinalProportion", typeof(int), typeof(Well));
    }
}
