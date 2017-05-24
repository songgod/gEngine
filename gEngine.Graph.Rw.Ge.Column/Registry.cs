using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Rw.Ge.Column
{
    static class Registry
    {
        static public Dictionary<string, RWWellColumn> DicWellColumnObjectRW { get; set; }

        static Registry()
        {
            DicWellColumnObjectRW = new Dictionary<string, RWWellColumn>();
            RegistObjRW(new RWWellDepth());
            RegistObjRW(new RWWellLogColumn());
            RegistObjRW(new RWWellSegmentColumn());
        }

        static public void RegistObjRW(RWWellColumn rw)
        {
            if (DicWellColumnObjectRW.ContainsKey(rw.SupportType))
                return;
            DicWellColumnObjectRW.Add(rw.SupportType, rw);
        }

        static public RWWellColumn GetObjectRW(string type)
        {
            if (!DicWellColumnObjectRW.ContainsKey(type))
                return null;
            return DicWellColumnObjectRW[type];
        }
    }
}
