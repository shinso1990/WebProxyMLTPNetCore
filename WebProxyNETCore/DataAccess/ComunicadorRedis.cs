
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProxyNETCore.DataAccess
{
    public class ComunicadorRedis
    {
        private string _host;

        public ComunicadorRedis(string host)
        {
            _host = host;
        }

        public StackExchange.Redis.ConnectionMultiplexer Instance
        {
            get
            {
                return StackExchange.Redis.ConnectionMultiplexer.Connect(_host); ;
            }
        }


        public void Delete(string key)
        {
            using (Instance)
            {
                Instance.GetDatabase().KeyDelete(key);
            }
        }

        public string Get(string key)
        {
            using (Instance)
            {
                return Instance.GetDatabase().StringGet(key) ;
            }
        }

        public bool Set(string key, string value)
        {
            using (Instance)
            {
                return Instance.GetDatabase().StringSet(key,value);
            }
        }


    }
}
