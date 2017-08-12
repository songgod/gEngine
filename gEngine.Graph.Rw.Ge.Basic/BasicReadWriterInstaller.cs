using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Rw.Ge.Basic
{
    public class BasicReadWriterInstaller : IGeReadWriterInstaller
    {
        public void InstallLayerReadWriter()
        {
            Registry.RegistLayerRW(new RWBasicLayer());
        }

        public void InstallObjectReadWriter()
        {
            
        }
    }
}
