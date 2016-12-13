using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gEngine.Graph.Interface;
using gEngine.Utility;

namespace gEngine.Graph.Ge
{
    public class Well : Object, IWell
    {
        public Well()
        {
            Columns = new IWellColumns();
            Depths = new ObsDoubles();
        }

        private IWellColumns columns;
        public IWellColumns Columns
        {
            get
            {
                return columns;
            }

            set
            {
                columns = value;
            }
        }

        private ObsDoubles depths;
        public ObsDoubles Depths
        {
            get
            {
                return depths;
            }

            set
            {
                depths = value;
            }
        }
    }
}
