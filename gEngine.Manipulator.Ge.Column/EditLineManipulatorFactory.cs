using gEngine.Graph.Ge.Basic;
using gEngine.Graph.Ge.Column;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Manipulator.Ge.Column
{
    class EditLineManipulatorFactory : IObjectManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "EditLineManipulatorFactory";
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
            return new EditLineManipulator();
        }
    }
}
