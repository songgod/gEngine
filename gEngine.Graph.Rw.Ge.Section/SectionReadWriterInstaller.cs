using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Rw.Ge.Section
{
    public class SectionReadWriterInstaller : IGeReadWriterInstaller
    {
        public void InstallLayerReadWriter()
        {
            Registry.RegistLayerRW(new RWLayerBase());
            Registry.RegistLayerRW(new RwSectionLayer());
        }

        public void InstallObjectReadWriter()
        {
            Registry.RegistObjRW(new RWSandObject());
            Registry.RegistObjRW(new RWStratumObject());
        }
    }
}
