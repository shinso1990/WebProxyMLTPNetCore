using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProxyNETCore.DataAccess;

namespace WebProxyNETCore.Service
{
    public class MongoDBService : IMongoDBService
    {
        public string ConnectionStringMongoDB { get; }
        public string DefaultDB { get; }

        public string CollectionUsuarios { get; }

        public string CollectionConfigGrales { get; }

        public string CollectionConfigProxy { get; }

        public string CollectionInfoReqRes { get; }

        public MongoDBService(string connectionString, string defualtDb, 
            string collectionUsuarios, string collectionConfigGrales,
            string collectionConfigProxy, string collectionInfoReqRes)
        {
            ConnectionStringMongoDB = connectionString;
            DefaultDB = defualtDb;
            CollectionUsuarios = collectionUsuarios;
            CollectionConfigGrales = collectionConfigGrales;
            CollectionConfigProxy = collectionConfigProxy;
            CollectionInfoReqRes = collectionInfoReqRes;
        }

        public ComunicadorMongoDB<T> GetInstance<T>(string collectionName) where T : class
        {
            return new ComunicadorMongoDB<T>(collectionName, DefaultDB, ConnectionStringMongoDB);
        }



    }
}
