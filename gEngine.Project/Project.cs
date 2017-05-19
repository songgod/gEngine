using gEngine.Data.Interface;
using gEngine.Graph.Interface;
using gEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Project
{
    public class Project
    {
        public Project()
        {
            Maps = new MapCollection();
            OpenMaps = new IMaps();
            Maps.OnItemRemoved += Maps_OnItemRemoved;
            OpenMaps.OnItemRemoved += OpenMaps_OnItemRemoved;
        }

        public bool Open(string url)
        {
            Url = url;
            return Read();
        }

        public bool Save()
        {
            if (String.IsNullOrEmpty(Url))
                return false;
            return Write();
        }

        public bool SaveAs(string url)
        {
            Url = url;
            return Save();
        }

        private void OpenMaps_OnItemRemoved(int aIndex, IMap aItem)
        {
            int index = 0;
            foreach (var item in Maps)
            {
                if (item.Item2 == aItem)
                {
                    Maps[index] = new Tuple<string, IMap>(Maps[index].Item1, null);
                    break;
                }
                index++;
            }
        }

        private void Maps_OnItemRemoved(int aIndex, Tuple<string, IMap> aItem)
        {
            if (OpenMaps.Contains(aItem.Item2))
                OpenMaps.Remove(aItem.Item2);
        }

        public class MapCollection : ObservedCollection<Tuple<string, IMap>> { }

        public class DBSourceTuple : Tuple<string, IDBSource>
        {
            public DBSourceTuple(string url, IDBSource db) : base(url, db)
            {
            }
        }

        public MapCollection Maps { get; private set; }

        public IMaps OpenMaps { get; set; }

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

        public bool Read()
        {
            return false;
        }
        public bool Write()
        {
            return false;
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
            bool find = false;
            int index = 0;
            foreach (var item in Maps)
            {
                if (item.Item1 == url)
                {
                    find = true;
                    break;
                }
                index++;
            }

            IMap map = gEngine.Graph.Interface.Registry.ReadMap(url);
            if (map == null)
                return null;

            if (find==false)
            {
                Maps.Add(new Tuple<string, IMap>(url, map));
            }
            else
            {
                Maps[index] = new Tuple<string, IMap>(url, map);
            }

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

        public IMap NewMap(string type, string name,ILayers layers)
        {
            if (String.IsNullOrEmpty(type) || String.IsNullOrEmpty(name))
                return null;

            IMap map = gEngine.Graph.Interface.Registry.CreateMap(type);
            if (map == null)
                return null;
            map.Name = name;
            Maps.Add(new Tuple<string, IMap>("", map));

            foreach (var layer in layers)
            {
                map.Layers.Add(layer);
            }

            OpenMaps.Add(map);
            return map;
        }

        public bool WriteMap(IMap map,string url)
        {
            if (map == null)
                return false;

            if (string.IsNullOrEmpty(url))

                return false;

            bool a = gEngine.Graph.Interface.Registry.WriteMap(map, url);

            return true;
        }
    }
}
