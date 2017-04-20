using gEngine.Data.Interface;
using gEngine.Util;
using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gEngine.Data.Ge
{
    public class DBHorizon : DBBase, IDBHorizon
    {
        public DBHorizon()
        {
            SHorizonDatas = new List<string>();
            DHorizonDatas = new List<double>();
        }

        public string LayerNumber
        {
            get { return (string) GetValue(LayerNumberProperty); }
            set { SetValue(LayerNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayerNumberProperty =
            DependencyProperty.Register("LayerNumber", typeof(string), typeof(DBHorizon));

        public double Top_MeasuredDepth
        {
            get { return (double) GetValue(Top_MeasuredDepthProperty); }
            set { SetValue(Top_MeasuredDepthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Top_MeasuredDepthProperty =
            DependencyProperty.Register("Top_MeasuredDepth", typeof(double), typeof(DBHorizon));

        public double MeasuredThickness
        {
            get { return (double) GetValue(MeasuredThicknessProperty); }
            set { SetValue(MeasuredThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MeasuredThicknessProperty =
            DependencyProperty.Register("MeasuredThickness", typeof(double), typeof(DBHorizon));

        public List<string> SHorizonDatas
        {
            get { return (List<string>) GetValue(SHorizonDatasProperty); }
            set { SetValue(SHorizonDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SHorizonDatasProperty =
            DependencyProperty.Register("SHorizonDatas", typeof(List<string>), typeof(DBHorizon));

        public List<double> DHorizonDatas
        {
            get { return (List<double>) GetValue(DHorizonDatasProperty); }
            set { SetValue(DHorizonDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DHorizonDatasProperty =
            DependencyProperty.Register("DHorizonDatas", typeof(List<double>), typeof(DBHorizon));

    }

    public class DBHorizons : IDBHorizons
    {
        public DBHorizons()
        {
            Init();
        }

        #region Property

        private string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        private List<string> colNames;
        public List<string> ColNames
        {
            get
            {
                return colNames;
            }
            set
            {
                colNames = value;
            }
        }

        private List<IDBHorizon> horizons;
        public List<IDBHorizon> Horizons
        {
            get
            {
                return horizons;
            }
            set
            {
                horizons = value;
            }
        }

        private List<int> type;
        public List<int> Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        #endregion

        #region Method

        private void Init()
        {
            ColNames = new List<string>();
            Horizons = new List<IDBHorizon>();

            // 记录字符列
            type = new List<int>();
            type.Add(9);
            type.Add(14);
            type.Add(15);
        }

        #endregion
    }
}
