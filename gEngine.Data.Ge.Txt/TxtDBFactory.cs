using gEngine.Data.Interface;
using System;
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
                    res.Add(fi.Name.Substring(0,fi.Name.Length-4));
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
    }
}
