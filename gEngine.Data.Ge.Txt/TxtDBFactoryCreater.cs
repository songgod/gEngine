using gEngine.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Ge.Txt
{
    public class TxtDBFactoryCreater : IDBFactoryCreater
    {
        public string SupportType
        {
            get
            {
                return "Txt";
            }
        }

        public IDBFactory CreateFactory(string url)
        {
            return new TxtDBFactory() { DBPath = url };
        }
    }
}
