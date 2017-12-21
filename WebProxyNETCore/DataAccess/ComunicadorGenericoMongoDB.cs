using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebProxyNETCore.DataAccess
{
    public class ComunicadorMongoDB<T> where T : class
    {
        /*
        private string databaseName = "admin";
        private string mongoDBConStrin = "mongodb://alan:123456789@ec2-34-201-91-193.compute-1.amazonaws.com:27017/admin?connectTimeoutMS=900000";
        */
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        protected IMongoCollection<T> _collection;

        public ComunicadorMongoDB(string collectionName, string databaseName, 
            string connectionString)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
            _collection = _database.GetCollection<T>(collectionName);
        }

        /*
        public ComunicadorMongoDB(string collectionName)
        {
            string conStringMongDb = mongoDBConStrin;
            _client = new MongoClient(conStringMongDb);
            _database = _client.GetDatabase(databaseName);
            
            _collection = _database.GetCollection<T>(collectionName);
        }
        */
        public List<T> Get( System.Linq.Expressions.Expression<Func<T, bool>> filter) 
        {
            try
            {
                List<T> listado = new List<T>();
                using (var cursor = _collection.FindAsync(filter).Result)
                {
                    while (cursor.MoveNextAsync().Result)
                    {
                        var batch = cursor.Current;
                        listado.AddRange(batch.ToList());
                    }
                }
                return listado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
       
        public List<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, ProjectionDefinition<T> fields)
        {
            try
            {
                List<T> listado = new List<T>();
                using (var cursor = _collection.Find(filter).Project<T>(fields).ToCursor())
                {
                    while (cursor.MoveNextAsync().Result)
                    {
                        var batch = cursor.Current;
                        listado.AddRange(batch.ToList());
                    }
                }
                return listado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ReplaceOne(System.Linq.Expressions.Expression<Func<T, bool>> filter, T objeto)
        {
            try
            {
                _collection.ReplaceOne<T>(filter, objeto);
            }
            catch( Exception e)
            {
                throw e;
            }
        }
        public void Insert( T obj)
        {
            try
            {
                List<T> listado = new List<T>();
                _collection.InsertOne(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public long DeleteOne(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return _collection.DeleteOne(filter).DeletedCount;
        }
        public long DeleteMany(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return _collection.DeleteMany(filter).DeletedCount ;
        }

        public List<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, int startIndex, int pageSize)
        {
            try
            {
                List<T> listado = new List<T>();
                

                using (var cursor = _collection.Find(filter).Skip(startIndex).Limit(pageSize).ToCursor())
                {
                    while (cursor.MoveNextAsync().Result)
                    {
                        var batch = cursor.Current;
                        listado.AddRange(batch.ToList());
                    }
                }
                return listado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, ProjectionDefinition<T> fields, int startIndex, int pageSize)
        {
            try
            {
                List<T> listado = new List<T>();
                using (var cursor = _collection.Find(filter).Project<T>(fields).Skip(startIndex).Limit(pageSize).ToCursor() )
                {
                    while (cursor.MoveNextAsync().Result)
                    {
                        var batch = cursor.Current;
                        listado.AddRange(batch.ToList());
                    }
                }
                return listado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long Count(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            try
            {
                return _collection.Find(filter).Count();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        internal List<BsonDocument> groupByURI(string URI)
        {
            BsonDocument match = null;
            if(URI != null)
                match = new BsonDocument
                {
                    {
                        "$match",
                        new BsonDocument
                        {
                            { "URI", URI }
                        }
                    }
               };

            var group = new BsonDocument
                {
                    { "$group",
                        new BsonDocument
                            {
                                {
                                    "_id", "$URI"
                                },
                                {
                                    "Count", new BsonDocument
                                    {
                                        {
                                            "$sum", 1
                                        }
                                    }
                                },
                                {
                                    "Permitido", new BsonDocument
                                    {
                                        {
                                            "$sum", "$Permitido"
                                        }
                                    }
                                },
                                {
                                    "Limitado", new BsonDocument
                                    {
                                        {
                                            "$sum", "$Limitado"
                                        }
                                    }
                                },
                                {
                                    "Bloqueado", new BsonDocument
                                    {
                                        {
                                            "$sum", "$Bloqueado"
                                        }
                                    }
                                },
                                {
                                    "Errado", new BsonDocument
                                    {
                                        {
                                            "$sum", "Errado"
                                        }
                                    }
                                }
                            }
                  }
                };

            var pipeline = new[] { group };
            if (match != null)
                pipeline = new[] { match, group };
            

            List<BsonDocument> listado = new List<BsonDocument>();
            using (var cursor = _collection.Aggregate<BsonDocument>(pipeline))
            {
                while (cursor.MoveNextAsync().Result)
                {
                    var batch = cursor.Current;
                    listado.AddRange(batch.ToList());
                }
            }
            return listado;
        }

        internal List<BsonDocument> groupByIP(string IP)
        {
            BsonDocument match = null;
            if (IP != null)
                match = new BsonDocument
                {
                    {
                        "$match",
                        new BsonDocument
                        {
                            { "IP", IP }
                        }
                    }
               };

            var group = new BsonDocument
                {
                    { "$group",
                        new BsonDocument
                            {
                                {
                                    "_id", "$IP"
                                },
                                {
                                    "Count", new BsonDocument
                                    {
                                        {
                                            "$sum", 1
                                        }
                                    }
                                },
                                {
                                    "Permitido", new BsonDocument
                                    {
                                        {
                                            "$sum", "$Permitido"
                                        }
                                    }
                                },
                                {
                                    "Limitado", new BsonDocument
                                    {
                                        {
                                            "$sum", "$Limitado"
                                        }
                                    }
                                },
                                {
                                    "Bloqueado", new BsonDocument
                                    {
                                        {
                                            "$sum", "$Bloqueado"
                                        }
                                    }
                                },
                                {
                                    "Errado", new BsonDocument
                                    {
                                        {
                                            "$sum", "Errado"
                                        }
                                    }
                                }
                            }
                  }
                };


            var pipeline = new[] { group };
            if (match != null)
                pipeline = new[] { match, group };


            List<BsonDocument> listado = new List<BsonDocument>();
            using (var cursor = _collection.Aggregate<BsonDocument>(pipeline) )
            {
                while (cursor.MoveNextAsync().Result)
                {
                    var batch = cursor.Current;
                    listado.AddRange(batch.ToList());
                }
            }
            return listado;
        }
        
    }
}
