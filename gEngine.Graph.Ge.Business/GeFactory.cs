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
    public class GeFactory
    {
        private static GeFactory s_factory = null;
        private Dictionary<Type, ICreator> _creators;

        private GeFactory()
        {
            _creators = new Dictionary<Type, ICreator>();
        }

        public static GeFactory Single()
        {
            if (s_factory == null)
            {
                s_factory = new GeFactory();
                s_factory.Register(new WellLocationsCreator());
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
            do
            {
                if (_creators.ContainsKey(type))
                {
                    ICreator creator = _creators[type];
                    return creator.Create(db);
                }
                type = type.BaseType;
            } while (type!=null);

            return null;
        }
    }
}
