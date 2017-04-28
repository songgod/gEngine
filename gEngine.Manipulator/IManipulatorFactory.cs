using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Manipulator
{
    public interface IManipulatorFactory
    {
        string Name { get; }
        IManipulatorBase CreateManipulator(object param);
    }
}
