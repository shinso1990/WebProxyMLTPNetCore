using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace WebProxyNETCore.Service
{
    public class RedisService : IRedisService
    {
        public RedisService(string connectionString)
        {
            _connectionStringRedis = connectionString;
        }
        private string _connectionStringRedis;

        public ConnectionMultiplexer Instance => ConnectionMultiplexer.Connect(_connectionStringRedis);


        public bool Delete(string key)
        {
            using (var redis = Instance)
            {
                return redis.GetDatabase().KeyDelete(key);
            }
        }

        public string Get(string key)
        {
            using (var redis = Instance)
            {
                return redis.GetDatabase().StringGet(key);
            }
        }

        public bool Set(string key, string value)
        {
            using (var redis = Instance)
            {
                return redis.GetDatabase().StringSet(key, value);
            }
        }
    }
}
