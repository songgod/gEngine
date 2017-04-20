using gEngine.Data.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Data.Ge.Txt
{
    public class TxtDBFactory : IDBFactory
    {
        public TxtDBFactory()
        {
            WellLocationsFolder = "WellLocations";
            WellsFolder = "Wells";
            DiscreteDatasFolder = "DiscreteDatas";
            HorizonsFolder = "Horizons";
        }
        public string DBType { get { return "Txt"; } }
        public string DBPath { get; set; }
        public string WellLocationsFolder { get; set; }
        public string WellsFolder { get; set; }
        public string DiscreteDatasFolder { get; set; }
        public string HorizonsFolder { get; set; }

        protected List<string> GetTxtFilesNames(string folder)
        {
            List<string> res = new List<string>();
            DirectoryInfo di = new DirectoryInfo(folder);
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Extension == ".txt")
                {
                    res.Add(fi.Name.Substring(0, fi.Name.Length - 4));
                }
            }
            return res;
        }

        public List<string> WellLocationsNames
        {
            get
            {
                return GetTxtFilesNames(DBPath + "/" + WellLocationsFolder);
            }
        }

        public IDBWellLocations GetWellLocations(string name)
        {
            string fullpath = DBPath + "/" + WellLocationsFolder + "/" + name + ".txt";
            return new TXTWellLocations() { TxtFile = fullpath };
        }

        public List<string> WellNames
        {
            get
            {
                return GetTxtFilesNames(DBPath + "/" + WellsFolder);
            }
        }

        public IDBWell GetWell(string name)
        {
            string fullpath = DBPath + "/" + WellsFolder + "/" + name + ".txt";
            return new TxtWell() { TxtFile = fullpath };
        }

        public List<string> HorizonsNames
        {
            get
            {
                return GetTxtFilesNames(DBPath + "/" + HorizonsFolder);
            }
        }

        public IDBHorizons GetHorizons(string name)
        {
            string fullpath = DBPath + "/" + HorizonsFolder + "/" + name + ".txt";
            return new TxtHorizon() { TxtFile = fullpath };
        }

        public List<string> DiscreteDataNames
        {
            get
            {
                return GetTxtFilesNames(DBPath + "/" + DiscreteDatasFolder);
            }
        }

        public IDBDiscreteDatas GetDiscreteData(string name)
        {
            string fullpath = DBPath + "/" + DiscreteDatasFolder + "/" + name + ".txt";
            return new TxtDiscreteData() { TxtFile = fullpath };
        }

        public List<IDBHorizons> GetHorizonDataByWells(HashSet<string> wellNames, string horizonName)
        {
            IDBHorizons horizons = GetHorizons(horizonName);
            List<IDBHorizons> lsHorizon = new List<IDBHorizons>();
            foreach (var wellName in wellNames)
            {
                IEnumerable<IDBHorizon> e = horizons.Horizons.Where(s => s.Name == wellName);
                IEnumerator etor = e.GetEnumerator();
                IDBHorizons cs = new DBHorizons();
                cs.ColNames = horizons.ColNames;
                cs.Name = wellName;
                while (etor.MoveNext())
                {
                    IDBHorizon horizon = (IDBHorizon) etor.Current;
                    cs.Horizons.Add(horizon);
                }

                lsHorizon.Add(cs);
            }
            return lsHorizon;
        }
    }
}
