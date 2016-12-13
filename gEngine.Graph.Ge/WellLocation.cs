using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static gEngine.Graph.Interface.Enums;

namespace gEngine.Graph.Ge
{
    
    public class WellLocation : Object, IWellLocation
    {
        private string wellNum;
        public string WellNum
        {
            get
            {
                return wellNum;
            }

            set
            {
                wellNum = value;
                RaiseProertyChanged("WellNum");
            }
        }

        private WellCategory wellCategory;
        public WellCategory WellCategory
        {
            get
            {
                return wellCategory;
            }

            set
            {
                wellCategory = value;
                RaiseProertyChanged("WellCategory");
            }
        }

        private WellType wellType;
        public WellType WellType
        {
            get
            {
                return wellType;
            }

            set
            {
                wellType = value;
                RaiseProertyChanged("WellType");
            }
        }

        private double wellXaxis;
        public double WellXaxis
        {
            get
            {
                return wellXaxis;
            }

            set
            {
                wellXaxis = value;
                RaiseProertyChanged("WellXaxis");
            }
        }

        private double wellYaxis;
        public double WellYaxis
        {
            get
            {
                return wellYaxis;
            }

            set
            {
                wellYaxis = value;
                RaiseProertyChanged("WellYaxis");
            }
        }
        private double x;
        public double X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
                RaiseProertyChanged("X");
            }
        }

        private double y;
        public double Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
                RaiseProertyChanged("Y");
            }
        }

    }
}
