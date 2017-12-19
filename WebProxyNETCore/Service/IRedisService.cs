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


    }
}
