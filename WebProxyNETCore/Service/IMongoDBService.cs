using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProxyNETCore.DataAccess;

namespace WebProxyNETCore.Service
{
    public interface IMongoDBService
    {
        
        string ConnectionStringMongoDB { get;  }
        string DefaultDB { get;  }

        string CollectionUsuarios { get; }
        string CollectionConfigGrales { get; }
        string CollectionConfigProxy { get; }


        ComunicadorMongoDB<T> GetInstance<T>(string collectionName) where T : class;


    }
}
