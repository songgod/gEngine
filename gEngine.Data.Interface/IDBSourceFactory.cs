using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Interface
{
    public interface IDBSourceFactory
    {
        string SupportType { get; }
        IDBSource CreateSource(string url);
    }
}
