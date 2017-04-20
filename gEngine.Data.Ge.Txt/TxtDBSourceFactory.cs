using gEngine.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Ge.Txt
{
    public class TxtDBSourceFactory : IDBSourceFactory
    {
        public string SupportType
        {
            get
            {
                return "Txt";
            }
        }

        public IDBSource CreateSource(string url)
        {
            return new TxtDBSource() { DBPath = url };
        }
    }
}
