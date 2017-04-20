using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Interface
{
    public interface IDBFactoryCreater
    {
        string SupportType { get; }
        IDBFactory CreateFactory(string url);
    }
}
