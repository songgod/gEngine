using gEngine.Data.Interface;
using gEngine.Graph.Ge.Business.Creator;
using gEngine.Graph.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Graph.Ge.Business
{
    public class GraphGeFactory
    {
        private static GraphGeFactory s_factory = null;
        private Dictionary<Type, ICreator> _creators;

        private GraphGeFactory()
        {
            _creators = new Dictionary<Type, ICreator>();
        }

        public static GraphGeFactory Single()
        {
            if (s_factory == null)
            {
                s_factory = new GraphGeFactory();
                s_factory.Register(new WellLocationsCreator());
                s_factory.Register(new WellCreator());
            }

            return s_factory;
        }

        public void Register(ICreator creator)
        {
            if (creator == null)
                return;

            Type type = creator.ProcessType();
            if (_creators.ContainsKey(type))
                throw new Exception("creator type is already exsit");
            _creators.Add(type, creator);
        }

        public IObjects Create(IDBBase db)
        {
            if (db == null)
                return null;

            Type type = db.GetType();
            Type[] interfaces = type.GetInterfaces();
            foreach (Type item in interfaces.Reverse())
            {
                if (_creators.ContainsKey(item))
                {
                    ICreator creator = _creators[item];
                    return creator.Create(db);
                }
            }

            return null;
        }
    }
}
