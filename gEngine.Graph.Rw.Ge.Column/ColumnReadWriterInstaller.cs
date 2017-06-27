using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Rw.Ge.Column
{
    public class ColumnReadWriterInstaller : IGeReadWriterInstaller
    {
        public void InstallLayerReadWriter()
        {
        }

        public void InstallObjectReadWriter()
        {
            Registry.RegistObjRW(new RWWell());
            Registry.RegistObjRW(new RWWellColumn());
            Registry.RegistObjRW(new RWWellDepth());
            Registry.RegistObjRW(new RWWellLogColumn());
            Registry.RegistObjRW(new RWWellSegmentColumn());
        }
    }
}
