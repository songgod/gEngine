using gEngine.Graph.Interface;
using gEngine.Utility;
using static gEngine.Graph.Interface.Enums;

namespace gEngine.Graph.Ge
{
    public class WellColumn : Object, IWellColumn
    {
        public WellColumn()
        {
            Values = new ObsDoubles();
        }

        private IWell owner;
        public IWell Owner
        {
            get
            {
                return owner;
            }

            set
            {
                owner = value;
            }
        }

        private ObsDoubles values;
        public ObsDoubles Values
        {
            get
            {
                return values;
            }

            set
            {
                values = value;
            }
        }
        private MathType mathType;
        public MathType MathTyp
        {
            get
            {
                return mathType;
            }
            set
            {
                mathType = value;
            }
        }

        private double xOffset;
        public double XOffset
        {
            get
            {
                return xOffset;
            }
            set
            {
                xOffset = value;
            }
        }

        private double yOffset;
        public double YOffset
        {
            get
            {
                return yOffset;
            }
            set
            {
                yOffset = value;
            }
        }

        private double scaleX;
        public double ScaleX
        {
            get
            {
                return scaleX;
            }
            set
            {
                scaleX = value;
            }
        }
    }
}
