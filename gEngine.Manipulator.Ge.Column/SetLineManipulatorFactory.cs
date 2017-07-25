using gEngine.Graph.Ge.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Manipulator.Ge.Column
{
    class SetLineManipulatorFactory : IObjectManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "SetLineManipulatorFactory";
            }
        }

        public Type SupportIObjectType
        {
            get
            {
                return typeof(Line);
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new SetLineManipulator();
        }
    }
}
