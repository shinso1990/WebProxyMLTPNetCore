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

    }
}
