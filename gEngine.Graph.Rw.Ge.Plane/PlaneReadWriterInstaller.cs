using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Rw.Ge.Plane
{
    public class PlaneReadWriterInstaller : IGeReadWriterInstaller
    {
        public void InstallLayerReadWriter()
        {
            Registry.RegistLayerRW(new RWWellLocationLayer());
        }

        public void InstallObjectReadWriter()
        {
            Registry.RegistObjRW(new RWWellLocation());
        }
    }
}
