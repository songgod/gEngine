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
            Registry.RegistLayerRW(new RWSectionLayer());
        }

        public void InstallObjectReadWriter()
        {
            
        }
    }
}
