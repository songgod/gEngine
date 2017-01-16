using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Util
{
    public interface ICommand
    {
        void Do();
        void UnDo();
    }
}
