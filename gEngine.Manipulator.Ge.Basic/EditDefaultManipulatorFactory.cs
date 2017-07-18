using gEngine.Graph.Ge.Plane;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Manipulator.Ge.Basic
{
    public class EditDefaultManipulatorFactory : IObjectManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "EditDefaultManipulator";
            }
        }

        public Type SupportIObjectType
        {
            get
            {
                return typeof(WellLocation);
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new EditDefaultManipulator();
        }
    }
}
