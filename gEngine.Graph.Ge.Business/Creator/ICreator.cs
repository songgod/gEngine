using gEngine.Data.Interface;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Ge.Business.Creator
{
    public interface ICreator
    {
        Type ProcessType();
        IObjects Create(IDBBase db);
    }
}
