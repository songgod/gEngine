using gEngine.Graph.Ge.Column;
using gEngine.Graph.Interface;
using gEngine.Manipulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Manipulator.Ge.Column
{
    public class EditRectangleManipulatorFactory: IObjectManipulatorFactory
    {
        public string Name
        {
            get
            {
                return "EditRectangleManipulator";
            }
        }

        public Type SupportIObjectType
        {
            get
            {
                return typeof(Well);
            }
        }

        public IManipulatorBase CreateManipulator(object param)
        {
            return new EditRectangleManipulator();
        }
    }
}
