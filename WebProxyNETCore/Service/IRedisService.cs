using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProxyNETCore.Service
{
    public interface IRedisService
    {
        ConnectionMultiplexer Instance { get; }

        string Get(string key);
        bool Set(string key, string value);
        bool Delete(string key);
    }
}
