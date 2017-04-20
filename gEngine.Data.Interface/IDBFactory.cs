using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Interface
{
    public interface IDBFactory
    {
        string DBType { get; }
        List<string> WellLocationsNames { get; }
        IDBWellLocations GetWellLocations(string name);
        List<string> WellNames { get; }
        IDBWell GetWell(string name);

        List<string> HorizonsNames { get; }

        IDBHorizons GetHorizons(string name);

        List<string> DiscreteDataNames { get; }

        IDBDiscreteDatas GetDiscreteData(string name);

        List<IDBHorizons> GetHorizonDataByWells(HashSet<string> wellNames, string horizonName);
    }
}
