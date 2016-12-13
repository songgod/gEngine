using gEngine.Graph.Interface;
using gEngine.Utility;

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
    }
}
