using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Manipulator
{
    public interface IObjectManipulatorFactory: IManipulatorFactory
    {
        IObject SupportIObject { get; }
    }
}
