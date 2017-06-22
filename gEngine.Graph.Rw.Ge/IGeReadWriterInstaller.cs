using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Rw.Ge
{
    public interface IGeReadWriterInstaller
    {
        void InstallLayerReadWriter();

        void InstallObjectReadWriter();
    }
}
