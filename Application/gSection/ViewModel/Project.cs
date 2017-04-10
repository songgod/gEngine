using gEngine.Data.Ge.Txt;
using gEngine.Data.Interface;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSection.ViewModel
{
    public class Project
    {
        static Project()
        {
            Single = new Project();
        }
        public static Project Single { get; }
        protected Project() 
        {
            Maps = new IMaps();
            OpenMaps = new IMaps();
            String dbpath = @"D:\gSectionData";
            DBFactory = new TxtDBFactory() { DBPath = dbpath };
        }

        public IMaps Maps { get; set; }

        public IMaps OpenMaps { get; set; }

        public IDBFactory DBFactory { get; set; }
    }
}
