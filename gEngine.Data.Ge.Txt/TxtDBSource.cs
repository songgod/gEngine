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
    public class TxtDBSource : IDBSource
    {
        public TxtDBSource()
        {
            WellLocationsFolder = "WellLocations";
            WellsFolder = "Wells";
            DiscreteDatasFolder = "DiscreteDatas";
            HorizonsFolder = "Horizons";
        }
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
            TxtWell w = new TxtWell();
            if (w.ReadFromTxt(fullpath) == false)
                return null;
            return w;
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

        public IDBHorizons GetHorizonsByWell(string wellName, string horizonName)
        {
            IDBHorizons horizons = GetHorizons(horizonName);
            IEnumerable<IDBHorizon> e = horizons.Horizons.Where(s => s.Name == wellName);
            IEnumerator etor = e.GetEnumerator();
            IDBHorizons horizonResult = new DBHorizons();
            horizonResult.ColNames = horizons.ColNames;
            horizonResult.Name = wellName;
            while (etor.MoveNext())
            {
                IDBHorizon horizon = (IDBHorizon) etor.Current;
                horizonResult.Horizons.Add(horizon);
            }
            return horizonResult;
        }

        public IDBDiscreteDatas GetDiscretesByWell(string wellName, string discreteName)
        {
            IDBDiscreteDatas discretes = GetDiscreteData(discreteName);
            IEnumerable<IDBDiscreteData> e = discretes.DiscreteDatas.Where(s => s.Name == wellName);
            IEnumerator etor = e.GetEnumerator();
            IDBDiscreteDatas discreteResult = new DBDiscreteDatas();
            discreteResult.ColNames = discretes.ColNames;
            discreteResult.Name = wellName;
            while (etor.MoveNext())
            {
                IDBDiscreteData discrete = (IDBDiscreteData) etor.Current;
                discreteResult.DiscreteDatas.Add(discrete);
            }
            return discreteResult;
        }
    }
}
