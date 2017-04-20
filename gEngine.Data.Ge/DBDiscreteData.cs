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
    public class DBDiscreteData : DBBase, IDBDiscreteData
    {
        public DBDiscreteData()
        {
            SDiscreteDatas = new List<string>();
            DDiscreteDatas = new List<double>();
        }

        public string LayerNumber
        {
            get { return (string) GetValue(LayerNumberProperty); }
            set { SetValue(LayerNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayerNumberProperty =
            DependencyProperty.Register("LayerNumber", typeof(string), typeof(DBDiscreteData));

        public string SerialNumber
        {
            get { return (string) GetValue(SerialNumberProperty); }
            set { SetValue(SerialNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SerialNumberProperty =
            DependencyProperty.Register("SerialNumber", typeof(string), typeof(DBDiscreteData));

        public double Top_MeasuredDepth
        {
            get { return (double) GetValue(Top_MeasuredDepthProperty); }
            set { SetValue(Top_MeasuredDepthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Top_MeasuredDepthProperty =
            DependencyProperty.Register("Top_MeasuredDepth", typeof(double), typeof(DBDiscreteData));

        public double MeasuredThickness
        {
            get { return (double) GetValue(MeasuredThicknessProperty); }
            set { SetValue(MeasuredThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MeasuredThicknessProperty =
            DependencyProperty.Register("MeasuredThickness", typeof(double), typeof(DBDiscreteData));

        
        public List<string> SDiscreteDatas
        {
            get { return (List<string>) GetValue(SDiscreteDatasProperty); }
            set { SetValue(SDiscreteDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SDiscreteDatasProperty =
            DependencyProperty.Register("SDiscreteDatas", typeof(List<string>), typeof(DBDiscreteData));

        public List<double> DDiscreteDatas
        {
            get { return (List<double>) GetValue(DDiscreteDatasProperty); }
            set { SetValue(DDiscreteDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Depths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DDiscreteDatasProperty =
            DependencyProperty.Register("DDiscreteDatas", typeof(List<double>), typeof(DBDiscreteData));
    }

    public class DBDiscreteDatas :  IDBDiscreteDatas
    {
        public DBDiscreteDatas()
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

        private List<IDBDiscreteData> discreteDatas;
        public List<IDBDiscreteData> DiscreteDatas
        {
            get
            {
                return discreteDatas;
            }
            set
            {
                discreteDatas = value;
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
            DiscreteDatas = new List<IDBDiscreteData>();

            // 记录字符列
            type = new List<int>();
            type.Add(12);
            type.Add(22);
        }

        #endregion
    }
}
