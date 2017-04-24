using gEngine.Data.Ge.Txt;
using gEngine.Data.Interface;
using gEngine.Graph.Interface;
using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSection.ViewModel
{
    public class Project
    {
        public static Project Single { get; private set; }

        public static void NewProject()
        {
            Single = new Project();
        }

        public static bool OpenProject(string url)
        {
            Single = new Project();
            Single.Url = url;
            return Single.Read();
        }

        public static bool SaveProject()
        {
            if (Single == null || String.IsNullOrEmpty(Single.Url))
                return false;
            return Single.Save();
        }

        public static bool SaveProjectAs(string url)
        {
            if (Single == null || String.IsNullOrEmpty(url))
                return false;
            Single.Url = url;
            return Single.Save();
        }

        protected Project() 
        {
            Maps = new MapCollection();
            OpenMaps = new IMaps();
            Maps.OnItemRemoved += Maps_OnItemRemoved;
            OpenMaps.OnItemRemoved += OpenMaps_OnItemRemoved;
        }

        private void OpenMaps_OnItemRemoved(int aIndex, IMap aItem)
        {
            int index = Maps.FindIndex(item => item.Item2 == aItem);
            if (index < 0 || index >= Maps.Count)
                return;

            Maps[index] = new Tuple<string, IMap>(Maps[index].Item1, null);
        }

        private void Maps_OnItemRemoved(int aIndex, Tuple<string, IMap> aItem)
        {
            if(OpenMaps.Contains(aItem.Item2))
                OpenMaps.Remove(aItem.Item2);
        }

        public bool Read()
        {
            return false;
        }
        public bool Save()
        {
            return false;
        }

        public class MapCollection : ObservedCollection<Tuple<string, IMap>>
        {

        }

        public class DBSourceTuple : Tuple<string, IDBSource>
        {
            public DBSourceTuple(string url, IDBSource db) : base(url, db)
            {
            }
        }

        public MapCollection Maps { get; private set; }

        public IMaps OpenMaps { get; private set; }

        public DBSourceTuple DBTuple { get; set; }

        public IDBSource DBSource
        {
            get
            {
                if (DBTuple != null)
                    return DBTuple.Item2;
                return null;
            }
        }
        private string url;
        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
                Read();
            }
        }

        public bool OpenDBSource(string url)
        {
            IDBSource source = gEngine.Data.Interface.Register.CreateDBSource(url);
            if (source == null)
                return false;
            DBTuple = new Project.DBSourceTuple(url, source);
            return true;
        }

        public IMap OpenMap(string url)
        {
            int index = Maps.FindIndex(s => s.Item1 == url);
            if (index < 0 || index >= Maps.Count)
                return null;

            IMap map = gEngine.Graph.Interface.Registry.ReadMap(url);
            if (map == null)
                return null;

            Maps[index] = new Tuple<string, IMap>(url, map);
            OpenMaps.Add(map);
            return map;
        }

        public IMap NewMap(string type, string name)
        {
            if (String.IsNullOrEmpty(type) || String.IsNullOrEmpty(name))
                return null;

            IMap map = gEngine.Graph.Interface.Registry.CreateMap(type);
            if (map == null)
                return null;
            map.Name = name;
            Maps.Add(new Tuple<string, IMap>("", map));
            OpenMaps.Add(map);
            return map;
        }
    }
}
