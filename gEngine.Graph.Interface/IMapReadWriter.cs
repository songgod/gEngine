using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Interface
{
    public interface IMapReadWriter
    {
        string SupportType { get; }
        IMap ReadMap(string url);

        bool WriteMap(IMap map, string url);

        IMap CreateMap();

        bool DeleteMap(string projectUr,string mapUrl);
    }
}
